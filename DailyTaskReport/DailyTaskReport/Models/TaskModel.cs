using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DailyTaskReport.Models
{
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
}