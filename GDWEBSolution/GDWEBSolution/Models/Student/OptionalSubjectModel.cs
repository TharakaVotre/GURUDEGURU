﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GDWEBSolution.Models.Student
{
    public class OptionalSubjectModel
    {



        [Display(Name = "Seq No")]
      
        public string SequanceNo { get; set; }

        [Display(Name = "School Id")]
        [Required(ErrorMessage = "Please Select School Name")]
        public string SchoolId { get; set; }


        [Display(Name = "Academic Year")]
        [Required(ErrorMessage = "Please Select the Academic Year")]
        public string AcademicYear { get; set; }


        [Display(Name = "School Name")]

        public string SchoolName { get; set; }

        [Display(Name = "Student Id")]

        [Required(ErrorMessage = "Student ID is required")]
        public string StudentId { get; set; }


      
        [Display(Name = "Student Name")]
        [Required(ErrorMessage = "Student Name is required")]
        public string StudentName { get; set; }

   

   



        [Display(Name = "Grade Id")]

        [Required(ErrorMessage = "Grade Name is required")]
        public string GradeId { get; set; }
        [Display(Name = "Class Id")]

        [Required(ErrorMessage = "Class Name is required")]
        public string ClassId { get; set; }





        [Display(Name = "Subject Id")]
        public string SubjectId { get; set; }

        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        [Display(Name = "Active")]
        public string IsActive { get; set; }


    }
}