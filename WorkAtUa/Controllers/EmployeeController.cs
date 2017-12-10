using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkAtUa.Core.ServiceLocator;
using WorkAtUa.Entities;

namespace WorkAtUa.Controllers
{
    public class EmployeeController : Controller
    {
        //
        // GET: /Employee/
        IDbManager manager = IoCContainer.ServiceLocator.Resolve<IDbManager>();
        public ActionResult Index()
        {
            manager = IoCContainer.ServiceLocator.Resolve<IDbManager>();
            MyUser u = manager.GetUserByEmail(this.Session["Email"].ToString());

            ViewBag.User = u;


            return View();
        }

        public ActionResult SearchAgents()
        {
            manager = IoCContainer.ServiceLocator.Resolve<IDbManager>();
            MyUser u = manager.GetUserByEmail(this.Session["Email"].ToString());
            ViewBag.User = u;
            ViewBag.SearchAgents = manager.GetSearchAgentsByUserId(u.Id);
            ViewBag.Results = manager.GetSearchResults(u.Id);

            return View("SearchAgents");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            manager.DeleteSearchAgent(id);

            MyUser u = manager.GetUserByEmail(this.Session["Email"].ToString());
            ViewBag.User = u;
            ViewBag.SearchAgents = manager.GetSearchAgentsByUserId(u.Id);
            ViewBag.Results = manager.GetSearchResults(u.Id);

            return View("SearchAgents");
        }

        [HttpPost]
        public ActionResult DeleteVacancy(int id)
        {
            MyUser u = manager.GetUserByEmail(this.Session["Email"].ToString());

            manager.UnsubscribeFromVacancy(id, u.Id);

            ViewBag.Vacancies = manager.GetUserVacancies(u.Id);
            ViewBag.User = u;
            ViewBag.SearchAgents = manager.GetSearchAgentsByUserId(u.Id);
            ViewBag.Results = manager.GetSearchResults(u.Id);

            return View("Vacancies");
        }

        [HttpPost]
        public ActionResult Add(string request)
        {
            MyUser u = manager.GetUserByEmail(this.Session["Email"].ToString());
            ViewBag.User = u;
            ViewBag.SearchAgents = manager.GetSearchAgentsByUserId(u.Id);
            ViewBag.Results = manager.GetSearchResults(u.Id);

            manager.AddSearchAgent(new MySearchAgent(-1, u.Id, request));

            return View("SearchAgents");
        }

        [HttpPost]
        public ActionResult SearchAjax(string request, string city)
        {
            return Json(new { resultMessage = "Ваш комментарий добавлен успешно!" });
        }

        public ActionResult Vacancies()
        {
            MyUser u = manager.GetUserByEmail(this.Session["Email"].ToString());

            ViewBag.User = u;
            ViewBag.Vacancies = manager.GetUserVacancies(u.Id);
            
            return View("Vacancies");
        }
    }
}
