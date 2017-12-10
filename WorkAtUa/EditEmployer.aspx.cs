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
    public partial class EditEmployer : System.Web.UI.Page
    {
        IDbManager userManager;
        UserRepositoryBase userBase;
        CompanyRepositoryBase companyBase;

        MyUser user;
        MyCompanyDetail company;

        protected void Page_Load(object sender, EventArgs e)
        {
            userManager = IoCContainer.ServiceLocator.Resolve<IDbManager>();
            userBase = (UserRepositoryBase)IoCContainer.ServiceLocator.Resolve<IRepository<MyUserDataGrid>>();
            companyBase = (CompanyRepositoryBase)IoCContainer.ServiceLocator.Resolve<IRepository<MyCompanyDetail>>();

            user = this.Session["CurrentUser"] as MyUser;
            company = userManager.GetCompanyDetailById(user.CompanyDetails);

            if (!IsPostBack)
            {
                userCategories.DataSource = userManager.GetCategories().Select(c => c.Name).ToList();
                userCategories.DataBind();

                userCity.DataSource = userManager.GetCities();
                userCity.DataBind();

                userPassword.Text = user.Password;
                companyName.Text = company.Name;
                companyType.SelectedIndex = company.TypeOfCompany;
                companyWebSite.Text = company.WebSite;

                userCity.SelectedValue = user.City;
                userBirthay.Value = user.Birthday.ToShortDateString();
                userName.Text = user.Name;
                userSurname.Text = user.Surname;
                userFathersName.Text = user.FathersName;
                userPhone.Text = user.Phone;

                foreach (ListItem i in userCategories.Items)
                {
                    if (user.Categories.Contains(userManager.GetCategoryIdByName(i.Text)))
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

                my.Id = user.Id;
                my.Email = user.Email;
                my.Password = userPassword.Text;
                my.Birthday = DateTime.Parse(userBirthay.Value);
                my.Name = userName.Text;
                my.Surname = userSurname.Text;
                my.FathersName = userFathersName.Text;
                my.Phone = userPhone.Text;
                my.City = userCity.SelectedValue;
                my.Roles = "Employer";
                

                MyCompanyDetail detail = new MyCompanyDetail();
                detail.Id = company.Id;
                detail.Name = companyName.Text;
                detail.WebSite = companyWebSite.Text;
                detail.TypeOfCompany = (companyType.Items.IndexOf(companyType.SelectedItem) == 0) ? 0 : 1;

                List<string> categories = userCategories.Items.OfType<ListItem>().Where(i => i.Selected).Select(i1 => i1.Text).ToList();

                for (int i = 0; i < categories.Count; i++)
                {
                    if (i == categories.Count - 1)
                        my.Categories += categories[i];
                    else
                        my.Categories += (categories[i] + ", ");
                }


                companyBase.Save(detail);

                my.CompanyDetails = userManager.GetLastCompId();

                userBase.Save(my);

                Response.Redirect("~/Employer.aspx");
            }

        }
    }
}