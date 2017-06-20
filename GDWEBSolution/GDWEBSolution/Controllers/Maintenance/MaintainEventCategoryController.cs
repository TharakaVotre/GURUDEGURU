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
    public class MaintainEventCategoryController : Controller
    {
        //
        // GET: /MaintainEventCategory/

        SchoolMGTEntitiesConnectionString Connection = new SchoolMGTEntitiesConnectionString();
        UserSession USession = new UserSession();
        // GET: /TeacherCategory/
        string UserId = null;
        public ActionResult Index()
        {
            Authentication("MaECa");
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
            Authentication("MaECa");
            return View();
        }

        //
        // POST: /Application Status/Create

        [HttpPost]
        public ActionResult Create(tblEventcategory Model)
        {
            Authentication("MaECa");
            try
            {
                UserId = USession.User_Id;
                string Message = Request.Form["Message"];
                string Approve = Request.Form["Approval"];
                Connection.GDsetEventCategory(Model.EventCategoryDesc, Message, Approve, UserId, "Y");
                Connection.SaveChanges();

                //return View();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return View();
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
            Authentication("MaECa");
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
            Authentication("MaECa");
            UserId = USession.User_Id;
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
            Authentication("MaECa");
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
            Authentication("MaECa");
            try
            {
                UserId = USession.User_Id;
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
    }
}