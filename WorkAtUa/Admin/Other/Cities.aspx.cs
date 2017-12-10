using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WorkAtUa.Core.ServiceLocator;
using WorkAtUa.Entities;

namespace WorkAtUa.Admin.Other
{
    public partial class Cities : System.Web.UI.Page
    {
        IRepository<MyCity> rep;

        bool isEditing = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            rep = IoCContainer.ServiceLocator.Resolve<IRepository<MyCity>>();

            if (!IsPostBack)
            {
                UpdateData();
            }
        }

        public void UpdateData()
        {
            repCities.DataSource = rep.GetAll();
            repCities.DataBind();
        }

        protected void repCities_ItemCommand(object source, RepeaterCommandEventArgs e)
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


                MyCity my = rep.GetEntityById(id) as MyCity;

                for (int i = 0; i < e.Item.Controls.Count; i++)
                {
                    TextBox t = e.Item.Controls[i] as TextBox;
                    if (t != null)
                    {
                        t.Style.Add("display", "block");
                        t.Text = my.Name;
                    }
                }

                int index = -1;
                for (int i = 0; i < repCities.Items.Count; i++)
                {
                    if (repCities.Items[i] == e.Item)
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
                List<MyCity> users = rep.GetAll();
                MyCity my = new MyCity();
                users.Add(my);

                repCities.DataSource = users;
                repCities.DataBind();

                RepeaterItem item = repCities.Items.OfType<RepeaterItem>().ElementAt(repCities.Items.Count - 1);

                for (int i = 0; i < item.Controls.Count; i++)
                {
                    TextBox t = item.Controls[i] as TextBox;
                    if (t != null)
                    {
                        t.Style.Add("display", "block");
                        object obj = my.Name;
                        t.Text = (obj == null) ? "" : obj.ToString();
                    }
                }

                int index = -1;
                for (int i = 0; i < repCities.Items.Count; i++)
                {
                    if (repCities.Items[i] == item)
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            int index = Convert.ToInt32(ViewState["EditedIndex"]);

            RepeaterItem item = repCities.Items[index];

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

            RepeaterItem item = repCities.Items[index];
            MyCity my = new MyCity();
            if (id != -1)
            {
                my = rep.GetEntityById(id) as MyCity;
            }

            for (int i = 0; i < item.Controls.Count; i++)
            {
                TextBox t = item.Controls[i] as TextBox;
                if (t != null)
                {
                    string propName = t.ToolTip;

                    if (my.Name != t.Text)
                        my.Name = t.Text;

                    t.Style.Add("display", "none");
                }
            }

            rep.Save(my);

            ViewState.Add("EditedId", -1);
            ViewState.Add("EditedIndex", -1);

            btnSave.Style.Add("display", "none");
            btnCancel.Style.Add("display", "none");

            isEditing = true;

            UpdateData();
        }
    }
}