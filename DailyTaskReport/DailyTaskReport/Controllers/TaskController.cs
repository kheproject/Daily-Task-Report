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
using System.Collections;
using System.Data;

namespace DailyTaskReport.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        IDictionary config;
        private String connection = string.Empty;
        private AESEncryption encdata = new AESEncryption();
        private String encStringKey = "B905BD7BFBD902DCB115B327F9018CEA";

        public TaskController()
        {
            config = (IDictionary)(ConfigurationManager.GetSection("kp8dtrSection"));
            connection = config["server"].ToString();
        }

        public ActionResult getTask()
        {
            user_tasks data = new user_tasks();
            List<daily_task> daily = new List<daily_task>();
            List<task_list> tasks = new List<task_list>();
            
            try
            {
                using (MySqlConnection con = new MySqlConnection(connection))
                {
                    con.Open();
                    using (MySqlCommand cmd = con.CreateCommand())
                    {
                        String dateMonth = getServerMonth();
                        dateMonth = dateMonth.Length < 2 ? "0" + dateMonth : dateMonth;

                        cmd.CommandText = "SELECT * FROM kpDailyTask.Report" + dateMonth
                                        + " WHERE user = @user ORDER BY date DESC, timeFrom DESC;";
                        cmd.Parameters.AddWithValue("user", encdata.AESDecrypt(Session["_user"].ToString().Replace(' ', '+'), encStringKey));
                        MySqlDataReader rdr = cmd.ExecuteReader();
                        if (rdr.HasRows)
                        {
                            String currDate = string.Empty;
                            while(rdr.Read())
                            {
                                if (currDate == string.Empty)
                                    currDate = rdr["date"].ToString();
                                if (currDate == rdr["date"].ToString())
                                {
                                    tasks.Add(new task_list
                                    {
                                        timeFrom = rdr["timeFrom"].ToString(),
                                        timeTo = rdr["timeTo"].ToString(),
                                        task = rdr["Task"].ToString(),
                                        woNo = rdr["WOno"].ToString()
                                    });
                                }
                                else
                                {
                                    daily.Add(new daily_task
                                    {
                                        task_date = Convert.ToDateTime(currDate),
                                        taskLists = tasks
                                    });

                                    tasks = new List<task_list>();
                                    currDate = rdr["date"].ToString();
                                    tasks.Add(new task_list
                                    {
                                        timeFrom = rdr["timeFrom"].ToString(),
                                        timeTo = rdr["timeTo"].ToString(),
                                        task = rdr["Task"].ToString(),
                                        woNo = rdr["WOno"].ToString()
                                    });
                                }
                            }

                            //final daily add from last date result
                                daily.Add(new daily_task
                                {
                                    task_date = Convert.ToDateTime(currDate),
                                    taskLists = tasks
                                });

                            data = new user_tasks { user = encdata.AESDecrypt(Session["_user"].ToString().Replace(' ', '+'), encStringKey), tasks = daily };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error: " + ex.ToString());
            }
            return PartialView(data);
        }

        public ActionResult addTask()
        {

            return PartialView();
        }
        
        [HttpPost]
        public JsonResult addTask(user_tasks employee)
        {
            TaskResponse response = new TaskResponse();
            if (ModelState.IsValid)
            {
                employee.user = encdata.AESDecrypt(employee.user.Replace(' ', '+'), encStringKey);
                //response = addUserTasks(tasks);
            }
            else
            {
                response.code = 0;
                response.message = "Please don't leave 'Time' or 'Task' field empty";
            }
            return new JsonResult { Data = response };
        }


        private String getServerMonth()
        {
            String date = string.Empty;
            try
            {
                using (MySqlConnection con = new MySqlConnection(connection))
                {
                    con.Open();
                    using (MySqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "SELECT MONTH(DATE_ADD(NOW(), INTERVAL 15 HOUR )) AS \"current_date\";";
                        MySqlDataReader rdr = cmd.ExecuteReader();

                        if (rdr.HasRows)
                        {
                            rdr.Read();
                            date = rdr["current_date"].ToString();
                            rdr.Close();
                        }
                    }
                }
            }
            catch (Exception)
            {
                date = "error";
            }
            return date;
        }
        //private TaskResponse addUserTasks(TaskLists tasks)
        //{
        //    TaskResponse response = new TaskResponse();
        //    try 
        //    {
        //        string month = tasks.dateTime.Month < 10 ? "0" + tasks.dateTime.Month : tasks.dateTime.Month.ToString();
        //        using (MySqlConnection con = new MySqlConnection(connection))
        //        {
        //            con.Open();
        //            MySqlTransaction trans = con.BeginTransaction(IsolationLevel.ReadCommitted);
        //            using (MySqlCommand cmd = con.CreateCommand())
        //            {
        //                string values = string.Empty;
        //                for (int count = 0; count < tasks.taskLists.Count; count++)
        //                {
        //                    values += " ( @user, @date, @timeFrom" + count + ", @timeTo" + count + ", @woNO" + count + ", @tasks" + count + " ) ,";
        //                    cmd.Parameters.AddWithValue("timeFrom" + count, tasks.taskLists[count].timeFrom);
        //                    cmd.Parameters.AddWithValue("timeTo" + count, tasks.taskLists[count].timeTo);
        //                    cmd.Parameters.AddWithValue("woNO" + count, tasks.taskLists[count].woNo);
        //                    cmd.Parameters.AddWithValue("tasks" + count, tasks.taskLists[count].task);
        //                }

        //                cmd.CommandText = "INSERT INTO kpDailyTask.Report" + month + "(user, date, timeFrom, timeTo, WOno, Task)"
        //                                + "VALUES" + values.Substring(0, values.Length - 1) + ";";
        //                cmd.Parameters.AddWithValue("user", tasks.encUser);
        //                cmd.Parameters.AddWithValue("date", tasks.dateTime);

        //                if (cmd.ExecuteNonQuery() > 0)
        //                {
        //                    trans.Commit();
        //                    response.code = 1;
        //                    response.message = "Successfully Added!.";
        //                }
        //                else
        //                {
        //                    response.code = 0;
        //                    response.message = "Server error upon adding, please try again";
        //                }   
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        response.code = -1;
        //        response.message = "Unexpected error occured, try again in a few minutes...";
        //    }
        //    return response;
        //}
    }
}