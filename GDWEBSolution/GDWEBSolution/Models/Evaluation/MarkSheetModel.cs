using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GDWEBSolution.Models.Evaluation
{
    public class MarkSheetModel
    {
         [Display(Name = "Student Name")]
        public String studentName { get; set; }

        [Display(Name = "Student Id")]
        public String StudentId { get; set; }
         
        [Display(Name = "Evaluation No")]
        public long EvaluationNo { get; set; }

          [Display(Name = "Evaluation Type")]
        public long EvaluationType { get; set; }

         [Display(Name = "Accedamic Year")]
        public String AccedamicYear { get; set; }

          [Display(Name = "Grade")]
        public String Grade { get; set; }

          [Display(Name = "Class")]
        public String Class { get; set; }

         [Display(Name = "School Id")]
        public String SchoolId { get; set; }

 

        [Display(Name = "Mark")]
        public decimal Mark { get; set; }

        [Display(Name = "Subject Id")]
        public string SubjectId { get; set; }

        [Display(Name = "Subject Name")]
        public string SubjectName { get; set; }

         [Display(Name = "Evaluation Type Description")]
        public string EvaluationTypeDesc { get; set; }





         public string CreatedBy { get; set; }
         public System.DateTime CreatedDate { get; set; }
         public string ModifiedBy { get; set; }
         public Nullable<System.DateTime> ModifiedDate { get; set; }

         [Display(Name = "Active")]
         public string IsActive { get; set; }


        

        








    }
}