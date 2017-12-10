using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WorkAtUa.Core.ServiceLocator;
using WorkAtUa.DBManager;
using WorkAtUa.Entities;

namespace WorkAtUa
{
    public partial class RegisterVacancy : System.Web.UI.Page
    {
        MyUser user;
        IDbManager userManager;
        protected void Page_Load(object sender, EventArgs e)
        {
            userManager = IoCContainer.ServiceLocator.Resolve<IDbManager>();
            
            if (Session["CurrentUser"] == null)
            {
                user = new MyUser();
                user = userManager.GetUserById(2);
                user.CompanyDetails = 1;
            }
            else
            {
                user = Session["CurrentUser"] as MyUser;
            }

            if (!IsPostBack)
            {
                vacCategories.DataSource = userManager.GetCategories().Select(c => c.Name);
                vacCategories.DataBind();
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
                MyVacancy my = new MyVacancy();
                my.ProfessionName = userProfession.Text;
                my.MinSalary = Convert.ToInt32(txtMinSalary.Text);
                my.MaxSalary = Convert.ToInt32(txtMaxSalary.Text);
                my.Requirments = txtReq.Text;
                my.TypeOfEmployment = txtTypeOfEmp.Text;
                my.Description = txtDesc.Text;
                my.CompanyId = user.CompanyDetails;
                my.Categories = vacCategories.Items.OfType<ListItem>().Where(i => i.Selected == true).Select(i1 => i1.Text).ToList();
                my.EmployerName = user.Surname + " " + user.Name + " " + user.FathersName;
                my.EmployerPhone = user.Phone;
                my.City = userCity.SelectedValue;
                my.CreationTime = DateTime.Now;

                userManager.RegisterVacancy(my);

                Response.Redirect("~/Employer");
            }
        }


    }
}