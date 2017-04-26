using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GDWEBSolution.Models.Event
{
    public class Events
    {
      public string id { get; set; }
      public string name { get; set; }
      public string startDate { get; set; }
      public string endDate { get; set; }
      public string fromtime { get; set; }

      public string syear { get; set; }
      public string smonth { get; set; }
      public string sday { get; set; }

      public string eyear { get; set; }
      public string emonth { get; set; }
      public string eday { get; set; }
     

      public string totime { get; set; }
      public string organizer { get; set; }
      public string category { get; set; }
    }
}