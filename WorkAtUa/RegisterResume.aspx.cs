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
    public partial class RegisterResume : System.Web.UI.Page
    {
        MyUser user;
        IDbManager userManager;
        protected void Page_Load(object sender, EventArgs e)
        {
            userManager = IoCContainer.ServiceLocator.Resolve<IDbManager>();

            if (!IsPostBack)
            {
                chkBoxCategories.DataSource = userManager.GetCategories().Select(c => c.Name);
                chkBoxCategories.DataBind();
            }

            if (Session["CurrentUser"] == null)
            {
                user = new MyUser();
                user.CompanyDetails = 1;
            }
            else
            {
                user = Session["CurrentUser"] as MyUser;
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

                MyUserDetail detail = new MyUserDetail();
                MyJobExperience exp = new MyJobExperience();
                MyEducation edu = new MyEducation();

                detail.Header = txtVacancyHeader.Text;
                detail.Salary = Convert.ToDecimal(txtSalary.Text);
                detail.Description = txtDesc.Text;

                exp.Company = txtCompany.Text;
                exp.City = txtCity.Text;
                exp.Post = txtPost.Text;
                exp.Start = DateTime.Parse(txtStartDate.Text);
                exp.End = DateTime.Parse(txtEndDate.Text);
                exp.Desc = txtDesc.Text;

                edu.Level = dropEduLevel.SelectedValue;
                edu.Place = txtPlace.Text;
                edu.Speciality = txtSpeciality.Text;


                userManager.RegisterResume(user, detail, edu, exp);
            }

            else
            {
                txtCompany.Text = "";
                txtCity.Text = "";
                txtPost.Text = "";
                txtStartDate.Text = "";
                txtEndDate.Text = "";
                txtDesc.Text = "";
            }
        }

        
    }
}