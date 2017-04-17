using GDWEBSolution.Models;
using GDWEBSolution.Models.HomeWork;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GDWEBSolution.Controllers
{
    public class HomeWorkController : Controller
    {
        //
        // GET: /HomeWork/
        private SchoolMGTEntitiesConnectionString Connection = new SchoolMGTEntitiesConnectionString();
        string UserId = "ADMIN";
        string SchoolId = "CKC";
        string StudentId = "";
        long TeacherId = 1;
        public ActionResult Index(string FromDate,string ToDate)
        {
            try
            {
                if (FromDate == null && ToDate == null && Session["FromDate"]!=null)
                {
                   FromDate= Session["FromDate"].ToString();
                   ToDate = Session["ToDate"].ToString();
                }
                if (FromDate != null && ToDate != null)
                {
                     Session["FromDate"]=FromDate;
                     Session["ToDate"]=ToDate;
                }
                DateTime StartDate = Convert.ToDateTime(FromDate);
                DateTime EndDate = Convert.ToDateTime(ToDate);
                string stDate = StartDate.ToString("yyyyMMdd");
                string edDate = EndDate.ToString("yyyyMMdd");
                var Grade = Connection.GDgetHomeWorkAdd(SchoolId, TeacherId, "Y", stDate, edDate);
                List<GDgetHomeWorkAdd_Result> Gradelist = Grade.ToList();

                HomeWorkModel tcm = new HomeWorkModel();

                List<HomeWorkModel> tcmlist = Gradelist.Select(x => new HomeWorkModel
                {
                    AssignmentNo = x.AssignmentNo,
                    AssignmentDescription = x.AssignmentDescription,
                    DueDate=x.DueDate,
                    SchoolId = x.SchoolId,
                    GradeId = x.GradeId,
                    ClassId = x.ClassId,
                   
                    TeacherId = x.TeacherId,
                    FilePath = x.FilePath,
                    BatchNo = x.BatchNo,
                    BatchDescription = x.BatchDescription,
                    SubjectId = x.SubjectId,
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate,


                }).ToList();



                return View(tcmlist);
            }
            catch (Exception ex) {
                Errorlog.ErrorManager.LogError(ex);
                return View();
            }
        }

        public ActionResult Details(string code)
        {
            return View();
        }

        //

        public ActionResult ShowAddView(int id)
        {

            DropDownList();

           
            return PartialView("AddView");
        }

        private void DropDownList()
        {
            var sub = Connection.GDgetAllSubject("Y");
            List<GDgetAllSubject_Result> Sublist = sub.ToList();

            ViewBag.SubjectId = new SelectList(Sublist, "SubjectId", "SubjectName");


            var Grade = Connection.GDgetAllGradeMaintenance("Y");
            List<GDgetAllGradeMaintenance_Result> Gradelist = Grade.ToList();

            ViewBag.GradeId = new SelectList(Gradelist, "GradeId", "GradeName");

            var Class = Connection.GDgetAllClass("Y");
            List<GDgetAllClass_Result> Classlist = Class.ToList();

            ViewBag.ClassId = new SelectList(Classlist, "ClassId", "ClassName");

        }

       
        [HttpPost]
        public ActionResult Create(HomeWorkModel Model)
        {
            string _path = "";
            

            try
            {
                long dueId=0;
                if (ModelState.IsValid)
                {
                    if (Model.File.ContentLength > 0)
                    {


                        string _FileName = Path.GetFileName(Model.File.FileName); //;
                        _path = Path.Combine(Server.MapPath("~/Uploads"), TeacherId.ToString() + DateTime.Now.Date.ToString("yyyyMMdd")+_FileName);
                        Model.File.SaveAs(_path);
                        string Filepath = _path;
                      
                        Connection.GDsetHomeWork(Model.AssignmentDescription, SchoolId, Model.GradeId, Model.ClassId, Filepath, TeacherId, Model.BatchNo, Model.BatchDescription, Model.SubjectId, Model.AssignmentNo, Model.DueDate, dueId, UserId, "Y");
                        Connection.SaveChanges();
                    }
                    }
                

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

        public ActionResult Edit(long Code, string dates)
        {
            DropDownList();

            HomeWorkModel TModel = new HomeWorkModel();

            tblAssignmentHeader TCtable = Connection.tblAssignmentHeaders.SingleOrDefault(x => x.AssignmentNo == Code);
            TModel.SubjectId = TCtable.SubjectId;
            TModel.GradeId = TCtable.GradeId;
            TModel.ClassId = TCtable.ClassId;
            TModel.DueDate = Convert.ToDateTime(dates);
            TModel.AssignmentDescription = TCtable.AssignmentDescription;
            TModel.BatchNo = TCtable.BatchNo;
            TModel.BatchDescription = TCtable.BatchDescription;
            TModel.FilePath = TCtable.FilePath;
           TModel.AssignmentNo=TCtable.AssignmentNo;


             
            return PartialView("EditView", TModel);
        }

        //
        // POST: /TeacherCategory/Edit/5

        [HttpPost]
        public ActionResult Edit(HomeWorkModel Model)
        {
            try
            {
                long dueId = 0;
                if (ModelState.IsValid)
                {
                    string _path = null;
                    if (Model.File != null)
                    {
                        if (Model.File.ContentLength > 0)
                        {


                            string _FileName = Path.GetFileName(Model.File.FileName); //;
                            _path = Path.Combine(Server.MapPath("~/Uploads"), TeacherId.ToString() + DateTime.Now.Date.ToString("yyyyMMddHHmmSS")+ _FileName );
                            Model.File.SaveAs(_path);
                            string Filepath = _path;
                            Connection.GDModifyHomeWork(Model.AssignmentDescription, SchoolId, Model.GradeId, Model.ClassId, Filepath, Model.BatchNo, Model.BatchDescription, Model.SubjectId, Model.AssignmentNo, Model.DueDate, dueId, UserId);                      
                            Connection.SaveChanges();
                        }
                    }
                    else {
                        Connection.GDModifyHomeWork(Model.AssignmentDescription, SchoolId, Model.GradeId, Model.ClassId, Model.FilePath, Model.BatchNo, Model.BatchDescription, Model.SubjectId, Model.AssignmentNo, Model.DueDate, dueId, UserId);
                        Connection.SaveChanges();
                    }

                }
               
               

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return View();
            }
        }


        public ActionResult ShowDeleteModel(long Code)
        {

            return PartialView("AddView");
        }
        //
        // GET: /TeacherCategory/Delete/5

        public ActionResult Delete(long Code)
        {
            HomeWorkModel TModel = new HomeWorkModel();
            TModel.AssignmentNo = Code;
            return PartialView("DeleteView", TModel);
        }

        //
        // POST: /TeacherCategory/Delete/5

        [HttpPost]
        public ActionResult Delete(HomeWorkModel Model)
        {
            try
            {
                Connection.GDDeleteHomeWork("N", Model.AssignmentNo, UserId);
                Connection.SaveChanges();


                return Json(true, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return View();
            }
        }
    }
}

