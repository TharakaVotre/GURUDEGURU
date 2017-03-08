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
        //
        // GET: /TeacherCategory/

        public ActionResult Index()
        {

            List<tblTeacherCategory> Categorylist = Connection.tblTeacherCategories.ToList();

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

        //
        // GET: /TeacherCategory/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //

        public ActionResult ShowAddCategoryModel(int TeacherCategoryID)
        {
            return PartialView("AddTeacherCategory");
        }

        // GET: /TeacherCategory/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /TeacherCategory/Create

        [HttpPost]
        public ActionResult Create(TeacherCategoryModel Model)
        {
            try
            {
                

                tblTeacherCategory TCategory = new tblTeacherCategory();

                TCategory.CreatedBy = "ADMIN";
                TCategory.CreatedDate = DateTime.Now;
                if (Model.IsActiveBool == true){TCategory.IsActive = "Y";}
                else { TCategory.IsActive = "N"; }
                TCategory.TeacherCategoryId = 0;
                TCategory.TeacherCategoryName = Model.TeacherCategoryName;

                Connection.tblTeacherCategories.Add(TCategory);
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


        public ActionResult ShowEditCategoryModel(int TeacherCategoryID)
        {
            return PartialView("AddTeacherCategory");
        }
        //
        // GET: /TeacherCategory/Edit/5

        public ActionResult Edit(int CategoryId)
        {
            TeacherCategoryModel TModel = new TeacherCategoryModel();

            tblTeacherCategory TCtable = Connection.tblTeacherCategories.SingleOrDefault(x =>x.TeacherCategoryId == CategoryId);
            TModel.IsActive = TCtable.IsActive;
            if(TCtable.IsActive.Equals("Y")){ TModel.IsActiveBool = true;}
            else{TModel.IsActiveBool = false;}
            TModel.TeacherCategoryName = TCtable.TeacherCategoryName;
            TModel.TeacherCategoryId = TCtable.TeacherCategoryId;

            return PartialView("EditTeacherCategory",TModel);
        }

        //
        // POST: /TeacherCategory/Edit/5

        [HttpPost]
        public ActionResult Edit(TeacherCategoryModel Model)
        {
            try
            {
               
                tblTeacherCategory TCtable = Connection.tblTeacherCategories.SingleOrDefault(x => x.TeacherCategoryId == Model.TeacherCategoryId);

                if (Model.IsActiveBool == true) { TCtable.IsActive = "Y"; }
                else { TCtable.IsActive = "N"; }
                TCtable.TeacherCategoryName = Model.TeacherCategoryName;
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
        //
        // GET: /TeacherCategory/Delete/5

        public ActionResult Delete(int CategoryId)
        {
            TeacherCategoryModel TModel = new TeacherCategoryModel();
            TModel.TeacherCategoryId = CategoryId;
            return PartialView("DeleteTeacherCategory",TModel);
        }

        //
        // POST: /TeacherCategory/Delete/5

        [HttpPost]
        public ActionResult Delete(TeacherCategoryModel Model)
        {
            try
            {
                tblTeacherCategory TCtable = Connection.tblTeacherCategories.Find(Model.TeacherCategoryId);
                Connection.tblTeacherCategories.Remove(TCtable);
                Connection.SaveChanges();

                
                return Json(true,JsonRequestBehavior.AllowGet);
                //return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
