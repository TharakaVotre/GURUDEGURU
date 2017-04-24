using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GDWEBSolution.Models.Application
{
    public class ApplicationModel
    {
        public long AppStatusSeqNo{get;set;}
        public string RefNo{get;set;} 
        public long StatusCode{get;set;} 
        public string Remarks {get;set;}
        public DateTime CreatedDate { get; set; } 
        public string StatusDescription {get;set;} 
    }
}