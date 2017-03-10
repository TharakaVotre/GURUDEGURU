using GDWEBSolution.Models;
using GDWEBSolution.Models.Maintenance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GDWEBSolution.Controllers
{
    public class MaintainEventCategoryController : Controller
    {
        //
        // GET: /MaintainEventCategory/

        SchoolMGTEntitiesConnectionString Connection = new SchoolMGTEntitiesConnectionString();
        //
        // GET: /TeacherCategory/
        string UserId = "ADMIN";
        public ActionResult Index()
        {
            try
            {
                var Category = Connection.GDgetAllEventCategory("Y");
                List<GDgetAllEventCategory_Result> Categorylist = Category.ToList();

                EventCategoryModel tcm = new EventCategoryModel();

                List<EventCategoryModel> tcmlist = Categorylist.Select(x => new EventCategoryModel
                {

                    EventCategoryId = x.EventCategoryId,
                    EventCategoryDesc = x.EventCategoryDesc,
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate,
                    IsActive = x.IsActive,
                    ModifiedBy = x.ModifiedBy,
                    ModifiedDate = x.ModifiedDate,
                    BroadcastMessage = x.BroadcastMessage,
                    ParentApprovalNeeded = x.ParentApprovalNeeded
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

        public ActionResult Details(long id)
        {
            return View();
        }

        //

        public ActionResult ShowAddEventCategory(long id)
        {
            
            return PartialView("AddEventCategory");
        }

        // GET: /TeacherCategory/Create

        public ActionResult Create()
        {
           
            return View();
        }

        //
        // POST: /Application Status/Create

        [HttpPost]
        public ActionResult Create(tblEventcategory Model)
        {
            try
            {
                string Message = Request.Form["Message"];
                string Approve = Request.Form["Approval"];
                Connection.GDsetEventCategory(Model.EventCategoryDesc, Message, Approve, UserId, "Y");
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


        public ActionResult ShowEditEventCategory(long CategoryId)
        {
           
            return PartialView("AddEventCategory");
        }
        //
        // GET: /TeacherCategory/Edit/5

        public ActionResult Edit(long CategoryId)
        {
            try
            {
                EventCategoryModel TModel = new EventCategoryModel();

                tblEventcategory TCtable = Connection.tblEventcategories.SingleOrDefault(x => x.EventCategoryId == CategoryId);
                TModel.IsActive = TCtable.IsActive;

                TModel.EventCategoryId = TCtable.EventCategoryId;
                TModel.EventCategoryDesc = TCtable.EventCategoryDesc;
                TModel.BroadcastMessage = TCtable.BroadcastMessage;
                TModel.ParentApprovalNeeded = TCtable.ParentApprovalNeeded;

                return PartialView("EditEventCategory", TModel);
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
        public ActionResult Edit(EventCategoryModel Model)
        {
            try
            {

                tblEventcategory TCtable = Connection.tblEventcategories.SingleOrDefault(x => x.EventCategoryId == Model.EventCategoryId);
               
                Connection.GDModifyEventCategory(Model.EventCategoryDesc, Model.EventCategoryId,UserId,Model.BroadcastMessage,Model.ParentApprovalNeeded);
                Connection.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return View();
                
            }
        }


        public ActionResult ShowDeleteCategoryModel(long CategoryId)
        {
            
            return PartialView("AddEventCategory");
        }
        //
        // GET: /TeacherCategory/Delete/5

        public ActionResult Delete(long CategoryId)
        {
            try
            {
                EventCategoryModel TModel = new EventCategoryModel();
                TModel.EventCategoryId = CategoryId;
                return PartialView("DeleteEventCategory", TModel);
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
        public ActionResult Delete(EventCategoryModel Model)
        {
            try
            {
                Connection.GDdeleteEventCategory("N", Model.EventCategoryId, UserId);
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