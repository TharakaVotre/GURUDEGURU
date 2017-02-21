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
    
    public partial class tblAssignmentHeader
    {
        public tblAssignmentHeader()
        {
            this.tblAssignmentDueDates = new HashSet<tblAssignmentDueDate>();
            this.tblAssignmentQuestionAids = new HashSet<tblAssignmentQuestionAid>();
            this.tblAssignmentQuestionAnswers = new HashSet<tblAssignmentQuestionAnswer>();
        }
    
        public long AssignmentNo { get; set; }
        public string AssignmentDescription { get; set; }
        public string SchoolId { get; set; }
        public string GradeId { get; set; }
        public string ClassId { get; set; }
        public string StudentId { get; set; }
        public long TeacherId { get; set; }
        public string FilePath { get; set; }
        public string BatchNo { get; set; }
        public string BatchDescription { get; set; }
        public int SubjectId { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string IsActive { get; set; }
    
        public virtual ICollection<tblAssignmentDueDate> tblAssignmentDueDates { get; set; }
        public virtual tblTeacher tblTeacher { get; set; }
        public virtual tblClass tblClass { get; set; }
        public virtual tblGrade tblGrade { get; set; }
        public virtual tblSchool tblSchool { get; set; }
        public virtual tblStudent tblStudent { get; set; }
        public virtual tblSubject tblSubject { get; set; }
        public virtual tblUser tblUser { get; set; }
        public virtual tblUser tblUser1 { get; set; }
        public virtual ICollection<tblAssignmentQuestionAid> tblAssignmentQuestionAids { get; set; }
        public virtual ICollection<tblAssignmentQuestionAnswer> tblAssignmentQuestionAnswers { get; set; }
    }
}
