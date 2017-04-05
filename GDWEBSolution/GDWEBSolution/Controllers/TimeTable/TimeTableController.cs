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

        public ActionResult Details(int id)
        {
            return View();
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
