using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GDWEBSolution.Models.HomeWork
{
    public class HomeWorkModel
    {

        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }

        [Display(Name = "Assignment No")]
        public long AssignmentNo { get; set; }

        [Display(Name = "Description")]
        public string AssignmentDescription { get; set; }

        [Display(Name = "SchoolId")]
        public string SchoolId { get; set; }

        [Display(Name = "Grade")]
        public string GradeId { get; set; }

        [Display(Name = "Class")]
        public string ClassId { get; set; }

        [Display(Name = "StudentId")]
        public string StudentId { get; set; }

        [Display(Name = "TeacherId")]
        public long TeacherId { get; set; }

        [Display(Name = "FilePath")]
        public string FilePath { get; set; }

        [Display(Name = "File")]
      //  [ValidateFile]
        public HttpPostedFileBase File { get; set; }

        [Display(Name = "Batch No")]
        public string BatchNo { get; set; }

        [Display(Name = "Batch Description")]
        public string BatchDescription { get; set; }

        [Display(Name = "Subject")]
        public int SubjectId { get; set; }

        [Display(Name = "CreatedBy")]
        public string CreatedBy { get; set; }

        [Display(Name = "CreatedDate")]
        public DateTime CreatedDate { get; set; }
            
        [Display(Name = "Name")]
        public string ModifiedBy { get; set; }
            
        [Display(Name = "Name")]
        public DateTime ModifiedDate { get; set; }
            
        [Display(Name = "Name")]
        public string IsActive { get; set; }
    }
}