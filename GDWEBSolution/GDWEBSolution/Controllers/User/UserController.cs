using GDWEBSolution.Models;
using GDWEBSolution.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GDWEBSolution.Controllers.User
{
    public class UserController : Controller
    {
        SchoolMGTEntitiesConnectionString Connection = new SchoolMGTEntitiesConnectionString();
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Category()
        {
            List<tblUserCategory> Categorylist = Connection.tblUserCategories.ToList();

            TeacherCategoryModel tcm = new TeacherCategoryModel();

            List<UserCategoryModel> tcmlist = Categorylist.Select(x => new UserCategoryModel
            {
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate,
                IsActive = x.IsActive,
                ModifiedBy = x.ModifiedBy,
                ModifiedDate = x.ModifiedDate

            }).ToList();



            return View(tcmlist);
        }

        public ActionResult Function() // UserCategoryFunction----
        {
            var Flist = Connection.SMGTgetUserCategoryFunction("%").ToList();

            UCategoryFunctionModel tcm = new UCategoryFunctionModel();

            List<UCategoryFunctionModel> tcmlist = Flist.Select(x => new UCategoryFunctionModel
            {
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName,
                FunctionId = x.FunctionId,
                FunctionName = x.FunctionName,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate,
                IsActive = x.IsActive,
                ModifiedBy = x.ModifiedBy,
                ModifiedDate = x.ModifiedDate

            }).ToList();



            return View(tcmlist);
        }

        private void LoadUCFDropdowns()
        {
            List<tblFunction> Funtionlist = Connection.tblFunctions.ToList();
            ViewBag.FunctionList = new SelectList(Funtionlist, "FunctionId", "FunctionName");

            List<tblUserCategory> UCategorylist = Connection.tblUserCategories.ToList();
            ViewBag.UCategoryNameList = new SelectList(UCategorylist, "CategoryId", "CategoryName");
        }
        //
        // GET: /User/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /User/Create
        public ActionResult Create()
        {
            return PartialView("Add");
        }

        public ActionResult CreateCategory()
        {
            return PartialView("AddCategory");
        }

        [HttpPost]
        public ActionResult CreateCategory(UserCategoryModel Model)
        {
            try
            {

                tblUserCategory Category = new tblUserCategory();

                Category.CreatedBy = "ADMIN";
                Category.CreatedDate = DateTime.Now;
                if (Model.Active == true) { Category.IsActive = "Y"; }
                else { Category.IsActive = "N"; }
                Category.CategoryId = Model.CategoryId;
                Category.CategoryName = Model.CategoryName;

                Connection.tblUserCategories.Add(Category);
                Connection.SaveChanges();

                //return View();

                return RedirectToAction("Category");
            }
            catch (Exception Ex)
            {
                Errorlog.ErrorManager.LogError("CreateCategory(UserCategoryModel Model) @ UserController", Ex);
                return RedirectToAction("Category");
            }
        }


        public ActionResult EditCategory(string CategoryId)
        {
            UserCategoryModel TModel = new UserCategoryModel();

            tblUserCategory TCtable = Connection.tblUserCategories.SingleOrDefault(x => x.CategoryId == CategoryId);
            TModel.IsActive = TCtable.IsActive;
            if (TCtable.IsActive.Equals("Y")) { TModel.Active = true; }
            else { TModel.Active = false; }
            TModel.CategoryName = TCtable.CategoryName;
            TModel.CategoryId = TCtable.CategoryId;

            return PartialView("EditCategory", TModel);
        }

        [HttpPost]
        public ActionResult EditCategory(UserCategoryModel Model)
        {
            try
            {

                tblUserCategory TCtable = Connection.tblUserCategories.SingleOrDefault(x => x.CategoryId == Model.CategoryId);

                if (Model.Active == true) { TCtable.IsActive = "Y"; }
                else { TCtable.IsActive = "N"; }
                TCtable.CategoryName = Model.CategoryName;
                TCtable.ModifiedDate = DateTime.Now;
                TCtable.ModifiedBy = "ADMIN";
                Connection.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult NewUCFunction()
        {
            LoadUCFDropdowns();
            return PartialView("UCFunctions");
        }
        [HttpPost]
        public ActionResult NewUCFunction(UCategoryFunctionModel Model)
        {
            try
            {

                tblUserCategoryFunction Category = new tblUserCategoryFunction();

                Category.CreatedBy = "ADMIN";
                Category.CreatedDate = DateTime.Now;
                if (Model.Active == true) { Category.IsActive = "Y"; }
                else { Category.IsActive = "N"; }
                Category.CategoryId = Model.CategoryId;
                Category.FunctionId = Model.FunctionId;

                Connection.tblUserCategoryFunctions.Add(Category);
                Connection.SaveChanges();

                //return View();

                return RedirectToAction("Category");
            }
            catch (Exception Ex)
            {
                Errorlog.ErrorManager.LogError("CreateCategory(UserCategoryModel Model) @ UserController", Ex);
                return RedirectToAction("Category");
            }
        }

        public ActionResult DeleteUCFunction(string CategoryId, string FunctionId)
        {
            UCategoryFunctionModel TModel = new UCategoryFunctionModel();
            TModel.FunctionId = FunctionId;
            TModel.CategoryId = CategoryId;
            return PartialView("DeleteUCFunction", TModel);
        }

        [HttpPost]
        public ActionResult DeleteUCFunction(UCategoryFunctionModel Model)
        {
            try
            {
                tblUserCategoryFunction Tble = Connection.tblUserCategoryFunctions.Find(Model.CategoryId, Model.FunctionId);
                Connection.tblUserCategoryFunctions.Remove(Tble);
                Connection.SaveChanges();

                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }
        //
        // POST: /User/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
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

        //
        // GET: /User/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /User/Edit/5

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
        // GET: /User/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /User/Delete/5

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
