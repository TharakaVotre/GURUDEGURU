using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GDWEBSolution
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Other",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Login", id = UrlParameter.Optional }
            );
            routes.MapRoute("Studentextracuricular",
                         "student/getschoolextracuricluar/",
                         new { controller = "Student", action = "GetSchoolextracuricluar" },
                         new[] { "GDWEBSolution.Controllers.Student" });

            routes.MapRoute("Houseincharge",
                         "school/gethouseincharge/",
                         new { controller = "School", action = "GetHouseIncharge" },
                         new[] { "GDWEBSolution.Controllers.School" });
            routes.MapRoute("SchoolHouse",
                     "student/housedropdown/",
                     new { controller = "Student", action = "HouseDropdown" },
                     new[] { "GDWEBSolution.Controllers.Student" });



            routes.MapRoute("evaluationdropd",
                     "evaluationbyschool/getevaluationdropdown/",
                     new { controller = "EvaluationbySchool", action = "GetEvaluationDropdown" },
                     new[] { "GDWEBSolution.Controllers.EvaluationbySchool" });

           


            routes.MapRoute("Studentgrade",
                        "StudentOptionalSubject/GetSchoolGrade/",
                        new { controller = "StudentOptionalSubject", action = "GetSchoolGrade" },
                        new[] { "GDWEBSolution.Controllers.StudentOptionalSubject" });

            routes.MapRoute("Studentclass",
                       "StudentOptionalSubject/GetSchoolgrdclass/",
                       new { controller = "StudentOptionalSubject", action = "GetSchoolgrdclass" },
                       new[] { "GDWEBSolution.Controllers.StudentOptionalSubject" });


            routes.MapRoute("StudentoptionalSubject",
                       "StudentOptionalSubject/GetOptionalSubjects/",
                       new { controller = "StudentOptionalSubject", action = "GetOptionalSubjects" },
                       new[] { "GDWEBSolution.Controllers.StudentOptionalSubject" });

            routes.MapRoute("StudentoptionalSubjectStud",
                      "StudentOptionalSubject/GetOptionalSubjectsStud/",
                      new { controller = "StudentOptionalSubject", action = "GetOptionalSubjectsStud" },
                      new[] { "GDWEBSolution.Controllers.StudentOptionalSubject" });

            routes.MapRoute("Studentgradeclass",
                     "EvaluationbySchool/GetSchoolgrdclass/",
                     new { controller = "EvaluationbySchool", action = "GetSchoolgrdclass" },
                     new[] { "GDWEBSolution.Controllers.EvaluationbySchool" });


        }
    }
}