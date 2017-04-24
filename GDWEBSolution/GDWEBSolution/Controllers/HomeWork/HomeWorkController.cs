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
        string UserId = "kamalasiri";
        string SchoolId = "CKC";
       
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
                if (FromDate != "" && ToDate!="")
                {
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
                    GradeId = x.GradeName,
                    Class=x.ClassName,
                    Subject=x.SubjectName,
                    ClassId = x.ClassId,
                   Grade=x.GradeId,
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
                return View();
                
            }
            catch (Exception ex) {
                Errorlog.ErrorManager.LogError(ex);
                return View();
            }
        }

        public ActionResult Details(long code, string dates)
        {
            DropDownList("%");
            HomeWorkModel TModel = new HomeWorkModel();

            tblAssignmentHeader TCtable = Connection.tblAssignmentHeaders.SingleOrDefault(x => x.AssignmentNo == code);
            TModel.SubjectId = TCtable.SubjectId;
            TModel.GradeId = TCtable.GradeId;
            TModel.ClassId = TCtable.ClassId;
            TModel.DueDate = Convert.ToDateTime(dates);
            TModel.AssignmentDescription = TCtable.AssignmentDescription;
            TModel.BatchNo = TCtable.BatchNo;
            TModel.BatchDescription = TCtable.BatchDescription;
            TModel.FilePath = TCtable.FilePath;
            TModel.AssignmentNo = TCtable.AssignmentNo;
            return PartialView("DetailView", TModel);
        }

        public FileResult Download(string path)
        {
            string Filepath =Server.MapPath("~/Uploads/" + path);
            byte[] fileBytes = System.IO.File.ReadAllBytes(Filepath);
            
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, path);

        }
        //

        public ActionResult ShowAddView(int id)
        {

            DropDownList("");

           
            return PartialView("AddView");
        }

        private void DropDownList(string id)
        {
             var Grade = Connection.GDgetAllGradeMaintenance("Y");
            List<GDgetAllGradeMaintenance_Result> Gradelist = Grade.ToList();

            ViewBag.GradeId = new SelectList(Gradelist, "GradeId", "GradeName");
            if(id!=""){
            var Class = Connection.GDgetGradeActiveClass(id,SchoolId,"Y");
            List<GDgetGradeActiveClass_Result> Classlist = Class.ToList();

            ViewBag.ClassId = new SelectList(Classlist, "ClassId", "ClassName");

            var Subject = Connection.GDgetGradeActiveSubject(id, "Y");
            List<GDgetGradeActiveSubject_Result> Subjectlist = Subject.ToList();

            ViewBag.SubjectId = new SelectList(Subjectlist, "SubjectId", "SubjectName");
            }
          
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
                        string filename= TeacherId.ToString() + DateTime.Now.Date.ToString("yyyyMMdd")+_FileName;
                        _path = Path.Combine(Server.MapPath("~/Uploads"), filename);
                        Model.File.SaveAs(_path);
                        string Filepath = _path;

                        Connection.GDsetHomeWork(Model.AssignmentDescription, SchoolId, Model.GradeId, Model.ClassId, filename, TeacherId, Model.BatchNo, Model.BatchDescription, Model.SubjectId, Model.AssignmentNo, Model.DueDate, dueId, UserId, "Y");
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

           DropDownList(TCtable.GradeId);
             
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
                            string filename = TeacherId.ToString() + DateTime.Now.Date.ToString("yyyyMMddHHmmSS") + _FileName;
                            _path = Path.Combine(Server.MapPath("~/Uploads"), filename );
                            Model.File.SaveAs(_path);

                            Connection.GDModifyHomeWork(Model.AssignmentDescription, SchoolId, Model.GradeId, Model.ClassId, filename, Model.BatchNo, Model.BatchDescription, Model.SubjectId, Model.AssignmentNo, Model.DueDate, dueId, UserId);                      
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



        public ActionResult ParentView(string FromDate, string ToDate)
        {
            try
            {
                if (FromDate == null && ToDate == null && Session["FromDate"] != null)
                {
                    FromDate = Session["FromDate"].ToString();
                    ToDate = Session["ToDate"].ToString();
                }
                if (FromDate != null && ToDate != null)
                {
                    Session["FromDate"] = FromDate;
                    Session["ToDate"] = ToDate;
                }
                if (FromDate != "" && ToDate != "")
                {
                    DateTime StartDate = Convert.ToDateTime(FromDate);
                    DateTime EndDate = Convert.ToDateTime(ToDate);
                    string stDate = StartDate.ToString("yyyyMMdd");
                    string edDate = EndDate.ToString("yyyyMMdd");
                    var Grade = Connection.GDgetParentHomeWork(SchoolId, stDate, edDate,UserId,"Y");
                    List<GDgetParentHomeWork_Result> Gradelist = Grade.ToList();

                    HomeWorkModel tcm = new HomeWorkModel();

                    List<HomeWorkModel> tcmlist = Gradelist.Select(x => new HomeWorkModel
                    {
                        AssignmentNo = x.AssignmentNo,
                        AssignmentDescription = x.AssignmentDescription,
                        DueDate = x.DueDate,
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
                return View();

            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return View();
            }
        }


        public JsonResult getClass(string id)
        {
            var states = Connection.GDgetGradeActiveClass(id,SchoolId,"Y");
            List<SelectListItem> listates = new List<SelectListItem>();

            listates.Add(new SelectListItem { Text = "", Value = "" });
            if (states != null)
            {
                foreach (var x in states)
                {
                    listates.Add(new SelectListItem { Text = x.ClassName, Value = x.ClassId });

                }

            }


            return Json(new SelectList(listates, "Value", "Text", JsonRequestBehavior.AllowGet));
        }

      
        public JsonResult getSubject(string id)
        {
           
            var states = Connection.GDgetGradeActiveSubject(id,"Y"); ;
            List<SelectListItem> listates = new List<SelectListItem>();

          
            listates.Add(new SelectListItem { Text = "", Value = "" });
            if (states != null)
            {
                foreach (var x in states)
                {
                    listates.Add(new SelectListItem { Text = x.SubjectName, Value = x.SubjectId.ToString() });

                }



            }


            return Json(new SelectList(listates, "Value", "Text", JsonRequestBehavior.AllowGet));
        }  



    }
}

