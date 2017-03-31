using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GDWEBSolution.Models.Schools
{
    public class SchoolExtraModel
    {
        [Display(Name = "School Name")]
        public string SchoolId { get; set; }

        [Display(Name = "Activity Code")]
        public string ActivityCode { get; set; }

        [Display(Name = "Activity Name")]
        public string  ActivityName { get; set; }

  

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