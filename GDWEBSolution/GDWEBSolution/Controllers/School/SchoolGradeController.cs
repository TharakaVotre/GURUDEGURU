using GDWEBSolution.Models;
using GDWEBSolution.Models.Schools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GDWEBSolution.Controllers.School
{
    public class SchoolGradeController : Controller
    {
        //
        // GET: /SchoolGrade/

        string UserId = "User1";
        //
        // GET: /School/
        SchoolMGTEntitiesConnectionString Connection = new SchoolMGTEntitiesConnectionString();

        public ActionResult SchoolGradeIndex()
        {
            var school = Connection.SMGTgetAllSchoolGrades("Y");
            List<SMGTgetAllSchoolGrades_Result> Categorylist = school.ToList();
            SchoolGradeModel schl = new SchoolGradeModel();
            List<SchoolGradeModel> tcmlist = Categorylist.Select(x => new SchoolGradeModel
            {
                SchoolId = x.SchoolId,
                GradeId  =x.GradeId,
                IsActive=x.IsActive,
                CreatedBy=x.CreatedBy,
                CreatedDate=x.CreatedDate,
                ModifiedBy = x.ModifiedBy,
                ModifiedDate = x.ModifiedDate

            }).ToList();



            return View(tcmlist);
        }

        //
        // GET: /SchoolGrade/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /SchoolGrade/Create

        public ActionResult SchoolGradeCreate()
        {
            List<tblGrade> Gradelist = Connection.tblGrades.ToList();
            ViewBag.SchoolGradeDrpDown = new SelectList(Gradelist, "GradeId", "GradeName");
            List<tblSchool> Schoollist = Connection.tblSchools.ToList();
            ViewBag.SchoolDrpDown = new SelectList(Schoollist, "SchoolId", "SchoolName");



            return PartialView("SchoolGradeCreate");
        }

        //
        // POST: /SchoolGrade/Create

        [HttpPost]
        public ActionResult Create(SchoolGradeModel Model)
        {
            try
            {
                Model.IsActive = "Y";

                Connection.SMGTsetSchoolGrade(Model.SchoolId, Model.GradeId, UserId, Model.IsActive);

                //Connection.DCISsetSchoolGrade(Model.SchoolId, Model.SchoolGroup, Model.SchoolName, Model.SchoolRank, "Y", Model.Division,
                //    Model.District, Model.Division, UserId, Model.Address1, Model.Address2, Model.Address3, "email.asp", Model.Fax, "", Convert.ToInt16(Model.MinuteforPeriod), Model.Telephone, Model.SchoolCategory, Model.Province
                //    );
                Connection.SaveChanges();

                //return View();

                return RedirectToAction("SChoolGradeIndex");
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

                        Errorlog.ErrorManager.LogError(dbEx);

                    }
                }
                throw raise;
            }






        }

        //
        // GET: /SchoolGrade/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /SchoolGrade/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /SchoolGrade/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /SchoolGrade/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
