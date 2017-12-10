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
    public partial class Comments : System.Web.UI.Page
    {
        IRepository<MyComment> rep;
        IRepository<MyUserDataGrid> repUsers;

        bool isEditing = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            rep = IoCContainer.ServiceLocator.Resolve<IRepository<MyComment>>();
            repUsers = IoCContainer.ServiceLocator.Resolve<IRepository<MyUserDataGrid>>();
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
                MyComment my;
                try
                {
                    my = rep.GetEntityById(id) as MyComment;
                }
                catch (InvalidOperationException e1)
                {
                    my = new MyComment();
                }

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
                            List<MyUserDataGrid> users = repUsers.GetAll();
                            lst.DataSource = users.Select(c => c.Email);
                            lst.DataBind();

                            string name = DBManager.DbManager.GetUserEmailById(my.UserId);

                            lst.SelectedValue = (name == null) ? lst.Items[0].Value : name;
                        }
                    }
                }

                int index = -1;
                for (int i = 0; i < repComments.Items.Count; i++)
                {
                    if (repComments.Items[i] == e.Item)
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
                List<MyComment> comments = rep.GetAll();
                MyComment my = new MyComment();
                comments.Add(my);

                repComments.DataSource = comments;
                repComments.DataBind();

                RepeaterItem item = repComments.Items.OfType<RepeaterItem>().ElementAt(repComments.Items.Count - 1);

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
                            List<MyUserDataGrid> users = repUsers.GetAll();
                            List<string> emails = users.Select(c => c.Email).ToList();
                            lst.DataSource = emails;
                            lst.DataBind();
                        }
                    }
                }

                int index = -1;
                for (int i = 0; i < repComments.Items.Count; i++)
                {
                    if (repComments.Items[i] == item)
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
            repComments.DataSource = rep.GetAll();
            repComments.DataBind();
        }

        public object GetPropertyValue(MyComment my, string name)
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

            RepeaterItem item = repComments.Items[index];

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

            RepeaterItem item = repComments.Items[index];
            MyComment my = new MyComment();
            if (id != -1)
            {
                my = rep.GetEntityById(id) as MyComment;
            }

            for (int i = 0; i < item.Controls.Count; i++)
            {
                TextBox t = item.Controls[i] as TextBox;
                if (t != null)
                {
                    string propName = t.ToolTip;

                    switch (propName)
                    {
                        case "VacancyId":
                            if (my.VacancyId != Convert.ToInt32(t.Text))
                                my.VacancyId = Convert.ToInt32(t.Text);
                             
                            break;
                        case "Date":
                            DateTime date = new DateTime();
                            DateTime.TryParse(t.Text, out date);
                            if (my.Date != date && date != new DateTime())
                                my.Date = date;
                            break;
                        case "Content":
                            if (my.Content != t.Text)
                                my.Content = t.Text;
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
                        int val = DBManager.DbManager.GetUserIdByEmail1(lst.SelectedValue);
                        if (my.UserId != val)
                        {
                            my.UserId = val;
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