using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GDWEBSolution.Models.SchoolCalender
{
    public class SchoolCalenderModel
    {
        [Display(Name = "CalenderSeqNo")]
        public long CalenderSeqNo { get; set; }

        [Display(Name = "School Id")]
        public string SchoolId { get; set; }

        [Display(Name = "Acadamic Year")]
        public string AcadamicYear { get; set; }

        [Display(Name = "Acadamic Date")]
        public DateTime AcadamicDate { get; set; }

         [Display(Name = "IsHoliday")]
        public string IsHoliday { get; set; }

         [Display(Name = "Remark")]
        public string SpecialComment { get; set; }


         [Display(Name = "Event")]
         public string DateComment { get; set; }

        [Display(Name = "FromDate")]
        public DateTime FromDate { get; set; }

        [Display(Name = "ToDate")]
        public DateTime ToDate { get; set; }

        public string StartMonth { get; set; }
        public string EndMonth { get; set; }

  
         [Display(Name = "Name")]
        public string CreatedBy { get; set; }

         [Display(Name = "Name")]
        public string CreatedDate { get; set; }

         [Display(Name = "Name")]
        public string ModifiedBy { get; set; }

         [Display(Name = "Name")]
        public string ModifiedDate { get; set; }

         [Display(Name = "Name")]
        public string IsActive { get; set; }


    }
}