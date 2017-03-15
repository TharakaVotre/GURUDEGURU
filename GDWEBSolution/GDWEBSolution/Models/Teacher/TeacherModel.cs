using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GDWEBSolution.Models.Teacher
{
    public class TeacherModel
    {
        [Display(Name = "Teacher Id")]
        public long TeacherId { get; set; }

        [Required(ErrorMessage = "Name Required")]
        [RegularExpression(@"^[0-9a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Special Characters Are Not  Allowed.")]
        //[StringLength(100, ErrorMessage = "Name Cannot Be Longer Than 100 Characters.")]
        [Display(Name = "Teacher Name *")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address 1 Required")]
        [Display(Name = "Address *")]
        //[StringLength(150, ErrorMessage = "Address 1 Cannot Be Longer Than 150 Characters.")]
      //  [RegularExpression(@"^[0-9a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Special Characters Are Not  Allowed.")]
        public string Address1 { get; set; }

        [Display(Name = "Address Line 2")]
        // [StringLength(150, ErrorMessage = "Address 2 Cannot Be Longer Than 150 Characters.")]
      //  [RegularExpression(@"^[0-9a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Special Characters Are Not  Allowed.")]
        public string Address2 { get; set; }

        [Display(Name = "Address Line 3")]
        //  [StringLength(150, ErrorMessage = "Address 3 Cannot Be Longer Than 150 Characters.")]
     //   [RegularExpression(@"^[0-9a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Special Characters Are Not  Allowed.")]
        public string Address3 { get; set; }

        [Required(ErrorMessage = "Teacher Category Required")]
        [Display(Name = "Teacher Category Id *")]
        public int TeacherCategoryId { get; set; }

        [Display(Name = "Teacher Category Name *")]
        public string TeacherCategoryName { get; set; }

        [Required(ErrorMessage = "Required")]
      //  [RegularExpression(@"^[0-9a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Special Characters Are Not  Allowed.")]
        [Display(Name = "Telephone *")]
        //  [StringLength(20, ErrorMessage = "Telephone Cannot Be Longer Than 20 Characters.")]
        public string Telephone { get; set; }

        [Required(ErrorMessage = "Gender Required")]
        [Display(Name = "Gender *")]
        public string Gender { get; set; }

        [Display(Name = "Description")]
        //  [StringLength(500, ErrorMessage = "Description Cannot Be Longer Than 500 Characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Employee No Required")]
        [Display(Name = "Employee No *")]
        //  [StringLength(20, ErrorMessage = "Employee No Cannot Be Longer Than 20 Characters.")]
        public string EmployeeNo { get; set; }

        [Required(ErrorMessage = "NIC Required")]
        //  [StringLength(20, ErrorMessage = "NIC Cannot Be Longer Than 20 Characters.")]
        [Display(Name = "NIC *")]
        public string NIC { get; set; }

        //[Required(ErrorMessage = "Drivers License Requried")]
        [Display(Name = "Drivers License ")]
        //  [StringLength(20, ErrorMessage = "Drivers License Cannot Be Longer Than 20 Characters.")]
        public string DrivingLicense { get; set; }

        //[Required(ErrorMessage = "Passport Requried")]
        [Display(Name = "Passport ")]
        [StringLength(20, ErrorMessage = "Passport Cannot Be Longer Than 20 Characters.")]
        public string Passport { get; set; }

        [Required(ErrorMessage = "User Name Required")]
        //  [StringLength(20, ErrorMessage = "User Name Cannot Be Longer Than 20 Characters.")]
        //  [Remote("IsUserNameExits", "Teacher", HttpMethod = "POST", ErrorMessage = "User name already exists. Please enter a different user name.")]
        [Display(Name = "User Name *")]
        [RegularExpression(@"^[0-9a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Special Characters Are Not  Allowed.")]
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