using GDWEBSolution.Models;
using GDWEBSolution.Models.Report;
using GDWEBSolution.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GDWEBSolution.Controllers.ExtraCurriculerActivity
{
    public class ExtraCurriculerActivityController : Controller
    {
        SchoolMGTEntitiesConnectionString Connection = new SchoolMGTEntitiesConnectionString();
        UserSession USession = new UserSession();
        //
        // GET: /ExtraCurriculerActivity/
        string SchoolId = null; 
        public ActionResult Index()
        {
            try
            {

                SchoolId = USession.School_Id;
                var Activity = Connection.GDgetSchoolExtraCurriculerActivity(SchoolId, "Y");
                List<GDgetSchoolExtraCurriculerActivity_Result> Activitylist = Activity.ToList();
                ViewBag.ExtraCurriculerActivity = new SelectList(Activitylist, "ActivityCode", "ActivityName");
               
                var Grade = Connection.GDgetSchoolGrade(SchoolId, "Y");
                List<GDgetSchoolGrade_Result> Gradelist = Grade.ToList();
                ViewBag.GradeId = new SelectList(Gradelist, "GradeId", "GradeName");
                
               
                return View();
            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return View(); 
            }
        }

        public JsonResult getClass(string id)
        {
            SchoolId = USession.School_Id;
            var states = Connection.GDgetGradeActiveClass(id, SchoolId, "Y");
            List<SelectListItem> listates = new List<SelectListItem>();
            listates.Add(new SelectListItem { Text = "", Value = "" });
            if (states != null)
            {
                foreach (var x in states)
                {
                    listates.Add(new SelectListItem { Text = x.ClassName, Value = x.ClassId });
                }
            }
            return Json(new SelectList(listates, "Value", "Text", JsonRequestBehavior.AllowGet));
        }

        public ActionResult StudantReport(string Activity, string GradeId, string ClassId)
        {
            
            try
            {
                if (GradeId == "All")
                {
                    GradeId = "%";
                }
                if (ClassId == "All")
                {
                    ClassId = "%";
                }
                SchoolId = USession.School_Id;
                var student = Connection.GDgetExtraCurriculerAllStudentReport(SchoolId,Activity,GradeId,ClassId,"Y");
                List<GDgetExtraCurriculerAllStudentReport_Result> studentlist = student.ToList();

                StudentReportModel tcm = new StudentReportModel();

                List<StudentReportModel> tcmlist = studentlist.Select(x => new StudentReportModel
                {
                    StudentName = x.studentName,
                    GradeName = x.GradeName,
                    ClassName = x.ClassName

                }).ToList();
                return PartialView("StudantReports", tcmlist);
            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return View();
            }
        }
    }
}
