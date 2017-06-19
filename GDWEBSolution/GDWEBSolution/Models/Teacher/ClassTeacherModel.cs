using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GDWEBSolution.Models.Teacher
{
    public class ClassTeacherModel
    {
        [Display(Name = "School Id")]
        public string SchoolId { get; set; }
        [Display(Name = "Teacher Id")]
        public string TeacherId { get; set; }
        [Display(Name = "Teacher Name")]
        public string TeacherName { get; set; }
        [Display(Name = "Grade Id")]
        public string GradeId { get; set; }
        [Display(Name = "Grade")]
        public string GradeName { get; set; }
        [Display(Name = "Class Id")]
        public string ClassId { get; set; }
        [Display(Name = "Class")]
        public string ClassName { get; set; }
        [Display(Name = "Accedamic Year")]
        public string AccedamicYear { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        [Display(Name = "Active")]
        public string IsActive { get; set; }

        public List<ClassTeacherModel> ClassTeacherList { get; set; }
    }
}