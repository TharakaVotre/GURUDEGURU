using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GDWEBSolution.Models.Maintenance
{
    public class EventCategoryModel
    {
        [Display(Name = "Code")]
        public long EventCategoryId { get; set; }

        [Required(ErrorMessage = "Please Enter Event Category Description")]
        [Display(Name = "Description")]
        public string EventCategoryDesc { get; set; }

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