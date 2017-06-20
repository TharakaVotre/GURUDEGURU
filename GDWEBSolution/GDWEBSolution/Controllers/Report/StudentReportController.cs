﻿using GDWEBSolution.Models;
using GDWEBSolution.Models.Maintenance;
using GDWEBSolution.Models.Report;
using GDWEBSolution.Models.Student;
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
        string SchoolId = "Scl13140";
        string UserId = "PARENT1";
       

        
        //
        // GET: /StudentReport/

      
         
        public ActionResult Index(string AccYear, string Evealuation)
        {
            try
            {
                if (AccYear!=null)
                {
                    Session["AccYear"] = AccYear;
                    if (Evealuation != null && Evealuation != "")
                    {
                        Session["Evealuation"] = Evealuation;
                    }
                    else
                    {
                        Session["Evealuation"] = "0";
                    }

                }
                Dropdowns();

                if (Session["AccYear"] != null)
                {

                    long EvealuationType = Convert.ToInt64(Session["Evealuation"]);

                    var Group = Connection.GDgetAttentionRequiredSubjectsSchool(SchoolId, Session["AccYear"].ToString(), EvealuationType, "");
                     
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
                      var Group1 = Connection.GDgetAttentionRequiredSubjectsGradeWiseSchool(SchoolId, AccYear, EvealuationType, "");

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
            var Grade = Connection.GDgetSchoolGrade(SchoolId,"Y");
            List<GDgetSchoolGrade_Result> Gradelist = Grade.ToList();

            ViewBag.GradeId = new SelectList(Gradelist, "GradeId", "GradeName");
            ViewBag.stGradeId = new SelectList(Gradelist, "GradeId", "GradeName");

            var Eveluation = Connection.GDgetClassEveluation(SchoolId, "%", "%", "Y");
            List<GDgetClassEveluation_Result> Eveluationlist = Eveluation.ToList();

            ViewBag.Eveluation = new SelectList(Eveluationlist, "EvaluationDetailSeqNo", "EvaluationDescription");
        }

        public JsonResult getClass(string id)
        {
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


        public JsonResult getStudent(string id, string cId)
        {
            var states = Connection.SMGTgetStudentforoptionalSubject(SchoolId,id,"%",cId);
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

             public JsonResult getSubject(string Gid)
        {
            var states = Connection.GDgetGradeSubject(SchoolId,Gid,"Y");
            List<SelectListItem> listates = new List<SelectListItem>();
            listates.Add(new SelectListItem { Text = "", Value = "" });
            if (states != null)
            {
                foreach (var x in states)
                {
                    listates.Add(new SelectListItem { Text = x.SubjectName, Value = x.SubjectId.ToString() });
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


        private void Dropdown2()
        {
            var Grade = Connection.GDgetSchoolGrade(SchoolId, "Y");
            List<GDgetSchoolGrade_Result> Gradelist = Grade.ToList();

            ViewBag.GradeId = new SelectList(Gradelist, "GradeId", "GradeName");
            ViewBag.ClassGradeId = new SelectList(Gradelist, "GradeId", "GradeName");
            ViewBag.SubGradeId = new SelectList(Gradelist, "GradeId", "GradeName");

           
            var ExtraCurriculerActivity = Connection.GDgetSchoolExtraCurriculerActivity(SchoolId, "Y");
            List<GDgetSchoolExtraCurriculerActivity_Result> ExtraCurriculerActivitylist = ExtraCurriculerActivity.ToList();

            ViewBag.ExtraCurriculerActivity = new SelectList(ExtraCurriculerActivitylist, "ActivityCode", "ActivityName");
        }
        public ActionResult StudentSubjectIndex()
        {
            Dropdown2();
            return View();
        }
        public ActionResult StudentSubject(string AccYear,string GradeId, string StudentId)
        {
            var Subject = Connection.GDgetStudentSubject(SchoolId, AccYear, GradeId, StudentId, "Y");
            List<GDgetStudentSubject_Result> Subjectlist = Subject.ToList();

            SubjectModel tcm = new SubjectModel();

            List<SubjectModel> tcmlist = Subjectlist.Select(x => new SubjectModel
            {
                SubjectId = x.SubjectId,
                ShortName = x.ShortName,
                SubjectName = x.SubjectName,
                Optional=x.Optional

            }).ToList();



            return PartialView("StudentsSubjects", tcmlist);
        }

        public ActionResult StudentInClass( string ClassId,string GradeId)
        {
            if (ClassId == "")
            {
                ClassId = "%";
            }
            var Student = Connection.GDgetStudentInClass(SchoolId, GradeId, ClassId, "Y");
            List<GDgetStudentInClass_Result> Studentlist = Student.ToList();

            StudentModel tcm = new StudentModel();

            List<StudentModel> tcmlist = Studentlist.Select(x => new StudentModel
            {
                StudentId = x.StudentId,
                StudentName = x.studentName,
                Gender = x.Gender ,
                DateOfBirth = x.DateofBirth

            }).ToList();
            return PartialView("StudentInClasss", tcmlist);
        }

        public ActionResult StudentInExtraActivity(string ActivityId)
        {
           
            var Student = Connection.GDgetExtraCurriculerStudent(SchoolId, ActivityId, "Y");
            List<GDgetExtraCurriculerStudent_Result> Studentlist = Student.ToList();

            StudentModel tcm = new StudentModel();

            List<StudentModel> tcmlist = Studentlist.Select(x => new StudentModel
            {
                StudentId = x.StudentId,
                StudentName = x.studentName,
                Gender = x.Gender,
                DateOfBirth = x.DateofBirth

            }).ToList();
            return PartialView("StudentInExtraCurriculerActivity", tcmlist);
        }

        public ActionResult StudentInSubject(string GradeId, string ClassId, string SubjectId)
        {
            int subId = Convert.ToInt32(SubjectId);
            var Student = Connection.GDgetSubjectStudent(SchoolId, GradeId, ClassId, subId,0);
            List<GDgetSubjectStudent_Result> Studentlist = Student.ToList();

            StudentModel tcm = new StudentModel();

            List<StudentModel> tcmlist = Studentlist.Select(x => new StudentModel
            {
                StudentId = x.StudentId,
                StudentName = x.studentName,
                Gender = x.Gender,
                DateOfBirth = x.DateofBirth

            }).ToList();
            return PartialView("StudentInSubjects", tcmlist);
        }
    }
}
