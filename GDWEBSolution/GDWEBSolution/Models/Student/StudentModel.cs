using GDWEBSolution.Models.Schools;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GDWEBSolution.Models.Student
{
    public class StudentModel
    {

        [Display(Name = "School Id")]
       // 
        public string SchoolId { get; set; }

        [Display(Name = "School Id")]
        [Required(ErrorMessage = "Please Select School Name")]
        public string SchoolIdw { get; set; }


        [Display(Name = "School Name")]
       
        public string SchoolName { get; set; }

        [Display(Name = "Student Id")]
       
        [Required(ErrorMessage = "Student ID is required")]
        public string StudentId { get; set; }


        public string ImagePathname { get; set; }
        [Display(Name = "Student Name")]
        [Required(ErrorMessage = "Student Name is required")]
        public string StudentName { get; set; }

        [Display(Name = "Date of Birth *")]
        public Nullable<System.DateTime> DateOfBirth { get; set; }

        [Display(Name = "Student Image")]
        [ValidateFile]
        public HttpPostedFileBase StudentImageFile { get; set; }

      

        [Display(Name = "Grade Name")]

        [Required(ErrorMessage = "Grade Name is required")]
        public string GradeId { get; set; }
        [Display(Name = "Class Name")]

        [Required(ErrorMessage = "Class Name is required")]
        public string ClassId { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }


        [Display(Name = "User Id")]
        [Required(ErrorMessage = "User Id is required")]

        public string UserId { get; set; }

        [Display(Name = "House Id")]
        public string HouseId { get; set; }

        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        [Display(Name = "Active")]
        public string IsActive { get; set; }





    }
}