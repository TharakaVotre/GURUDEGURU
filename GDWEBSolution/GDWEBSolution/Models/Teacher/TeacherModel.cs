using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GDWEBSolution.Models.Teacher;

namespace GDWEBSolution.Models
{
    public class TeacherModel
    {
        [Display(Name = "Teacher Id")]
        public long TeacherId { get; set; }

        [Required (ErrorMessage = "Name Required")]
        [Display(Name = "Teacher Name *")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address 1 Required")]
        [Display(Name = "Address *")]
        public string Address1 { get; set; }

        [Display(Name = "Address Line 2")]
        public string Address2 { get; set; }

        [Display(Name = "Address Line 3")]
        public string Address3 { get; set; }

        [Required(ErrorMessage = "Teacher Category Required")]
        [Display(Name = "Teacher Category Id *")]
        public int TeacherCategoryId { get; set; }

        [Display(Name = "Teacher Category Name *")]
        public string TeacherCategoryName { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Telephone *")]
        public string Telephone { get; set; }

        [Required(ErrorMessage = "Gender Required")]
        [Display(Name = "Gender *")]
        public string Gender { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Employee No Required")]
        [Display(Name = "Employee No *")]
        public string EmployeeNo { get; set; }

        [Required(ErrorMessage = "NIC Required")]
        [Display(Name = "NIC *")]
        public string NIC { get; set; }

        //[Required(ErrorMessage = "Drivers License Requried")]
        [Display(Name = "Drivers License ")]
        public string DrivingLicense { get; set; }

        //[Required(ErrorMessage = "Passport Requried")]
        [Display(Name = "Passport ")]
        public string Passport { get; set; }

        [Required(ErrorMessage = "User Name Required")]
      //  [Remote("IsUserNameExits", "Teacher", HttpMethod = "POST", ErrorMessage = "User name already exists. Please enter a different user name.")]
        [Display(Name = "User Name *")]
        public string UserId { get; set; }

        [Display(Name = "Profile Picture")]
        public string ImgUrl { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Date of Birth *")]
        public Nullable<System.DateTime> DateOfBirth { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Display(Name = "Created Date")]
        public System.DateTime CreatedDate { get; set; }

        [Display(Name = "Modified By")]
        public string ModifiedBy { get; set; }

        [Display(Name = "Modified Date")]
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        [Display(Name = "Active")]
        public string IsActive { get; set; }

        [Display(Name = "School Id")]
        public string SchoolID { get; set; }

        [Display(Name = "School Name")]
        public string SchoolName { get; set; }

        public List<QualificationModel> QualificationList { get; set; }
        public List<tblTeacherExtraCurricularActivity> ExCurricularList { get; set; }
        public List<tblTeacherSubject> SubjectList { get; set; }





    }
}