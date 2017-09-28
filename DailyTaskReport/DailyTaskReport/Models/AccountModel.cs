using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace DailyTaskReport.Models
{
    public class AccountResponse
    {
        public AccountResponse()
        {
            userInfo = new AccountModel();
        }
        public Int16 code { get; set; }
        public String msg { get; set; }
        public AccountModel userInfo { get; set; }
    }

    public class AccountModel
    {
        public LoginCredentials credentials { get; set; }

        [Display(Name = "ID Number")]
        [RegularExpression("[0-9]")]
        public String idNo { get; set; }

        [Display(Name = "First Name")]
        [RegularExpression("[a-zA-Z]")]
        public String fName { get; set; }
        
        [Display(Name = "Middle Name")]
        [RegularExpression("[a-zA-Z]")]
        public String mName { get; set; }

        [Display(Name = "Last Name")]
        [RegularExpression("[a-zA-Z]")]
        public String lName { get; set; }

        [Display(Name = "First Name")]
        public DateTime birthdate { get; set; }

        [Display(Name = "Birthdate")]
        public String Designation { get; set; }

        [Display(Name = "Contact No.")]
        public String ContactNo { get; set; }

        [Display(Name = "Role")]
        public String Role { get; set; }

        [Display(Name = "Member since")]
        public DateTime registeredDate { get; set; }
    }

    public class LoginCredentials
    {
        [Required]
        [Display(Name = "User")]
        [StringLength(12, ErrorMessage = "{0} must consist of 12 numeric digits")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "{0} accepts alphanumberic only")]
        public String user { get; set; }

        [Required]
        [Display(Name = "Password")]
        public String password { get; set; }
    }
}