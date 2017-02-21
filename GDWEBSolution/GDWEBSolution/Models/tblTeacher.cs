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
    
    public partial class tblTeacher
    {
        public tblTeacher()
        {
            this.tblAssignmentHeaders = new HashSet<tblAssignmentHeader>();
            this.tblTeacherExtraCurricularActivities = new HashSet<tblTeacherExtraCurricularActivity>();
            this.tblTeacherQualifications = new HashSet<tblTeacherQualification>();
            this.tblTeacherSchools = new HashSet<tblTeacherSchool>();
            this.tblTeacherSubjects = new HashSet<tblTeacherSubject>();
        }
    
        public long TeacherId { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public int TeacherCategoryId { get; set; }
        public string Telephone { get; set; }
        public string Gender { get; set; }
        public string Description { get; set; }
        public string EmployeeNo { get; set; }
        public string NIC { get; set; }
        public string DrivingLicense { get; set; }
        public string Passport { get; set; }
        public string UserId { get; set; }
        public string ImgUrl { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string IsActive { get; set; }
    
        public virtual ICollection<tblAssignmentHeader> tblAssignmentHeaders { get; set; }
        public virtual tblTeacherCategory tblTeacherCategory { get; set; }
        public virtual tblUser tblUser { get; set; }
        public virtual tblUser tblUser1 { get; set; }
        public virtual ICollection<tblTeacherExtraCurricularActivity> tblTeacherExtraCurricularActivities { get; set; }
        public virtual ICollection<tblTeacherQualification> tblTeacherQualifications { get; set; }
        public virtual ICollection<tblTeacherSchool> tblTeacherSchools { get; set; }
        public virtual ICollection<tblTeacherSubject> tblTeacherSubjects { get; set; }
    }
}