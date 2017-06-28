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
    public class MaintainSubjectCategoryController : Controller
    {
        //
        // GET: /MaintainSubjectCategory/
        private SchoolMGTEntitiesConnectionString Connection = new SchoolMGTEntitiesConnectionString();
        UserSession USession = new UserSession();
        string UserId = null;

       [UserFilter(Function_Id = "MaSCa")]
        public ActionResult Index()
        {
             
            try
            {
                var Group = Connection.GDgetAllSubjectCategory("Y");
                List<GDgetAllSubjectCategory_Result> Grouplist = Group.ToList();

                SubjectCategoryModel tcm = new SubjectCategoryModel();

                List<SubjectCategoryModel> tcmlist = Grouplist.Select(x => new SubjectCategoryModel
                {
                    SubjectCategoryId = x.SubjectCategoryId,
                    SubjectCategoryName = x.SubjectCategoryName,
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
[UserFilter(Function_Id = "MaSCa")]
        public ActionResult Create()
        {
             
            return View();
        }

        //
        // POST: /Application Status/Create
        [UserFilter(Function_Id = "MaSCa")]
        [HttpPost]
        public ActionResult Create(tblSubjectCategory Model)
        {
             
            UserId = USession.User_Id;
            try
            {

                Connection.GDsetSubjectCategory(Model.SubjectCategoryName, UserId, "Y");
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


        public ActionResult ShowEditEventCategory(int Code)
        {

            return PartialView("AddView");
        }
        //
        // GET: /TeacherCategory/Edit/5
        [UserFilter(Function_Id = "MaSCa")]
        public ActionResult Edit(int Code)
        {
             
            try{
            SubjectCategoryModel TModel = new SubjectCategoryModel();

            tblSubjectCategory TCtable = Connection.tblSubjectCategories.SingleOrDefault(x => x.SubjectCategoryId == Code);
            TModel.IsActive = TCtable.IsActive;

            TModel.SubjectCategoryId = TCtable.SubjectCategoryId;
            TModel.SubjectCategoryName = TCtable.SubjectCategoryName;

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
        [UserFilter(Function_Id = "MaSCa")]
        [HttpPost]
        public ActionResult Edit(SubjectCategoryModel Model)
        {
            
            UserId = USession.User_Id;
            try
            {

                tblSubjectCategory TCtable = Connection.tblSubjectCategories.SingleOrDefault(x => x.SubjectCategoryId == Model.SubjectCategoryId);

               Connection.GDModifySubjectCategory(Model.SubjectCategoryName, Model.SubjectCategoryId, UserId);
                Connection.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return View();
               
            }
        }


        public ActionResult ShowDeleteCategoryModel(long Code)
        {

            return PartialView("AddView");
        }
        //
        // GET: /TeacherCategory/Delete/5
        [UserFilter(Function_Id = "MaSCa")]
        public ActionResult Delete(int Code)
        {
             
            try{
            SubjectCategoryModel TModel = new SubjectCategoryModel();
            TModel.SubjectCategoryId = Code;
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
        [UserFilter(Function_Id = "MaSCa")]
        [HttpPost]
        public ActionResult Delete(SubjectCategoryModel Model)
        {
            
            UserId = USession.User_Id;
            try
            {
                Connection.GDdeleteSubjectCategory("N", Model.SubjectCategoryId, UserId);
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
