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

        public ActionResult ShowTimeTblView(string ClassId, string GreadId,string SchoolId,string Day)
        {
            var STQlist = Connection.SMGTgetDayTimeTabel(ClassId,GreadId,SchoolId,Day).ToList();

            List<TimeTableModel> List = STQlist.Select(x => new TimeTableModel
            {
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
            return PartialView("TimeTblView", List);
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
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
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
