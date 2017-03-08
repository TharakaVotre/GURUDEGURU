using GDWEBSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GDWEBSolution.Controllers
{
    public class MaintainParentObservationTypeController : Controller
    {
        //
        // GET: /MaintainParentObservationType/

        private SchoolMGTEntitiesConnectionString Connection = new SchoolMGTEntitiesConnectionString();
        string UserId = "ADMIN";
        public ActionResult Index()
        {
            var Observation = Connection.GDgetAllParentObservationType("Y");
            List<GDgetAllParentObservationType_Result> Observationlist = Observation.ToList();

            GDgetAllParentObservationType_Result tcm = new GDgetAllParentObservationType_Result();

            List<GDgetAllParentObservationType_Result> tcmlist = Observationlist.Select(x => new GDgetAllParentObservationType_Result
            {
                Description = x.Description,
                ObTypeId = x.ObTypeId,
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
        public ActionResult Create(tblParentObservationType Model)
        {
            try
            {

                Connection.GDsetParentObservationType(Model.Description, UserId, "Y");
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


        public ActionResult ShowEdit(int Code)
        {

            return PartialView("AddView");
        }
        //
        // GET: /TeacherCategory/Edit/5

        public ActionResult Edit(int Code)
        {

            GDgetAllParentObservationType_Result TModel = new GDgetAllParentObservationType_Result();

            tblParentObservationType TCtable = Connection.tblParentObservationTypes.SingleOrDefault(x => x.ObTypeId == Code);
            TModel.IsActive = TCtable.IsActive;

            TModel.ObTypeId = TCtable.ObTypeId;
            TModel.Description = TCtable.Description;

            return PartialView("EditView", TModel);
        }

        //
        // POST: /TeacherCategory/Edit/5

        [HttpPost]
        public ActionResult Edit(GDgetAllParentObservationType_Result Model)
        {
            try
            {

                tblParentObservationType TCtable = Connection.tblParentObservationTypes.SingleOrDefault(x => x.ObTypeId == Model.ObTypeId);

                Connection.GDModifyParentObservationType(Model.Description, Model.ObTypeId, UserId);
                Connection.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult ShowDeleteModel(int Code)
        {

            return PartialView("AddView");
        }
        //
        // GET: /TeacherCategory/Delete/5

        public ActionResult Delete(int Code)
        {
            GDgetAllParentObservationType_Result TModel = new GDgetAllParentObservationType_Result();
            TModel.ObTypeId = Code;
            return PartialView("DeleteView", TModel);
        }

        //
        // POST: /TeacherCategory/Delete/5

        [HttpPost]
        public ActionResult Delete(GDgetAllParentObservationType_Result Model)
        {
            try
            {
                Connection.GDdeleteParentObservationType("N", Model.ObTypeId, UserId);
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

