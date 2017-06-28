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
    public class MaintainApplicationStatusController : Controller
    {
        //
        // GET: /MaintainApplicationStatus/

        SchoolMGTEntitiesConnectionString Connection = new SchoolMGTEntitiesConnectionString();
        UserSession USession = new UserSession();
        // GET: /TeacherCategory/
        string UserId = null;

       [UserFilter(Function_Id = "MaAS")]
        public ActionResult Index()
        {
            
            try
            {
                var status = Connection.GDgetAllApplicationStatus("Y", 0);
                List<GDgetAllApplicationStatus_Result> Categorylist = status.ToList();

                ApplicationStatusModel tcm = new ApplicationStatusModel();

                List<ApplicationStatusModel> tcmlist = Categorylist.Select(x => new ApplicationStatusModel
                {
                    StatusCode = x.StatusCode,
                    StatusDescription = x.StatusDescription,
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate,
                    IsActive = x.IsActive,
                    ModifiedBy = x.ModifiedBy,
                    ModifiedDate = x.ModifiedDate

                }).ToList();



                return View(tcmlist);
            }
            catch (Exception ex) {
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

        public ActionResult ShowAddApplicationStatus(long id)
        {
            return PartialView("AddApplicationStatus");
        }

        // GET: /TeacherCategory/Create
           [UserFilter(Function_Id = "MaAS")]
        public ActionResult Create()
        {
            
            return View();
        }

        //
        // POST: /Application Status/Create
           [UserFilter(Function_Id = "MaAS")]
        [HttpPost]
        public ActionResult Create(tblApplicationStatu Model)
        {
           
            UserId = USession.User_Id;
            try
            {

                Connection.GDsetApplicationStatus(Model.StatusDescription, UserId, "Y");
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


        public ActionResult ShowEditApplicationStatus(long CategoryID)
        {
            return PartialView("AddApplicationStatus");
        }
        //
        // GET: /TeacherCategory/Edit/5
       [UserFilter(Function_Id = "MaAS")]
        public ActionResult Edit(long typeId)
        {
           
            try
            {
                ApplicationStatusModel TModel = new ApplicationStatusModel();

                tblApplicationStatu TCtable = Connection.tblApplicationStatus.SingleOrDefault(x => x.StatusCode == typeId);
                TModel.IsActive = TCtable.IsActive;

                TModel.StatusDescription = TCtable.StatusDescription;
                TModel.StatusCode = TCtable.StatusCode;

                return PartialView("EditApplicationStatus", TModel);
            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return View();
               
            }
        }

        //
        // POST: /TeacherCategory/Edit/5
           [UserFilter(Function_Id = "MaAS")]
        [HttpPost]
        public ActionResult Edit(ApplicationStatusModel Model)
        {
           
            try
            {
                UserId = USession.User_Id;
                tblApplicationStatu TCtable = Connection.tblApplicationStatus.SingleOrDefault(x => x.StatusCode == Model.StatusCode);

                
                Connection.GDModifyAllApplicationStatus(Model.StatusDescription,Model.StatusCode,UserId);
                Connection.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return View();
                
            }
        }


        public ActionResult ShowDeleteCategoryModel(long TypeID)
        {
            return PartialView("AddApplicationStatus");
        }
        //
        // GET: /TeacherCategory/Delete/5
           [UserFilter(Function_Id = "MaAS")]
        public ActionResult Delete(long CategoryId)
        {
            
            try
            {
                ApplicationStatusModel TModel = new ApplicationStatusModel();
                TModel.StatusCode = CategoryId;
                return PartialView("DeleteApplicationStatus", TModel);
            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return View();
                
            }
        }

        //
        // POST: /TeacherCategory/Delete/5
           [UserFilter(Function_Id = "MaAS")]
        [HttpPost]
        public ActionResult Delete(ApplicationStatusModel Model)
        {
            
            try
            {
                UserId = USession.User_Id;
                Connection.GDdeleteAllApplicationStatus("N", Model.StatusCode, UserId);
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