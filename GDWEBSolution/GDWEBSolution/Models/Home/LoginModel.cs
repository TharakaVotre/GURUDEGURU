using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GDWEBSolution.Models.Home
{
    public class LoginModel
    {
        [Required(ErrorMessage = "User Name Required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password  Required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "New Password  Required")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm Password  Required")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Email  Required")]
        public string LoginEmail { get; set; }

        [Required(ErrorMessage = "Please Enter the Code")]
        [Range(0, int.MaxValue, ErrorMessage = "Please Enter Valid 6 Digit Number")]
        public string Code { get; set; }
    }
}