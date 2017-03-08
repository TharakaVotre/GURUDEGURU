using GDWEBSolution.Models;
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
        string UserId = "ADMIN";
        public ActionResult Index()
        {
            var Group = Connection.GDgetAllSchoolRank("Y");
            List<GDgetAllSchoolRank_Result> Grouplist = Group.ToList();

            GDgetAllSchoolRank_Result tcm = new GDgetAllSchoolRank_Result();

            List<GDgetAllSchoolRank_Result> tcmlist = Grouplist.Select(x => new GDgetAllSchoolRank_Result
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
            return View();
        }

        //
        // POST: /Application Status/Create

        [HttpPost]
        public ActionResult Create(tblSchoolRank Model)
        {
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

            GDgetAllSchoolRank_Result TModel = new GDgetAllSchoolRank_Result();

            tblSchoolRank TCtable = Connection.tblSchoolRanks.SingleOrDefault(x => x.SchoolRankId == Code);
            TModel.IsActive = TCtable.IsActive;

            TModel.SchoolRankId = TCtable.SchoolRankId;
            TModel.SchoolRankName = TCtable.SchoolRankName;

            return PartialView("EditView", TModel);
        }

        //
        // POST: /TeacherCategory/Edit/5

        [HttpPost]
        public ActionResult Edit(GDgetAllSchoolRank_Result Model)
        {
            try
            {

                tblSchoolRank TCtable = Connection.tblSchoolRanks.SingleOrDefault(x => x.SchoolRankId == Model.SchoolRankId);

                Connection.GDModifySchoolRank(Model.SchoolRankName, Model.SchoolRankId, UserId);
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

        public ActionResult Delete(int Code)
        {
            GDgetAllSchoolRank_Result TModel = new GDgetAllSchoolRank_Result();
            TModel.SchoolRankId = Code;
            return PartialView("DeleteView", TModel);
        }

        //
        // POST: /TeacherCategory/Delete/5

        [HttpPost]
        public ActionResult Delete(GDgetAllSchoolRank_Result Model)
        {
            try
            {
                Connection.GDdeleteSchoolRank("N", Model.SchoolRankId, UserId);
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
