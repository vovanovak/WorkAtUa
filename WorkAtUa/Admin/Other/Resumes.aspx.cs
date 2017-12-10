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
    public partial class Resumes : System.Web.UI.Page
    {
        IRepository<MyUserDetail> rep;

        bool isEditing = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            rep = IoCContainer.ServiceLocator.Resolve<IRepository<MyUserDetail>>();

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


                MyUserDetail my = rep.GetEntityById(id) as MyUserDetail;

                for (int i = 0; i < e.Item.Controls.Count; i++)
                {
                    TextBox t = e.Item.Controls[i] as TextBox;
                    if (t != null)
                    {
                        t.Style.Add("display", "block");
                        object obj = GetPropertyValue(my, t.ToolTip);
                        t.Text = ((obj == null) ? "" : obj.ToString());
                    }

                }

                int index = -1;
                for (int i = 0; i < repResumes.Items.Count; i++)
                {
                    if (repResumes.Items[i] == e.Item)
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
                List<MyUserDetail> users = rep.GetAll();
                MyUserDetail my = new MyUserDetail();
                users.Add(my);

                repResumes.DataSource = users;
                repResumes.DataBind();

                RepeaterItem item = repResumes.Items.OfType<RepeaterItem>().ElementAt(repResumes.Items.Count - 1);

                for (int i = 0; i < item.Controls.Count; i++)
                {
                    TextBox t = item.Controls[i] as TextBox;
                    if (t != null)
                    {
                        t.Style.Add("display", "block");
                        object obj = GetPropertyValue(my, t.ToolTip);
                        t.Text = (obj == null) ? "" : obj.ToString();
                    }

                }

                int index = -1;
                for (int i = 0; i < repResumes.Items.Count; i++)
                {
                    if (repResumes.Items[i] == item)
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
            repResumes.DataSource = rep.GetAll();
            repResumes.DataBind();
        }

        public object GetPropertyValue(MyUserDetail my, string name)
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

            RepeaterItem item = repResumes.Items[index];

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

            RepeaterItem item = repResumes.Items[index];
            MyUserDetail my = new MyUserDetail();
            if (id != -1)
            {
                my = rep.GetEntityById(id) as MyUserDetail;
            }

            for (int i = 0; i < item.Controls.Count; i++)
            {
                TextBox t = item.Controls[i] as TextBox;
                if (t != null)
                {
                    string propName = t.ToolTip;

                    switch (propName)
                    {
                        case "Header":
                            if (my.Header != t.Text)
                                my.Header = t.Text;
                            break;
                        case "Salary":
                            if (my.Salary != Convert.ToDecimal(t.Text))
                                my.Salary = Convert.ToDecimal(t.Text);
                            break;
                        case "JobExperience":
                            if (my.JobExperience != Convert.ToInt32(t.Text))
                                my.JobExperience = Convert.ToInt32(t.Text);
                            break;
                        case "Education":
                            if (my.Education != Convert.ToInt32(t.Text))
                                my.Education = Convert.ToInt32(t.Text);
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