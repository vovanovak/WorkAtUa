using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.Entity;
using WorkAtUa.DAL;
using WorkAtUa.Entities;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace WorkAtUa.DBManager
{
    public class DbManager:IDbManager
    {
        static DBModel ctx = DBContextSingleton.Context;

        public static string GetUserFullNameById(int uId)
        {
            User user = ctx.Users.First(u => u.Id == uId);
            return string.Format("{0} {1} {2}", user.Surname, user.Name, user.FathersName);
        }

        #region Func

        Func<Vacancy, MyVacancy> vacToMy = new Func<Vacancy, MyVacancy>(c => new MyVacancy()
        {
            Id = c.Id,
            ProfessionName = c.ProfessionName,
            MinSalary = c.MinSalary,
            MaxSalary = c.MaxSalary,
            TypeOfEmployment = c.TypeOfEmployment,
            CompanyId = c.CompanyId,
            EmployerName = c.EmployerName,
            EmployerPhone = c.EmployerPhone,
            City = c.City,
            Requirments = c.Requirments,
            Description = c.Description,
            Categories = c.VacancyInCategories.Select(v => v.Category.Name).ToList(),
            IsSolved = c.IsSolved

        });

        #endregion

        public List<MySearchResult> GetSearchResults(int userId)
        {
           
           return ctx.SearchResults.Where(r => r.SearchAgent.UserId == userId).
               Select(r1 => new MySearchResult()
               { Id = r1.Id, AgentId = r1.AgentId, VacancyId = r1.VacancyId }).ToList();
        }
        public static int GetUserCount()
        {
            
            return ctx.Users.Count();
        }
        public static int GetVacancyCount1()
        {
            
            return ctx.Vacancies.Count();
        }

        public int GetVacancyCount()
        {
            
            return ctx.Vacancies.Count();
        }

        public MyUser GetUserByEmail(string email)
        {
            
            return UserToMyUser(ctx.Users.FirstOrDefault(u => u.Email == email));
        }

        public static MyVacancy VacToMy(Vacancy v)
        {
            MyVacancy my = new MyVacancy();

            my.Id = v.Id;
            my.ProfessionName = v.ProfessionName;
            my.MinSalary = v.MinSalary;
            my.MaxSalary = v.MaxSalary;
            my.Requirments = v.Requirments;
            my.TypeOfEmployment = v.TypeOfEmployment;
            my.Description = v.Description;
            my.EmployerName = v.EmployerName;
            my.EmployerPhone = v.EmployerPhone;
            my.City = v.City;
            my.CompanyId = v.CompanyId;
            my.CreationTime = v.CreationTime.Value;
            my.IsSolved = v.IsSolved;

            return my;
        }

        public static MyVacancy GetVacancyById(int id)
        {
            
            return VacToMy(ctx.Vacancies.First(v => v.Id == id));
        }

        public static string GetObjDetails(object obj)
        {
           
            StringBuilder builder = new StringBuilder();

            try
            {

                foreach (var i in obj.GetType().GetProperties())
                {
                    builder.AppendLine(string.Format("{0}: {1}", i.Name, i.GetValue(obj)));
                }
            }
            catch (Exception e)
            {

            }

            return builder.ToString();
        }

        public static int GetUserIdByEmail1(string email)
        {
            

            int? id = null;

            try
            {
                id = ctx.Users.First(c => c.Email == email).Id;
            }
            catch (Exception e)
            {

            }

            return id == null ? -1 : id.Value;
        }

        public static string GetUserEmailById(int id)
        {
            

            string str = null;

            try
            {
                str = ctx.Users.First(c => c.Id == id).Email;
            }
            catch (Exception e)
            {

            }

            return str;
        }

        public static int GetCompanyIdByName(string name)
        {
            

            int? id = null;

            try
            {
                id = ctx.CompanyDetails.First(c => c.Name == name).Id;
            }

            catch (Exception e)
            {

            }

            return id == null ? -1 : id.Value;
        }

        public static string GetCompanyNameById(int id)
        {
            

            string str = null;

            try
            {
                str = ctx.CompanyDetails.First(c => c.Id == id).Name;
            }
            catch (Exception e)
            {

            }

            return str;
        }
        
        public static MyUser UserToMyUser(User u)
        {
            List<int> rolesId = u.UserInRoles.Where(r => r.UserId == u.Id).Select(r => r.RoleId).ToList();
            List<int> categoriesId = u.UserInCategories.Where(c => c.UserId == u.Id).Select(c => c.CategoryId).ToList();

            return new MyUser(u.Id, u.Email, u.Password, u.Birthday, 
                    u.Name, u.Surname, u.FathersName, u.Phone, u.City.Name, (u.CompanyDetail == null) ? -1 : u.CompanyDetail.Id,
                    (u.UserDetail == null)? -1 : u.UserDetail.Id, rolesId,
                    categoriesId);
        }

        public List<MyUser> GetAllUsers()
        {
            
                    
            List<MyUser> users = new List<MyUser>();
            
            
            foreach (var u in ctx.Users)
            {
                users.Add(UserToMyUser(u));
            }

            return users;
        }

        public MyUser GetUserById(int id)
        {
            
            User u = ctx.Users.First(user => user.Id == id);
            return UserToMyUser(u);
        }

        public void AddCompanyDetail(MyCompanyDetail companyDetail)
        {
            
            ctx.CompanyDetails.Add(new CompanyDetail() { Name = companyDetail.Name, 
                TypeOfCompany = companyDetail.TypeOfCompany, WebSite = companyDetail.WebSite });
            ctx.SaveChanges();
           
        }
        public void AddUserRoles(MyUser my, User u)
        {
            
            List<UserInRole> roles = new List<UserInRole>();
            foreach (var i in my.Roles)
            {
                roles.Add(new UserInRole() { RoleId = i, UserId = u.Id });
            }
            ctx.UserInRoles.AddRange(roles.ToArray());
            ctx.SaveChanges();
        }

        public void AddUserCategories(MyUser my, User u)
        {
            
            List<UserInCategory> categories = new List<UserInCategory>();
            foreach (var i in my.Categories)
            {
                categories.Add(new UserInCategory() { UserId = u.Id, CategoryId = i });
            }
            ctx.UserInCategories.AddRange(categories.ToArray());
            ctx.SaveChanges();
        }

        public void AddUserDetail(MyUserDetail my)
        {
            
            ctx.UserDetails.Add(new UserDetail()
            {
                Header = my.Header,
                Description = my.Description,
                Salary = my.Salary
            });
            ctx.SaveChanges();
        }

        public void AddUser(MyUser my, MyCompanyDetail companyDetail, MyUserDetail userDetail)
        {
            

            User u = new User();
            u.Email = my.Email;
            u.Password = my.Password;
            u.Birthday = my.Birthday;
            u.Name = my.Name;
            u.Surname = my.Surname;
            u.FathersName = my.FathersName;
            u.Phone = my.Phone;
            u.CityId = GetCityIdByName(my.City);
            
            if (companyDetail != null)
            {
                AddCompanyDetail(companyDetail);
                int count = ctx.CompanyDetails.Count();
                u.CompanyDetails = ctx.CompanyDetails.Max(d1 => d1.Id);
            }
            if (userDetail != null)
            {
                AddUserDetail(userDetail);
                int count = ctx.UserDetails.Count();
                u.UserDetails = ctx.UserDetails.Max(d1 => d1.Id);
            }

            ctx.Users.Add(u);
            ctx.SaveChanges();

            u.Id = ctx.Users.Max(user=>user.Id);

            AddUserRoles(my, u);
            AddUserCategories(my, u);
        }

        public bool IsValid(string email, string password)
        {
            
            int count = ctx.Users.Where(u => u.Email == email && u.Password == password).Count();
            return count > 0;
        }

        public MyCompanyDetail GetCompanyDetailById(int id)
        {
            
            CompanyDetail detail = ctx.CompanyDetails.First(d => d.Id == id);
            MyCompanyDetail my = new MyCompanyDetail();

            my.Id = detail.Id;
            my.Name = detail.Name;
            my.WebSite = detail.WebSite;
            my.TypeOfCompany = detail.TypeOfCompany;

            return my;
        }

        public MyUserDetail GetUserDetailById(int id)
        {
            
            UserDetail detail = ctx.UserDetails.First(d => d.Id == id);
            MyUserDetail my = new MyUserDetail();
            my.Id = detail.Id;
            my.Header = detail.Header;
            my.Salary = detail.Salary;
            my.Description = detail.Description;
            if (detail.EducationId == null)
                my.Education = -1;
            else
                my.Education = detail.EducationId.Value;
            if (detail.JobExperienceId == null)
                my.JobExperience = -1;
            else
                my.JobExperience = detail.JobExperienceId.Value;

            return my;
        }


        public List<MyRole> GetRoles()
        {
            
            List<MyRole> roles = new List<MyRole>();
            foreach (var r in ctx.Roles)
            {
                roles.Add(new MyRole(r.Id, r.Name));
            }
            return roles;
        }

        public List<MyCategory> GetCategories()
        {
            
            List<MyCategory> categories = new List<MyCategory>();
            foreach (var c in ctx.Categories)
            {
                categories.Add(new MyCategory(c.Id, c.Name));
            }
            return categories;
        }


        public int GetRoleIdByName(string name)
        {
            
            return ctx.Roles.First(r => r.Name == name).Id;
        }

        public int GetCategoryIdByName(string name)
        {
            
            return ctx.Categories.First(c => c.Name == name).Id;
        }


        public bool CheckEmailUnique(string email)
        {
            
            return (ctx.Users.Where(u => u.Email == email).Count() == 0) ? true : false;
        }


        public List<string> GetCities()
        {
            
            return ctx.Cities.Select(c => c.Name).ToList();
        }

        public string GetCityNameById(int id)
        {
            
            return ctx.Cities.First(c => c.Id == id).Name;
        }

        public int GetCityIdByName(string name)
        {
            
            return ctx.Cities.First(c => c.Name == name).Id;
        }


        public List<string> GetUserRoles(string email)
        {
            

            User user = ctx.Users.First(u => u.Email == email);

            List<string> roles = user.UserInRoles.Select(u1 => u1.Role.Name).ToList();

            return roles;
        }


        public int GetUserIdByEmail(string email)
        {
            
            return ctx.Users.First(u => u.Email == email).Id;
        }


        public string GetRoleNameById(int id)
        {
            
            return ctx.Roles.First(u => u.Id == id).Name;
        }

        public void DeleteUserById(int id)
        { 
            

            List<UserInRole> userInRole = ctx.UserInRoles.Where(u => u.UserId == id).ToList();
            foreach (var r in userInRole)
            {
                ctx.UserInRoles.Remove(r);
            }

            List<UserInCategory> userInCat = ctx.UserInCategories.Where(u => u.UserId == id).ToList();
            foreach (var r in userInCat)
            {
                ctx.UserInCategories.Remove(r);
            }

            User user = ctx.Users.First(u => u.Id == id);
            if (user.UserDetails != null)
            {
                ctx.Educations.Remove(ctx.Educations.First(e => e.Id == user.UserDetail.EducationId));
                ctx.JobExperiences.Remove(ctx.JobExperiences.First(e => e.Id == user.UserDetail.JobExperienceId));

                UserDetail detail = ctx.UserDetails.First(d => d.Id == user.UserDetails);
                ctx.UserDetails.Remove(detail);
            }
            if (user.CompanyDetails != null)
            {
                CompanyDetail detail = ctx.CompanyDetails.First(d => d.Id == user.CompanyDetails);
                ctx.CompanyDetails.Remove(detail);
            }

            ctx.Users.Remove(user);
            ctx.SaveChanges();
        }


        public void RegisterVacancy(MyVacancy my)
        {
            

            Vacancy vac = new Vacancy();
            vac.ProfessionName = my.ProfessionName;
            vac.MinSalary = my.MinSalary;
            vac.MaxSalary = my.MaxSalary;
            vac.CompanyId = my.CompanyId;
            vac.Requirments = my.Requirments;
            vac.TypeOfEmployment = my.TypeOfEmployment;
            vac.Description = my.Description;
            vac.EmployerName = my.EmployerName;
            vac.EmployerPhone = my.EmployerPhone;
            vac.City = my.City;
            vac.CreationTime = my.CreationTime;
            vac.IsSolved = my.IsSolved;

            ctx.Vacancies.Add(vac);
            ctx.SaveChanges();

            my.Id = ctx.Vacancies.Max(v => v.Id);

            AddVacancyInCategory(my);
        }

        public void AddVacancyInCategory(MyVacancy my)
        {
            

            foreach (var i in my.Categories)
            {
                ctx.VacancyInCategories.Add(new VacancyInCategory() { CategoryId = GetCategoryIdByName(i), VacancyId = my.Id });
            }

            ctx.SaveChanges();
        }


        public void RegisterResume(MyUser u, MyUserDetail my, MyEducation edu, MyJobExperience exp)
        {
            

            AddEducation(edu);
            AddJobExp(exp);

            int eduId = ctx.Educations.Max(e => e.Id);
            int expId = ctx.JobExperiences.Max(e => e.Id);
            my.Education = eduId;
            my.JobExperience = expId;

            UserDetail detail = new UserDetail();

            detail.Header = my.Header;
            detail.Salary = my.Salary;
            detail.Description = my.Description;
            detail.EducationId = my.Education;
            detail.JobExperienceId = my.JobExperience;

            ctx.UserDetails.Add(detail);

            ctx.Users.First(u1 => u1.Id == u.Id).UserDetail = detail;

            ctx.SaveChanges();
        }

        public void AddEducation(MyEducation edu)
        {
            

            Education e = new Education();
            e.Level = edu.Level;
            e.Place = edu.Place;
            e.Speciality = edu.Speciality;

            ctx.Educations.Add(e);

            ctx.SaveChanges();
        }

        public void AddJobExp(MyJobExperience exp)
        {
            

            JobExperience e = new JobExperience();
            e.Company = exp.Company;
            e.City = exp.City;
            e.Post = exp.Post;
            e.Start = exp.Start;
            e.End = exp.End;
            e.Desc = exp.Desc;

            ctx.JobExperiences.Add(e);

            ctx.SaveChanges();
        }

        public MyUser ToUser(MyUserDataGrid my)
        {
            List<int> roles = new List<int>();
            List<int> categories = new List<int>();

            List<string> rolesNames = my.Roles.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach (var i in rolesNames) {
                roles.Add(GetRoleIdByName(i));
            }

            List<string> categoriesNames = my.Categories.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach (var i in categoriesNames)
            {
                categories.Add(GetCategoryIdByName(i));
            }


            return new MyUser(my.Id, my.Email, my.Password, my.Birthday,
            my.Name, my.Surname, my.FathersName, my.Phone, my.City,
            my.CompanyDetails, my.UserDetails, roles, categories);
        }

        public MyUserDataGrid ToUserDataGrid(MyUser my)
        {
            StringBuilder sbRoles = new StringBuilder();
            StringBuilder sbCategories = new StringBuilder();

            for (int i = 0; i < my.Roles.Count;i++)
            {
                if (i != my.Roles.Count - 1)
                {
                    sbRoles.Append(GetRoleNameById(my.Roles[i]) + ", ");
                }
                else
                {
                    sbRoles.Append(GetRoleNameById(my.Roles[i]));
                }
            }

            for (int i = 0; i < my.Categories.Count; i++)
            {
                if (i != my.Categories.Count - 1)
                {
                    sbCategories.Append(GetCategoryNameById(my.Categories[i]) + ", ");
                }
                else
                {
                    sbCategories.Append(GetCategoryNameById(my.Categories[i]));
                }
            }

            

            return new MyUserDataGrid(my.Id, my.Email, my.Password, my.Birthday,
            my.Name, my.Surname, my.FathersName, my.Phone, my.City,
            my.CompanyDetails, my.UserDetails, sbRoles.ToString(), sbCategories.ToString());
        }

       

        public string GetCategoryNameById(int id)
        {
            
            return ctx.Categories.First(c => c.Id == id).Name;
        }

        public List<MyUserDataGrid> GetAllDataGridUsers()
        {
            
            List<MyUserDataGrid> usersGrid = new List<MyUserDataGrid>();
            List<MyUser> users = GetAllUsers();
            foreach (var u in users)
            {
                usersGrid.Add(ToUserDataGrid(u));
            }
            return usersGrid;
        }


        public List<MyVacancy> GetVacancies()
        {
            
            List<MyVacancy> my = new List<MyVacancy>();

            foreach (var i in ctx.Vacancies)
            {
                List<string> categories = i.VacancyInCategories.Select(c => c.Category.Name).ToList();

                my.Add(new MyVacancy() { Id = i.Id, CompanyId = i.CompanyId, 
                Description = i.Description, MinSalary = i.MinSalary, MaxSalary = i.MaxSalary,
                ProfessionName = i.ProfessionName, Requirments = i.Requirments,
                TypeOfEmployment = i.TypeOfEmployment, Categories = categories, IsSolved = i.IsSolved
                });
            }

            return my;
        }

        public List<MyComment> GetComments()
        {
            
            List<MyComment> comments = new List<MyComment>();

            foreach (var c in ctx.Comments)
            {
                comments.Add(new MyComment(c.Id, c.UserId, c.VacancyId, c.Date, c.Content));
            }

            return comments;
        }

        public List<MyUserDetail> GetResumes()
        {
            
            List<MyUserDetail> resumes = new List<MyUserDetail>();

            foreach (var r in ctx.UserDetails)
            {
                resumes.Add(new MyUserDetail(r.Id, r.Header, r.Salary, r.Description, r.EducationId.Value, r.JobExperienceId.Value));
            }

            return resumes;
        }

        public List<MyCompanyDetail> GetCompanies()
        {
            
            List<MyCompanyDetail> companies = new List<MyCompanyDetail>();

            foreach (var c in ctx.CompanyDetails)
            {
                companies.Add(new MyCompanyDetail(c.Id, c.Name, c.TypeOfCompany, c.WebSite));
            }

            return companies;
        }


        


        public MyVacancy GetVacancyByIdNonSt(int id)
        {
            

            return VacToMy(ctx.Vacancies.First(v => v.Id == id));
        }


        public List<MyVacancy> FindVacancies(string request, string city,
            bool searchOnlyInHeader, bool searchAnyOfTheseWords,
            string category, int salary, int range, string typeOfEmp)
        {
            
            
            List<MyVacancy> vacancies = ctx.Vacancies.Select(vacToMy).ToList();

            if (searchOnlyInHeader)
            {
                for (int i = 0; i < vacancies.Count; i++)
                {
                    if (!vacancies[i].ProfessionName.ToLower().Contains(request))
                        vacancies.RemoveAt(i);
                }
                return vacancies;
            }
            else
            {
                vacancies = FindVacancies(request, city);

                if (!string.IsNullOrEmpty(category) && !string.IsNullOrWhiteSpace(category))
                {
                    if (category != "All Categories")
                    {
                        for (int i = 0; i < vacancies.Count; i++)
                        {
                            if (!vacancies[i].Categories.Contains(category))
                                vacancies.RemoveAt(i);
                        }
                    }
                }

                if (!string.IsNullOrEmpty(typeOfEmp) && !string.IsNullOrWhiteSpace(typeOfEmp))
                {
                    for (int i = 0; i < vacancies.Count; i++)
                    {
                        if (!vacancies[i].TypeOfEmployment.Contains(category))
                            vacancies.RemoveAt(i);
                    }
                }

                //if (salary > 0)
                //{
                //    for (int i = 0; i < vacancies.Count; i++)
                //    {
                //        if (!(vacancies[i].MinSalary >= salary - range
                //            && vacancies[i].MaxSalary <= salary + range))
                //            vacancies.RemoveAt(i);
                //    }
                //}
            }

            return vacancies;
        }


        public List<MySearchAgent> GetSearchAgents()
        {
            
            return ctx.SearchAgents.Select(s => new MySearchAgent() { Id = s.Id, UserId = s.UserId, Request = s.Request}).ToList();
        }

        public void FilterVacancies(List<MyVacancy> vacancies, int userId)
        {
            

            foreach (var s in ctx.SearchResults)
            {
                for (int i = 0; i < vacancies.Count; i++)
                {
                    if (s.SearchAgent == null)
                        s.SearchAgent = ctx.SearchAgents.First(s1 => s1.Id == s.AgentId);
                    if (s.VacancyId == vacancies[i].Id && s.SearchAgent.UserId == userId)
                    {
                        vacancies.RemoveAt(i);
                        break;
                    }

                }
            }
        }


        public void AddSearchResult(int agentId, int vacancyId)
        {
            

            ctx.SearchResults.Add(new DAL.SearchResult() { VacancyId = vacancyId, AgentId = agentId });
            ctx.SaveChanges(); 
        }



        public void AddSearchAgent(MySearchAgent agent)
        {
            
            ctx.SearchAgents.Add(new SearchAgent() { Request = agent.Request, UserId = agent.UserId });
            ctx.SaveChanges();
        }


        public List<MySearchAgent> GetSearchAgentsByUserId(int id)
        {
            

            List<MySearchAgent> agents = new List<MySearchAgent>();

            foreach (SearchAgent s in ctx.SearchAgents)
            {
                if (s.UserId == id)
                {
                    agents.Add(new MySearchAgent(s.Id, s.UserId, s.Request));
                }
            }

            return agents;
        }


        public void DeleteSearchAgent(int id)
        {
            

            List<SearchAgent> agents = ctx.SearchAgents.Where(a => a.Id == id).ToList();
            agents.ForEach(new Action<SearchAgent>((SearchAgent s) => 
            {
                ctx.SearchAgents.Remove(s);
            }));

            ctx.SaveChanges();
        }


        public List<MyVacancy> GetUserVacancies(int userId)
        {
            
            
            return ctx.VacancyInUsers.Where(u => u.UserId == userId).Select(c => new MyVacancy()
            {
                Id = c.Vacancy.Id,
                ProfessionName = c.Vacancy.ProfessionName,
                MinSalary = c.Vacancy.MinSalary,
                MaxSalary = c.Vacancy.MaxSalary,
                TypeOfEmployment = c.Vacancy.TypeOfEmployment,
                CompanyId = c.Vacancy.CompanyId,
                EmployerName = c.Vacancy.EmployerName,
                EmployerPhone = c.Vacancy.EmployerPhone,
                City = c.Vacancy.City,
                Requirments = c.Vacancy.Requirments,
                Description = c.Vacancy.Description,
                Categories = c.Vacancy.VacancyInCategories.Select(v => v.Category.Name).ToList(),
                IsSolved = c.Vacancy.IsSolved

            }).ToList();

        }


        public void SubscribeForAVacancy(int vacId, int userId)
        {
            
            ctx.VacancyInUsers.Add(new VacancyInUser() { UserId = userId, VacancyId = vacId });
            ctx.SaveChanges();
        }


        public void UnsubscribeFromVacancy(int vacId, int userId)
        {
            

            VacancyInUser elem = ctx.VacancyInUsers.First(v => v.VacancyId == vacId && v.UserId == userId);
            ctx.VacancyInUsers.Remove(elem);

            ctx.SaveChanges();
        }


        public List<MyComment> GetCommentsByVacancyId(int vacId)
        {
            

            List<Comment> comments = ctx.Comments.Where(v => vacId == v.VacancyId).ToList();
            List<MyComment> res = comments.Select(c => new MyComment() { Id = c.Id, VacancyId = c.VacancyId, Date = c.Date, Content = c.Content, UserId = c.UserId }).ToList();

            return res;
        }


        public void AddComment(MyComment my)
        {
            

           
            ctx.Comments.Add(new Comment() { Content = my.Content, Date = my.Date, UserId = my.UserId, VacancyId = my.VacancyId });
        
            ctx.SaveChanges();
        }

        public static object GetUserNameById(int p)
        {
            
            User u = ctx.Users.First(u1 => u1.Id == p);
            return u.Name + " " + u.Surname;
        }


        public bool IsUserSubscribedForAVacancy(int userId, int vacId)
        {
            
            return ctx.Vacancies.First(v => v.Id == vacId).VacancyInUsers.Any(v1 => v1.UserId == userId);
        }


        public List<MyVacancy> GetEmployerVacancies(int p)
        {
            

            User u = ctx.Users.First(u1 => u1.Id == p);

            List<MyVacancy> vacancies = ctx.Vacancies.Where(v => v.EmployerPhone == u.Phone && v.EmployerName == (u.Surname + " " + u.Name + " " + u.FathersName)).Select(vacToMy).ToList();

            return vacancies;
        }

        public void MarkVacancyAsSolved(int vacId) 
        {
            
            ctx.Vacancies.First(v => v.Id == vacId).IsSolved = true;
            ctx.SaveChanges();
        }


        public void MarkVacancyAsUnsolved(int vacId)
        {
            
            ctx.Vacancies.First(v => v.Id == vacId).IsSolved = false;
            ctx.SaveChanges();
        }

        public static List<MyUser> GetSubscribedUsersByVacancyIdSt(int vacId)
        {
            
            return ctx.Vacancies.First(v => v.Id == vacId).VacancyInUsers.Select(v => UserToMyUser(v.User)).ToList();
        }

        public List<MyUser> GetSubscribedUsersByVacancyId(int vacId)
        {
            
            return ctx.Vacancies.First(v => v.Id == vacId).VacancyInUsers.Select(v => UserToMyUser(v.User)).ToList();
        }

        public List<MyVacancy> FindVacancies(string request, string city)
        {
            
            List<MyVacancy> lst = new List<MyVacancy>();
            List<Vacancy> vacs = ctx.Vacancies.ToList();
            List<Vacancy> vacancies = new List<Vacancy>();


            if (Regex.IsMatch(request, @"\d{1,}-\d{1,}[$$]"))
            {
                request = request.Remove(request.Length - 1);
                String[] strNums = request.Split('-');
                int num1 = Convert.ToInt32(strNums[0]);
                int num2 = Convert.ToInt32(strNums[1]);

                if (num1 < num2)
                {
                    foreach (var v in vacs)
                    {
                        if (v.MinSalary > num1 && v.MaxSalary < num2)
                        {
                            vacancies.Add(v);
                        }
                    }
                }
            }
            else
            {
                var cats = ctx.Categories.Where(c => c.Name == request).ToList();
                if (cats.Count > 0)
                {
                    foreach (var c in cats)
                    {
                        vacancies.AddRange(c.VacancyInCategories.Select(v => v.Vacancy));
                    }
                }
                else
                {
                    foreach (var v in vacs)
                    {
                        if (v.ProfessionName.ToLower().Contains(request.ToLower()) || v.Requirments.ToLower().Contains(request.ToLower()) || v.TypeOfEmployment.ToLower().Contains(request.ToLower()) || v.Description.ToLower().Contains(request.ToLower()))
                        {
                            if (!city.Contains("All Ukraine"))
                            {
                                if (v.City.Contains(city))
                                {
                                    vacancies.Add(v);
                                }
                            }
                            else
                            {
                                vacancies.Add(v);
                            }
                        }
                    }
                }
            }

            vacancies = vacancies.Where(v => v.IsSolved == false).ToList();
            lst = vacancies.Select(vacToMy).ToList();

            return lst;
        }

        public List<MyUser> FindResumes(string request, string city)
        {
            
            List<MyUser> lst = new List<MyUser>();
            List<User> users  = ctx.Users.ToList();
            List<User> res = new List<User>();


            var cats = ctx.Categories.Where(c => c.Name == request).ToList();
            if (cats.Count > 0)
            {
                foreach (var c in cats)
                {
                    res.AddRange(c.UserInCategories.Select(v => v.User));
                }
            }
            else
            {
                foreach (var u in users)
                {
                    if (u.UserDetails != null)
                    {
                        u.UserDetail = ctx.UserDetails.First(v => v.Id == u.UserDetails);
                        u.City = ctx.Cities.First(v => v.Id == u.CityId);

                        if (u.UserDetail.Description.ToLower().Contains(request.ToLower()) ||
                           
                            u.UserDetail.Header.ToLower().Contains(request.ToLower()) ||
                            u.Name.ToLower().Contains(request.ToLower()) ||
                            u.Surname.ToLower().Contains(request) ||
                            u.FathersName.ToLower().Contains(request) ||
                            u.Phone.ToLower().Contains(request) ||
                            u.Email.ToLower().Contains(request))
                        {
                            if (!city.Contains("All Ukraine"))
                            {
                                if (u.City.Name.Contains(city))
                                {
                                    res.Add(u);
                                    continue;
                                }
                            }
                            else
                            {
                                res.Add(u);
                                continue;
                            }
                        }
                        if (u.UserDetail.Education != null)
                        {
                            if (u.UserDetail.Education.Level.ToLower().Contains(request.ToLower()) ||
                                 u.UserDetail.Education.Place.ToLower().Contains(request.ToLower()) ||
                                 u.UserDetail.Education.Speciality.ToLower().Contains(request.ToLower()))
                            {
                                if (!city.Contains("All Ukraine"))
                                {
                                    if (u.City.Name.Contains(city))
                                    {
                                        res.Add(u);
                                        continue;
                                    }
                                }
                                else
                                {
                                    res.Add(u);
                                    continue;
                                }
                            }
                        }
                        
                            
                        if (u.UserDetail.JobExperience != null)
                        {
                            if ( u.UserDetail.JobExperience.Company.ToLower().Contains(request.ToLower()) ||
                                u.UserDetail.JobExperience.Desc.ToLower().Contains(request.ToLower()) ||
                                u.UserDetail.JobExperience.Post.ToLower().Contains(request.ToLower()))
                            {
                                if (!city.Contains("All Ukraine"))
                                {
                                    if (u.City.Name.Contains(city))
                                    {
                                        res.Add(u);
                                        continue;
                                    }
                                }
                                else
                                {
                                    res.Add(u);
                                    continue;
                                }
                            }
                        }
                    }
                }
            }

            lst = res.Select(u => UserToMyUser(u)).ToList();
            return lst;
        }

        public static UserDetail GetResume(int uId)
        {
            
            return ctx.Users.First(u => u.Id == uId).UserDetail;
        }

        public static MyJobExperience GetJobExperienceByUserDetailId(int udId)
        {
            
            JobExperience e = ctx.UserDetails.First(u => u.Id == udId).JobExperience;
            if (e == null)
                return null;
            else
                return new MyJobExperience(e.Id, e.Company, e.City, e.Post, e.Start, e.End, e.Desc);
        }

        public static MyEducation GetEducationByUserDetailId(int udId)
        {
            
            Education e = ctx.UserDetails.First(u => u.Id == udId).Education;
            if (e == null)
                return null;
            else
                return new MyEducation(e.Id, e.Level, e.Place, e.Speciality);
        }


        public void FilterResumes(List<MyUser> resumes, int userId)
        {
            


            foreach (var s in ctx.SearchResultsEmployers)
            {
                for (int i = 0; i < resumes.Count; i++)
                {
                    if (s.SearchAgentsEmployer == null)
                        s.SearchAgentsEmployer = ctx.SearchAgentsEmployers.First(s1 => s1.Id == s.AgentId);

                    if (s.UserId == resumes[i].Id && s.AgentId == s.AgentId)
                    {
                        resumes.RemoveAt(i);
                        break;
                    }


                }
            }
        }


        public void AddSearchResultEmployer(int agentId, int userId)
        {
            
            ctx.SearchResultsEmployers.Add(new SearchResultsEmployer() { Id = -1, AgentId = agentId, UserId = userId });
            ctx.SaveChanges();
        }

        public void AddSearchAgentEmployer(MySearchAgentEmployer agent)
        {
            
            ctx.SearchAgentsEmployers.Add(new SearchAgentsEmployer() { Request = agent.Request, UserId = agent.UserId });
            ctx.SaveChanges();
        }


        public List<MySearchAgentEmployer> GetSearchAgentsEmployer()
        {
            
            return ctx.SearchAgentsEmployers.Select(s => new MySearchAgentEmployer() { Id = s.Id, UserId = s.UserId, Request = s.Request }).ToList();

        }


        public void DeleteSearchAgentEmployer(int id)
        {
            

            List<SearchAgentsEmployer> agents = ctx.SearchAgentsEmployers.Where(a => a.Id == id).ToList();
            agents.ForEach(new Action<SearchAgentsEmployer>((SearchAgentsEmployer s) =>
            {
                ctx.SearchAgentsEmployers.Remove(s);
            }));

            ctx.SaveChanges();
        }


        public List<MySearchAgentEmployer> GetSearchAgentsEmployerByUserId(int uId)
        {
            
            return ctx.SearchAgentsEmployers.Where(s => s.UserId == uId).Select(s1 => new MySearchAgentEmployer() { Id = s1.Id, Request = s1.Request, UserId = s1.UserId }).ToList();
        }

        public List<MySearchResultEmployer> GetSearchEmployerResults(int uId)
        {
            
            return ctx.SearchResultsEmployers.Where(r => r.UserId == uId).Select(r1 => new MySearchResultEmployer() { Id = r1.Id, UserId = r1.UserId, AgentId = r1.AgentId }).ToList();
        }


        public MyEducation GetEducationById(int id)
        {
            
                var v  = ctx.Educations.Where(e => e.Id == id);
                MyEducation my = v.Select(edu => new MyEducation() { Id = edu.Id, Level = edu.Level, Place = edu.Place, Speciality = edu.Speciality }).First();
                return my;
           
        }

        public MyJobExperience GetJobExpById(int id)
        {
            
                var v = ctx.JobExperiences.Where(j => j.Id == id);
                MyJobExperience my = v.Select(job => new MyJobExperience() { City = job.City, Company = job.Company, Desc = job.Desc, End = job.End, Id = job.Id, Post = job.Post, Start = job.Start }).First();
                return my;
            
           
        }


        public void EditEducation(MyEducation edu)
        {

            if (edu.Id == -1)
                AddEducation(edu);
            else
            {
                Education e = ctx.Educations.First(e1 => e1.Id == edu.Id);
                e.Level = edu.Level;
                e.Place = edu.Place;
                e.Speciality = edu.Speciality;

                ctx.SaveChanges();
            }
        }

        public void EditJobExp(MyJobExperience exp)
        {
            if (exp.Id == -1)
                AddJobExp(exp);
            else
            {
                JobExperience e = ctx.JobExperiences.First(e1 => e1.Id == exp.Id);
                e.Company = exp.Company;
                e.City = exp.City;
                e.Post = exp.Post;
                e.Start = exp.Start;
                e.End = exp.End;
                e.Desc = exp.Desc;

                ctx.SaveChanges();
            }
        }


        public int GetLastEduId()
        {
            return ctx.Educations.Max(e => e.Id);
        }

        public int GetLastJobExpId()
        {
            return ctx.JobExperiences.Max(e => e.Id);
        }


        public int GetLastCompId()
        {
            return ctx.CompanyDetails.Max(e => e.Id);
        }
    }

    
}
