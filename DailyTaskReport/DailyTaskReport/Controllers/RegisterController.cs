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
        public ActionResult Register(AccountModel user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(connection))
                    {
                        con.Open();
                        MySqlTransaction trans = con.BeginTransaction(IsolationLevel.ReadCommitted);
                        using (MySqlCommand cmd = con.CreateCommand())
                        {
                            cmd.CommandText = "INSERT INTO kpDailyTask.employees (IDno, user, password, FName, MName, LName, birthDate, Designation, ContactNo, Role, Regdate)"
                                            + "VALUES (@idNo, @user, @password, @fName, @mName, @lName, @birthdate, @ContactNo, );";
                            cmd.Parameters.AddWithValue("idNo",user.idNo);
                            cmd.Parameters.AddWithValue("user", user.lName.Substring(0, 4) + user.idNo);
                            cmd.Parameters.AddWithValue("password", "Mlinc1234");
                            cmd.Parameters.AddWithValue("fName", user.fName);
                            cmd.Parameters.AddWithValue("mName", user.mName);
                            cmd.Parameters.AddWithValue("lName", user.lName);
                            cmd.Parameters.AddWithValue("birthdate", user.birthdate);
                            cmd.Parameters.AddWithValue("ContactNo", user.ContactNo);

                            if (cmd.ExecuteNonQuery() > 0)
                            {
                                trans.Commit();
                                //response.code = 1;
                                //response.message = "Successfully Added!.";
                            }
                            else
                            {
                                trans.Rollback();
                                //response.code = 0;
                                //response.message = "Server error upon adding, please try again";
                            }
                        }
                    }
                }
                catch (Exception) 
                {

                }
            }
            return PartialView();
        }
    }
}