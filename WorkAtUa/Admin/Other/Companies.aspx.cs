using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WorkAtUa.Core.ServiceLocator;
using WorkAtUa.Entities;

namespace WorkAtUa.Admin.Other
{
    public partial class Companies : System.Web.UI.Page
    {
        IRepository<MyCompanyDetail> rep;

        bool isEditing = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            rep = IoCContainer.ServiceLocator.Resolve<IRepository<MyCompanyDetail>>();

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


                MyCompanyDetail my = rep.GetEntityById(id) as MyCompanyDetail;

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
                            if (my.TypeOfCompany == 0)
                            {
                                lst.SelectedValue = "Direct Employer";
                            }
                            else
                            {
                                lst.SelectedValue = "Active Agency";
                            }
                        }
                    }
                }

                int index = -1;
                for (int i = 0; i < repCompanies.Items.Count; i++)
                {
                    if (repCompanies.Items[i] == e.Item)
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
                List<MyCompanyDetail> users = rep.GetAll();
                MyCompanyDetail my = new MyCompanyDetail();
                users.Add(my);

                repCompanies.DataSource = users;
                repCompanies.DataBind();

                RepeaterItem item = repCompanies.Items.OfType<RepeaterItem>().ElementAt(repCompanies.Items.Count - 1);

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
                          
                            lst.SelectedValue = "Direct Employer";
                            
                        }
                    }
                }

                int index = -1;
                for (int i = 0; i < repCompanies.Items.Count; i++)
                {
                    if (repCompanies.Items[i] == item)
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
            repCompanies.DataSource = rep.GetAll();
            repCompanies.DataBind();
        }

        public object GetPropertyValue(MyCompanyDetail my, string name)
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

            RepeaterItem item = repCompanies.Items[index];

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

            RepeaterItem item = repCompanies.Items[index];
            MyCompanyDetail my = new MyCompanyDetail();
            if (id != -1)
            {
                my = rep.GetEntityById(id) as MyCompanyDetail;
            }

            for (int i = 0; i < item.Controls.Count; i++)
            {
                TextBox t = item.Controls[i] as TextBox;
                if (t != null)
                {
                    string propName = t.ToolTip;

                    switch (propName)
                    {
                        case "Name":
                            if (my.Name != t.Text)
                                my.Name = t.Text;
                            break;
                       
                        case "WebSite":
                            if (my.WebSite != t.Text)
                                my.WebSite = t.Text;
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
                        if (lst.SelectedIndex == 0) 
                        {
                            my.TypeOfCompany = 0;

                        }
                        else
                        {
                            my.TypeOfCompany = 1;
                        }
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