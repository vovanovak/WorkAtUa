using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WorkAtUa.Core.ServiceLocator;
using WorkAtUa.DBManager;
using WorkAtUa.Entities;

namespace WorkAtUa
{
    public partial class EditEmployee : System.Web.UI.Page
    {
        IDbManager userManager;
        UserRepositoryBase userBase;
        ResumeRepositoryBase resumeBase;
        MyUser userToEdit;
        MyUserDetail detail;
        MyJobExperience exp;
        MyEducation edu;

        protected void Page_Load(object sender, EventArgs e)
        {
            userBase = (UserRepositoryBase)IoCContainer.ServiceLocator.Resolve<IRepository<MyUserDataGrid>>();
            resumeBase = (ResumeRepositoryBase)IoCContainer.ServiceLocator.Resolve<IRepository<MyUserDetail>>();
            userManager = IoCContainer.ServiceLocator.Resolve<IDbManager>();

            userToEdit = this.Session["CurrentUser"] as MyUser;
            detail = userManager.GetUserDetailById(userToEdit.UserDetails);
            if (detail.JobExperience == -1)
            {
                exp = new MyJobExperience();
            }
            else
            {
                exp = userManager.GetJobExpById(detail.JobExperience);
            }
            if (detail.Education == -1)
            {
                edu = new MyEducation();
            }
            else
            {
                edu = userManager.GetEducationById(detail.Education);
            }

            if (!IsPostBack)
            {
                userCategories.DataSource = userManager.GetCategories().Select(c => c.Name).ToList();
                userCategories.DataBind();

                userCity.DataSource = userManager.GetCities();
                userCity.DataBind();

                userCity.SelectedValue = userToEdit.City;
                userBirthay.Value = userToEdit.Birthday.ToString("dd.mm.yyyy");
                userName.Text = userToEdit.Name;
                userSurname.Text = userToEdit.Surname;
                userFathersName.Text = userToEdit.FathersName;
                userPhone.Text = userToEdit.Phone;

                

                txtVacancyHeader.Text = detail.Header;
                txtSalary.Text = detail.Salary.ToString();
                txtSelfDesc.Text = detail.Description;
                
                txtCompany.Text = exp.Company;
                txtCityExp.Text = exp.City;
                txtPost.Text = exp.Post;
                txtStartDate.Text = exp.Start.ToShortDateString();
                txtEndDate.Text = exp.End.ToShortDateString();
                txtDesc.Text = exp.Desc;

                dropEduLevel.SelectedValue = edu.Level;
                txtPlace.Text = edu.Place;
                txtSpeciality.Text = edu.Speciality;

                

                foreach (ListItem i in userCategories.Items)
                {
                    if (userToEdit.Categories.Contains(userManager.GetCategoryIdByName(i.Text)))
                        i.Selected = true;
                }
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
                MyUserDataGrid my = new MyUserDataGrid();
                my.Id = userToEdit.Id;
                my.Email = userToEdit.Email;
                my.Password = userToEdit.Password;
                my.Birthday = DateTime.Parse(userBirthay.Value);
                my.Name = userName.Text;
                my.Surname = userSurname.Text;
                my.FathersName = userFathersName.Text;
                my.Phone = userPhone.Text;
                my.City = userCity.SelectedValue;
                my.UserDetails = detail.Id;

                StringBuilder roles = new StringBuilder();
                for (int i = 0; i < userToEdit.Roles.Count; i++)
                {
                    if (i == userToEdit.Roles.Count - 1)
                    {
                        roles.Append(userManager.GetRoleNameById(userToEdit.Roles[i]));
                    }
                    else
                    {
                        roles.Append(userManager.GetRoleNameById(userToEdit.Roles[i]) + ", ");
                    }
                }

                my.Roles = roles.ToString();
                roles.Clear();

                for (int i = 0; i < userCategories.Items.Count; i++)
                {
                    if (userCategories.Items[i].Selected)
                    {
                        if (i == userCategories.Items.Count - 1)
                        {
                            roles.Append(userCategories.Items[i].Text);
                        }
                        else
                        {
                            roles.Append(userCategories.Items[i].Text + ", ");
                        }
                    }

                }

                my.Categories = roles.ToString();

                userBase.Save(my);

                MyUserDetail ndetail = new MyUserDetail();
                MyJobExperience nexp = new MyJobExperience();
                MyEducation nedu = new MyEducation();

                ndetail.Id = detail.Id;
                ndetail.Header = txtVacancyHeader.Text;
                ndetail.Salary = Convert.ToDecimal(txtSalary.Text);
                ndetail.Description = txtDesc.Text;
                ndetail.Education = edu.Id;
                ndetail.JobExperience = exp.Id;

                nexp.Id = exp.Id;
                nexp.Company = txtCompany.Text;
                nexp.City = txtCityExp.Text;
                nexp.Post = txtPost.Text;
                nexp.Start = DateTime.Parse(txtStartDate.Text);
                nexp.End = DateTime.Parse(txtEndDate.Text);
                nexp.Desc = txtDesc.Text;

                nedu.Id = edu.Id;
                nedu.Level = dropEduLevel.SelectedValue;
                nedu.Place = txtPlace.Text;
                nedu.Speciality = txtSpeciality.Text;

                userManager.EditEducation(nedu);
                userManager.EditJobExp(nexp);

                if (nedu.Id == -1 || nexp.Id == -1)
                {
                    ndetail.Education = userManager.GetLastEduId();
                    ndetail.JobExperience = userManager.GetLastJobExpId();
                }

                resumeBase.Save(ndetail);

                Response.Redirect("~/Employee");
            }
        }

    }
}