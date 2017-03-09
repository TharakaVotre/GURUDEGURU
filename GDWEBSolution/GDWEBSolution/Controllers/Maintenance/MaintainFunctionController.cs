using GDWEBSolution.Models;
using GDWEBSolution.Models.Maintenance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GDWEBSolution.Controllers
{
    public class MaintainFunctionController : Controller
    {
        //
        // GET: /MaintainFunction/

        private SchoolMGTEntitiesConnectionString Connection = new SchoolMGTEntitiesConnectionString();
        string UserId = "ADMIN";
        public ActionResult Index()
        {
            try{
            var Function = Connection.GDgetAllFunction("Y");
            List<GDgetAllFunction_Result> Functionlist = Function.ToList();

            FunctionModel tcm = new FunctionModel();

            List<FunctionModel> tcmlist = Functionlist.Select(x => new FunctionModel
            {
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
            catch (Exception ex)
            {
                return View();
                Errorlog.ErrorManager.LogError(ex);
            }
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
        public ActionResult Create(tblFunction Model)
        {
            try
            {

                Connection.GDsetFunction(Model.FunctionId, Model.FunctionName, UserId, "Y");
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


        public ActionResult ShowEditEventCategory(string Code)
        {

            return PartialView("AddView");
        }
        //
        // GET: /TeacherCategory/Edit/5

        public ActionResult Edit(string Code)
        {
            try{
            FunctionModel TModel = new FunctionModel();

            tblFunction TCtable = Connection.tblFunctions.SingleOrDefault(x => x.FunctionId == Code);
            TModel.IsActive = TCtable.IsActive;

            TModel.FunctionId = TCtable.FunctionId;
            TModel.FunctionName = TCtable.FunctionName;

            return PartialView("EditView", TModel);
            }
            catch (Exception ex)
            {
                return View();
                Errorlog.ErrorManager.LogError(ex);
            }
        }

        //
        // POST: /TeacherCategory/Edit/5

        [HttpPost]
        public ActionResult Edit(FunctionModel Model)
        {
            try
            {

                tblFunction TCtable = Connection.tblFunctions.SingleOrDefault(x => x.FunctionId == Model.FunctionId);

                Connection.GDModifyFunction(Model.FunctionName, Model.FunctionId, UserId);
                Connection.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
                Errorlog.ErrorManager.LogError(ex);
            }
        }


        public ActionResult ShowDeleteModel(string Code)
        {

            return PartialView("AddView");
        }
        //
        // GET: /TeacherCategory/Delete/5

        public ActionResult Delete(string Code)
        {
            try
            {
                FunctionModel TModel = new FunctionModel();
                TModel.FunctionId = Code;
                return PartialView("DeleteView", TModel);
            }
            catch (Exception ex)
            {
                return PartialView();
                Errorlog.ErrorManager.LogError(ex);
            }
        }

        //
        // POST: /TeacherCategory/Delete/5

        [HttpPost]
        public ActionResult Delete(FunctionModel Model)
        {
            try
            {
                Connection.GDdeleteFunction("N", Model.FunctionId, UserId);
                Connection.SaveChanges();


                return Json(true, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("Index");
            
             }
            catch (Exception ex)
            {
                return View();
                Errorlog.ErrorManager.LogError(ex);
            }
        }
    }
}
