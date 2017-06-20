using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GDWEBSolution.Models.Evaluation
{
    public class EvaluationModel
    {



        [Display(Name = "Evaluation Detail SeqNo")]
        public long EvaluationDetailSeqNo { get; set; }


        [Display(Name = "Grade")]
        public string GradeName { get; set; }

        [Display(Name = "Class")]
        public string ClassName { get; set; }
        
             [Display(Name = "Scheduled Date")]
        public string ScheduledDates { get; set; }
        
         [Display(Name = "Time Start")]
        public string ScheduledTimeStartst { get; set; }
        
         [Display(Name = "Time End")]
        public string ScheduledTimeEndst { get; set; }


        [Display(Name = "Evaluation Type")]
        public long EvaluationType { get; set; }


        [Display(Name = "Evaluation Type")]
        public string EvaluationTypeDesc { get; set; }

        [Display(Name = "Grade")]
        public String Grade { get; set; }
        [Display(Name = "Class")]
        public String Class { get; set; }



         [Required(ErrorMessage = "You must provide a date")]
        [Display(Name = "Scheduled Date")]
        public DateTime ScheduledDate { get; set; }

        [Display(Name = "Time Start")]
        public TimeSpan ScheduledTimeStart { get; set; }
         [Required(ErrorMessage = "You must provide a start time")]
        public string ScheduledTimeStarts { get; set; }
        
        [Display(Name = "Time End")]
        public TimeSpan ScheduledTimeEnd { get; set; }
         [Required(ErrorMessage = "You must provide an end time")]
        public string ScheduledTimeEnds { get; set; }

         [Display(Name = "School Id")]
        public String  SchoolId { get; set; }


         [Display(Name = "School Id")]
         public String SchoolId1 { get; set; }

        [Display(Name = "Test Paper Fee")]
        public decimal TestPaperFee { get; set; }
         [Required(ErrorMessage = "You must provide an evaluation")]
        [Display(Name = "Evaluation Description")]
        public long  EvaluationNo { get; set; }

        [Display(Name = "Evaluation")]
        public string  EvaluationDescription { get; set; }

        [Display(Name = "Accedamic Year")]
        public string  AcademicYear { get; set; }


              public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        [Display(Name = "Active")]
        public string IsActive { get; set; }
    }
}