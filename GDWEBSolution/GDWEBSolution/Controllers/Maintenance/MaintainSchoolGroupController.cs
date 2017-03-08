using GDWEBSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GDWEBSolution.Controllers
{
    public class MaintainSchoolGroupController : Controller
    {
        //
        // GET: /MaintainSchoolGroup/

        private SchoolMGTEntitiesConnectionString Connection = new SchoolMGTEntitiesConnectionString();
        string UserId = "ADMIN";
        public ActionResult Index()
        {
            var Group = Connection.GDgetAllSchoolGroup("Y");
            List<GDgetAllSchoolGroup_Result> Grouplist = Group.ToList();

            GDgetAllSchoolGroup_Result tcm = new GDgetAllSchoolGroup_Result();

            List<GDgetAllSchoolGroup_Result> tcmlist = Grouplist.Select(x => new GDgetAllSchoolGroup_Result
            {
                GroupId = x.GroupId,
                GroupName = x.GroupName,
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
            return View();
        }

        //
        // POST: /Application Status/Create

        [HttpPost]
        public ActionResult Create(tblSchoolGroup Model)
        {
            try
            {

                Connection.GDsetSchoolGroup(Model.GroupName, UserId, "Y");
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


        public ActionResult ShowEditEventCategory(long Code)
        {

            return PartialView("AddView");
        }
        //
        // GET: /TeacherCategory/Edit/5

        public ActionResult Edit(long Code)
        {

            GDgetAllSchoolGroup_Result TModel = new GDgetAllSchoolGroup_Result();

            tblSchoolGroup TCtable = Connection.tblSchoolGroups.SingleOrDefault(x => x.GroupId == Code);
            TModel.IsActive = TCtable.IsActive;

            TModel.GroupId = TCtable.GroupId;
            TModel.GroupName = TCtable.GroupName;

            return PartialView("EditView", TModel);
        }

        //
        // POST: /TeacherCategory/Edit/5

        [HttpPost]
        public ActionResult Edit(GDgetAllSchoolGroup_Result Model)
        {
            try
            {

                tblSchoolGroup TCtable = Connection.tblSchoolGroups.SingleOrDefault(x => x.GroupId == Model.GroupId);

                Connection.GDModifySchoolGroup(Model.GroupName, Model.GroupId, UserId);
                Connection.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
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
            GDgetAllSchoolGroup_Result TModel = new GDgetAllSchoolGroup_Result();
            TModel.GroupId = Code;
            return PartialView("DeleteView", TModel);
        }

        //
        // POST: /TeacherCategory/Delete/5

        [HttpPost]
        public ActionResult Delete(GDgetAllSchoolGroup_Result Model)
        {
            try
            {
                Connection.GDdeleteSchoolGroup("N", Model.GroupId, UserId);
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
