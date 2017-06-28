using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GDWEBSolution.Models.Configuration
{
    public class Grade_SubjectModel
    {
        
        [Display(Name = "Academic Year")]
        public string AcademicYear { get; set; }


        [Display(Name = "Grade Id")]
        public string GradeId { get; set; }

         [Display(Name = "Grade Name")]
        public string GradeName { get; set; }


        [Display(Name = "Subject Id")]
        public int SubjectId { get; set; }

         [Required]
         [Display(Name = "Optional")]
        public string Optional { get; set; }

        [Display(Name = "Category Name")]
         public int SubjectCategoryId { get; set; }

           [Display(Name = "Category Name")]
        public string SubjectCategoryName { get; set; }

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

           [Display(Name = "Short Name")]
        public string ShortName { get; set; }

           [Display(Name = "Subject Name")]
        public string SubjectName { get; set; }
       
    }
}

      
     