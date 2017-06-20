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
    public class MaintainSchoolRankController : Controller
    {
        //
        // GET: /MaintainSchoolRank/

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
            Authentication("MaSRa");
            try
            {
                var Group = Connection.GDgetAllSchoolRank("Y");
                List<GDgetAllSchoolRank_Result> Grouplist = Group.ToList();

                SchoolRankModel tcm = new SchoolRankModel();

                List<SchoolRankModel> tcmlist = Grouplist.Select(x => new SchoolRankModel
                {
                    SchoolRankId = x.SchoolRankId,
                    SchoolRankName = x.SchoolRankName,
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
            Authentication("MaSRa");
            return View();
        }

        //
        // POST: /Application Status/Create

        [HttpPost]
        public ActionResult Create(tblSchoolRank Model)
        {
            Authentication("MaSRa");
            UserId = USession.User_Id;
            try
            {

                Connection.GDsetSchoolRank(Model.SchoolRankName, UserId, "Y");
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
            Authentication("MaSRa");
           
            try
            {
                SchoolRankModel TModel = new SchoolRankModel();

                tblSchoolRank TCtable = Connection.tblSchoolRanks.SingleOrDefault(x => x.SchoolRankId == Code);
                TModel.IsActive = TCtable.IsActive;

                TModel.SchoolRankId = TCtable.SchoolRankId;
                TModel.SchoolRankName = TCtable.SchoolRankName;

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
        public ActionResult Edit(SchoolRankModel Model)
        {
            Authentication("MaSRa");
            UserId = USession.User_Id;
            try
            {

                tblSchoolRank TCtable = Connection.tblSchoolRanks.SingleOrDefault(x => x.SchoolRankId == Model.SchoolRankId);

                Connection.GDModifySchoolRank(Model.SchoolRankName, Model.SchoolRankId, UserId);
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
            Authentication("MaSRa");
            try{
            SchoolRankModel TModel = new SchoolRankModel();
            TModel.SchoolRankId = Code;
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
        public ActionResult Delete(SchoolRankModel Model)
        {
            Authentication("MaSRa");
            UserId = USession.User_Id;
            try
            {
                Connection.GDdeleteSchoolRank("N", Model.SchoolRankId, UserId);
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
