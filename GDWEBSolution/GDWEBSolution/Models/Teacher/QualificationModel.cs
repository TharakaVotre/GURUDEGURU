using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GDWEBSolution.Models.Teacher
{
    public class QualificationModel
    {
        [Display(Name = "Teacher Id")]
        public long TeacherId { get; set; }

        [Display(Name = "Teacher Name")]
        public long TeacherName { get; set; }

        public string SchoolId { get; set; }

        [Display(Name = "Qualification Id")]
        public int QualificationId { get; set; }

        [Display(Name = "Qualification Name")]
        public string QualificationName { get; set; }

        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string IsActive { get; set; }
    }
}