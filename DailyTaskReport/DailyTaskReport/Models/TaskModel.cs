using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailyTaskReport.Models
{
    public class TaskModel
    {
        public String idNo { get; set; }
        public DateTime dateTime { get; set; }
        public String woNo { get; set; }
        public String task { get; set; }
    }
}