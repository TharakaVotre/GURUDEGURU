using GDWEBSolution.Models;
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
        //
        // GET: /TeacherCategory/
        string UserId = "ADMIN";
        public ActionResult Index()
        {
            var status=Connection.GDgetAllApplicationStatus("Y",0);
            List<GDgetAllApplicationStatus_Result> Categorylist = status.ToList();

            GDgetAllApplicationStatus_Result tcm = new GDgetAllApplicationStatus_Result();

            List<GDgetAllApplicationStatus_Result> tcmlist = Categorylist.Select(x => new GDgetAllApplicationStatus_Result
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

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Application Status/Create

        [HttpPost]
        public ActionResult Create(tblApplicationStatu Model)
        {
            try
            {

                Connection.GDsetApplicationStatus(Model.StatusDescription, UserId, "Y");
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


        public ActionResult ShowEditApplicationStatus(long CategoryID)
        {
            return PartialView("AddApplicationStatus");
        }
        //
        // GET: /TeacherCategory/Edit/5

        public ActionResult Edit(long typeId)
        {
            GDgetAllApplicationStatus_Result TModel = new GDgetAllApplicationStatus_Result();

            tblApplicationStatu TCtable = Connection.tblApplicationStatus.SingleOrDefault(x => x.StatusCode == typeId);
            TModel.IsActive = TCtable.IsActive;

            TModel.StatusDescription = TCtable.StatusDescription;
            TModel.StatusCode = TCtable.StatusCode;

            return PartialView("EditApplicationStatus", TModel);
        }

        //
        // POST: /TeacherCategory/Edit/5

        [HttpPost]
        public ActionResult Edit(GDgetAllApplicationStatus_Result Model)
        {
            try
            {

                tblApplicationStatu TCtable = Connection.tblApplicationStatus.SingleOrDefault(x => x.StatusCode == Model.StatusCode);

                
                Connection.GDModifyAllApplicationStatus(Model.StatusDescription,Model.StatusCode,UserId);
                Connection.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult ShowDeleteCategoryModel(long TypeID)
        {
            return PartialView("AddApplicationStatus");
        }
        //
        // GET: /TeacherCategory/Delete/5

        public ActionResult Delete(long CategoryId)
        {
            GDgetAllApplicationStatus_Result TModel = new GDgetAllApplicationStatus_Result();
            TModel.StatusCode = CategoryId;
            return PartialView("DeleteApplicationStatus", TModel);
        }

        //
        // POST: /TeacherCategory/Delete/5

        [HttpPost]
        public ActionResult Delete(GDgetAllApplicationStatus_Result Model)
        {
            try
            {
                Connection.GDdeleteAllApplicationStatus("N", Model.StatusCode, UserId);
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