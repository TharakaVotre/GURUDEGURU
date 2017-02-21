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
    
    public partial class tblParent
    {
        public tblParent()
        {
            this.tblParentStudents = new HashSet<tblParentStudent>();
            this.tblParentToSchoolMessageHeaders = new HashSet<tblParentToSchoolMessageHeader>();
            this.tblSchoolToParentMessageDetails = new HashSet<tblSchoolToParentMessageDetail>();
        }
    
        public long ParentId { get; set; }
        public string ParentName { get; set; }
        public int RelationshipId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string HomeTelephone { get; set; }
        public string PersonalEmail { get; set; }
        public string PersonalMobile { get; set; }
        public string Occupation { get; set; }
        public string OfficeAddress1 { get; set; }
        public string OfficeAddress2 { get; set; }
        public string OfficeAddress3 { get; set; }
        public string OfficePhone { get; set; }
        public string officeEmail { get; set; }
        public string NIC { get; set; }
        public string UserId { get; set; }
        public string ImgUrl { get; set; }
        public System.DateTime DateofBirth { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string IsActive { get; set; }
    
        public virtual tblRelashionship tblRelashionship { get; set; }
        public virtual tblUser tblUser { get; set; }
        public virtual tblUser tblUser1 { get; set; }
        public virtual ICollection<tblParentStudent> tblParentStudents { get; set; }
        public virtual ICollection<tblParentToSchoolMessageHeader> tblParentToSchoolMessageHeaders { get; set; }
        public virtual ICollection<tblSchoolToParentMessageDetail> tblSchoolToParentMessageDetails { get; set; }
    }
}
