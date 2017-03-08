using GDWEBSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GDWEBSolution.Controllers
{
    public class MaintainStudentEvaluationTypeController : Controller
    {
        //
        // GET: /MaintainStudentEvaluationType/

        private SchoolMGTEntitiesConnectionString Connection = new SchoolMGTEntitiesConnectionString();
        string UserId = "ADMIN";
        public ActionResult Index()
        {
            var Group = Connection.GDgetAllEvaluationType("Y");
            List<GDgetAllEvaluationType_Result> Grouplist = Group.ToList();

            GDgetAllEvaluationType_Result tcm = new GDgetAllEvaluationType_Result();

            List<GDgetAllEvaluationType_Result> tcmlist = Grouplist.Select(x => new GDgetAllEvaluationType_Result
            {
              EvaluationTypeCode=x.EvaluationTypeCode,
              EvaluationTypeDesc=x.EvaluationTypeDesc,
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

        public ActionResult Details(long code)
        {
            return View();
        }

        //

        public ActionResult ShowAddView(long code)
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
        public ActionResult Create(tblEvaluationType Model)
        {
            try
            {

                Connection.GDsetEvaluationType(Model.EvaluationTypeDesc, UserId, "Y");
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


        public ActionResult ShowEditEventCategory(long Code)
        {

            return PartialView("AddView");
        }
        //
        // GET: /TeacherCategory/Edit/5

        public ActionResult Edit(long Code)
        {

            GDgetAllEvaluationType_Result TModel = new GDgetAllEvaluationType_Result();

            tblEvaluationType TCtable = Connection.tblEvaluationTypes.SingleOrDefault(x => x.EvaluationTypeCode == Code);
            TModel.IsActive = TCtable.IsActive;

            TModel.EvaluationTypeCode = TCtable.EvaluationTypeCode;
            TModel.EvaluationTypeDesc = TCtable.EvaluationTypeDesc;

            return PartialView("EditView", TModel);
        }

        //
        // POST: /TeacherCategory/Edit/5

        [HttpPost]
        public ActionResult Edit(GDgetAllEvaluationType_Result Model)
        {
            try
            {

                tblEvaluationType TCtable = Connection.tblEvaluationTypes.SingleOrDefault(x => x.EvaluationTypeCode == Model.EvaluationTypeCode);

                Connection.GDModifyEvaluationTypee(Model.EvaluationTypeDesc, Model.EvaluationTypeCode, UserId);
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

        public ActionResult Delete(long Code)
        {
            GDgetAllEvaluationType_Result TModel = new GDgetAllEvaluationType_Result();
            TModel.EvaluationTypeCode = Code;
            return PartialView("DeleteView", TModel);
        }

        //
        // POST: /TeacherCategory/Delete/5

        [HttpPost]
        public ActionResult Delete(GDgetAllEvaluationType_Result Model)
        {
            try
            {
                Connection.GDdeleteEvaluationType("N", Model.EvaluationTypeCode, UserId);
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
