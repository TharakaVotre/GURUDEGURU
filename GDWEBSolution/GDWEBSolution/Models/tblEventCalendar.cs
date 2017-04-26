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
    
    public partial class tblEventCalendar
    {
        public tblEventCalendar()
        {
            this.tblEventParticipants = new HashSet<tblEventParticipant>();
        }
    
        public string SchoolId { get; set; }
        public long EventNo { get; set; }
        public string EventTitle { get; set; }
        public string EventDescription { get; set; }
        public System.DateTime FromDate { get; set; }
        public System.DateTime ToDate { get; set; }
        public System.TimeSpan FromTime { get; set; }
        public System.TimeSpan ToTime { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string EventOrganizer { get; set; }
        public long EventCategory { get; set; }
        public string IsActive { get; set; }
    
        public virtual tblEventcategory tblEventcategory { get; set; }
        public virtual tblSchool tblSchool { get; set; }
        public virtual tblUser tblUser { get; set; }
        public virtual tblUser tblUser1 { get; set; }
        public virtual ICollection<tblEventParticipant> tblEventParticipants { get; set; }
    }
}
