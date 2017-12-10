using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
using WorkAtUa.DAL;
using WorkAtUa.Entities;
using WorkAtUa.DBManager;
using WorkAtUa.Core.ServiceLocator;

namespace WorkAtUa
{
    public partial class RegUser : System.Web.UI.Page
    {
        IDbManager userManager;

        protected void Page_Load(object sender, EventArgs e)
        {
            userManager = IoCContainer.ServiceLocator.Resolve<IDbManager>();

           
            if (!IsPostBack)
            {
                userCategories.DataSource = userManager.GetCategories().Select(c => c.Name).ToList();
                userCategories.DataBind();

                userCity.DataSource = userManager.GetCities();
                userCity.DataBind();
            }
            this.PreRender += RegUser_PreRender;
        }

        void RegUser_PreRender(object sender, EventArgs e)
        {
            foreach (IValidator i in this.Validators)
            {
                i.Validate();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            foreach (IValidator i in this.Validators)
            {
                i.Validate();
            }

            if (IsValid)
            {

                MyUser my = new MyUser();
                my.Email = userEmail.Text;
                my.Password = userPassword.Text;
                my.Birthday = DateTime.Parse(userBirthay.Value);
                my.Name = userName.Text;
                my.Surname = userSurname.Text;
                my.FathersName = userFathersName.Text;
                my.Phone = userPhone.Text;
                my.City = userCity.SelectedValue;
                my.Roles.Add(userManager.GetRoleIdByName("Employer"));

                MyCompanyDetail detail = new MyCompanyDetail();
                detail.Name = companyName.Text;
                detail.WebSite = companyWebSite.Text;
                detail.TypeOfCompany = (companyType.Items.IndexOf(companyType.SelectedItem) == 0) ? 0 : 1;

                List<string> categories = userCategories.Items.OfType<ListItem>().Where(i => i.Selected).Select(i1 => i1.Text).ToList();
                List<int> categoryId = new List<int>();

                foreach (var c in categories)
                {
                    categoryId.Add(userManager.GetCategoryIdByName(c));
                }

                my.Categories = categoryId;

                userManager.AddUser(my, detail, null);

                Response.Redirect("~/SignIn.aspx");
            }
            
        }

        
    }
}