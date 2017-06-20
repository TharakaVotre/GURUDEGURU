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
    public class MaintainParentObservationTypeController : Controller
    {
        //
        // GET: /MaintainParentObservationType/

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
            Authentication("MaPao");
            try{
            var Observation = Connection.GDgetAllParentObservationType("Y");
            List<GDgetAllParentObservationType_Result> Observationlist = Observation.ToList();

            ParentObservationTypeModel tcm = new ParentObservationTypeModel();

            List<ParentObservationTypeModel> tcmlist = Observationlist.Select(x => new ParentObservationTypeModel
            {
                Description = x.Description,
                ObTypeId = x.ObTypeId,
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
            Authentication("MaPao");
            return View();
        }

        //
        // POST: /Application Status/Create

        [HttpPost]
        public ActionResult Create(tblParentObservationType Model)
        {
            Authentication("MaPao");
            UserId = USession.User_Id;
            try
            {

                Connection.GDsetParentObservationType(Model.Description, UserId, "Y");
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


        public ActionResult ShowEdit(int Code)
        {

            return PartialView("AddView");
        }
        //
        // GET: /TeacherCategory/Edit/5

        public ActionResult Edit(int Code)
        {
            Authentication("MaPao");
           
            try{
            ParentObservationTypeModel TModel = new ParentObservationTypeModel();

            tblParentObservationType TCtable = Connection.tblParentObservationTypes.SingleOrDefault(x => x.ObTypeId == Code);
            TModel.IsActive = TCtable.IsActive;

            TModel.ObTypeId = TCtable.ObTypeId;
            TModel.Description = TCtable.Description;

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
        public ActionResult Edit(ParentObservationTypeModel Model)
        {
            Authentication("MaPao");
            UserId = USession.User_Id;
            try
            {

                tblParentObservationType TCtable = Connection.tblParentObservationTypes.SingleOrDefault(x => x.ObTypeId == Model.ObTypeId);

                Connection.GDModifyParentObservationType(Model.Description, Model.ObTypeId, UserId);
                Connection.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
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
            Authentication("MaPao");
           
            try
            {
                ParentObservationTypeModel TModel = new ParentObservationTypeModel();
                TModel.ObTypeId = Code;
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
        public ActionResult Delete(ParentObservationTypeModel Model)
        {
            Authentication("MaPao");
            UserId = USession.User_Id;
            try
            {
                Connection.GDdeleteParentObservationType("N", Model.ObTypeId, UserId);
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

