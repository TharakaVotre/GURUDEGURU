using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GDWEBSolution.Models.Menu
{
    public class MenuItem
    {
        public MenuItem()
        {
            this.ChildMenuItems = new List<MenuItem>();
        }

        public string Name { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string Link { get; set; }
        public Nullable<int> ParentItemId { get; set; }
        public virtual ICollection<MenuItem> ChildMenuItems { get; set; }
    }

}