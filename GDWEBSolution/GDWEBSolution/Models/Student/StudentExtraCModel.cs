using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GDWEBSolution.Models.Student
{
    public class StudentExtraCModel
    {
        
            [Display(Name = "School Name")]
            public string SchoolId { get; set; }

            [Display(Name = "School Name")]
            public string SchoolIdE { get; set; }

            [Display(Name = "Student Name")]
            public string StudentId { get; set; }
        
             [Display(Name = "Student Name")]
            public string StudentIdE { get; set; }


            [Display(Name = "Grade Name")]
            public string GradeIdE { get; set; }
            [Display(Name = "Class Name")]
            public string ClassIdE { get; set; }

            [Display(Name = "Activity Code")]
            public string ActivityCode { get; set; }

            [Display(Name = "Activity Name")]
            public string ActivityName { get; set; }



            [Display(Name = "School Name")]
            public string SchoolName { get; set; }


            public string CreatedBy { get; set; }
            public System.DateTime CreatedDate { get; set; }
            public string ModifiedBy { get; set; }
            public Nullable<System.DateTime> ModifiedDate { get; set; }

            [Display(Name = "Active")]
            public string IsActive { get; set; }




        


    }
}