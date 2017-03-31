using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GDWEBSolution.Models.Schools
{
    public class SchoolModel
    {

        
        [Display(Name = "School Id")]
        //[Required(ErrorMessage = "Please enter your full SchoolId")]
        public string SchoolId { get; set; }

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
        [Display(Name = "Minute for Period")]

        [Required(ErrorMessage = "You must provide a valid time period")]
     
        //[MaxValue(100)]
        //[MinValue(1)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Period must be numeric")]
        public string MinuteforPeriod { get; set; }
        [Display(Name = "Fax")]
        public string Fax { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]

        
        public string Email { get; set; }
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

        [Display(Name = "Grade Id")]
        public string GradeId { get; set; }
        [Display(Name = "Class Id")]
        public string ClassId { get; set; }
        [Display(Name = "Accedamic Year")]
        public string AccedamicYear { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
       
        [Display(Name = "Active")]
        public string IsActive { get; set; }


    }
}