using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GDWEBSolution.Models.AnnualFunctions
{
    public class StudentGradeAdvanceModel
    {
        [Required]
        [Display(Name = "Student Id")]
        public string StudentId { get; set; }

        [Display(Name = "Grade Id")]
        public string GradeId { get; set; }

        [Display(Name = "Grade Name")]
        public string GradeName { get; set; }

        [Required]
        [Display(Name = "Student Name")]
        public string StudentName { get; set; }

        [Display(Name = "Class Id")]
        public string ClassId { get; set; }

         [Display(Name = "House Id")]
        public string HouseId { get; set; }

         [Display(Name = "Class Name")]
        public string ClassName { get; set; }

         [Display(Name = "House Name")]
        public string HouseName { get; set; }

       
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Display(Name = "Created Date")]
        public System.DateTime CreatedDate { get; set; }

        [Display(Name = "Modified By")]
        public string ModifiedBy { get; set; }

        [Display(Name = "Modified Date")]
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string IsActive { get; set; }
       
    }
}