﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GDWEBSolution.Models.User
{
    public class UserCategoryModel
    {
        [Display(Name = "Category ID")]
        public string CategoryId { get; set; }

        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        [Display(Name = "Created Date")]
        public System.DateTime CreatedDate { get; set; }
        [Display(Name = "Modified By")]
        public string ModifiedBy { get; set; }
        [Display(Name = "Modified Date")]
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        [Display(Name = "Is Active")]
        public string IsActive { get; set; }
        public bool Active { get; set; }

        [Display(Name = "Is Application Side")]
        public string Is_ApplicationSide { get; set; }
        public bool IsApplicationSide { get; set; }
    }
}