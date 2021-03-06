﻿using GDWEBSolution.Filters;
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
    public class MaintainExtraCurricularActivityController : Controller
    {
        //
        // GET: /MaintainExtraCurricularActivity/

        private SchoolMGTEntitiesConnectionString Connection = new SchoolMGTEntitiesConnectionString();
        UserSession USession = new UserSession();
        string UserId = null;

        [UserFilter(Function_Id = "MaExA")]
        public ActionResult Index()
        {
            
            try{
            var Activity = Connection.GDgetAllExtraCurricularActivity("Y");
            List<GDgetAllExtraCurricularActivity_Result> Categorylist = Activity.ToList();

            ExtraCurricularActivityModel tcm = new ExtraCurricularActivityModel();

            List<ExtraCurricularActivityModel> tcmlist = Categorylist.Select(x => new ExtraCurricularActivityModel
            {
                ActivityCode = x.ActivityCode,
                ActivityName = x.ActivityName,
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
        [UserFilter(Function_Id = "MaExA")]
        public ActionResult Create()
        {
            
            return View();
        }

        //
        // POST: /Application Status/Create
        [UserFilter(Function_Id = "MaExA")]
        [HttpPost]
        public ActionResult Create(tblExtraCurricularActivity Model)
        {
             
            UserId = USession.User_Id;
            try
            {

                Connection.GDsetExtraCurricularActivity(Model.ActivityCode,Model.ActivityName, UserId, "Y");
                Connection.SaveChanges();

                //return View();

                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return Json("Exit", JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult ShowEditEventCategory(string Code)
        {

            return PartialView("AddView");
        }
        //
        // GET: /TeacherCategory/Edit/5
        [UserFilter(Function_Id = "MaExA")]
        public ActionResult Edit(string Code)
        {
             
            try{
            ExtraCurricularActivityModel TModel = new ExtraCurricularActivityModel();

            tblExtraCurricularActivity TCtable = Connection.tblExtraCurricularActivities.SingleOrDefault(x => x.ActivityCode == Code);
            TModel.IsActive = TCtable.IsActive;

            TModel.ActivityCode = TCtable.ActivityCode;
            TModel.ActivityName = TCtable.ActivityName;
           
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
        [UserFilter(Function_Id = "MaExA")]
        [HttpPost]
        public ActionResult Edit(ExtraCurricularActivityModel Model)
        {
             
            UserId = USession.User_Id;
            try
            {

                tblExtraCurricularActivity TCtable = Connection.tblExtraCurricularActivities.SingleOrDefault(x => x.ActivityCode == Model.ActivityCode);

                Connection.GDModifyExtraCurricularActivit(Model.ActivityName, Model.ActivityCode, UserId);
                Connection.SaveChanges();

                return RedirectToAction("Index");
            
            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return View();
                
            }
        }


        public ActionResult ShowDeleteCategoryModel(string Code)
        {

            return PartialView("AddView");
        }
        //
        // GET: /TeacherCategory/Delete/5
        [UserFilter(Function_Id = "MaExA")]
        public ActionResult Delete(string Code)
        {
             
            ExtraCurricularActivityModel TModel = new ExtraCurricularActivityModel();
            TModel.ActivityCode = Code;
            return PartialView("DeleteView", TModel);
        }

        //
        // POST: /TeacherCategory/Delete/5
        [UserFilter(Function_Id = "MaExA")]
        [HttpPost]
        public ActionResult Delete(ExtraCurricularActivityModel Model)
        {
             
            UserId = USession.User_Id;
            try
            {
                Connection.GDdeleteExtraCurricularActivity("N", Model.ActivityCode, UserId);
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