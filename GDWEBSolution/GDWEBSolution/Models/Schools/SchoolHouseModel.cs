using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GDWEBSolution.Models.Schools
{
    public class SchoolHouseModel
    {


        [Display(Name = "School Id")]
        public string SchoolId { get; set; }

        [Display(Name = "House Id")]
        public string HouseId { get; set; }

        [Display(Name = "House Incharge Name")]
        public long HouseInchargeId { get; set; }

        [Display(Name = "House Incharge Name")]
        public string HouseInchargeName { get; set; }

        [Display(Name = "House Name")]
        public string HouseName { get; set; }

        [Display(Name = "School Name")]
        public string SchoolName { get; set; }


        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        [Display(Name = "Active")]
        public string IsActive { get; set; }



    }
}