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
using Microsoft.Owin.Security;

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
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            if (Request.GetOwinContext().Authentication.User.HasClaim(ClaimTypes.Authentication, "KP8DTR"))
            {
                try
                {
                    String role = string.Empty;
                    role = this.Session["_Role"].ToString();
                    if (role != string.Empty )
                        return View("_Home");
                    else
                        return RedirectToAction("Logout", "Index");
                }
                catch (Exception)
                {
                    return RedirectToAction("Logout", "Index");
                }
            }
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
                if (usr.code == 1)
                {
                    var userLogged = new ClaimsIdentity(new[] { new Claim(ClaimTypes.UserData, usr.userInfo.credentials.user),
                                                                new Claim(ClaimTypes.Authentication, "KP8DTR"),
                                                                new Claim(ClaimTypes.Role, usr.userInfo.Role),
                                                                new Claim(ClaimTypes.GivenName, usr.userInfo.fName)
                                                              }, "KP8DTR");
                    Request.GetOwinContext().Authentication.SignIn(new AuthenticationProperties { IsPersistent = false }, userLogged);
                    
                    this.Session["_user"] = encdata.AESEncrypt(usr.userInfo.credentials.user, encStringKey);
                    this.Session["FName"] = usr.userInfo.fName;
                    this.Session["MName"] = usr.userInfo.mName;
                    this.Session["LName"] = usr.userInfo.lName;
                    this.Session["RDate"] = usr.userInfo.registeredDate;
                    this.Session["_Role"] = usr.userInfo.Role;
                    this.Session["BDate"] = usr.userInfo.birthdate;
                    this.Session["ConNo"] = usr.userInfo.ContactNo;
                    this.Session["Desig"] = usr.userInfo.Designation;

                    return PartialView("_Home");
                }
            }
            return PartialView("_Login");
        }

        private AccountResponse getUser(LoginCredentials login)
        {
            AccountResponse user = new AccountResponse();
            try
            {
                using (MySqlConnection con = new MySqlConnection(connection))
                {
                    con.Open();
                    using (MySqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM kpDailyTask.employees WHERE user = @user AND password = @pwrd;";
                        cmd.Parameters.AddWithValue("user", login.user);
                        cmd.Parameters.AddWithValue("pwrd", login.password);

                        MySqlDataReader rdr = cmd.ExecuteReader();
                        if (rdr.HasRows)
                        {
                            user.code = 1;
                            user.userInfo.credentials.user = login.user;
                            user.userInfo.credentials.password = login.password;
                            user.msg = "Registered user";
                            rdr.Read();
                            user.userInfo.idNo = rdr["IDno"].ToString();
                            user.userInfo.fName = rdr["FName"].ToString();
                            user.userInfo.mName = rdr["MName"].ToString();
                            user.userInfo.lName = rdr["LName"].ToString();
                            user.userInfo.birthdate = Convert.ToDateTime(rdr["birthDate"].ToString());
                            user.userInfo.Designation = rdr["Designation"].ToString();
                            user.userInfo.ContactNo = rdr["ContactNo"].ToString();
                            user.userInfo.Role = rdr["Role"].ToString();
                            user.userInfo.registeredDate = Convert.ToDateTime(rdr["RegDate"].ToString());
                            rdr.Close();
                        }
                        else
                        {
                            user.code = 0;
                            user.msg = "Invalid user or password";
                            ViewBag.errorMsg = user.msg;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                user.code = -1;
                user.msg = "System error : " + ex.ToString();
                ViewBag.errorMsg = "Server error, Try again";
            }
            return user;
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut("KP8DTR");
            Session.Clear();
            return RedirectToAction("Index", "Index");
        }
    }
}