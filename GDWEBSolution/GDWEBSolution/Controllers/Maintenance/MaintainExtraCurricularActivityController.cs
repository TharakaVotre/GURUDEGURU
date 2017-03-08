using GDWEBSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GDWEBSolution.Controllers
{
    public class MaintainExtraCurricularActivityController : Controller
    {
        //
        // GET: /MaintainExtraCurricularActivity/

        private SchoolMGTEntitiesConnectionString Connection = new SchoolMGTEntitiesConnectionString();
        string UserId = "ADMIN";
        public ActionResult Index()
        {
            var Activity = Connection.GDgetAllExtraCurricularActivity("Y");
            List<GDgetAllExtraCurricularActivity_Result> Categorylist = Activity.ToList();

            GDgetAllExtraCurricularActivity_Result tcm = new GDgetAllExtraCurricularActivity_Result();

            List<GDgetAllExtraCurricularActivity_Result> tcmlist = Categorylist.Select(x => new GDgetAllExtraCurricularActivity_Result
            {
                ActivityCode = x.ActivityCode,
                ActivityName = x.ActivityName,
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

        public ActionResult Details(string code)
        {
            return View();
        }

        //

        public ActionResult ShowAddView(string code)
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
        public ActionResult Create(tblExtraCurricularActivity Model)
        {
            try
            {

                Connection.GDsetExtraCurricularActivity(Model.ActivityCode,Model.ActivityName, UserId, "Y");
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


        public ActionResult ShowEditEventCategory(string Code)
        {

            return PartialView("AddView");
        }
        //
        // GET: /TeacherCategory/Edit/5

        public ActionResult Edit(string Code)
        {

            GDgetAllExtraCurricularActivity_Result TModel = new GDgetAllExtraCurricularActivity_Result();

            tblExtraCurricularActivity TCtable = Connection.tblExtraCurricularActivities.SingleOrDefault(x => x.ActivityCode == Code);
            TModel.IsActive = TCtable.IsActive;

            TModel.ActivityCode = TCtable.ActivityCode;
            TModel.ActivityName = TCtable.ActivityName;
           
            return PartialView("EditView", TModel);
        }

        //
        // POST: /TeacherCategory/Edit/5

        [HttpPost]
        public ActionResult Edit(GDgetAllExtraCurricularActivity_Result Model)
        {
            try
            {

                tblExtraCurricularActivity TCtable = Connection.tblExtraCurricularActivities.SingleOrDefault(x => x.ActivityCode == Model.ActivityCode);

                Connection.GDModifyExtraCurricularActivit(Model.ActivityName, Model.ActivityCode, UserId);
                Connection.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult ShowDeleteCategoryModel(string Code)
        {

            return PartialView("AddView");
        }
        //
        // GET: /TeacherCategory/Delete/5

        public ActionResult Delete(string Code)
        {
            GDgetAllExtraCurricularActivity_Result TModel = new GDgetAllExtraCurricularActivity_Result();
            TModel.ActivityCode = Code;
            return PartialView("DeleteView", TModel);
        }

        //
        // POST: /TeacherCategory/Delete/5

        [HttpPost]
        public ActionResult Delete(GDgetAllExtraCurricularActivity_Result Model)
        {
            try
            {
                Connection.GDdeleteExtraCurricularActivity("N", Model.ActivityCode, UserId);
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