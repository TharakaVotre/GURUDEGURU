using GDWEBSolution.Models;
using GDWEBSolution.Models.TimeTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GDWEBSolution.Controllers.TimeTable
{
    public class TimeTableController : Controller
    {
        //
        // GET: /TimeTable/
        SchoolMGTEntitiesConnectionString Connection = new SchoolMGTEntitiesConnectionString();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowTimeTblView(string ClassId, string GreadId,string SchoolId,string Day,bool ForEditDelte)
        {
            var STQlist = Connection.SMGTgetDayTimeTabel(ClassId,GreadId,SchoolId,Day).ToList();

            List<TimeTableModel> List = STQlist.Select(x => new TimeTableModel
            {
                Day = x.Day,
                PeriodSeqNo = x.PeriodSequenceNo,
                FromTime_String = x.FromTime,
                ToTime_String = x.ToTime,
                SubjectId = x.SubjectId,
                SubjectName = x.SubjectName,
                SeqNo = x.SeqNo

            }).ToList();

            ViewBag.Class = ClassId;
            ViewBag.Gread = GreadId;
            ViewBag.School = SchoolId;
            ViewBag.Day = Day;
            if (ForEditDelte)
            {
                return PartialView("EditView", List);
            }
            else
            {
                return PartialView("TimeTblView", List);
            }
        }
        //
        // GET: /TimeTable/Details/5

        public ActionResult AddEditTimeTable()
        {
            var SchoolGrade = Connection.SMGTgetSchoolGrade("CKC").ToList();//Need to Pass a Session Schoolid
            List<tblGrade> SchoolGradeList = SchoolGrade.Select(x => new tblGrade
            {
                GradeId = x.GradeId,
                GradeName = x.GradeName,
                IsActive = x.IsActive

            }).ToList();
            ViewBag.SchoolGrades = new SelectList(SchoolGradeList, "GradeId", "GradeName");
            return View();
        }

        public ActionResult ShowGradeClasses(string GradeId)
        {
            List<tblClass> ClassesList = Connection.tblClasses.Where(r => r.SchoolId == "CKC" && r.GradeId == GradeId).ToList();
            ViewBag.GradeClassse = new SelectList(ClassesList, "ClassId", "ClassName");

            List<tblDaysOfWeek> DayList = Connection.tblDaysOfWeeks.ToList();
            ViewBag.DayOfWeek = new SelectList(DayList, "DayName", "DayName");

            var GradeSubject = Connection.SMGTgetGradeSubjects(GradeId).ToList();//Need to Pass a Session Schoolid
            List<tblSubject> GradeSubjectList = GradeSubject.Select(x => new tblSubject
            {
                SubjectId = x.SubjectId,
                ShortName = x.ShortName,
                SubjectName = x.SubjectName,
                IsActive = x.IsActive

            }).ToList();
            ViewBag.GradeSubjectList = new SelectList(GradeSubjectList, "SubjectId", "SubjectName");
            return PartialView("TClassNSubject");
        }

        public ActionResult ShowEditTimeTbl(long SeqNo)
        {
            TimeTableModel TModel = new TimeTableModel();

            tblTimeTable TCtable = Connection.tblTimeTables.SingleOrDefault(x => x.SeqNo == SeqNo);

            DateTime F = DateTime.Parse(TCtable.FromTime.ToString());
            DateTime T = DateTime.Parse(TCtable.ToTime.ToString());

           // F.Subtract(-TimeSpan.FromHours(Convert.ToDouble(F.ToString("HH")))).ToShortTimeString();

            TModel.FromTime_String = F.ToString("hh:mm tt");
            TModel.ToTime_String = T.ToString("hh:mm tt");

            TModel.SubjectId = TCtable.SubjectId;
            TModel.GradeId = TCtable.GradeId;
            TModel.ClassId = TCtable.ClassId;
            TModel.SchoolId = TCtable.SchoolId;

            var GradeSubject = Connection.SMGTgetGradeSubjects(TModel.GradeId).ToList();//Need to Pass a Session Schoolid
            List<tblSubject> EGradeSubjectList = GradeSubject.Select(x => new tblSubject
            {
                SubjectId = x.SubjectId,
                ShortName = x.ShortName,
                SubjectName = x.SubjectName,
                IsActive = x.IsActive

            }).ToList();
            ViewBag.SubjectEdit = new SelectList(EGradeSubjectList, "SubjectId", "SubjectName");
            return PartialView("EditTimeTbl", TModel);
        }

        [HttpPost]
        public JsonResult EditTimeTable(TimeTableModel Model)
        {
            try
            {
                tblTimeTable Newt = Connection.tblTimeTables.SingleOrDefault(x => x.SeqNo == Model.SeqNo);

                Newt.ModifiedBy = "ADMIN";
                Newt.ModifiedDate = DateTime.Now;

                DateTime F = DateTime.Parse(Model.FromTime_String);
                DateTime T = DateTime.Parse(Model.ToTime_String);

                Newt.FromTime = TimeSpan.Parse(F.ToString("HH:mm"));
                Newt.ToTime = TimeSpan.Parse(T.ToString("HH:mm"));
                Newt.SubjectId = Model.SubjectId;

                Connection.SaveChanges();

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }

        //
        // GET: /TimeTable/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /TimeTable/Create

        [HttpPost]
        public ActionResult Create(TimeTableModel Model)
        {
            try
            {
                tblTimeTable TimeTbl = new tblTimeTable();

                TimeTbl.CreatedBy = "ADMIN";
                TimeTbl.CreatedDate = DateTime.Now;

                TimeTbl.AcademicYear = "2017"; // Parameter
                TimeTbl.SchoolId = "CKC"; //Session
                TimeTbl.GradeId = Model.GradeId;
                TimeTbl.ClassId = Model.ClassId;
                TimeTbl.Day = Model.Day;
                TimeTbl.SubjectId = Model.SubjectId;

                DateTime F = DateTime.Parse(Model.FromTime_String);
                DateTime T = DateTime.Parse(Model.ToTime_String);

                TimeTbl.FromTime = TimeSpan.Parse(F.ToString("HH:mm"));
                TimeTbl.ToTime = TimeSpan.Parse(T.ToString("HH:mm"));

                TimeTbl.IsActive = "Y";
                TimeTbl.PeriodSequenceNo = Model.PeriodSeqNo; 

                Connection.tblTimeTables.Add(TimeTbl);
                Connection.SaveChanges();
                //return View();
                var result = new { r = "S" , Grade = Model.GradeId, Class = Model.ClassId , Day = Model.Day };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                var result = new { r = "E"};
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        //
        // GET: /TimeTable/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /TimeTable/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /TimeTable/Delete/5

        public ActionResult Delete(int SeqNo)
        {
            TimeTableModel TModel = new TimeTableModel();
            TModel.SeqNo = SeqNo;
            return PartialView("DeleteTimeSlot", TModel);
        }

        //
        // POST: /TimeTable/Delete/5

        [HttpPost]
        public ActionResult Delete(TimeTableModel Model)
        {
            try
            {
                tblTimeTable TCtable = Connection.tblTimeTables.Find(Model.SeqNo);
                Connection.tblTimeTables.Remove(TCtable);
                Connection.SaveChanges();


                return Json(true, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
