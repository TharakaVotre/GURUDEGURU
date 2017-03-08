using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GDWEBSolution.Models.Teacher
{
    public class ExtraActivityModel
    {
        [Display(Name = "Teacher Id")]
        public long TeacherID { get; set; }

        [Display(Name = "Teacher Name")]
        public long TeacherName { get; set; }
        [Display(Name = "Activity Code")]
        public string ActivityCode { get; set; }
        [Display(Name = "Activity Name")]
        public string ActivityName { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        [Display(Name = "Created Date")]
        public System.DateTime CreatedDate { get; set; }
        [Display(Name = "Modified By")]
        public string ModifiedBy { get; set; }
        [Display(Name = "Modified Date")]
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        [Display(Name = "Is Active")]
        public string IsActive { get; set; }

        public string SchoolId { get; set; }
    }
}