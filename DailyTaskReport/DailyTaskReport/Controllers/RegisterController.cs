using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DailyTaskReport.Models;
namespace DailyTaskReport.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(AccountModel user)
        {
            if (ModelState.IsValid)
            {

            }
            return View("Index");
        }
    }
}