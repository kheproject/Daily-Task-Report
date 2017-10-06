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