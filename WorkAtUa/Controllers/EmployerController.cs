using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkAtUa.Core.ServiceLocator;
using WorkAtUa.Entities;

namespace WorkAtUa.Controllers
{
    public class EmployerController : Controller
    {
        //
        // GET: /Employer/

        IDbManager manager = IoCContainer.ServiceLocator.Resolve<IDbManager>();

        public ActionResult Index()
        {
            manager = IoCContainer.ServiceLocator.Resolve<IDbManager>();
            MyUser u = manager.GetUserByEmail(this.Session["Email"].ToString());

            ViewBag.User = u;


            return View();
        }

        public ActionResult Vacancies()
        {
            MyUser u = manager.GetUserByEmail(this.Session["Email"].ToString());

            ViewBag.User = u;
            ViewBag.Vacancies = manager.GetEmployerVacancies(u.Id);

            return View("Vacancies");
        }


        [HttpPost]
        public ActionResult MarkAsSolved(int id)
        {
            MyUser u = manager.GetUserByEmail(this.Session["Email"].ToString());

            manager.MarkVacancyAsSolved(id);

            ViewBag.Vacancies = manager.GetEmployerVacancies(u.Id);
            ViewBag.User = u;
            ViewBag.SearchAgents = manager.GetSearchAgentsByUserId(u.Id);
            ViewBag.Results = manager.GetSearchResults(u.Id);

            return View("Vacancies");
        }

        [HttpPost]
        public ActionResult MarkAsUnsolved(int id)
        {
            MyUser u = manager.GetUserByEmail(this.Session["Email"].ToString());

            manager.MarkVacancyAsUnsolved(id);

            ViewBag.Vacancies = manager.GetEmployerVacancies(u.Id);
            ViewBag.User = u;
            ViewBag.SearchAgents = manager.GetSearchAgentsByUserId(u.Id);
            ViewBag.Results = manager.GetSearchResults(u.Id);

            return View("Vacancies");
        }

        [HttpPost]
        public ActionResult Search(string request, string city)
        {
            List<MyUser> lst = manager.FindResumes(request, city);
            List<string> cities = manager.GetCities();
            ViewBag.Users = lst;
            ViewBag.Cities = cities;
            ViewBag.Request = request;
            ViewBag.City = city;

            return View("Search");
        }

        public ActionResult Search()
        {
            List<MyUser> lst = manager.FindResumes("", "All Ukraine");
            List<string> cities = manager.GetCities();
            ViewBag.Users = lst;
            ViewBag.Cities = cities;

            ViewBag.CurrentUser = this.Session["CurrentUser"];

            return View("Search");
        }

        [HttpGet]
        public ActionResult Resume(int id)
        {
            MyUser my = manager.GetUserById(id);
            MyUserDetail d = manager.GetUserDetailById(my.UserDetails);
            ViewBag.User = my;
            ViewBag.Resume = d;
            ViewBag.Education = DBManager.DbManager.GetEducationByUserDetailId(d.Id);
            ViewBag.JobExp = DBManager.DbManager.GetJobExperienceByUserDetailId(d.Id);

            ViewBag.CurrentUser = this.Session["CurrentUser"];
           
            return View("Item");
        }

        [HttpPost]
        public ActionResult SearchAjax(string request, string city)
        {
            return Json(new { resultMessage = "" });
        }

        [HttpPost]
        public ActionResult Add(string request)
        {
            MyUser u = manager.GetUserByEmail(this.Session["Email"].ToString());
            ViewBag.User = u;
            ViewBag.SearchAgents = manager.GetSearchAgentsEmployerByUserId(u.Id);
            ViewBag.Results = manager.GetSearchEmployerResults(u.Id);

            manager.AddSearchAgentEmployer(new MySearchAgentEmployer(-1, u.Id, request));

            return View("SearchAgents");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            manager.DeleteSearchAgentEmployer(id);

            MyUser u = manager.GetUserByEmail(this.Session["Email"].ToString());
            ViewBag.User = u;
            ViewBag.SearchAgents = manager.GetSearchAgentsEmployerByUserId(u.Id);
            ViewBag.Results = manager.GetSearchEmployerResults(u.Id);

            return View("SearchAgents");
        }

        public ActionResult SearchAgents()
        {
            manager = IoCContainer.ServiceLocator.Resolve<IDbManager>();
            MyUser u = manager.GetUserByEmail(this.Session["Email"].ToString());
            ViewBag.User = u;
            ViewBag.SearchAgents = manager.GetSearchAgentsEmployerByUserId(u.Id);
            ViewBag.Results = manager.GetSearchEmployerResults(u.Id);

            return View("SearchAgents");
        }
    }
}
