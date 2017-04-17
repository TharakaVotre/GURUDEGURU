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
        [Required]
        [Display(Name = "Name")]
        public string AcademicYear { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string GradeId { get; set; }
        public string GradeName { get; set; }

        [Required]
        [Display(Name = "Name")]
        public int SubjectId { get; set; }

         [Required]
        [Display(Name = "Name")]
        public string Optional { get; set; }

        [Display(Name = "Created By")]
         public int SubjectCategoryId { get; set; }
        public string SubjectCategoryName { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Display(Name = "Created Date")]
        public System.DateTime CreatedDate { get; set; }

        [Display(Name = "Modified By")]
        public string ModifiedBy { get; set; }

        [Display(Name = "Modified Date")]
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string IsActive { get; set; }
        public string ShortName { get; set; }
        public string SubjectName { get; set; }
       
    }
}

      
     