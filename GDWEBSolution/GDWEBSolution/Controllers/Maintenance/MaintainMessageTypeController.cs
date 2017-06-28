using GDWEBSolution.Filters;
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
    public class MaintainMessageTypeController : Controller
    {
        //
        // GET: /MaintainMessageType/
        private SchoolMGTEntitiesConnectionString Connection = new SchoolMGTEntitiesConnectionString();
        UserSession USession = new UserSession();
        string UserId = null;

      [UserFilter(Function_Id = "MaMsg")]
        public ActionResult Index()
        {
             
            try
            {
                var msg = Connection.GDgetAllMassageType("Y");
                List<GDgetAllMassageType_Result> msglist = msg.ToList();

                MessageTypeModel tcm = new MessageTypeModel();

                List<MessageTypeModel> tcmlist = msglist.Select(x => new MessageTypeModel
                {
                    MessageTypeDescription = x.MessageTypeDescription,
                    MessageTypeId = x.MessageTypeId,
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

        public ActionResult Details(long code)
        {
            return View();
        }

        //

        public ActionResult ShowAddView(long code)
        {
            return PartialView("AddView");
        }

        // GET: /TeacherCategory/Create
         [UserFilter(Function_Id = "MaMsg")]
        public ActionResult Create()
        {
            
            return View();
        }

        //
        // POST: /Application Status/Create
         [UserFilter(Function_Id = "MaMsg")]
        [HttpPost]
        public ActionResult Create(tblMessageType Model)
        {
            
            UserId = USession.User_Id;
            try
            {

                Connection.GDsetMassageType(Model.MessageTypeDescription, UserId, "Y");
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


        public ActionResult ShowEdit(long Code)
        {

            return PartialView("AddView");
        }
        //
        // GET: /TeacherCategory/Edit/5
         [UserFilter(Function_Id = "MaMsg")]
        public ActionResult Edit(long Code)
        {
             
           
            try{
            MessageTypeModel TModel = new MessageTypeModel();

            tblMessageType TCtable = Connection.tblMessageTypes.SingleOrDefault(x => x.MessageTypeId == Code);
            TModel.IsActive = TCtable.IsActive;

            TModel.MessageTypeId = TCtable.MessageTypeId;
            TModel.MessageTypeDescription = TCtable.MessageTypeDescription;

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
         [UserFilter(Function_Id = "MaMsg")]
        [HttpPost]
        public ActionResult Edit(MessageTypeModel Model)
        {
            
            UserId = USession.User_Id;
            try
            {

                tblMessageType TCtable = Connection.tblMessageTypes.SingleOrDefault(x => x.MessageTypeId == Model.MessageTypeId);

                Connection.GDModifyMassageType(Model.MessageTypeDescription, Model.MessageTypeId, UserId);
                Connection.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return View();
                
            }
        }


        public ActionResult ShowDeleteModel(string Code)
        {

            return PartialView("AddView");
        }
        //
        // GET: /TeacherCategory/Delete/5
         [UserFilter(Function_Id = "MaMsg")]
        public ActionResult Delete(long Code)
        {
             
            
            try{
            MessageTypeModel TModel = new MessageTypeModel();
            TModel.MessageTypeId = Code;
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
         [UserFilter(Function_Id = "MaMsg")]
        [HttpPost]
        public ActionResult Delete(MessageTypeModel Model)
        {
            
            UserId = USession.User_Id;
            try
            {
                Connection.GDdeleteMassageType("N", Model.MessageTypeId, UserId);
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

