using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GDWEBSolution.Models.Report
{
    public class StudentReportModel
    {
        public decimal Mark { get; set; }
        public string SubjectName{get;set;}
        public int SubjectId { get; set; }
        public string StudentId { get; set; }
        public long Seq { get; set; }
        public string StudentName { get; set; }
    }
}