using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DailyTaskReport.Models
{
    public class getCalendarList
    {
        public getCalendarList()
        {
            Months = new List<SelectListItem>
                   { new SelectListItem { Value = "1", Text = "January"},
                     new SelectListItem { Value = "2", Text = "February"},
                     new SelectListItem { Value = "3", Text = "March"},
                     new SelectListItem { Value = "4", Text = "April"},
                     new SelectListItem { Value = "5", Text = "May"},
                     new SelectListItem { Value = "6", Text = "June"},
                     new SelectListItem { Value = "7", Text = "July"},
                     new SelectListItem { Value = "8", Text = "August"},
                     new SelectListItem { Value = "9", Text = "September"},
                     new SelectListItem { Value = "10", Text = "October"},
                     new SelectListItem { Value = "11", Text = "November"},
                     new SelectListItem { Value = "12", Text = "December"}
                   };
        }
        public List<SelectListItem> Months { get; set; }
    }
}