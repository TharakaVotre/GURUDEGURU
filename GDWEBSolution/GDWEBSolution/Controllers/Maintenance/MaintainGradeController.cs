using GDWEBSolution.Models;
using GDWEBSolution.Models.Maintenance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GDWEBSolution.Controllers
{
    public class MaintainGradeController : Controller
    {
        //
        // GET: /MaintainGrade/

        private SchoolMGTEntitiesConnectionString Connection = new SchoolMGTEntitiesConnectionString();
        string UserId = "ADMIN";
        public ActionResult Index()
        {
            try
            {
                var Grade = Connection.GDgetAllGradeMaintenance("Y");
                List<GDgetAllGradeMaintenance_Result> Gradelist = Grade.ToList();

                GradeModel tcm = new GradeModel();

                List<GradeModel> tcmlist = Gradelist.Select(x => new GradeModel
                {
                    GradeId = x.GradeId,
                    GradeName = x.GradeName,
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

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Application Status/Create

        [HttpPost]
        public ActionResult Create(tblGrade Model)
        {
            try
            {

                Connection.GDsetGrade(Model.GradeId, Model.GradeName, UserId, "Y");
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


        public ActionResult ShowEdit(string Code)
        {

            return PartialView("AddView");
        }
        //
        // GET: /TeacherCategory/Edit/5

        public ActionResult Edit(string Code)
        {
            try
            {
                GradeModel TModel = new GradeModel();

                tblGrade TCtable = Connection.tblGrades.SingleOrDefault(x => x.GradeId == Code);
                TModel.IsActive = TCtable.IsActive;

                TModel.GradeId = TCtable.GradeId;
                TModel.GradeName = TCtable.GradeName;

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
        public ActionResult Edit(GradeModel Model)
        {
            try
            {

                tblGrade TCtable = Connection.tblGrades.SingleOrDefault(x => x.GradeId == Model.GradeId);

                Connection.GDModifyGrade(Model.GradeName, Model.GradeId, UserId);
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

        public ActionResult Delete(string Code)
        {
            try
            {
                GradeModel TModel = new GradeModel();
                TModel.GradeId = Code;
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
        public ActionResult Delete(GradeModel Model)
        {
            try
            {
                Connection.GDdeleteGradeMaintenance("N", Model.GradeId, UserId);
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
