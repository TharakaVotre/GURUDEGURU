using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GDWEBSolution.Models.Message
{
    public class PtoSMessageHeaderModel
    {
        [Display(Name = "Seq No")]
        public long SeqNo { get; set; }
        [Display(Name = "School Id")]
        public string SchoolId { get; set; }
        [Display(Name = "Message Id")]
        public long MessageId { get; set; }
        [Display(Name = "Message")]
        public string Message { get; set; }

        [Display(Name = "Message Type Id")]
        public long MessageType { get; set; }

        [Display(Name = "Message Type")]
        public string MessageTypeDes { get; set; }

        [Display(Name = "Recepient User")]
        public string RecepientUser { get; set; }
        [Display(Name = "Status")]
        public string Status { get; set; }
        [Display(Name = "Authorization Date")]
        public System.DateTime AuthorizationDate { get; set; }
        [Display(Name = "Authorized By")]
        public string AuthorizedBy { get; set; }
        [Display(Name = "Authorization Status")]
        public string AuthStatus { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        [Display(Name = "Created Date")]
        public System.DateTime CreatedDate { get; set; }
        [Display(Name = "Modified By")]
        public string ModifiedBy { get; set; }
        [Display(Name = "Modified Date")]
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        [Display(Name = "Is Active")]
        public string IsActive { get; set;}
        public string TeacherName {get;set;}
        public string Subject { get; set; }
        
        }

  
}