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

        public ActionResult Index()
        {
            List<TaskLists> tasks = new List<TaskLists>();
            List<TaskModel> byTime = new List<TaskModel>();
            List<taskDBStructure> data = new List<taskDBStructure>();

            try
            {
                using (MySqlConnection con = new MySqlConnection(connection))
                {
                    con.Open();
                    using (MySqlCommand cmd = con.CreateCommand())
                    {
                        String dateMonth = string.Empty;
                        cmd.CommandText = "SELECT MONTH(DATE_ADD(NOW(), INTERVAL 15 HOUR )) AS \"current_date\";";
                        MySqlDataReader rdr = cmd.ExecuteReader();
                        
                        if (rdr.HasRows)
                        {
                            rdr.Read();
                                dateMonth = rdr["current_date"].ToString();
                            rdr.Close();
                        }
                        
                        dateMonth = dateMonth.Length < 2 ? "0" + dateMonth : dateMonth;

                        cmd.CommandText = "SELECT * FROM kpDailyTask.Report" + dateMonth
                                        + " WHERE user = @user ORDER BY date, timeFrom DESC;";
                        cmd.Parameters.AddWithValue("user", encdata.AESDecrypt(Session["_user"].ToString().Replace(' ', '+'), encStringKey));
                        rdr = cmd.ExecuteReader();
                        if (rdr.HasRows)
                        {   
                            while (rdr.Read())
                            {
                                data.Add(new taskDBStructure
                                {
                                    user= rdr["user"].ToString(),
                                    date = rdr["date"].ToString(),
                                    timeFrom = rdr["timeFrom"].ToString(),
                                    timeTo = rdr["timeTo"].ToString(),
                                    woNo = rdr["WOno"].ToString(),
                                    task = rdr["Task"].ToString()
                                });
                            }

                            String lastDate = string.Empty;
                            foreach (var info in data)
                            {
                                //executes once
                                if (lastDate == string.Empty)
                                    lastDate = info.date;
                                if (lastDate == info.date)
                                {
                                    byTime.Add(new TaskModel
                                    {
                                        timeFrom = info.timeFrom,
                                        timeTo = info.timeTo,
                                        woNo = info.woNo,
                                        task = info.task
                                    });
                                }
                                else
                                {
                                    tasks.Add(new TaskLists
                                    {
                                        encUser = info.user
                                    });
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            return PartialView(new TaskDisplayResponse { taskDisplay = tasks});
        }
    }
}