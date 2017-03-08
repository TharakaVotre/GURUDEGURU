using GDWEBSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GDWEBSolution.Controllers
{
    public class MaintainSchoolCategoryController : Controller
    {
        //
        // GET: /MaintainSchoolCategory/
        private SchoolMGTEntitiesConnectionString Connection = new SchoolMGTEntitiesConnectionString();
        string UserId = "ADMIN";
        public ActionResult Index()
        {
            var Category = Connection.GDgetAllSchoolCategory("Y");
            List<GDgetAllSchoolCategory_Result> Categorylist = Category.ToList();

            GDgetAllSchoolCategory_Result tcm = new GDgetAllSchoolCategory_Result();

            List<GDgetAllSchoolCategory_Result> tcmlist = Categorylist.Select(x => new GDgetAllSchoolCategory_Result
            {
                SchoolCategoryId = x.SchoolCategoryId,
                SchoolCategoryName = x.SchoolCategoryName,
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
        public ActionResult Create(tblSchoolCategory Model)
        {
            try
            {

                Connection.GDsetSchoolCategory( Model.SchoolCategoryName, UserId, "Y");
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

            GDgetAllSchoolCategory_Result TModel = new GDgetAllSchoolCategory_Result();

            tblSchoolCategory TCtable = Connection.tblSchoolCategories.SingleOrDefault(x => x.SchoolCategoryId == Code);
            TModel.IsActive = TCtable.IsActive;

            TModel.SchoolCategoryId = TCtable.SchoolCategoryId;
            TModel.SchoolCategoryName = TCtable.SchoolCategoryName;

            return PartialView("EditView", TModel);
        }

        //
        // POST: /TeacherCategory/Edit/5

        [HttpPost]
        public ActionResult Edit(GDgetAllSchoolCategory_Result Model)
        {
            try
            {

                tblSchoolCategory TCtable = Connection.tblSchoolCategories.SingleOrDefault(x => x.SchoolCategoryId == Model.SchoolCategoryId);

                Connection.GDModifySchoolCategory(Model.SchoolCategoryName, Model.SchoolCategoryId, UserId);
                Connection.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult ShowDeleteCategoryModel(int Code)
        {

            return PartialView("AddView");
        }
        //
        // GET: /TeacherCategory/Delete/5

        public ActionResult Delete(int Code)
        {
            GDgetAllSchoolCategory_Result TModel = new GDgetAllSchoolCategory_Result();
            TModel.SchoolCategoryId = Code;
            return PartialView("DeleteView", TModel);
        }

        //
        // POST: /TeacherCategory/Delete/5

        [HttpPost]
        public ActionResult Delete(GDgetAllSchoolCategory_Result Model)
        {
            try
            {
                Connection.GDdeleteSchoolCategory("N", Model.SchoolCategoryId, UserId);
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