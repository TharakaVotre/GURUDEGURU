using GDWEBSolution.Models.Schools;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace GDWEBSolution.Models.Parent
{
    public class ParentModel
    
    {
        [Display(Name = "Student Name")]
        public string StudentName { get; set; }

        [Display(Name = "Student Name")]
        public string StudentId { get; set; }

        [Display(Name = "User Name")]
     
        public string UserId { get; set; }


        [Display(Name = "Class Name")]
        public string ClassId { get; set; }

        [Display(Name = "Grade Name")]
        public string GradeId { get; set; }

        [Display(Name = "School Name")]

        public string SchoolId { get; set; }


        [Display(Name = "School Name")]

        public string SchoolName { get; set; }

        
        [Display(Name = "Parent Name")]
    
        public string ParentName { get; set; }

                [Display(Name = "Parent Id")]

        public string ParentId { get; set; }


        [Display(Name = "Relationship Name")]
      
        public int RelationshipId { get; set; }

        [Display(Name = "Relationship Name")]

        public int RelationshipName { get; set; }


        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address is required")]
        public string Address1 { get; set; }

       
        
       
        
        [Display(Name = "Address3")]
        public string Address3 { get; set; }

        [Display(Name = " Address2")]
        [Required(ErrorMessage = "Address is required")]
        public string Address2 { get; set; }



        [Display(Name = "Email")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]

        public string PersonalEmail { get; set; }


        [Display(Name = "Home Contact No")]

       
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string HomeTelephone { get; set; }


        

            [Display(Name = "Mobile Contact No")]

        
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string PersonalMobile { get; set; }


            [Display(Name = "Occupation")]
            public string Occupation { get; set; }


            [Display(Name = "Office Address")]

            public string OfficeAddress1 { get; set; }





            [Display(Name = "Office Address")]
            public string OfficeAddress3 { get; set; }

            [Display(Name = "Office Address")]
          
            public string OfficeAddress2 { get; set; }


            [Display(Name = "Office Contact No")]


            [DataType(DataType.PhoneNumber)]
            [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
            public string OfficePhone { get; set; }


            [Display(Name = "Email")]
            [Required(ErrorMessage = "The email address is required")]
            [EmailAddress(ErrorMessage = "Invalid Email Address")]


            public string officeEmail { get; set; }



            [Display(Name = "Identy Card No")]
            public string NIC { get; set; }
        [Display(Name = "Password")]
            [DataType(DataType.Password)]
            [StringLength(255, MinimumLength = 8)]
           
            [MembershipPassword()]
            public string Password { get; set; }
        [Display(Name = " Confirm Password")]
            [Compare("Password")]
            [DataType(DataType.Password)]
            [StringLength(255, MinimumLength = 8)]
        
            [MembershipPassword()]
            public string ConfirmPassword { get; set; }

               

             [Display(Name = "Image Path")]
             public string ImagePath { get; set; }

             [Display(Name = "Image")]
             [ValidateFile]
             public HttpPostedFileBase File { get; set; }

        [Display(Name = "Date of Birth")]
        public Nullable<System.DateTime> DateofBirth { get; set; }


        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        [Display(Name = "Active")]
        public string IsActive { get; set; }




    }
}