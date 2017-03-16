using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GDWEBSolution.Models.Schools
{
    public class SchoolModel
    {


        [Display(Name = "School Id")]
        public string SchoolId { get; set; }

        [Display(Name = "Province")]
        public string Province { get; set; }

        [Display(Name = "School Category")]
        public int SchoolCategory { get; set; }
        [Display(Name = "Telephone")]
        public string Telephone { get; set; }
        [Display(Name = "Minute for Period")]
        public string MinuteforPeriod { get; set; }
        [Display(Name = "Fax")]
        public string Fax { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Address3")]
        public string Address3 { get; set; }
        [Display(Name = " Address2")]
        public string Address2 { get; set; }

        [Display(Name = "Address1")]
        public string Address1 { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "District")]
        public string District { get; set; }

        [Display(Name = "Division")]
        public string Division { get; set; }
        [Display(Name = "School Rank")]
        public string SchoolRank { get; set; }

        [Display(Name = "School Group")]
        public string SchoolGroup { get; set; }
 
        [Display(Name = "Teacher Id")]
        public string TeacherId { get; set; }

        [Display(Name = "School Name")]
        public string SchoolName { get; set; }

        [Display(Name = "Grade Id")]
        public string GradeId { get; set; }
        [Display(Name = "Class Id")]
        public string ClassId { get; set; }
        [Display(Name = "Accedamic Year")]
        public string AccedamicYear { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
       
        [Display(Name = "Active")]
        public string IsActive { get; set; }


    }
}