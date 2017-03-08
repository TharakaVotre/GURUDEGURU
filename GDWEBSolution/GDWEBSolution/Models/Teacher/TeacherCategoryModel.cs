using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GDWEBSolution.Models
{
    public class TeacherCategoryModel
    {

        [Display(Name = "Teacher Category ID")]
        public int TeacherCategoryId { get; set; }

        [Required(ErrorMessage="Please Enter a Teacher Category Name")]
        [Display(Name = "Teacher Category Name")]
        public string TeacherCategoryName { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Display(Name = "Created Date")]
        public System.DateTime CreatedDate { get; set; }

        [Display(Name = "Modified By")]
        public string ModifiedBy { get; set; }

        [Display(Name = "Modified Date")]
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        [Display(Name = "Active")]
        public string IsActive { get; set; }

        [Display(Name = "Active")]
        public bool IsActiveBool { get; set; }
    }
}