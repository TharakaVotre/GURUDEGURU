using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace GDWEBSolution.Models.Schools
{
    public class SchoolAdminModel
    {


        [Display(Name = "School Id")]
        //[Required(ErrorMessage = "Please enter your full SchoolId")]
        public string SchoolId { get; set; }

        [Display(Name = "User Id")]

        [Required(ErrorMessage = "User Id is required")]
        //[Required(ErrorMessage = "Please enter your full SchoolId")]
        public string AdminUserId { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "User Name is required")]

        //[Required(ErrorMessage = "Please enter your full SchoolId")]
        public string AdminName{ get; set; }



        [Display(Name = "School")]
        //[Required(ErrorMessage = "Please enter your full SchoolId")]
        public string SchoolId3 { get; set; }

        [Display(Name = "School")]
        [Required(ErrorMessage = "The School is required")]
        //[Required(ErrorMessage = "Please enter your full SchoolId")]
        public string SchoolId4 { get; set; }

        [Display(Name = "Province")]
        public Nullable<int> Province { get; set; }


        [Display(Name = "Web Address")]
        [Url]
        public string WebAddress { get; set; }

        [Display(Name = "Logo Path")]
        public string LogoPath { get; set; }

        [Display(Name = "Image Path")]
        public string ImagePath { get; set; }

        [Display(Name = "School Category")]
        public int SchoolCategory { get; set; }

        [Display(Name = "School Category")]
        public string SchoolCategoryName { get; set; }

        [Display(Name = "Telephone")]

        [Required(ErrorMessage = "You must provide a PhoneNumber")]

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string Telephone { get; set; }
        [Display(Name = "Minutes for Period")]

        [Required(ErrorMessage = "You must provide a valid time period")]
     
        //[MaxValue(100)]
        //[MinValue(1)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Period must be numeric")]
        public string MinuteforPeriod { get; set; }
        [Display(Name = "Fax")]
        public string Fax { get; set; }
        [Display(Name = "Email")]
       
        [EmailAddress(ErrorMessage = "Invalid Email Address")]

        
        public string Email { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string AdminPersonalEmail { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(255, MinimumLength = 8)]

        [MembershipPassword()]
        public string Password { get; set; }
        [Display(Name = " Confirm Password")]
        [Required(ErrorMessage = " Confirm Password is required")]
        [Compare("Password")]
        [DataType(DataType.Password)]
        [StringLength(255, MinimumLength = 8)]

        [MembershipPassword()]
        public string ConfirmPassword { get; set; }


        [Display(Name = "Mobile Contact No")]


        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string PersonalMobile { get; set; }


        [Display(Name = "Address3")]
        public string Address3 { get; set; }
        [Display(Name = " Address2")]
        public string Address2 { get; set; }
      
        
       
        [Display(Name = "School Image")]
    [ValidateFile]
        public HttpPostedFileBase File { get; set; }


        [Display(Name = "Logo Image")]
        [ValidateFile]
        public HttpPostedFileBase LogoFile { get; set; }


      

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address is required")]
        public string Address1 { get; set; }


        [Display(Name = "Address1")]
       
        public string Addresst { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "District")]
        public Nullable<int> District { get; set; }

        [Display(Name = "Division")]
        public Nullable<int> Division { get; set; }
        [Display(Name = "School Rank")]
        public Nullable <int> SchoolRank { get; set; }

        [Display(Name = "School Group")]
        public Nullable <long> SchoolGroup { get; set; }

        [Display(Name = "School Group")]
        public string GroupName { get; set; }
 
        [Display(Name = "Teacher Id")]
        public string TeacherId { get; set; }

        [Display(Name = "School Name")]
        [Required(ErrorMessage = "School Name is required")]
        public string SchoolName { get; set; }

        [Display(Name = "School")]
 
        public string SchoolNamel { get; set; }

        [Display(Name = "Grade")]
        public string GradeId { get; set; }
        [Display(Name = "Class")]
        public string ClassId { get; set; }


        [Display(Name = "Class Id")]
        public string ClassIdName { get; set; }


        [Display(Name = "Class Name")]
        public string ClassName { get; set; }
        [Display(Name = "Accedamic Year")]
        public string AccedamicYear { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
       
        [Display(Name = "Active")]
        public string IsActive { get; set; }



           [Display(Name = "Rank Name")]
        public string SchoolRankName { get; set; }

         [Display(Name = "District Name")]
        public string DistrictName { get; set; }


         [Display(Name = "Division Name")]
        public string DivisionName { get; set; }


         [Display(Name = "Province Name")]
        public string ProvinceName { get; set; }
        


    }
}