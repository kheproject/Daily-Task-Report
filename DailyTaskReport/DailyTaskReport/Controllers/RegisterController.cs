using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DailyTaskReport.Models;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Configuration;
using System.Data;

namespace DailyTaskReport.Controllers
{
    public class RegisterController : Controller
    {
        IDictionary config;
        private String connection = string.Empty;

        public RegisterController()
        {
            config = (IDictionary)(ConfigurationManager.GetSection("kp8dtrSection"));
            connection = config["server"].ToString();
        }
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(AccountModel user)
        {
            if (ModelState.IsValid)
            {
                //Formatting user name inputs to capitalize
                //──────────────────────────────────────────────────────────────────────────────────────────
                String[] userName = new String[] { user.fName, user.mName, user.lName };
                for (int count = 0; count < userName.Length ; count++)
                {
                    String formattedName = string.Empty;
                    String[] nameLists = userName[count].Split(' ');
                    if (nameLists.Length > 0)
                    {
                        foreach (var _name in nameLists)
                            formattedName += (_name[0].ToString().ToUpper() + _name.Substring(1).ToLower() + ' ');
                        userName[count] = formattedName.Remove(formattedName.Length - 1);
                    }
                }
                user.fName = userName[0]; user.mName = userName[1]; user.lName = userName[2];
                //────────────────────────────────────────────────────────────────────────────────────────────
                try
                {
                    using (MySqlConnection con = new MySqlConnection(connection))
                    {
                        con.Open();
                        MySqlTransaction trans = con.BeginTransaction(IsolationLevel.ReadCommitted);
                        using (MySqlCommand cmd = con.CreateCommand())
                        {
                            cmd.CommandText = "INSERT INTO kpDailyTask.employees (IDno, user, password, FName, MName, LName, birthDate, ContactNo, Role, Designation, Regdate)"
                                            + "VALUES (@idNo, @user, @password, @fName, @mName, @lName, @birthdate, @ContactNo," 
                                            + "\"PRG1\", \"Programmer 1\", DATE_ADD(NOW(), INTERVAL 15 HOUR));";
                            cmd.Parameters.AddWithValue("idNo", user.idNo);
                            cmd.Parameters.AddWithValue("user", user.lName.Substring(0, 4).ToUpper() + user.idNo);
                            cmd.Parameters.AddWithValue("password", user.credentials.password);
                            cmd.Parameters.AddWithValue("fName", user.fName);
                            cmd.Parameters.AddWithValue("mName", user.mName);
                            cmd.Parameters.AddWithValue("lName", user.lName);
                            cmd.Parameters.AddWithValue("birthdate", user.birthdate);
                            cmd.Parameters.AddWithValue("ContactNo", user.ContactNo);

                            try
                            {
                                if (cmd.ExecuteNonQuery() > 0)
                                {
                                    trans.Commit();
                                    ViewBag.successMsg = "Registered Successfully!";
                                }
                                else
                                {
                                    trans.Rollback();
                                    ViewBag.errorMsg = "Server error upon registering, please try again";
                                }
                            }
                            catch (MySqlException mysqlEx)
                            {
                                if (mysqlEx.Message.Contains("Duplicate"))
                                    ViewBag.errorMsg = "ID has already been registered";
                                else
                                    ViewBag.errorMsg = "An error has occured, Please try again. if problem persist please contact admin.";
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    ViewBag.errorMsg = "Server Connection Error, Please Try Again Later...";
                }
            }
            return PartialView();
        }
    }
}