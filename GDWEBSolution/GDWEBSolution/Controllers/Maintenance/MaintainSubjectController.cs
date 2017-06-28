using GDWEBSolution.Filters;
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
    public class MaintainSubjectController : Controller
    {
        //
        // GET: /MaintainSubject/

        private SchoolMGTEntitiesConnectionString Connection = new SchoolMGTEntitiesConnectionString();
        UserSession USession = new UserSession();
        string UserId = null;

      [UserFilter(Function_Id = "Masub")]
        public ActionResult Index()
        {
            
            try
            {
                var Group = Connection.GDgetAllSubject("Y");
                List<GDgetAllSubject_Result> Grouplist = Group.ToList();

                SubjectModel tcm = new SubjectModel();

                List<SubjectModel> tcmlist = Grouplist.Select(x => new SubjectModel
                {
                    SubjectId = x.SubjectId,
                    ShortName = x.ShortName,
                    SubjectName = x.SubjectName,
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
        [UserFilter(Function_Id = "Masub")]
        public ActionResult Create()
        {
            
            return View();
        }

        //
        // POST: /Application Status/Create
        [UserFilter(Function_Id = "Masub")]
        [HttpPost]
        public ActionResult Create(tblSubject Model)
        {
           
            UserId = USession.User_Id;
            try
            {

                Connection.GDsetSubject(Model.ShortName,Model.SubjectName, UserId, "Y");
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
        [UserFilter(Function_Id = "Masub")]
        public ActionResult Edit(int Code)
        {
            
            try{
            SubjectModel TModel = new SubjectModel();

            tblSubject TCtable = Connection.tblSubjects.SingleOrDefault(x => x.SubjectId == Code);
            TModel.IsActive = TCtable.IsActive;

            TModel.SubjectId = TCtable.SubjectId;
            TModel.ShortName = TCtable.ShortName;
            TModel.SubjectName = TCtable.SubjectName;

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
        [UserFilter(Function_Id = "Masub")]
        [HttpPost]
        public ActionResult Edit(SubjectModel Model)
        {
            
            UserId = USession.User_Id;
            try
            {

                tblSubject TCtable = Connection.tblSubjects.SingleOrDefault(x => x.SubjectId == Model.SubjectId);

                Connection.GDModifySubject(Model.ShortName,Model.SubjectName, Model.SubjectId, UserId);
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
        [UserFilter(Function_Id = "Masub")]
        public ActionResult Delete(int Code)
        {
            

            try{
            SubjectModel TModel = new SubjectModel();
            TModel.SubjectId = Code;
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
        [UserFilter(Function_Id = "Masub")]
        [HttpPost]
        public ActionResult Delete(SubjectModel Model)
        {
            
            UserId = USession.User_Id;
            try
            {
                Connection.GDdeleteSubject("N", Model.SubjectId, UserId);
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

