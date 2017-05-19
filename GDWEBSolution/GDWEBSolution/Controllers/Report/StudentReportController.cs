using GDWEBSolution.Models;
using GDWEBSolution.Models.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GDWEBSolution.Controllers.Report
{
    public class StudentReportController : Controller
    {
        SchoolMGTEntitiesConnectionString Connection = new SchoolMGTEntitiesConnectionString();
        string SchooId = "CKC";
        string UserId = "PARENT1";
       

        
        //
        // GET: /StudentReport/

      
         
        public ActionResult Index(string AccYear, string EvealuationTypes)
        {
            try
            {
                if (AccYear!=null)
                {
                    Session["AccYear"] = AccYear;
                    if (EvealuationTypes != null && EvealuationTypes != "")
                    {
                        Session["EvealuationTypes"] = EvealuationTypes;
                    }
                    else
                    {
                        Session["EvealuationTypes"] = "0";
                    }

                }
                Dropdowns();

                if (Session["AccYear"] != null)
                {

                    long EvealuationType = Convert.ToInt64(Session["EvealuationTypes"]);

                    var Group = Connection.GDgetAttentionRequiredSubjectsSchool(SchooId, Session["AccYear"].ToString(), EvealuationType, "");
                     
                     List<GDgetAttentionRequiredSubjectsSchool_Result> Grouplist = Group.ToList();
                    
                     SubjectWiseReportModel tcm = new SubjectWiseReportModel();

                     List<SubjectWiseReportModel> tcmlist = Grouplist.Select(x => new SubjectWiseReportModel
                     {
                         Maximum = x.Maximum.ToString(),
                         Minimun = x.Minimun.ToString(),
                         Avarage = x.Avarage.ToString(),
                         Subject = x.SubjectName
                     }).ToList();
                     ViewBag.Subject = tcmlist;
                      var Group1 = Connection.GDgetAttentionRequiredSubjectsGradeWiseSchool(SchooId, AccYear, EvealuationType, "");

                     List<GDgetAttentionRequiredSubjectsGradeWiseSchool_Result> Grouplist1 = Group1.ToList();

                     GradeWiseReportModel tcm1 = new GradeWiseReportModel();
                     List<GradeWiseReportModel> tcmlist1 = Grouplist1.Select(x => new GradeWiseReportModel
                     {
                         Maximum = x.Maximum.ToString(),
                         Minimun = x.Minimun.ToString(),
                         Avarage = x.Avarage.ToString(),
                         Subject = x.SubjectName,
                         Grade=x.GradeName
                     }).ToList();
         ViewData["Classs"] = tcmlist1;
              
                return View();
                }
                return View();
            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);

                return View();
            }
        }

        private void Dropdowns()
        {
            var Grade = Connection.GDgetAllGradeMaintenance("Y");
            List<GDgetAllGradeMaintenance_Result> Gradelist = Grade.ToList();

            ViewBag.GradeId = new SelectList(Gradelist, "GradeId", "GradeName");
            ViewBag.stGradeId = new SelectList(Gradelist, "GradeId", "GradeName");

            var EveluationType = Connection.GDgetAllEvaluationType("Y");
            List<GDgetAllEvaluationType_Result> EveluationTypelist = EveluationType.ToList();

            ViewBag.EveluationType = new SelectList(EveluationTypelist, "EvaluationTypeCode", "EvaluationTypeDesc");
        }

        public JsonResult getClass(string id)
        {
            var states = Connection.GDgetGradeActiveClass(id, SchooId, "Y");
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


        public JsonResult getStudent(string id, string cId)
        {
            var states = Connection.SMGTgetStudentforoptionalSubject(SchooId,id,"%",cId);
            List<SelectListItem> listates = new List<SelectListItem>();
            listates.Add(new SelectListItem { Text = "", Value = "" });
            if (states != null)
            {
                foreach (var x in states)
                {
                    listates.Add(new SelectListItem { Text = x.StudentName, Value = x.StudentId });
                }
            }
            return Json(new SelectList(listates, "Value", "Text", JsonRequestBehavior.AllowGet));
        }

        public ActionResult StudantReport(string studentId)
        {
            try
            {
                long EvealuationType= Convert.ToInt64(Session["EvealuationTypes"]);
                var student = Connection.GdgetStudentAttentionRequiredSubjects(Session["AccYear"].ToString(), studentId, EvealuationType);
                List<GdgetStudentAttentionRequiredSubjects_Result> studentlist = student.ToList();

                StudentReportModel tcm = new StudentReportModel();

                List<StudentReportModel> tcmlist = studentlist.Select(x => new StudentReportModel
                    {
                       Mark=x.Mark,
                       SubjectName=x.SubjectName
                    }).ToList();
                return PartialView("StudantReports", tcmlist);
            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return View();
            }
        }

        public ActionResult ClassReport(string ClassId,string GradeId)
        {
            try
            {
                string AccYear = Session["AccYear"].ToString();
                long EvealuationType = Convert.ToInt64(Session["EvealuationTypes"]);
                var MarkInClass = Connection.GDgetClassAttentionRequiredSubjects(AccYear, GradeId, ClassId, EvealuationType);
                List<GDgetClassAttentionRequiredSubjects_Result> MarkInClasslist = MarkInClass.ToList();

                ClassReportModel tcm = new ClassReportModel();

                List<ClassReportModel> tcmlist = MarkInClasslist.Select(x => new ClassReportModel
                {
                    Average = x.Average.ToString(),
                    Maximum = x.Maximum.ToString(),
                    Minimum = x.Minimum.ToString(),
                    SubjectName=x.SubjectName,
                    
                }).ToList();
                return PartialView("ClassReports", tcmlist);
            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return View();
            }
        }



        public ActionResult ParentReport(string AccYear, string EveluationType)
        {
            try
            {
                Dropdowns();
                if (EveluationType == "") {
                    EveluationType = "0";
                }
                long EveluationTypes = Convert.ToInt64(EveluationType);
                var StudentMark = Connection.GDgetParentAttentionRequiredSubject(AccYear, EveluationTypes, UserId);
                List<GDgetParentAttentionRequiredSubject_Result> StudentMarklist = StudentMark.ToList();

                ClassReportModel tcm = new ClassReportModel();

                List<StudentReportModel> tcmlist = StudentMarklist.Select(x => new StudentReportModel
                {
                    Mark = x.Mark,
                    SubjectName = x.SubjectName,

                }).ToList();
                return PartialView("AttentionRequiredSubjectsParents", tcmlist);
            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return PartialView("AttentionRequiredSubjectsParents");
            }
        }
    }
}
