using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GDWEBSolution.Models.Schools
{
    public class SchoolSubjectModel
    {

        [Display(Name = "Academic Year")]
        public string AcademicYear { get; set; }

        [Display(Name = "School Id")]
        public string SchoolId { get; set; }
        
             [Display(Name = "School Id")]
        public string SchoolIds { get; set; }

        [Display(Name = "Subject Category Name")]
        public string SubjectCategoryId { get; set; }


        [Display(Name = "Subject Name")]
        public string SubjectId { get; set; }

        [Display(Name = "Subject Name")]
        public string SubjectName { get; set; }

        [Display(Name = "Optional")]
        public string Optional { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        [Display(Name = "Active")]
        public string IsActive { get; set; }



    }
}