using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GDWEBSolution.Models.User
{
    public class UserModel
    {
        [Display(Name = "User Id")]
        public string UserId { get; set; }
        [Display(Name = "Login Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string LoginEmail { get; set; }
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name = "School Id")]
        public string SchoolId { get; set; }
        [Display(Name = "User Category")]
        public string UserCategory { get; set; }
        [Display(Name = "Person Name")]
        public string PersonName { get; set; }
        [Display(Name = "Job Description")]
        public string JobDescription { get; set; }
        [Required(ErrorMessage = "Your Must Provide a Mobile Number")]
        [Display(Name = "Mobile")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string Mobile { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        [Display(Name = "Created Date")]
        public System.DateTime CreatedDate { get; set; }
        [Display(Name = "Modified By")]
        public string ModifiedBy { get; set; }
        [Display(Name = "Modified Date")]
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        [Display(Name = "Is Active")]
        public string IsActive { get; set; }
    }
}