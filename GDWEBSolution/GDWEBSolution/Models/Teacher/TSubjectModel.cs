using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GDWEBSolution.Models.Teacher
{
    public class TSubjectModel
    {
        [Display(Name = "Seq No")]
        public long TeacherSubjectSeqNo { get; set; }

        [Display(Name = "Acedemic Year")]
        public string AcedemicYear { get; set; }

        [Display(Name = "Teacher")]
        public long Teacher_ID { get; set; }

        [Display(Name = "Teacher Name")]
        public long TeacherId { get; set; }


        [Display(Name = "School")]
        public string SchoolIds { get; set; }

        [Display(Name = "Grade")]
        public string GradeId { get; set; }

        [Display(Name = "Grade")]
        public string GradeName { get; set; }

        [Display(Name = "Class")]
        public string ClassId { get; set; }

        [Display(Name = "Subject")]
        public int SubjectId { get; set; }

        [Display(Name = "Subject Name")]
        public string SubjectName { get; set; }

        
        public string CreatedBy { get; set; }

        public System.DateTime CreatedDate { get; set; }

        public string ModifiedBy { get; set; }

        public Nullable<System.DateTime> ModifiedDate { get; set; }

        [Display(Name = "Is Active")]
        public string IsActive { get; set; }
    }
}