//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GDWEBSolution
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblMessageType
    {
        public tblMessageType()
        {
            this.tblParentToSchoolMessageHeaders = new HashSet<tblParentToSchoolMessageHeader>();
            this.tblSchoolToParentMessageHeaders = new HashSet<tblSchoolToParentMessageHeader>();
        }
    
        public long MessageTypeId { get; set; }
        public string MessageTypeDescription { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string IsActive { get; set; }
    
        public virtual tblUser tblUser { get; set; }
        public virtual tblUser tblUser1 { get; set; }
        public virtual ICollection<tblParentToSchoolMessageHeader> tblParentToSchoolMessageHeaders { get; set; }
        public virtual ICollection<tblSchoolToParentMessageHeader> tblSchoolToParentMessageHeaders { get; set; }
    }
}
