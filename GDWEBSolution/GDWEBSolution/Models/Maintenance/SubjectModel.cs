using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GDWEBSolution.Models.Maintenance
{
    public class SubjectModel
    {
        [Display(Name = "Code")]
        public int SubjectId { get; set; }

        [Display(Name = "Short Name")]
        public string ShortName { get; set; }

        [Display(Name = "Subject Name")]
        public string SubjectName { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Display(Name = "Created Date")]
        public System.DateTime CreatedDate { get; set; }

        [Display(Name = "Modified By")]
        public string ModifiedBy { get; set; }

        [Display(Name = "Modified Date")]
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        [Display(Name = "Broadcast Message")]
        public string BroadcastMessage { get; set; }

        [Display(Name = "Parent Approval Needed")]
        public string ParentApprovalNeeded { get; set; }


        [Display(Name = "Active")]
        public string IsActive { get; set; }
    }
}