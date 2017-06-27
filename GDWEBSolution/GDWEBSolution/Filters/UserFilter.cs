using GDWEBSolution.Models;
using GDWEBSolution.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
/*
 * Created Date : 2017/6/5
 * Author : Tharaka Madusanka
 */
namespace GDWEBSolution.Filters
{
    public class UserFilter : ActionFilterAttribute
    {
        public string Function_Id { get; set; }

        SchoolMGTEntitiesConnectionString Connection = new SchoolMGTEntitiesConnectionString();
        
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            UserSession _session = new UserSession();

            if (_session.User_Id != "" || _session.User_Category != "")
            {
                var count = Connection.tblUserCategoryFunctions.Count(
                                        u => u.CategoryId == _session.User_Category 
                                        && u.FunctionId == Function_Id);
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
                    {"action", "Login"}
                });
            }
            base.OnActionExecuting(filterContext);
        }
    }
}