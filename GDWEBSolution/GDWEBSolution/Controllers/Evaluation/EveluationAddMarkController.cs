using GDWEBSolution.Models;
using GDWEBSolution.Models.Report;
using GDWEBSolution.Models.Student;
using GDWEBSolution.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GDWEBSolution.Controllers.Evaluation
{
    public class EveluationAddMarkController : Controller
    {
        //
        SchoolMGTEntitiesConnectionString Connection = new SchoolMGTEntitiesConnectionString();
        UserSession USession = new UserSession();
        string SchoolId = null;
        string UserId = null;
        // GET: /EveluationAddMark/

        private void Authentication(string ControlerName)
        {

            if (USession.User_Id != "")
            {
                string CategoryId = USession.User_Category;
                tblUserCategoryFunction AccessControl = Connection.tblUserCategoryFunctions.SingleOrDefault(a => a.FunctionId == ControlerName && a.CategoryId == CategoryId && a.IsActive == "Y");

                if (AccessControl == null)
                {
                    //RedirectToAction("~/Prohibited");
                    Response.Redirect("~/Prohibited");
                }
                
            }
            else
            {
                // RedirectToAction();
                Response.Redirect("~/Home/Login");
            }
        }
        public ActionResult Index()
        {
            Authentication("EvAdM");
            try
            {

                List<GDgetSchoolGrade_Result> Gradelist = GetGradeDropdown();
                ViewBag.GradeId = new SelectList(Gradelist, "GradeId", "GradeName");
                ViewBag.ResultGradeId = new SelectList(Gradelist, "GradeId", "GradeName");
                ViewBag.EditGradeId = new SelectList(Gradelist, "GradeId", "GradeName");
                return View();
            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return View();
            }
        }


        public JsonResult getSubject(string Gid, string cId)
        {
            
            try
            {
                SchoolId = USession.School_Id;
                var states = Connection.GDgetGradeSubject(SchoolId, Gid, "Y");
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
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return Json(new SelectList("", "Value", "Text", JsonRequestBehavior.AllowGet));
            }
        }
        public JsonResult getClassEveluation(string Gid, string cId)
        {
            
            try
            {
                SchoolId = USession.School_Id;
                var states = Connection.GDgetClassEveluation(SchoolId, cId, Gid, "Y");
                List<SelectListItem> listates = new List<SelectListItem>();
                listates.Add(new SelectListItem { Text = "", Value = "" });
                if (states != null)
                {
                    foreach (var x in states)
                    {
                        listates.Add(new SelectListItem { Text = x.EvaluationDescription, Value = x.EvaluationDetailSeqNo.ToString() });
                    }
                }
                return Json(new SelectList(listates, "Value", "Text", JsonRequestBehavior.AllowGet));
            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return Json(new SelectList("", "Value", "Text", JsonRequestBehavior.AllowGet));
            }
        }
        public JsonResult getClass(string id)
        {
            try {
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
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return Json(new SelectList("", "Value", "Text", JsonRequestBehavior.AllowGet));
            }
        }

    

        private List<GDgetSchoolGrade_Result> GetGradeDropdown()
        {
            SchoolId = USession.School_Id;
            var Grade = Connection.GDgetSchoolGrade(SchoolId, "Y");
            List<GDgetSchoolGrade_Result> Gradelist = Grade.ToList();
            return Gradelist;
           
        }

        public ActionResult AddStudentSubjectMark(string Eveluation,string GradeId,string ClassId,string SubjectId)
        {
            Authentication("AdSuM");
            try
            {
                int subId = Convert.ToInt32(SubjectId);
                tblGradeSubject subjectStatus = Connection.tblGradeSubjects.SingleOrDefault(x => x.SubjectId == subId && x.GradeId == GradeId);
                ViewBag.SubjectStatus = subjectStatus.Optional;
                ViewBag.SubjectId = SubjectId;
                ViewBag.GradeId = GradeId;
                ViewBag.Eveluation = Eveluation;
                long EvelutionSeq = Convert.ToInt64(Eveluation);
                var Student = Connection.GDgetSubjectStudent(SchoolId, GradeId, ClassId, subId, EvelutionSeq);
                List<GDgetSubjectStudent_Result> Studentlist = Student.ToList();

                StudentModel tcm = new StudentModel();

                List<StudentModel> tcmlist = Studentlist.Select(x => new StudentModel
                {
                    StudentId = x.StudentId,
                    StudentName = x.studentName,
                    Gender = x.Gender,
                    DateOfBirth = x.DateofBirth,
                    Optional = x.Optional
                }).ToList();
                return PartialView("AddStudentSubjectMark", tcmlist);
            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return View();
            }
        }

        public ActionResult Create(string[] Mark, string[] StudentId, string Eveluation, string SubjectId, string GradeId,string Comment)
        {
            Authentication("AdSMS");
            try
            {
                SchoolId=USession.School_Id;
                UserId = USession.User_Id;
                long Eveluationseq=Convert.ToInt64(Eveluation);
                int SubId=Convert.ToInt32(SubjectId);
               
                for (int i = 0; i < StudentId.Length;i++ )
                {
                    string stu=StudentId[i];
                    string mark = Mark[i];
                     
                     if (mark!="")
                     {
                         decimal StMark = Convert.ToDecimal(mark);
                     Connection.GDsetStudentEveluationResult(Eveluationseq, stu, SubId, StMark, GradeId, Comment, SchoolId, UserId, "Y");
                }}




                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);

                return RedirectToAction("Index");
            }
        }


        public ActionResult ShowResult(string Eveluation, string ClassId, string GradeId) {
            Authentication("SubRe");
            try
            {
                SchoolId = USession.School_Id;
                var Subject = Connection.GDgetEveluationSubject(SchoolId, GradeId);
                List<GDgetEveluationSubject_Result> Subjectlist = Subject.ToList();

                OptionalSubjectModel tcm = new OptionalSubjectModel();

                List<OptionalSubjectModel> tcmlist = Subjectlist.Select(x => new OptionalSubjectModel
                {
                    SubjectId = x.SubjectId.ToString(),
                    SubjectName = x.SubjectName
                }).ToList();
                ViewBag.Subject = tcmlist;
                var Student = Connection.GDgetStudentInClass(SchoolId, GradeId, ClassId, "Y");
                List<GDgetStudentInClass_Result> Studentlist = Student.ToList();

                StudentModel Sm = new StudentModel();

                List<StudentModel> Smlist = Studentlist.Select(x => new StudentModel
                {
                    StudentName = x.studentName,
                    StudentId = x.StudentId
                }).ToList();
                ViewBag.StudentList = Smlist;
                long eveluationseq = Convert.ToInt64(Eveluation);
                var Result = Connection.GDgetEveluationResult(SchoolId, GradeId, eveluationseq);
                List<GDgetEveluationResult_Result> Resultlist = Result.ToList();

                StudentReportModel Rm = new StudentReportModel();

                List<StudentReportModel> Rmlist = Resultlist.Select(x => new StudentReportModel
                {
                    Mark = x.Mark,
                    SubjectId = x.SubjectId,
                    StudentId = x.StudentId
                }).ToList();
                ViewBag.ResultList = Rmlist;
                return PartialView("ShowResults");
            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return View();
            }
           
        }


        public ActionResult EditStudentResult(string EditEveluation, string EditClassId, string EditGradeId, string EditSubjectId)
        {
            Authentication("EdRes");
            try {
                SchoolId = USession.School_Id;
            if (EditEveluation != null)
            {
                Session["EditEveluation"] = EditEveluation;
                Session["EditClassId"] = EditClassId;
                Session["EditGradeId"] = EditGradeId;
                Session["EditSubjectId"] = EditSubjectId;
            }
            if (EditEveluation == null)
            {
                if (Session["EditEveluation"] != null)
                {
                    EditEveluation = Session["EditEveluation"].ToString();
                    EditClassId = Session["EditClassId"].ToString();
                    EditGradeId = Session["EditGradeId"].ToString();
                    EditSubjectId = Session["EditSubjectId"].ToString();
                }
            }
            List<GDgetSchoolGrade_Result> Gradelist = GetGradeDropdown();
            ViewBag.EditGradeId = new SelectList(Gradelist, "GradeId", "GradeName");
            long EveluationNo = Convert.ToInt64(EditEveluation);
            int Subject = Convert.ToInt32(EditSubjectId);
            var SubjectResult = Connection.GDgetSubjectsResult(SchoolId, EditGradeId, Subject, EveluationNo, "Y");
            List<GDgetSubjectsResult_Result> SubjectResultlist = SubjectResult.ToList();

            StudentReportModel tcm = new StudentReportModel();

            List<StudentReportModel> tcmlist = SubjectResultlist.Select(x => new StudentReportModel
            {
                Seq=x.EveluationResultSeqNo,
                StudentId = x.StudentId,
                StudentName = x.studentName,
                Mark=x.Mark
            }).ToList();
            return View("EditStudentResults", tcmlist);
            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return View();
            }
        }

        public ActionResult Edit(string StudentId,string ResultId,string Mark)
        {
            Authentication("ReSav");
            try
            {
                StudentReportModel TModel = new StudentReportModel();

                TModel.Mark = Convert.ToDecimal(Mark);
                tblStudent stu = Connection.tblStudents.SingleOrDefault(x => x.StudentId== StudentId);
                TModel.StudentId = StudentId;
                TModel.Seq =Convert.ToInt64(ResultId);
                TModel.StudentName = stu.studentName;
                return PartialView("EditView", TModel);
            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return View();

            }
        }
        
         [HttpPost]
        public ActionResult Edit(StudentReportModel Model)
        {
            Authentication("ReSav");
            try
            {
                UserId = USession.User_Id;
                Connection.GDModifyStudentEveluationResult(Model.Seq, Model.Mark, UserId);
                Connection.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return View();
            }
        }
       

        public ActionResult Delete(int ResultId)
        {
            Authentication("ReDe");
            try
            {
                StudentReportModel TModel = new StudentReportModel();
                TModel.Seq = ResultId;
                return PartialView("DeleteView", TModel);
            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return View();

            }
        }
        
        //
        // POST: /TeacherCategory/Delete/5

        [HttpPost]
        public ActionResult Delete(StudentReportModel Model)
        {
            Authentication("ReDe");
            try
            {
                UserId = USession.User_Id;
                Connection.GDdeleteStudentEveluationResult(Model.Seq, UserId,"N");
                Connection.SaveChanges();


                return Json(true, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return View();

            }
        }
    }
}
