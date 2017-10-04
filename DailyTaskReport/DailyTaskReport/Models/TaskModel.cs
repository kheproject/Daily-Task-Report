using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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
        [Required]
        public String timeFrom { get; set; }
        [Required]
        public String timeTo { get; set; }
        public String woNo { get; set; }
        [Required]
        public String task { get; set; }
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

    public class TaskDisplayResponse
    {
        public IEnumerable<TaskLists> taskDisplay { get; set; }
    }

    public class taskDBStructure
    {
        public String user { get; set; }
        public String date { get; set; }
        public String timeFrom { get; set; }
        public String timeTo { get; set; }
        public String woNo { get; set; }
        public String task { get; set; }
    }
}