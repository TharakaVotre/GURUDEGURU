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
    
    public partial class tblClass
    {
        public tblClass()
        {
            this.tblAssignmentHeaders = new HashSet<tblAssignmentHeader>();
            this.tblClassTeachers = new HashSet<tblClassTeacher>();
            this.tblEvaluationDetails = new HashSet<tblEvaluationDetail>();
            this.tblStudents = new HashSet<tblStudent>();
            this.tblStudentOptionalSubjects = new HashSet<tblStudentOptionalSubject>();
            this.tblTeacherSubjects = new HashSet<tblTeacherSubject>();
            this.tblTimeTables = new HashSet<tblTimeTable>();
        }
    
        public string ClassId { get; set; }
        public string ClassName { get; set; }
        public string GradeId { get; set; }
        public string SchoolId { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string IsActive { get; set; }
    
        public virtual ICollection<tblAssignmentHeader> tblAssignmentHeaders { get; set; }
        public virtual tblGrade tblGrade { get; set; }
        public virtual tblSchool tblSchool { get; set; }
        public virtual tblUser tblUser { get; set; }
        public virtual tblUser tblUser1 { get; set; }
        public virtual ICollection<tblClassTeacher> tblClassTeachers { get; set; }
        public virtual ICollection<tblEvaluationDetail> tblEvaluationDetails { get; set; }
        public virtual ICollection<tblStudent> tblStudents { get; set; }
        public virtual ICollection<tblStudentOptionalSubject> tblStudentOptionalSubjects { get; set; }
        public virtual ICollection<tblTeacherSubject> tblTeacherSubjects { get; set; }
        public virtual ICollection<tblTimeTable> tblTimeTables { get; set; }
    }
}
