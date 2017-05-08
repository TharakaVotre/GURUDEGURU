using GDWEBSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GDWEBSolution.Controllers.Message
{
    public class SPMessageController : Controller
    {
        //
        // GET: /SPMessage/
        SchoolMGTEntitiesConnectionString Connection = new SchoolMGTEntitiesConnectionString();
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /SPMessage/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /SPMessage/Create

        public ActionResult NewMessage()
        {
            var SchoolGrade = Connection.SMGTgetSchoolGrade("CKC").ToList();//Need to Pass a Session Schoolid
            List<tblGrade> SchoolGradeList = SchoolGrade.Select(x => new tblGrade
            {
                GradeId = x.GradeId,
                GradeName = x.GradeName,
                IsActive = x.IsActive

            }).ToList();
            ViewBag.SchoolGrades = new SelectList(SchoolGradeList, "GradeId", "GradeName");
            List<SMGT_getSchoolExactivity_Result> ex = Connection.SMGT_getSchoolExactivity("CKC").ToList();//Need to Pass a Session Schoolid

            ViewBag.SchoolExactivity = new SelectList(ex, "ActivityCode", "ActivityName");
            return View();
        }

        //
        // POST: /SPMessage/Create

        [HttpPost]
        public ActionResult NewMessage(FormCollection collection)
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
        public ActionResult ShowGradeClass(string GradeId)
        {
            List<tblClass> ClassesList = Connection.tblClasses.Where(r => r.SchoolId == "CKC" && r.GradeId == GradeId).ToList();
            ViewBag.GradeClassse = new SelectList(ClassesList, "ClassId", "ClassName");

            return PartialView("loadClass");
        }
        public ActionResult ShowParentByClass(string GradeId,string ClassId)
        {
            List<SMGT_getSchoolGreadClassParent_Result> gcp = Connection.SMGT_getSchoolGreadClassParent("CKC",GradeId,ClassId).ToList();
            ViewBag.GradeClassseParent = new SelectList(gcp, "ParentId", "ParentName");

            return PartialView("loadParent");
        }
        public ActionResult ShowParentByExActivity(string ExActivityId)
        {
            List<SMGT_getSchoolExactivityParent_Result> exl = Connection.SMGT_getSchoolExactivityParent("CKC",ExActivityId).ToList();
            ViewBag.ExactParents = new SelectList(exl, "ParentId", "ParentName");

            return PartialView("loadExParent");
        }
        //
        // GET: /SPMessage/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /SPMessage/Edit/5

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
        // GET: /SPMessage/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /SPMessage/Delete/5

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
