using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GDWEBSolution.Models.Maintenance
{
    public class SchoolRankModel
    {
        [Display(Name = "Code")]
        public long SchoolRankId { get; set; }

        [Display(Name = "Rank Name")]
        public string SchoolRankName { get; set; }

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
    }
}