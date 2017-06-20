using GDWEBSolution.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
/*
 * Created Date : 2017/2/23
 * Author : Tharaka Madusanka
 */

namespace GDWEBSolution.Controllers.Teacher
{
    public class TeacherCategoryController : Controller
    {
        SchoolMGTEntitiesConnectionString Connection = new SchoolMGTEntitiesConnectionString();

        public ActionResult Index()
        {
            var Category = Connection.GDgetAllTeacherCategory("Y");
            List<GDgetAllTeacherCategory_Result> Categorylist = Category.ToList();
            TeacherCategoryModel tcm = new TeacherCategoryModel();
            List<TeacherCategoryModel> tcmlist = Categorylist.Select(x => new TeacherCategoryModel
            {
                TeacherCategoryId = x.TeacherCategoryId,
                TeacherCategoryName = x.TeacherCategoryName,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate,
                IsActive = x.IsActive,
                ModifiedBy = x.ModifiedBy,
                ModifiedDate = x.ModifiedDate

            }).ToList();
            return View(tcmlist);
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult ShowAddCategoryModel(int TeacherCategoryID)
        {
            return PartialView("AddTeacherCategory");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TeacherCategoryModel Model)
        {
            try
            {
                
                tblTeacherCategory TCategory = new tblTeacherCategory();

                TCategory.CreatedBy = "ADMIN";
                TCategory.CreatedDate = DateTime.Now;
                TCategory.IsActive = "Y";
                TCategory.TeacherCategoryId = 0;
                TCategory.TeacherCategoryName = Model.TeacherCategoryName;

                Connection.tblTeacherCategories.Add(TCategory);
                Connection.SaveChanges();
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


        public ActionResult ShowEditCategoryModel(int TeacherCategoryID)
        {
            return PartialView("AddTeacherCategory");
        }

        public ActionResult Edit(int CategoryId)
        {
            TeacherCategoryModel TModel = new TeacherCategoryModel();

            tblTeacherCategory TCtable = Connection.tblTeacherCategories.SingleOrDefault(x =>x.TeacherCategoryId == CategoryId);
        
            TModel.TeacherCategoryName = TCtable.TeacherCategoryName;
            TModel.TeacherCategoryId = TCtable.TeacherCategoryId;
            TModel.ModifiedBy = "ADMIN"; //User session needed
            TModel.ModifiedDate = DateTime.Now;

            return PartialView("EditTeacherCategory",TModel);
        }

        [HttpPost]
        public ActionResult Edit(TeacherCategoryModel Model)
        {
            try
            {              
                tblTeacherCategory TCtable = Connection.tblTeacherCategories.SingleOrDefault(x => x.TeacherCategoryId == Model.TeacherCategoryId);
                TCtable.TeacherCategoryName = Model.TeacherCategoryName;
                TCtable.ModifiedBy = "ADMIN"; ;
                TCtable.ModifiedDate = DateTime.Now;
                Connection.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ShowDeleteCategoryModel(int TeacherCategoryID)
        {
            return PartialView("AddTeacherCategory");
        }

        public ActionResult Delete(int CategoryId)
        {
            TeacherCategoryModel TModel = new TeacherCategoryModel();
            TModel.TeacherCategoryId = CategoryId;
            return PartialView("DeleteTeacherCategory",TModel);
        }

        [HttpPost]
        public ActionResult Delete(TeacherCategoryModel Model)
        {
            try
            {
                tblTeacherCategory TCtable = Connection.tblTeacherCategories.SingleOrDefault(x => x.TeacherCategoryId == Model.TeacherCategoryId);
                TCtable.TeacherCategoryId = Model.TeacherCategoryId;
                TCtable.ModifiedBy = "ADMIN"; ;
                TCtable.IsActive = "N";
                TCtable.ModifiedDate = DateTime.Now;
                Connection.SaveChanges();
             
                return Json(true,JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return View();
            }
        }
    }
}
