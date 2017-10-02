using DailyTaskReport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using Microsoft.Owin.Security;
using System.Configuration;
using AESEncrypt;

namespace DailyTaskReport.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private AESEncryption encdata = new AESEncryption();
        private String encStringKey = "B905BD7BFBD902DCB115B327F9018CEA";

        public ActionResult getAddTask()
        {
            return PartialView();
        }
        
        [HttpPost]
        public JsonResult AddTasks(TaskLists tasks)
        {
            String user = encdata.AESDecrypt(tasks.encUser.Replace(' ', '+'), encStringKey);

            return new JsonResult { Data = tasks };
        }
    }
}