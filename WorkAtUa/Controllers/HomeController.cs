using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkAtUa.Core.ServiceLocator;
using WorkAtUa.Entities;


namespace WorkAtUa.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Main/

        IDbManager _manager = IoCContainer.ServiceLocator.Resolve<IDbManager>();

        public ActionResult Index()
        {
            List<string> categories = _manager.GetCategories().Select(c => c.Name).ToList();
            List<string> cities = _manager.GetCities();
            int vacCount = _manager.GetVacancyCount();

            ViewBag.Categories = categories;
            ViewBag.Cities = cities;
            ViewBag.VacanciesCount = vacCount;
            
            return View();
        }

        public ActionResult Res()
        {
            return View();
        }

        #region Comments
        //public ActionResult Expanded()
        //{
        //    List<string> categories = _manager.GetCategories().Select(c => c.Name).ToList();
        //    List<string> cities = _manager.GetCities();
        //    List<MyVacancy> vacancies = _manager.GetVacancies();
        //    ViewBag.Categories = categories;
        //    ViewBag.Cities = cities;
        //    ViewBag.Vacancies = vacancies;

        //    return View("Expanded");
        //}

        //[HttpPost]
        //public ActionResult Expanded(string request, string city, bool? searchOnlyInHeader,
        //    bool? searchAnyOfTheseWords, string category, int? salary, string typeOfEmp)
        //{
        //    bool searchOnly = (searchOnlyInHeader == null) ? false : searchOnlyInHeader.Value;
        //    bool searchAny = (searchAnyOfTheseWords == null) ? false : searchAnyOfTheseWords.Value;
        //    int sal = (salary == null) ? -1 : salary.Value;
        //    int range = 500;

        //    List<MyVacancy> lst = _manager.FindVacancies(request, city, searchOnly,
        //        searchAny, category, sal, range, typeOfEmp);
        //    List<string> categories = _manager.GetCategories().Select(c => c.Name).ToList();
        //    List<string> cities = _manager.GetCities();

        //    ViewBag.Vacancies = lst;
        //    ViewBag.Cities = cities;
        //    ViewBag.Request = request;
        //    ViewBag.City = city;
        //    ViewBag.Categories = categories;

        //    return View("Expanded");
        //}

        //public ActionResult Expanded(int id)
        //{
        //    MyVacancy my = _manager.GetVacancyByIdNonSt(id);
        //    ViewBag.Vacancy = my;

        //    string date;

        //    TimeSpan diff = DateTime.Now.Subtract(my.CreationTime);
        //    int days = Convert.ToInt32(diff.TotalDays) - 1;
        //    if (days == 0)
        //    {
        //        date = "today";
        //    }
        //    else
        //        if (days == 1)
        //        {
        //            date = "yesterday";
        //        }
        //        else
        //        {
        //            date = diff.TotalDays.ToString() + "days ago";
        //        }

        //    ViewBag.Date = date;

        //    return View("Item");
        //}
        #endregion

        [HttpPost]
        public ActionResult Search(string request, string city)
        {
            List<MyVacancy> lst = _manager.FindVacancies(request, city);
            List<string> cities = _manager.GetCities();
            ViewBag.Vacancies = lst;
            ViewBag.Cities = cities;
            ViewBag.Request = request;
            ViewBag.City = city;

            return View("Search");
        }

        [HttpPost]
        public ActionResult Subscribe(int vacId)
        {
            MyUser u = _manager.GetUserByEmail(this.Session["Email"].ToString());
            _manager.SubscribeForAVacancy(vacId, u.Id);

            MyVacancy my = _manager.GetVacancyByIdNonSt(vacId);
            ViewBag.Vacancy = my;
            ViewBag.Subscribed = _manager.IsUserSubscribedForAVacancy(vacId, u.Id);

            string date;

            TimeSpan diff = DateTime.Now.Subtract(my.CreationTime);
            int days = Convert.ToInt32(diff.TotalDays) - 1;
            if (days == 0)
            {
                date = "today";
            }
            else
                if (days == 1)
                {
                    date = "yesterday";
                }
                else
                {
                    date = Convert.ToInt32(diff.TotalDays).ToString() + " days ago";
                }
            ViewBag.CurrentUser = u;
            ViewBag.Date = date;
            ViewBag.Comments = _manager.GetCommentsByVacancyId(my.Id);

            return View("Item");
        }

        [HttpPost]
        public ActionResult Unsubscribe(int vacId)
        {
            MyUser u = _manager.GetUserByEmail(this.Session["Email"].ToString());
            _manager.UnsubscribeFromVacancy(vacId, u.Id);

            MyVacancy my = _manager.GetVacancyByIdNonSt(vacId);
            ViewBag.Vacancy = my;
            ViewBag.Subscribed = _manager.IsUserSubscribedForAVacancy(vacId, u.Id);
            string date;

            TimeSpan diff = DateTime.Now.Subtract(my.CreationTime);
            int days = Convert.ToInt32(diff.TotalDays) - 1;
            if (days == 0)
            {
                date = "today";
            }
            else
                if (days == 1)
                {
                    date = "yesterday";
                }
                else
                {
                    date = Convert.ToInt32(diff.TotalDays).ToString() + " days ago";
                }
            ViewBag.CurrentUser = u;
            ViewBag.Date = date;
            ViewBag.Comments = _manager.GetCommentsByVacancyId(my.Id);

            return View("Item");
        }

        [HttpPost]
        public ActionResult PostComment(string content, int vacId)
        {
            MyUser u = _manager.GetUserByEmail(this.Session["Email"].ToString());
            ViewBag.CurrentUser = u;
            

            _manager.AddComment(new MyComment(-1, u.Id, vacId, DateTime.Now, content));

            ViewBag.Subscribed = _manager.IsUserSubscribedForAVacancy(vacId, u.Id);

            MyVacancy my = _manager.GetVacancyByIdNonSt(vacId);
            ViewBag.Vacancy = my;

            string date;

            TimeSpan diff = DateTime.Now.Subtract(my.CreationTime);
            int days = Convert.ToInt32(diff.TotalDays) - 1;
            if (days == 0)
            {
                date = "today";
            }
            else
                if (days == 1)
                {
                    date = "yesterday";
                }
                else
                {
                    date = Convert.ToInt32(diff.TotalDays).ToString() + " days ago";
                }

            ViewBag.Date = date;
            ViewBag.CurrentUser = this.Session["CurrentUser"];
            ViewBag.Comments = _manager.GetCommentsByVacancyId(my.Id);

            return View("Item");
        }

        public ActionResult Search(int id)
        {
            MyVacancy my = _manager.GetVacancyByIdNonSt(id);
            ViewBag.Vacancy = my;
            
            string date;

           

            TimeSpan diff = DateTime.Now.Subtract(my.CreationTime);
            int days = Convert.ToInt32(diff.TotalDays) - 1;
            if (days == 0)
            {
                date = "today";
            }
            else
                if (days == 1) 
                { 
                    date = "yesterday"; 
                }
                else
                {
                    date = Convert.ToInt32(diff.TotalDays).ToString() + " days ago";
                }

            ViewBag.Date = date;
            ViewBag.CurrentUser = this.Session["CurrentUser"];
            if (ViewBag.CurrentUser != null)
            {
                ViewBag.Subscribed = _manager.IsUserSubscribedForAVacancy(ViewBag.CurrentUser.Id, id);
            }
            ViewBag.Comments = _manager.GetCommentsByVacancyId(my.Id);

            return View("Item");
        }


        [HttpPost]
        public ActionResult SearchAjax(string request, string city)
        {
            return Json(new { resultMessage = "Ваш комментарий добавлен успешно!" });
        }
    }
}
