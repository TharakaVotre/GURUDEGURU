using GDWEBSolution.Models;
using GDWEBSolution.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GDWEBSolution.Filters
{
    public class UserFilter : ActionFilterAttribute
    {
        public string Function_Id { get; set; }

        SchoolMGTEntitiesConnectionString Connection = new SchoolMGTEntitiesConnectionString();
        UserSession _session = new UserSession();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (_session.User_Id != "" || _session.User_Category != "")
            {
                var count = Connection.tblUserCategoryFunctions.Count(u => u.CategoryId == _session.User_Category && u.FunctionId == Function_Id);
                if (count == 0)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    {"controller", "Home"},
                    {"action", "Error"}
                });

                }
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    {"controller", "Home"},
                    {"action", "Logout"}
                });

            }
            base.OnActionExecuting(filterContext);
        }
    }
}