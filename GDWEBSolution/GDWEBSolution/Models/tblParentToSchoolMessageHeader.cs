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
    
    public partial class tblParentToSchoolMessageHeader
    {
        public tblParentToSchoolMessageHeader()
        {
            this.tblParentToSchoolMessageDetails = new HashSet<tblParentToSchoolMessageDetail>();
        }
    
        public string SchoolId { get; set; }
        public long MessageId { get; set; }
        public long ParentId { get; set; }
        public Nullable<long> MessageType { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string IsActive { get; set; }
        public Nullable<int> Attachments { get; set; }
        public string Subject { get; set; }
    
        public virtual tblMessageType tblMessageType { get; set; }
        public virtual tblParent tblParent { get; set; }
        public virtual ICollection<tblParentToSchoolMessageDetail> tblParentToSchoolMessageDetails { get; set; }
        public virtual tblSchool tblSchool { get; set; }
        public virtual tblUser tblUser { get; set; }
        public virtual tblUser tblUser1 { get; set; }
    }
}
