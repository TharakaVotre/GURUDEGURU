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
    
    public partial class tblTeacherQualification
    {
        public long TeacherId { get; set; }
        public string SchoolId { get; set; }
        public int QualificationId { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string IsActive { get; set; }
    
        public virtual tblQualification tblQualification { get; set; }
        public virtual tblSchool tblSchool { get; set; }
        public virtual tblTeacher tblTeacher { get; set; }
        public virtual tblUser tblUser { get; set; }
        public virtual tblUser tblUser1 { get; set; }
    }
}
