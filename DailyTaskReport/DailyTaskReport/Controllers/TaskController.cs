using DailyTaskReport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DailyTaskReport.Controllers
{
    public class TaskController : Controller
    {
        public ActionResult getAddTask()
        {
            return PartialView();
        }

        [HttpPost]
        public JsonResult AddTasks(TaskLists tasks)
        {
            return new JsonResult { Data = tasks };
        }
    }
}