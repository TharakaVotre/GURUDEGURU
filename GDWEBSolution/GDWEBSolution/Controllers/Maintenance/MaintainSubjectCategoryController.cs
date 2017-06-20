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

        public ActionResult Create()
        {
            Authentication("MaSCa");
            return View();
        }

        //
        // POST: /Application Status/Create

        [HttpPost]
        public ActionResult Create(tblSubjectCategory Model)
        {
            Authentication("MaSCa");
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

        public ActionResult Edit(int Code)
        {
            Authentication("MaSCa");
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

        [HttpPost]
        public ActionResult Edit(SubjectCategoryModel Model)
        {
            Authentication("MaSCa");
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

        public ActionResult Delete(int Code)
        {
            Authentication("MaSCa");
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

        [HttpPost]
        public ActionResult Delete(SubjectCategoryModel Model)
        {
            Authentication("MaSCa");
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
