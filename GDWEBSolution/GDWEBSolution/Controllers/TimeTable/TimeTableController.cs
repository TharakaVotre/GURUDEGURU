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
                SubjectName = x.SubjectName

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

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /TimeTable/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
