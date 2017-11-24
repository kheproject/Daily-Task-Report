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
            DateTime sDate = getServerDate();
            return PartialView(new TaskSearch() { Month = sDate.Month < 10 ? "0" + sDate.Month : sDate.Month.ToString(),
                                                  Year = sDate.Year
                                                });
        }

        public ActionResult searchTask(TaskSearch taskSearch)
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
                        cmd.CommandText = "SELECT *, TIME_FORMAT(timeFrom, \"%h:%i %p\") AS timeFrom_formatted, "
                                        + " TIME_FORMAT(timeTo, \"%h:%i %p\") AS timeTo_formatted FROM kpDailyTask.Report" + taskSearch.Month
                                        + " WHERE user = @user AND YEAR(DATE) = @year ORDER BY date DESC, timeFrom DESC;";
                        cmd.Parameters.AddWithValue("user", encdata.AESDecrypt(taskSearch.encUser.Replace(' ', '+'), encStringKey));
                        cmd.Parameters.AddWithValue("year", taskSearch.Year);
                        MySqlDataReader rdr = cmd.ExecuteReader();
                        if (rdr.HasRows)
                        {
                            String currDate = string.Empty;
                            while (rdr.Read())
                            {
                                if (currDate == string.Empty)
                                    currDate = rdr["date"].ToString();
                                if (currDate == rdr["date"].ToString())
                                {
                                    tasks.Add(new task_list
                                    {
                                        taskID = rdr["taskID"].ToString(),
                                        timeFrom = rdr["timeFrom_formatted"].ToString(),
                                        timeTo = rdr["timeTo_formatted"].ToString(),
                                        task = rdr["Task"].ToString(),
                                        woNo = rdr["WOno"].ToString(),
                                        confirmed = Convert.ToBoolean(rdr["confirmed"].ToString())
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
                                        taskID = rdr["taskID"].ToString(),
                                        timeFrom = rdr["timeFrom_formatted"].ToString(),
                                        timeTo = rdr["timeTo_formatted"].ToString(),
                                        task = rdr["Task"].ToString(),
                                        woNo = rdr["WOno"].ToString(),
                                        confirmed = Convert.ToBoolean(rdr["confirmed"].ToString())
                                    });
                                }
                            }

                            //final daily add from last date result-
                            daily.Add(new daily_task
                            {
                                task_date = Convert.ToDateTime(currDate),
                                taskLists = tasks
                            });

                            data = new user_tasks { user = taskSearch.encUser, tasks = daily };
                        }
                        else
                        {
                            ViewBag.message = "No tasks retrieved...";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error: " + ex.ToString());
            }
            return PartialView("_getTask", data);
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
                try
                {
                    employee.user = encdata.AESDecrypt(employee.user.Replace(' ', '+'), encStringKey).ToUpper();
                    response = addUserTasks(employee);
                }
                catch (Exception)
                {
                    response.code = -1;
                    response.message = "Unable to determine user, please refresh the page...";
                }
            }
            else
            {
                response.code = 0;
                response.message = "Please don't leave 'Time' or 'Task' field empty";
            }
            return new JsonResult { Data = response };
        }

        [HttpPost]
        public JsonResult removeTask()
        {
            return new JsonResult { };
        }

        private DateTime getServerDate()
        {
            DateTime date = new DateTime();
            try
            {
                using (MySqlConnection con = new MySqlConnection(connection))
                {
                    con.Open();
                    using (MySqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "SELECT DATE_ADD(NOW(), INTERVAL 16 HOUR ) AS \"current_date\";";
                        MySqlDataReader rdr = cmd.ExecuteReader();

                        if (rdr.HasRows)
                        {
                            rdr.Read();
                            date = Convert.ToDateTime(rdr["current_date"].ToString());
                            rdr.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return date;
        }

        private TaskResponse addUserTasks(user_tasks activities)
        {
            TaskResponse response = new TaskResponse();
            try
            {
                string month = activities.tasks[0].task_date.Month < 10 ? "0" + activities.tasks[0].task_date.Month : activities.tasks[0].task_date.Month.ToString();
                using (MySqlConnection con = new MySqlConnection(connection))
                {
                    con.Open();
                    MySqlTransaction trans = con.BeginTransaction(IsolationLevel.ReadCommitted);
                    using (MySqlCommand cmd = con.CreateCommand())
                    {
                        string values = string.Empty;
                        for (int count = 0; count < activities.tasks[0].taskLists.Count; count++)
                        {
                            //converts work order number to uppercase
                            activities.tasks[0].taskLists[count].woNo = activities.tasks[0].taskLists[count].woNo == null ? "N/A" : activities.tasks[0].taskLists[count].woNo.ToUpper();

                            values += " ( @user, @date, @timeFrom" + count + ", @timeTo" + count + ", @woNO" + count + ", @tasks" + count + " ) ,";
                            cmd.Parameters.AddWithValue("timeFrom" + count, activities.tasks[0].taskLists[count].timeFrom);
                            cmd.Parameters.AddWithValue("timeTo" + count, activities.tasks[0].taskLists[count].timeTo);
                            cmd.Parameters.AddWithValue("woNO" + count, activities.tasks[0].taskLists[count].woNo);
                            cmd.Parameters.AddWithValue("tasks" + count, activities.tasks[0].taskLists[count].task);
                        }

                        cmd.CommandText = "INSERT INTO kpDailyTask.Report" + month + "(user, date, timeFrom, timeTo, WOno, Task)"
                                        + "VALUES" + values.Substring(0, values.Length - 1) + ";";
                        cmd.Parameters.AddWithValue("user", activities.user);
                        cmd.Parameters.AddWithValue("date", activities.tasks[0].task_date);

                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            trans.Commit();
                            response.code = 1;
                            response.message = "Successfully Added!";
                        }
                        else
                        {
                            response.code = 0;
                            response.message = "Server error upon adding, please try again";
                        }
                    }
                }
            }
            catch (Exception)
            {
                response.code = -1;
                response.message = "Unexpected error occured, try again in a few minutes...";
            }
            return response;
        }
    }
}