using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GDWEBSolution.Models.Report
{
    public class AttentionRequiredSubjectsSchoolModel
    {
        public IEnumerable<SubjectWiseReportModel> Subject { get; set; }
        public IEnumerable<GradeWiseReportModel> School { get; set; }


    }
}