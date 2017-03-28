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
    
    public partial class tblStudent
    {
        public tblStudent()
        {
            this.tblAssignmentHeaders = new HashSet<tblAssignmentHeader>();
            this.tblEvaluationResults = new HashSet<tblEvaluationResult>();
            this.tblParentStudents = new HashSet<tblParentStudent>();
            this.tblStudentExtraCurricularActivities = new HashSet<tblStudentExtraCurricularActivity>();
            this.tblStudentOptionalSubjects = new HashSet<tblStudentOptionalSubject>();
        }
    
        public string SchoolId { get; set; }
        public string StudentId { get; set; }
        public string AcademicYear { get; set; }
        public string studentName { get; set; }
        public Nullable<System.DateTime> DateofBirth { get; set; }
        public string GradeId { get; set; }
        public string ClassId { get; set; }
        public string Gender { get; set; }
        public string UserId { get; set; }
        public string HouseId { get; set; }
        public string ImgUrl { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string IsActive { get; set; }
    
        public virtual ICollection<tblAssignmentHeader> tblAssignmentHeaders { get; set; }
        public virtual tblClass tblClass { get; set; }
        public virtual ICollection<tblEvaluationResult> tblEvaluationResults { get; set; }
        public virtual tblGrade tblGrade { get; set; }
        public virtual tblHouse tblHouse { get; set; }
        public virtual ICollection<tblParentStudent> tblParentStudents { get; set; }
        public virtual tblSchool tblSchool { get; set; }
        public virtual tblUser tblUser { get; set; }
        public virtual tblUser tblUser1 { get; set; }
        public virtual ICollection<tblStudentExtraCurricularActivity> tblStudentExtraCurricularActivities { get; set; }
        public virtual ICollection<tblStudentOptionalSubject> tblStudentOptionalSubjects { get; set; }
    }
}
