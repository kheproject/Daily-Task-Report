using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DailyTaskReport.Models;
using MySql.Data.MySqlClient;
using AESEncrypt;

namespace DailyTaskReport.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        IDictionary config;
        private String connection = string.Empty;
        private AESEncryption encdata = new AESEncryption();
        private String encStringKey = "B905BD7BFBD902DCB115B327F9018CEA";

        public HomeController() 
        {
            config = (IDictionary)(ConfigurationManager.GetSection("kp8dtrSection"));
            connection = config["server"].ToString();
        }
        // GET: Home
        public ActionResult Home()
        {
            String role = this.Session["_Role"].ToString();
            return PartialView("_Menu" + role);
        }
    }
}