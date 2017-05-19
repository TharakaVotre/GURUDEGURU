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

        [Display(Name = "Evaluation Type")]
        public long EvaluationType { get; set; }

        [Display(Name = "Grade")]
        public String Grade { get; set; }
        [Display(Name = "Class")]
        public String Class { get; set; }


        [Display(Name = "ScheduledDate")]
        public DateTime ScheduledDate { get; set; }

        [Display(Name = "Scheduled Time")]
        public TimeSpan ScheduledTime { get; set; }

         [Display(Name = "School Id")]
        public String  SchoolId { get; set; }


         [Display(Name = "School Id")]
         public String SchoolId1 { get; set; }

        [Display(Name = "TestPaper Fee")]
        public decimal TestPaperFee { get; set; }

        [Display(Name = "Evaluation Description")]
        public long  EvaluationNo { get; set; }

        [Display(Name = "Evaluation Description")]
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