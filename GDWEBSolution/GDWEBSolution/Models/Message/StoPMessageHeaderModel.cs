using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GDWEBSolution.Models.Message
{
    public class StoPMessageHeaderModel
    {
        public string SchoolId { get; set; }
        public long MessageId { get; set; }
        public string Subject { get; set; }
        public long MessageType { get; set; }
        public string Message { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string IsActive { get; set; }
    }
}