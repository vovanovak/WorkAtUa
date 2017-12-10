using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WorkAtUa.DBManager;
using WorkAtUa.Entities;
using WorkAtUa.Core.ServiceLocator;


namespace WorkAtUa
{
    public partial class RegisterEmployee : System.Web.UI.Page
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
                 my.Roles.Add(userManager.GetRoleIdByName("Employee"));

                 MyUserDetail detail = new MyUserDetail();

                 List<string> categories = new List<string>();

                 foreach (ListItem i in userCategories.Items)
                 {
                     if (i.Selected)
                     {
                         categories.Add(i.Text);
                     }
                 }

                 List<int> categoryId = new List<int>();

                 foreach (var c in categories)
                 {
                     categoryId.Add(userManager.GetCategoryIdByName(c));
                 }

                 my.Categories = categoryId;

                 userManager.AddUser(my, null, detail);

                 Response.Redirect("~/SignIn.aspx");
             }
        }
    }
}