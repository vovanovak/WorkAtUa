using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WorkAtUa.DBManager;
using WorkAtUa.Entities;
using WorkAtUa.Core.ServiceLocator;

namespace WorkAtUa
{
    public partial class SignIn : System.Web.UI.Page
    {
        string[] employeeFeatures = new string[] { "Create resume", "Receive new vacancies by email", "Select liked vacancies" };
        string[] employerFeatures = new string[] { "Create vacancy", "Receive new resumes by email", "Select liked resumes" };
        IDbManager userManager;

        protected void Page_Load(object sender, EventArgs e)
        {
            userManager = IoCContainer.ServiceLocator.Resolve<IDbManager>();

            if (this.Response.Cookies["Id"].Value != null)
            {
                MyUser u = userManager.GetUserById(Convert.ToInt32(this.Response.Cookies["Id"]));

                List<string> roles = userManager.GetUserRoles(u.Email);

                this.Session.Add("Email", txtEmail.Text);
                this.Session.Add("Password", txtPassword.Text);
                this.Session.Add("Role", roles);
                this.Session.Add("Name", u.Name + " " + u.Surname);
                this.Session.Add("Id", u.Id);
                this.Session.Add("CurrentUser", u);

                if (roles.Contains("Admin"))
                {
                    Response.Redirect("~/Admin/Main.aspx");
                }
                if (roles.Contains("Employer"))
                {
                    Response.Redirect("~/Employer");
                }
                if (roles.Contains("Employee"))
                {
                    Response.Redirect("~/Employee/");
                }
            }
            
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            bool res = userManager.IsValid(txtEmail.Text, txtPassword.Text);
            if (res)
            {
                List<string> roles = userManager.GetUserRoles(txtEmail.Text);
                MyUser u = userManager.GetUserById(userManager.GetUserIdByEmail(txtEmail.Text));

               
                this.Session.Add("Email", txtEmail.Text);
                this.Session.Add("Password", txtPassword.Text);
                this.Session.Add("Role", roles);
                this.Session.Add("Name", u.Name + " " + u.Surname);
                this.Session.Add("Id", u.Id);
                this.Session.Add("CurrentUser", u);

                if(rememberMe.Checked) {
                    this.Response.Cookies.Add(new HttpCookie("Email", txtEmail.Text));
                    this.Response.Cookies.Add(new HttpCookie("Id", u.Id.ToString()));
                }

                if (roles.Contains("Admin"))
                {
                    Response.Redirect("~/Admin/Main.aspx");
                }
                if (roles.Contains("Employer"))
                {
                    Response.Redirect("~/Employer");
                }
                if (roles.Contains("Employee"))
                {
                    Response.Redirect("~/Employee/");
                }
            }
        }

        protected void userRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder("<ul id=\"mainSignInSelectControlsList\">");
            if (userRole.SelectedValue == "I'm employee")
            {
                foreach (var s in employeeFeatures)
                {
                    sb.Append(string.Format("<li><img src=\"Images/login-li.gif\" />&nbsp{0}</li>", s));
                }
                mainSignInSelectControlsListContainer.InnerHtml = sb.ToString();
            }
            else
            {

                foreach (var s in employerFeatures)
                {
                    sb.Append(string.Format("<li><img src=\"Images/login-li.gif\" />&nbsp{0}</li>", s));
                }
                
            }

            sb.Append("</ul>");
            mainSignInSelectControlsListContainer.InnerHtml = sb.ToString();
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (userRole.SelectedValue == "I'm employer")
            {
                Response.Redirect("~/RegisterEmployer.aspx");
            }
            else
            {
                Response.Redirect("~/RegisterEmployee.aspx");
            }
        }
    }
}