using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DailyTaskReport.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Home()
        {
            return PartialView("_Menu");
        }

        public ActionResult getAddTask()
        {
            return PartialView();
        }
    }
}