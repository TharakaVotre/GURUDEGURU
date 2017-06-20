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
    public class MaintainQulificationController : Controller
    {
        //
        // GET: /MaintainQulification/

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
            Authentication("MaQul");
            try
            {
                var Qualification = Connection.GDgetAllQualification("Y");
                List<GDgetAllQualification_Result> Qualificationlist = Qualification.ToList();

                QulificationModel tcm = new QulificationModel();

                List<QulificationModel> tcmlist = Qualificationlist.Select(x => new QulificationModel
                {
                    QualificationId = x.QualificationId,
                    QualificationName = x.QualificationName,
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
            Authentication("MaQul");
            return View();
        }

        //
        // POST: /Application Status/Create

        [HttpPost]
        public ActionResult Create(tblQualification Model)
        {
            Authentication("MaQul");
            UserId = USession.User_Id;
            try
            {

                Connection.GDsetQualification(Model.QualificationName, UserId, "Y");
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
            Authentication("MaQul");
            
            try{
            QulificationModel TModel = new QulificationModel();

            tblQualification TCtable = Connection.tblQualifications.SingleOrDefault(x => x.QualificationId == Code);
            TModel.IsActive = TCtable.IsActive;

            TModel.QualificationId = TCtable.QualificationId;
            TModel.QualificationName = TCtable.QualificationName;

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
        public ActionResult Edit(QulificationModel Model)
        {
            Authentication("MaQul");
            UserId = USession.User_Id;
            try
            {

                tblQualification TCtable = Connection.tblQualifications.SingleOrDefault(x => x.QualificationId == Model.QualificationId);

                Connection.GDModifyQualification(Model.QualificationName, Model.QualificationId, UserId);
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
            Authentication("MaQul");
            
            try
            {
                QulificationModel TModel = new QulificationModel();
                TModel.QualificationId = Code;
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
        public ActionResult Delete(QulificationModel Model)
        {
            Authentication("MaQul");
            UserId = USession.User_Id;
            try
            {
                Connection.GDdeleteQualification("N", Model.QualificationId, UserId);
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


