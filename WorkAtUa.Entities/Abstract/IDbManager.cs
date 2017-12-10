using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkAtUa.Entities
{
    public interface IDbManager
    {
        int GetVacancyCount();
        MyCompanyDetail GetCompanyDetailById(int id);
        MyUserDetail GetUserDetailById(int id);
        List<MyUser> GetAllUsers();
        MyUser GetUserById(int id);
        void AddUser(MyUser user, MyCompanyDetail companyDetail, MyUserDetail userDetail);
        bool IsValid(string email, string password);
        List<MyRole> GetRoles();
        List<MyCategory> GetCategories();
        int GetRoleIdByName(string name);
        int GetCategoryIdByName(string name);
        bool CheckEmailUnique(string email);
        List<string> GetCities();
        string GetCityNameById(int id);
        int GetCityIdByName(string name);
        List<string> GetUserRoles(string email);
        int GetUserIdByEmail(string email);
        string GetRoleNameById(int id);
        void DeleteUserById(int id);
        void RegisterVacancy(MyVacancy my);
        void RegisterResume(MyUser u, MyUserDetail my, MyEducation edu, MyJobExperience exp);
        List<MyUserDataGrid> GetAllDataGridUsers();
        List<MyVacancy> GetVacancies();
        List<MyComment> GetComments();
        List<MyUserDetail> GetResumes();
        List<MyCompanyDetail> GetCompanies();
        List<MyUser> FindResumes(string request, string city);
        List<MyVacancy> FindVacancies(string request, string city);
        MyVacancy GetVacancyByIdNonSt(int id);
        MyUser GetUserByEmail(string email);
        List<MySearchAgent> GetSearchAgents();
        List<MySearchResult> GetSearchResults(int userId);
        List<MySearchAgent> GetSearchAgentsByUserId(int id);
        void FilterVacancies(List<MyVacancy> vacancies, int userId);
        void FilterResumes(List<MyUser> resumes, int userId);
        void AddSearchResult(int agentId, int vacancyId);
        void AddSearchAgent(MySearchAgent agent);
        void AddSearchResultEmployer(int agentId, int userId);
        void AddSearchAgentEmployer(MySearchAgentEmployer agent);
        void DeleteSearchAgent(int id);
        void DeleteSearchAgentEmployer(int id);
        List<MyVacancy> GetUserVacancies(int userId);
        void SubscribeForAVacancy(int vacId, int userId);
        void UnsubscribeFromVacancy(int vacId, int userId);
        List<MyComment> GetCommentsByVacancyId(int vacId);
        void AddComment(MyComment my);
        bool IsUserSubscribedForAVacancy(int userId, int vacId);
        void MarkVacancyAsSolved(int vacId);
        void MarkVacancyAsUnsolved(int vacId);
        List<MyVacancy> GetEmployerVacancies(int p);
        List<MyUser> GetSubscribedUsersByVacancyId(int vacId);
        List<MySearchAgentEmployer> GetSearchAgentsEmployerByUserId(int uId);
        List<MySearchResultEmployer> GetSearchEmployerResults(int uId);
        List<MySearchAgentEmployer> GetSearchAgentsEmployer();
        MyEducation GetEducationById(int id);
        MyJobExperience GetJobExpById(int id);
        void EditEducation(MyEducation edu);
        void EditJobExp(MyJobExperience exp);
        int GetLastEduId();
        int GetLastJobExpId();
        int GetLastCompId();
    }
}
