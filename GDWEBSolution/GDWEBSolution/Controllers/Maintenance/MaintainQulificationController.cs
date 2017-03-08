using GDWEBSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GDWEBSolution.Controllers
{
    public class MaintainQulificationController : Controller
    {
        //
        // GET: /MaintainQulification/

        private SchoolMGTEntitiesConnectionString Connection = new SchoolMGTEntitiesConnectionString();
        string UserId = "ADMIN";
        public ActionResult Index()
        {
            var Qualification = Connection.GDgetAllQualification("Y");
            List<GDgetAllQualification_Result> Qualificationlist = Qualification.ToList();

            GDgetAllParentObservationType_Result tcm = new GDgetAllParentObservationType_Result();

            List<GDgetAllQualification_Result> tcmlist = Qualificationlist.Select(x => new GDgetAllQualification_Result
            {
                QualificationId = x.QualificationId,
                QualificationName = x.QualificationName,
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
        public ActionResult Create(tblQualification Model)
        {
            try
            {

                Connection.GDsetQualification(Model.QualificationName, UserId, "Y");
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

            GDgetAllQualification_Result TModel = new GDgetAllQualification_Result();

            tblQualification TCtable = Connection.tblQualifications.SingleOrDefault(x => x.QualificationId == Code);
            TModel.IsActive = TCtable.IsActive;

            TModel.QualificationId = TCtable.QualificationId;
            TModel.QualificationName = TCtable.QualificationName;

            return PartialView("EditView", TModel);
        }

        //
        // POST: /TeacherCategory/Edit/5

        [HttpPost]
        public ActionResult Edit(GDgetAllQualification_Result Model)
        {
            try
            {

                tblQualification TCtable = Connection.tblQualifications.SingleOrDefault(x => x.QualificationId == Model.QualificationId);

                Connection.GDModifyQualification(Model.QualificationName, Model.QualificationId, UserId);
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
            GDgetAllQualification_Result TModel = new GDgetAllQualification_Result();
            TModel.QualificationId = Code;
            return PartialView("DeleteView", TModel);
        }

        //
        // POST: /TeacherCategory/Delete/5

        [HttpPost]
        public ActionResult Delete(GDgetAllQualification_Result Model)
        {
            try
            {
                Connection.GDdeleteQualification("N", Model.QualificationId, UserId);
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


