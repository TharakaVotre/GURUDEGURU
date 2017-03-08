using GDWEBSolution.Models;
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
            var Grade = Connection.GDgetAllGradeMaintenance("Y");
            List<GDgetAllGradeMaintenance_Result> Gradelist = Grade.ToList();

            GDgetAllGradeMaintenance_Result tcm = new GDgetAllGradeMaintenance_Result();

            List<GDgetAllGradeMaintenance_Result> tcmlist = Gradelist.Select(x => new GDgetAllGradeMaintenance_Result
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

            GDgetAllGradeMaintenance_Result TModel = new GDgetAllGradeMaintenance_Result();

            tblGrade TCtable = Connection.tblGrades.SingleOrDefault(x => x.GradeId == Code);
            TModel.IsActive = TCtable.IsActive;

            TModel.GradeId = TCtable.GradeId;
            TModel.GradeName = TCtable.GradeName;

            return PartialView("EditView", TModel);
        }

        //
        // POST: /TeacherCategory/Edit/5

        [HttpPost]
        public ActionResult Edit(GDgetAllGradeMaintenance_Result Model)
        {
            try
            {

                tblGrade TCtable = Connection.tblGrades.SingleOrDefault(x => x.GradeId == Model.GradeId);

                Connection.GDModifyGrade(Model.GradeName, Model.GradeId, UserId);
                Connection.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
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
            GDgetAllGradeMaintenance_Result TModel = new GDgetAllGradeMaintenance_Result();
            TModel.GradeId = Code;
            return PartialView("DeleteView", TModel);
        }

        //
        // POST: /TeacherCategory/Delete/5

        [HttpPost]
        public ActionResult Delete(GDgetAllGradeMaintenance_Result Model)
        {
            try
            {
                Connection.GDdeleteGradeMaintenance("N", Model.GradeId, UserId);
                Connection.SaveChanges();


                return Json(true, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
