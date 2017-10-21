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

        [Required(ErrorMessage = "required")]
        [Display(Name = "ID Number")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "must consist of 8 digits")]
        [RegularExpression("[0-9]*", ErrorMessage = "accepts digits only")]
        public String idNo { get; set; }

        [Required(ErrorMessage = "required")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "must consist at least 2 characters ")]
        [Display(Name = "First Name")]
        [RegularExpression("[a-zA-Z ]*", ErrorMessage = "accepts letters only")]
        public String fName { get; set; }

        [Required(ErrorMessage = "required")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "must consist at least 2 characters ")]
        [Display(Name = "Middle Name")]
        [RegularExpression("[a-zA-Z ]*", ErrorMessage = "accepts letters only")]
        public String mName { get; set; }

        [Required(ErrorMessage = "required")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "must consist at least 2 characters ")]
        [Display(Name = "Last Name")]
        [RegularExpression("[a-zA-Z ]*", ErrorMessage = "accepts letters only")]
        public String lName { get; set; }

        [Required(ErrorMessage = "required")]
        [MinimumAge(18, ErrorMessage = "must be 18 years old or up")]
        [Display(Name = "Birth date")]
        public DateTime birthdate { get; set; }

        [Display(Name = "Designation")]
        public String Designation { get; set; }

        [Display(Name = "Contact No.")]
        [StringLength(12, MinimumLength = 11, ErrorMessage = "minimum of 11 digits only")]
        [RegularExpression("[0-9]*", ErrorMessage = "accepts digits only")]
        public String ContactNo { get; set; }
        
        [Display(Name = "Role")]
        public String Role { get; set; }

        [Display(Name = "Member since")]
        public DateTime registeredDate { get; set; }
    }

    public class LoginCredentials
    {
        [Required(ErrorMessage = "required")]
        [Display(Name = "user")]
        [StringLength(12, ErrorMessage = "must consist of 12 characters")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "accepts alphanumeric only")]
        public String user { get; set; }

        [Required(ErrorMessage = "required")]
        [Display(Name = "password")]
        public String password { get; set; }

        [Required(ErrorMessage = "required")]
        [Display(Name = "confirm password")]
        [Compare("password", ErrorMessage = "passwords mismatch")]
        public String confirm_password { get; set; }
    }
    
    public class MinimumAgeAttribute : ValidationAttribute
    {
        int _minimumAge;

        public MinimumAgeAttribute(int minimumAge)
        {
            _minimumAge = minimumAge;
        }

        public override bool IsValid(object value)
        {
            DateTime date;
            try
            {
                if (DateTime.TryParse(value.ToString(), out date))
                    return date.AddYears(_minimumAge) < DateTime.Now;
            }
            catch (Exception) {  }

            return false;
        }
    }

    public class LoginModel
    {
        [Required(ErrorMessage = "required")]
        [Display(Name = "user")]
        [StringLength(12, ErrorMessage = "must consist of 12 characters")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "accepts alphanumeric only")]
        public String user { get; set; }

        [Required(ErrorMessage = "required")]
        [Display(Name = "password")]
        public String password { get; set; }
    }
}