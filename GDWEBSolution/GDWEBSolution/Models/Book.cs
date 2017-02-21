using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GDWEBSolution.Models
{
    public class Book
    {
        [Required(ErrorMessage ="Please Insert the Name")]
        public string Name { get; set; }
    }
}