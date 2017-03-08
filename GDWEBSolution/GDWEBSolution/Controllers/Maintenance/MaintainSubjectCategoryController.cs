using GDWEBSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GDWEBSolution.Controllers
{
    public class MaintainSubjectCategoryController : Controller
    {
        //
        // GET: /MaintainSubjectCategory/
        private SchoolMGTEntitiesConnectionString Connection = new SchoolMGTEntitiesConnectionString();
        string UserId = "ADMIN";
        public ActionResult Index()
        {
            var Group = Connection.GDgetAllSubjectCategory("Y");
            List<GDgetAllSubjectCategory_Result> Grouplist = Group.ToList();

            GDgetAllSubjectCategory_Result tcm = new GDgetAllSubjectCategory_Result();

            List<GDgetAllSubjectCategory_Result> tcmlist = Grouplist.Select(x => new GDgetAllSubjectCategory_Result
            {
                SubjectCategoryId = x.SubjectCategoryId,
                SubjectCategoryName = x.SubjectCategoryName,
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
        public ActionResult Create(tblSubjectCategory Model)
        {
            try
            {

                Connection.GDsetSubjectCategory(Model.SubjectCategoryName, UserId, "Y");
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

            GDgetAllSubjectCategory_Result TModel = new GDgetAllSubjectCategory_Result();

            tblSubjectCategory TCtable = Connection.tblSubjectCategories.SingleOrDefault(x => x.SubjectCategoryId == Code);
            TModel.IsActive = TCtable.IsActive;

            TModel.SubjectCategoryId = TCtable.SubjectCategoryId;
            TModel.SubjectCategoryName = TCtable.SubjectCategoryName;

            return PartialView("EditView", TModel);
        }

        //
        // POST: /TeacherCategory/Edit/5

        [HttpPost]
        public ActionResult Edit(GDgetAllSubjectCategory_Result Model)
        {
            try
            {

                tblSubjectCategory TCtable = Connection.tblSubjectCategories.SingleOrDefault(x => x.SubjectCategoryId == Model.SubjectCategoryId);

               Connection.GDModifySubjectCategory(Model.SubjectCategoryName, Model.SubjectCategoryId, UserId);
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
            GDgetAllSubjectCategory_Result TModel = new GDgetAllSubjectCategory_Result();
            TModel.SubjectCategoryId = Code;
            return PartialView("DeleteView", TModel);
        }

        //
        // POST: /TeacherCategory/Delete/5

        [HttpPost]
        public ActionResult Delete(GDgetAllSubjectCategory_Result Model)
        {
            try
            {
                Connection.GDdeleteSubjectCategory("N", Model.SubjectCategoryId, UserId);
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
