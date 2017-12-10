using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WorkAtUa.Controllers
{
    public class LogOutController : Controller
    {
        //
        // GET: /LogOut/

        public ActionResult Index()
        {
            this.Session.Clear();
            this.Response.Cookies.Clear();
            this.Response.Redirect("~/Home");

            return View();
        }
    }
}
