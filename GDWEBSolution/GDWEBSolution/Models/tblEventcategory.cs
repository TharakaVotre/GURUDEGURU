//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GDWEBSolution.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblEventcategory
    {
        public tblEventcategory()
        {
            this.tblEventCalendars = new HashSet<tblEventCalendar>();
        }
    
        public long EventCategoryId { get; set; }
        public string EventCategoryDesc { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ParentApprovalNeeded { get; set; }
        public string BroadcastMessage { get; set; }
        public string IsActive { get; set; }
    
        public virtual ICollection<tblEventCalendar> tblEventCalendars { get; set; }
        public virtual tblUser tblUser { get; set; }
        public virtual tblUser tblUser1 { get; set; }
    }
}
