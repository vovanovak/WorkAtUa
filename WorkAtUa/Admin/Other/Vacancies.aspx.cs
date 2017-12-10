using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WorkAtUa.Core.ServiceLocator;
using WorkAtUa.Entities;
using WorkAtUa.DBManager;


namespace WorkAtUa.Admin.Other
{
    public partial class Vacancies : System.Web.UI.Page
    {
        IRepository<MyVacancy> rep;
        IRepository<MyCompanyDetail> repCompanies;

        bool isEditing = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            rep = IoCContainer.ServiceLocator.Resolve<IRepository<MyVacancy>>();
            repCompanies = IoCContainer.ServiceLocator.Resolve<IRepository<MyCompanyDetail>>();

            if (!IsPostBack)
            {
                UpdateData();
            }
        }

        protected void repUsers_ItemCommand1(object source, RepeaterCommandEventArgs e)
        {
            #region Delete
            if (e.CommandName == "Delete")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                rep.Delete(id);
                UpdateData();
            }
            #endregion

            #region Edit
            if (e.CommandName == "Edit")
            {
                int id = Convert.ToInt32(e.CommandArgument);


                MyVacancy my = rep.GetEntityById(id) as MyVacancy;

                for (int i = 0; i < e.Item.Controls.Count; i++)
                {
                    TextBox t = e.Item.Controls[i] as TextBox;
                    if (t != null)
                    {
                        t.Style.Add("display", "block");
                        t.Text = GetPropertyValue(my, t.ToolTip).ToString();
                    }
                    else
                    {
                        DropDownList lst = e.Item.Controls[i] as DropDownList;

                        if (lst != null)
                        {
                            lst.Style.Add("display", "block");
                            List<MyCompanyDetail> companies = repCompanies.GetAll();
                            lst.DataSource = companies.Select(c => c.Name);
                            lst.DataBind();

                            string name = DBManager.DbManager.GetCompanyNameById(my.CompanyId);

                            lst.SelectedValue = (name == null) ? lst.Items[0].Value : name;
                        }
                    }
                }

                int index = -1;
                for (int i = 0; i < repVacancies.Items.Count; i++)
                {
                    if (repVacancies.Items[i] == e.Item)
                    {
                        index = i;
                    }
                }

                ViewState.Add("EditedId", id);
                ViewState.Add("EditedIndex", index);

                btnSave.Style.Add("display", "inline");
                btnCancel.Style.Add("display", "inline");

                isEditing = true;
            }
            #endregion

            #region Add

            if (e.CommandName == "Add")
            {
                List<MyVacancy> users = rep.GetAll();
                MyVacancy my = new MyVacancy();
                users.Add(my);

                repVacancies.DataSource = users;
                repVacancies.DataBind();

                RepeaterItem item = repVacancies.Items.OfType<RepeaterItem>().ElementAt(repVacancies.Items.Count - 1);

                for (int i = 0; i < item.Controls.Count; i++)
                {
                    TextBox t = item.Controls[i] as TextBox;
                    if (t != null)
                    {
                        t.Style.Add("display", "block");
                        object obj = GetPropertyValue(my, t.ToolTip);
                        t.Text = (obj == null) ? "" : obj.ToString();
                    }
                    else
                    {
                        DropDownList lst = item.Controls[i] as DropDownList;

                        if (lst != null)
                        {
                            lst.Style.Add("display", "block");
                            List<MyCompanyDetail> companies = repCompanies.GetAll();
                            lst.DataSource = companies.Select(c => c.Name);
                            lst.DataBind();

                            lst.SelectedValue = lst.Items[0].Value;
                        }
                    }
                }

                int index = -1;
                for (int i = 0; i < repVacancies.Items.Count; i++)
                {
                    if (repVacancies.Items[i] == item)
                    {
                        index = i;
                    }
                }

                ViewState.Add("EditedId", -1);
                ViewState.Add("EditedIndex", index);

                btnSave.Style.Add("display", "inline");
                btnCancel.Style.Add("display", "inline");

                isEditing = true;
            }

            #endregion
        }

        public void UpdateData()
        {
            repVacancies.DataSource = rep.GetAll();
            repVacancies.DataBind();
        }

        public object GetPropertyValue(MyVacancy my, string name)
        {
            PropertyInfo[] properties = my.GetType().GetProperties();

            foreach (var i in properties)
            {
                if (i.Name == name)
                {
                    return i.GetValue(my);
                }
            }

            return null;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            int index = Convert.ToInt32(ViewState["EditedIndex"]);

            RepeaterItem item = repVacancies.Items[index];

            for (int i = 0; i < item.Controls.Count; i++)
            {
                TextBox t = item.Controls[i] as TextBox;
                if (t != null)
                {
                    t.Style.Add("display", "none");

                }
                else
                {
                    DropDownList lst = item.Controls[i] as DropDownList;

                    if (lst != null)
                    {
                        lst.Style.Add("display", "none");
                    }
                }
            }


            ViewState.Add("EditedId", -1);
            ViewState.Add("EditedIndex", -1);

            btnSave.Style.Add("display", "none");
            btnCancel.Style.Add("display", "none");

            isEditing = false;
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.Validate();

            int index = Convert.ToInt32(ViewState["EditedIndex"]);
            int id = Convert.ToInt32(ViewState["EditedId"]);

            RepeaterItem item = repVacancies.Items[index];
            MyVacancy my = new MyVacancy();
            if (id != -1)
            {
                my = rep.GetEntityById(id) as MyVacancy;
            }

            for (int i = 0; i < item.Controls.Count; i++)
            {
                TextBox t = item.Controls[i] as TextBox;
                if (t != null)
                {
                    string propName = t.ToolTip;

                    switch (propName)
                    {
                        case "ProfessionName":
                            if (my.ProfessionName != t.Text)
                                my.ProfessionName = t.Text;
                            break;
                        case "EmployerName":
                            if (my.EmployerName != t.Text)
                                my.EmployerName = t.Text;
                            break;
                        case "EmployerPhone":
                            if (my.EmployerPhone != t.Text)
                                my.EmployerPhone = t.Text;
                            break;
                        case "MinSalary":
                            if (my.MinSalary != Convert.ToDecimal(t.Text))
                                my.MinSalary = Convert.ToDecimal(t.Text);
                            break;
                        case "MaxSalary":
                            if (my.MaxSalary != Convert.ToDecimal(t.Text))
                                my.MaxSalary = Convert.ToDecimal(t.Text);
                            break;
                        case "Requirments":
                            if (my.Requirments != t.Text)
                                my.Requirments = t.Text;
                            break;
                        case "TypeOfEmployment":
                            if (my.TypeOfEmployment != t.Text)
                                my.TypeOfEmployment = t.Text;
                            break;
                        case "Description":
                            if (my.Description != t.Text)
                                my.Description = t.Text;
                            break;
                        default:
                            break;
                    }

                    t.Style.Add("display", "none");
                }
                else
                {
                    DropDownList lst = item.Controls[i] as DropDownList;

                    if (lst != null)
                    {
                        int val = DBManager.DbManager.GetCompanyIdByName(lst.SelectedValue);
                        if (my.CompanyId != val)
                        {
                            my.CompanyId = val;
                        }

                        lst.Style.Add("display", "none");
                    }
                }
            }

            rep.Save(my);

            ViewState.Add("EditedId", -1);
            ViewState.Add("EditedIndex", -1);

            btnSave.Style.Add("display", "none");
            btnCancel.Style.Add("display", "none");

            isEditing = false;

            UpdateData();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

        }


    }
}