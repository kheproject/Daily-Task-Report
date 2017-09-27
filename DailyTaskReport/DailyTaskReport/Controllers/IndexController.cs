using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DailyTaskReport.Models;
using System.Security.Claims;
using System.Collections;
using System.Configuration;
using AESEncrypt;
using MySql.Data.MySqlClient;

namespace DailyTaskReport.Controllers
{
    [Authorize]
    public class IndexController : Controller
    {
        IDictionary config;
        private String connection = string.Empty;
        //private ILog kplog;
        private AESEncryption encdata = new AESEncryption();
        private String encStringKey = "B905BD7BFBD902DCB115B327F9018CEA";

        public IndexController()
        {
            config = (IDictionary)(ConfigurationManager.GetSection("kp8dtrSection"));
            connection = config["server"].ToString();
            //kplog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            if (Request.GetOwinContext().Authentication.User.HasClaim(ClaimTypes.Authentication, "KP8DTR"))
                return View("Home");
            else
                return View("LogIn");
        }
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult LogIn(LoginCredentials login)
        {
            if (ModelState.IsValid)
            {
                AccountResponse usr = getUser(login);
            }
            return PartialView("LogIn");
        }

        private AccountResponse getUser(LoginCredentials login)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(connection))
                {
                    con.Open();
                    using (MySqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM kpDailyTask.employees WHERE IDno = @idno AND password = @pwrd;";
                        cmd.Parameters.AddWithValue("idno", login.idno);
                        cmd.Parameters.AddWithValue("pwrd", login.password);

                        MySqlDataReader rdr = cmd.ExecuteReader();
                        if (rdr.HasRows)
                        {

                        }
                        else
                        {

                        }
                    }

                }
            }
            catch (Exception ex)
            {

            }
            return new AccountResponse { userInfo = new AccountModel { credentials = login,  } };
        }
    }
}