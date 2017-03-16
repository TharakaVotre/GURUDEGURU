using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GDWEBSolution.Models.Maintenance
{
    public class MessageTypeModel
    {
        [Display(Name = "Type Id")]
        public long MessageTypeId { get; set; }

        [Required(ErrorMessage = "Please Enter Message Type Description ")]
        [Display(Name = "Description")]
        public string MessageTypeDescription { get; set; }

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
    }
}