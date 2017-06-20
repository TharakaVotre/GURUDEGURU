using GDWEBSolution.Models;
using GDWEBSolution.Models.Maintenance;
using GDWEBSolution.Models.User;
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
        UserSession USession = new UserSession();
        string UserId = null;

        private void Authentication(string ControlerName)
        {

            if (USession.User_Id != "")
            {
                string CategoryId = USession.User_Category;
                tblUserCategoryFunction AccessControl = Connection.tblUserCategoryFunctions.SingleOrDefault(a => a.FunctionId == ControlerName && a.CategoryId == CategoryId && a.IsActive == "Y");

                if (AccessControl == null)
                {
                    //RedirectToAction("~/Prohibited");
                    Response.Redirect("~/Prohibited");
                }
                
            }
            else
            {
                // RedirectToAction();
                Response.Redirect("~/Home/Login");
            }
        }
        public ActionResult Index()
        {
            Authentication("MaSCa");
            try
            {
                var Category = Connection.GDgetAllSchoolCategory("Y");
                List<GDgetAllSchoolCategory_Result> Categorylist = Category.ToList();

                SchoolCategoryModel tcm = new SchoolCategoryModel();

                List<SchoolCategoryModel> tcmlist = Categorylist.Select(x => new SchoolCategoryModel
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
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return View();
               
            }
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
            Authentication("MaSCa");
            return View();
        }

        //
        // POST: /Application Status/Create

        [HttpPost]
        public ActionResult Create(tblSchoolCategory Model)
        {
            Authentication("MaSCa");
            UserId = USession.User_Id;
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
                        Errorlog.ErrorManager.LogError(dbEx);
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
            Authentication("MaSCa");
           
            try{
            SchoolCategoryModel TModel = new SchoolCategoryModel();

            tblSchoolCategory TCtable = Connection.tblSchoolCategories.SingleOrDefault(x => x.SchoolCategoryId == Code);
            TModel.IsActive = TCtable.IsActive;

            TModel.SchoolCategoryId = TCtable.SchoolCategoryId;
            TModel.SchoolCategoryName = TCtable.SchoolCategoryName;

            return PartialView("EditView", TModel);
            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return View();
                
            }
        }

        //
        // POST: /TeacherCategory/Edit/5

        [HttpPost]
        public ActionResult Edit(SchoolCategoryModel Model)
        {
            Authentication("MaSCa");
            UserId = USession.User_Id;
            try
            {

                tblSchoolCategory TCtable = Connection.tblSchoolCategories.SingleOrDefault(x => x.SchoolCategoryId == Model.SchoolCategoryId);

                Connection.GDModifySchoolCategory(Model.SchoolCategoryName, Model.SchoolCategoryId, UserId);
                Connection.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
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
            Authentication("MaSCa");
            
            try
            {
                SchoolCategoryModel TModel = new SchoolCategoryModel();
                TModel.SchoolCategoryId = Code;
                return PartialView("DeleteView", TModel);
            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return View();
               
            }
        }

        //
        // POST: /TeacherCategory/Delete/5

        [HttpPost]
        public ActionResult Delete(SchoolCategoryModel Model)
        {
            Authentication("MaSCa");
            UserId = USession.User_Id;
            try
            {
                Connection.GDdeleteSchoolCategory("N", Model.SchoolCategoryId, UserId);
                Connection.SaveChanges();


                return Json(true, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return View();
                
            }
        }
    }
}