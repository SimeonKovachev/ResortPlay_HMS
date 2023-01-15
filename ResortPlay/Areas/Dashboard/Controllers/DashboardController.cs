using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResortPlay.Areas.Dashboard.Controllers
{
    //[Authorize(Roles = "Administrator")]
    public class DashboardController : Controller
    {
        // GET: Dashboard/Dashboard
        public ActionResult Index()
        {
            return View();
        }
    }
}