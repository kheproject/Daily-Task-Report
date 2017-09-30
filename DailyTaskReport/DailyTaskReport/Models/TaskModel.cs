using System;
using System.Collections.Generic;
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
        public String idNo { get; set; }
        public DateTime dateTime { get; set; }
        public List<TaskModel> taskLists { get; set; }
    }
    public class TaskModel
    {
        public String timeFrom { get; set; }
        public String timeTo { get; set; }
        public String woNo { get; set; }
        public String task { get; set; }
    }
}