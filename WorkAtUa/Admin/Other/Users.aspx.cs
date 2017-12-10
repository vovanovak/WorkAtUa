using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WorkAtUa.Entities;
using WorkAtUa.Core.ServiceLocator;
using WorkAtUa.DBManager;
using System.Reflection;
using System.Text.RegularExpressions;

namespace WorkAtUa.Admin.Other
{
    public partial class AdminUser : System.Web.UI.Page
    {
        IRepository<MyUserDataGrid> rep;
        IRepository<MyCity> cityRep;

        bool isEditing = false;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            rep = IoCContainer.ServiceLocator.Resolve<IRepository<MyUserDataGrid>>();
            cityRep = IoCContainer.ServiceLocator.Resolve<IRepository<MyCity>>();

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
                

                MyUserDataGrid my = rep.GetEntityById(id) as MyUserDataGrid;

                for (int i = 0; i < e.Item.Controls.Count;i++)
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
                            List<MyCity> cities = cityRep.GetAll();
                            lst.DataSource = cities.Select(c => c.Name);
                            lst.DataBind();

                            lst.SelectedValue = my.City;
                        }
                    }
                }

                int index = -1;
                for(int i = 0;i<repUsers.Items.Count;i++)
                {
                    if (repUsers.Items[i] == e.Item)
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
                List<MyUserDataGrid> users = rep.GetAll();
                MyUserDataGrid my = new MyUserDataGrid() { Roles = "Admin" };
                users.Add(my);

                repUsers.DataSource = users;
                repUsers.DataBind();

                RepeaterItem item = repUsers.Items.OfType<RepeaterItem>().ElementAt(repUsers.Items.Count - 1);

                for (int i = 0; i < item.Controls.Count; i++)
                {
                    TextBox t = item.Controls[i] as TextBox;
                    if (t != null)
                    {
                        t.Style.Add("display", "block");
                        object obj = GetPropertyValue(my, t.ToolTip);
                        t.Text = (obj == null)? "" : obj.ToString();
                    }
                    else
                    {
                        DropDownList lst = item.Controls[i] as DropDownList;

                        if (lst != null)
                        {
                            lst.Style.Add("display", "block");
                            List<MyCity> cities = cityRep.GetAll();
                            lst.DataSource = cities.Select(c => c.Name);
                            lst.DataBind();

                            lst.SelectedValue = my.City;
                        }
                    }
                }

                int index = -1;
                for (int i = 0; i < repUsers.Items.Count; i++)
                {
                    if (repUsers.Items[i] == item)
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
            repUsers.DataSource = rep.GetAll();
            repUsers.DataBind();
        }

        public object GetPropertyValue(MyUserDataGrid my, string name)
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

            RepeaterItem item = repUsers.Items[index];

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

            RepeaterItem item = repUsers.Items[index];
            MyUserDataGrid my= new MyUserDataGrid();
            if (id != -1)
            {
                my = rep.GetEntityById(id) as MyUserDataGrid;
            }

            for (int i = 0; i < item.Controls.Count; i++)
            {

                TextBox t = item.Controls[i] as TextBox;
                if (t != null)
                {
                    string propName = t.ToolTip;

                    switch (propName)
                    {
                        case "Email":
                            my.Email = t.Text;
                            break;
                        case "Password":
                            if (my.Password != t.Text)
                                my.Password = t.Text;
                            break;
                        case "Birthday":
                            DateTime birth = new DateTime();
                            DateTime.TryParse(t.Text, out birth);
                            if (my.Birthday != birth && birth != new DateTime())
                                my.Birthday = birth;
                            break;
                        case "Name":
                            if (my.Name != t.Text)
                                my.Name = t.Text;
                            break;
                        case "Surname":
                            if (my.Surname != t.Text)
                                my.Surname = t.Text;
                            break;
                        case "FathersName":
                            if (my.FathersName != t.Text)
                                my.FathersName = t.Text;
                            break;
                        case "Phone":
                            if (my.Phone != t.Text)
                                my.Phone = t.Text;
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
                        if (my.City != lst.SelectedValue)
                        {
                            my.City = lst.SelectedValue;
                        }

                        lst.Style.Add("display", "none");
                    }
                }
            }

            rep.Save(my);

            if (id == -1)
            {
                id = rep.GetAll().Max(u => u.Id);
                UserRepositoryBase b = rep as UserRepositoryBase;

                b.AddRole(id, "Admin");
            }

            ViewState.Add("EditedId", -1);
            ViewState.Add("EditedIndex", -1);

            btnSave.Style.Add("display", "none");
            btnCancel.Style.Add("display", "none");

            isEditing = false;

            UpdateData();
        }

        
    }
}