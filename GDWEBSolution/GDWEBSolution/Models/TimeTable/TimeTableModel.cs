using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GDWEBSolution.Models.TimeTable
{
    public class TimeTableModel
    {
        public long SeqNo { get; set; }

        [Display(Name = "Academic Year")]
        public string AcademicYear { get; set; }

        [Display(Name = "School Id")]
        public string SchoolId { get; set; }
        [Display(Name = "School")]
        public string SchoolName { get; set; }

        [Display(Name = "Grade Id")]
        public string GradeId { get; set; }
        [Display(Name = "Grade")]
        public string GradeName { get; set; }

        [Display(Name = "Class Id")]
        public string ClassId { get; set; }
        [Display(Name = "Class")]
        public string ClassName { get; set; }

        [Display(Name = "Day")]
        public string Day { get; set; }

        [Display(Name = "Subject Id")]
        public int SubjectId { get; set; }
        [Display(Name = "Subject")]
        public string SubjectName { get; set; }

        [Display(Name = "From")]
        public System.TimeSpan FromTime { get; set; }
        [Display(Name = "From")]
        public string FromTime_String { get; set; }
        [Display(Name = "To")]
        public System.TimeSpan ToTime { get; set; }
        [Display(Name = "To")]
        public string ToTime_String { get; set; }

        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string IsActive { get; set; }
        [Display(Name = "Period Id")]
        public int PeriodSeqNo { get; set; }
    }
}