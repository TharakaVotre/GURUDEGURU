using GDWEBSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GDWEBSolution.Controllers
{
    public class MaintainSubjectController : Controller
    {
        //
        // GET: /MaintainSubject/

        private SchoolMGTEntitiesConnectionString Connection = new SchoolMGTEntitiesConnectionString();
        string UserId = "SYSTEM";
        public ActionResult Index()
        {
            var Group = Connection.GDgetAllSubject("Y");
            List<GDgetAllSubject_Result> Grouplist = Group.ToList();

            GDgetAllSubject_Result tcm = new GDgetAllSubject_Result();

            List<GDgetAllSubject_Result> tcmlist = Grouplist.Select(x => new GDgetAllSubject_Result
            {
                SubjectId = x.SubjectId,
                ShortName = x.ShortName,
                SubjectName = x.SubjectName,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate,
                IsActive = x.IsActive,
                ModifiedBy = x.ModifiedBy,
                ModifiedDate = x.ModifiedDate

            }).ToList();



            return View(tcmlist);
        }

        //
        // GET: /TeacherCategory/Details/5

        public ActionResult Details(int code)
        {
            return View();
        }

        //

        public ActionResult ShowAddView(int code)
        {
            return PartialView("AddView");
        }

        // GET: /TeacherCategory/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Application Status/Create

        [HttpPost]
        public ActionResult Create(tblSubject Model)
        {
            try
            {

                Connection.GDsetSubject(Model.ShortName,Model.SubjectName, UserId, "Y");
                Connection.SaveChanges();

                //return View();

                return RedirectToAction("Index");
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


        public ActionResult ShowEditEventCategory(int Code)
        {

            return PartialView("AddView");
        }
        //
        // GET: /TeacherCategory/Edit/5

        public ActionResult Edit(int Code)
        {

            GDgetAllSubject_Result TModel = new GDgetAllSubject_Result();

            tblSubject TCtable = Connection.tblSubjects.SingleOrDefault(x => x.SubjectId == Code);
            TModel.IsActive = TCtable.IsActive;

            TModel.SubjectId = TCtable.SubjectId;
            TModel.ShortName = TCtable.ShortName;
            TModel.SubjectName = TCtable.SubjectName;

            return PartialView("EditView", TModel);
        }

        //
        // POST: /TeacherCategory/Edit/5

        [HttpPost]
        public ActionResult Edit(GDgetAllSubject_Result Model)
        {
            try
            {

                tblSubject TCtable = Connection.tblSubjects.SingleOrDefault(x => x.SubjectId == Model.SubjectId);

                Connection.GDModifySubject(Model.ShortName,Model.SubjectName, Model.SubjectId, UserId);
                Connection.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult ShowDeleteCategoryModel(long Code)
        {

            return PartialView("AddView");
        }
        //
        // GET: /TeacherCategory/Delete/5

        public ActionResult Delete(int Code)
        {
            GDgetAllSubject_Result TModel = new GDgetAllSubject_Result();
            TModel.SubjectId = Code;
            return PartialView("DeleteView", TModel);
        }

        //
        // POST: /TeacherCategory/Delete/5

        [HttpPost]
        public ActionResult Delete(GDgetAllSubject_Result Model)
        {
            try
            {
                Connection.GDdeleteSubject("N", Model.SubjectId, UserId);
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

