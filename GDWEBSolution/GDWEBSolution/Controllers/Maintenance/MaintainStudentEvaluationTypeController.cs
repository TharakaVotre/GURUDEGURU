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
    public class MaintainStudentEvaluationTypeController : Controller
    {
        //
        // GET: /MaintainStudentEvaluationType/

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
            Authentication("MaStE");
            try{
            var Group = Connection.GDgetAllEvaluationType("Y");
            List<GDgetAllEvaluationType_Result> Grouplist = Group.ToList();

            StudentEvaluationTypeModel tcm = new StudentEvaluationTypeModel();

            List<StudentEvaluationTypeModel> tcmlist = Grouplist.Select(x => new StudentEvaluationTypeModel
            {
              EvaluationTypeCode=x.EvaluationTypeCode,
              EvaluationTypeDesc=x.EvaluationTypeDesc,
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

        public ActionResult Create()
        {
            Authentication("MaStE");
            return View();
        }

        //
        // POST: /Application Status/Create

        [HttpPost]
        public ActionResult Create(tblEvaluationType Model)
        {
            Authentication("MaStE");
            UserId=USession.User_Id;
            try
            {

                Connection.GDsetEvaluationType(Model.EvaluationTypeDesc, UserId, "Y");
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


        public ActionResult ShowEditEventCategory(long Code)
        {

            return PartialView("AddView");
        }
        //
        // GET: /TeacherCategory/Edit/5

        public ActionResult Edit(long Code)
        {
            Authentication("MaStE");
            try{
            StudentEvaluationTypeModel TModel = new StudentEvaluationTypeModel();

            tblEvaluationType TCtable = Connection.tblEvaluationTypes.SingleOrDefault(x => x.EvaluationTypeCode == Code);
            TModel.IsActive = TCtable.IsActive;

            TModel.EvaluationTypeCode = TCtable.EvaluationTypeCode;
            TModel.EvaluationTypeDesc = TCtable.EvaluationTypeDesc;

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
        public ActionResult Edit(StudentEvaluationTypeModel Model)
        {
            Authentication("MaStE");
            UserId = USession.User_Id;
            try
            {

                tblEvaluationType TCtable = Connection.tblEvaluationTypes.SingleOrDefault(x => x.EvaluationTypeCode == Model.EvaluationTypeCode);

                Connection.GDModifyEvaluationTypee(Model.EvaluationTypeDesc, Model.EvaluationTypeCode, UserId);
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

        public ActionResult Delete(long Code)
        {
            Authentication("MaStE");
            try
            {
                StudentEvaluationTypeModel TModel = new StudentEvaluationTypeModel();
                TModel.EvaluationTypeCode = Code;
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
        public ActionResult Delete(StudentEvaluationTypeModel Model)
        {
            Authentication("MaStE");
            UserId = USession.User_Id;
            try
            {
                Connection.GDdeleteEvaluationType("N", Model.EvaluationTypeCode, UserId);
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
