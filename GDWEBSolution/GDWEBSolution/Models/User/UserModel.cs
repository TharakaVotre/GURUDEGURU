using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GDWEBSolution.Models.User
{
    public class UserModel
    {
        public string UserId { get; set; }
        public string LoginEmail { get; set; }
        public string Password { get; set; }
        public string SchoolId { get; set; }
        public string UserCategory { get; set; }
        public string PersonName { get; set; }
        public string JobDescription { get; set; }
        public string Mobile { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string IsActive { get; set; }
    }
}