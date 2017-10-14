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
        public AccountModel()
        {
            credentials = new LoginCredentials();
        }
        public LoginCredentials credentials { get; set; }

        [Required]
        [Display(Name = "ID Number")]
        [RegularExpression("[0-9]*", ErrorMessage = "accepts digits only")]
        public String idNo { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [RegularExpression("[a-zA-Z ]*")]
        public String fName { get; set; }

        [Required]
        [Display(Name = "Middle Name")]
        [RegularExpression("[a-zA-Z ]*")]
        public String mName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [RegularExpression("[a-zA-Z ]*")]
        public String lName { get; set; }

        [Required]
        [Display(Name = "Birth date")]
        public DateTime birthdate { get; set; }

        [Display(Name = "Designation")]
        public String Designation { get; set; }

        [Display(Name = "Contact No.")]
        [RegularExpression("[0-9]*", ErrorMessage = "accepts digits only")]
        public String ContactNo { get; set; }
        
        [Display(Name = "Role")]
        public String Role { get; set; }

        [Display(Name = "Member since")]
        public DateTime registeredDate { get; set; }
    }

    public class LoginCredentials
    {
        [Required]
        [Display(Name = "user")]
        [StringLength(12, ErrorMessage = "must consist of 12 characters")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "accepts alphanumeric only")]
        public String user { get; set; }

        [Required]
        [Display(Name = "password")]
        public String password { get; set; }
    }
}