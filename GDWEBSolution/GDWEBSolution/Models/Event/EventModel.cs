using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GDWEBSolution.Models.Event
{
    public class EventModel
    {
        [Display(Name = "School Id")]
        public string SchoolId { get; set; }
        [Display(Name = "School")]
        public string SchoolName { get; set; }

        [Display(Name = "Event No")]
        public long EventNo { get; set; }
        [Display(Name = "Event No")]
        public string EventNo_S { get; set; }
        [Display(Name = "Event Desciption")]
        public string EventDescription { get; set; }
        [Display(Name = "Event Name")]
        public string EventName { get; set; }


        [Display(Name = "From Date")]
        public System.DateTime FromDate { get; set; }
        [Display(Name = "To Date")]
        public System.DateTime ToDate { get; set; }

        [Display(Name = "From Date")]
        public string SFromDate { get; set; }
        [Display(Name = "To Date")]
        public string SToDate { get; set; }

        [Display(Name = "From Time")]
        public System.TimeSpan FromTime { get; set; }
        [Display(Name = "To Time")]
        public System.TimeSpan ToTime { get; set; }

        [Display(Name = "From Time")]
        public string SFromTime { get; set; }
        [Display(Name = "To Time")]
        public string SToTime { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        [Display(Name = "Created Date")]
        public System.DateTime CreatedDate { get; set; }
        [Display(Name = "Modified By")]
        public string ModifiedBy { get; set; }
        [Display(Name = "Modified Date")]
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        [Display(Name = "Organizer")]
        public string EventOrganizer { get; set; }

        public string IsActive { get; set; }
        [Display(Name = "Event Category Id")]
        public long EventCategoryId { get; set; }
        [Display(Name = "Event Category")]
        public string EventCategoryDesc { get; set; }
        [Display(Name = "Parent Approval Needed")]
        public string ParentApprovalNeeded { get; set; }
        [Display(Name = "Broad Cast Message")]
        public string BroadcastMessage { get; set; }
    }
}