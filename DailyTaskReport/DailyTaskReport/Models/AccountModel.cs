using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DailyTaskReport.Models
{
    public class AccountResponse
    {
        public Int16 code { get; set; }
        public String msg { get; set; }
        public AccountModel userInfo { get; set; }
    }

    public class AccountModel
    {
        public LoginCredentials credentials { get; set; }

        [Display(Name = "First Name")]
        [RegularExpression("[a-zA-Z")]
        public String fName { get; set; }
        
        [Display(Name = "Middle Name")]
        [RegularExpression("[a-zA-Z")]
        public String mName { get; set; }

        [Display(Name = "Last Name")]
        [RegularExpression("[a-zA-Z")]
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
        [Display(Name = "ID No.")]
        [StringLength(8, ErrorMessage = "{0} must consist of 8 numeric digits")]
        [RegularExpression("[0-1]")]
        public String idno { get; set; }

        [Required]
        [Display(Name = "Password")]
        public String password { get; set; }
    }
}