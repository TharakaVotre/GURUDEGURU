using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GDWEBSolution.Models.Schools
{
    public class SchoolGradeModel
    {
      
        [Display(Name = "School Id")]
        public string SchoolId { get; set; }

        [Display(Name = "School Name")]
        public string SchoolName { get; set; }

        [Display(Name = "Grade Name")]
        public string GradeName { get; set; }

         [Display(Name = "Grade")]
        public string GradeId { get; set; }
         [Display(Name = "School")]

         public string SchoolNamel { get; set; }
         [Display(Name = "Grade")]
         public string GradeId1 { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        [Display(Name = "Active")]
        public string IsActive { get; set; }



    }
}