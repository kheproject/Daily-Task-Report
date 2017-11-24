using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DailyTaskReport.Models
{
    public class user_tasks
    {
        public user_tasks()
        {
            List<daily_task> tasks = new List<daily_task>();
            this.tasks = tasks;
        }
        [Required]
        public String user { get; set; }

        public List<daily_task> tasks { get; set; }
    }
    public class daily_task
    {
        public daily_task()
        {
            List<task_list> taskLists = new List<task_list>();
            this.taskLists = taskLists;
        }
        [Required]
        public DateTime task_date { get; set; }
        public List<task_list> taskLists { get; set; }
    }
    public class task_list
    {
        public String taskID { get; set; }
        [Required]
        public String timeFrom { get; set; }
        [Required]
        public String timeTo { get; set; }
        [StringLength(18, MinimumLength = 18, ErrorMessage = "invalid work order no.")]
        public String woNo { get; set; }
        [Required]
        public String task { get; set; }
        [Required]
        public Boolean confirmed { get; set; }
    }

    public class TaskLists
    {
        public TaskLists()
        {
            List<TaskModel> taskLists = new List<TaskModel>();
            this.taskLists = taskLists;
        }
        [Required]
        public String encUser { get; set; }

        [Required]
        public DateTime dateTime { get; set; }

        public List<TaskModel> taskLists { get; set; }
    }
    public class TaskModel
    {
        [Required]
        public String timeFrom { get; set; }

        [Required]
        public String timeTo { get; set; }

        public String woNo { get; set; }

        [Required]
        public String task { get; set; }
    }

    public class TaskResponse
    {
        public Int32 code { get; set; }
        public String message { get; set; }
    }

    public class TaskSearch
    {
        public TaskSearch()
        {
            MonthLists = new List<SelectListItem>
                       { new SelectListItem { Value = "01", Text = "January"},
                         new SelectListItem { Value = "02", Text = "February"},
                         new SelectListItem { Value = "03", Text = "March"},
                         new SelectListItem { Value = "04", Text = "April"},
                         new SelectListItem { Value = "05", Text = "May"},
                         new SelectListItem { Value = "06", Text = "June"},
                         new SelectListItem { Value = "07", Text = "July"},
                         new SelectListItem { Value = "08", Text = "August"},
                         new SelectListItem { Value = "09", Text = "September"},
                         new SelectListItem { Value = "10", Text = "October"},
                         new SelectListItem { Value = "11", Text = "November"},
                         new SelectListItem { Value = "12", Text = "December"}
                       };
        }
        public List<SelectListItem> MonthLists { get; set; }
        [Required]
        public String Month { get; set; }
        [Required]
        public Int32 Year { get; set; }
        [Required]
        public String encUser { get; set; }
    }
}