﻿using GDWEBSolution.Models;
using GDWEBSolution.Models.HomeWork;
using GDWEBSolution.Models.User;
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
        UserSession USession = new UserSession();
        string UserId = null;
        string SchoolId = null;
       
        long TeacherId = 0;

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
                else
                {
                    UserId = USession.User_Id;
                    SchoolId = USession.School_Id; ;
                    tblTeacher teacher=Connection.tblTeachers.SingleOrDefault(a=>a.UserId==UserId);
                    if (teacher!=null)
                    TeacherId = teacher.TeacherId;
                }
            }
            else
            {
                // RedirectToAction();
                Response.Redirect("~/Home/Login");
            }
        }
        public ActionResult Index(string FromDate,string ToDate)
        {
            Authentication("HomIn");
            try
            {

                if (FromDate == null && Session["FromDate"]==null) {
                    
                    ToDate = DateTime.Now.ToShortDateString();
                    FromDate = DateTime.Now.AddMonths(-3).ToShortDateString();
                }
                else
                if (FromDate == null && ToDate == null && Session["FromDate"]!=null)
                {
                   FromDate= Session["FromDate"].ToString();
                   ToDate = Session["ToDate"].ToString();
                }else
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
                ViewBag.FromDate = FromDate;
                ViewBag.ToDate = ToDate;
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
                    Class=x.ClassName,
                    Subject=x.SubjectName,
                    ClassId = x.ClassId,
                    Grade=x.GradeName,
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
            Authentication("HomDe");
            try
            {
                DropDownList("%");
                HomeWorkModel TModel = new HomeWorkModel();

                tblAssignmentHeader TCtable = Connection.tblAssignmentHeaders.SingleOrDefault(x => x.AssignmentNo == code);
                TModel.SubjectId = TCtable.SubjectId;
                TModel.GradeId = TCtable.GradeId;
                TModel.ClassId = TCtable.ClassId;
                tblClass TClass = Connection.tblClasses.SingleOrDefault(x => x.ClassId == TCtable.ClassId && x.GradeId == TCtable.GradeId && x.SchoolId == SchoolId);
                TModel.Class = TClass.ClassName;
                tblGrade TGrade = Connection.tblGrades.SingleOrDefault(x => x.GradeId == TCtable.GradeId);
                TModel.Grade = TGrade.GradeName;
                tblSubject TSubject = Connection.tblSubjects.SingleOrDefault(x => x.SubjectId == TCtable.SubjectId);
                TModel.Subject = TSubject.SubjectName;
                TModel.stringDueDate = dates;
                TModel.AssignmentDescription = TCtable.AssignmentDescription;
                TModel.BatchNo = TCtable.BatchNo;
                TModel.BatchDescription = TCtable.BatchDescription;
                TModel.FilePath = TCtable.FilePath;
                TModel.AssignmentNo = TCtable.AssignmentNo;
                return PartialView("DetailView", TModel);
            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return View();
            }
        }

        public FileResult Download(string path)
        {
            Authentication("HomDo");
            try
            {
                string Filepath = Server.MapPath("~/Uploads/" + path);
                byte[] fileBytes = System.IO.File.ReadAllBytes(Filepath);

                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, path);
            }
            catch(Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return null;
            }
        }
        //

        public ActionResult ShowAddView(int id)
        {
            Authentication("HomIn");
            DropDownList("");

           
            return PartialView("AddView");
        }

        private void DropDownList(string id)
        {
            SchoolId = USession.School_Id;
             var Grade = Connection.GDgetSchoolGrade(SchoolId,"Y");
             List<GDgetSchoolGrade_Result> Gradelist = Grade.ToList();

            ViewBag.GradeId = new SelectList(Gradelist, "GradeId", "GradeName");
           
          
        }

       
        [HttpPost]
        public ActionResult Create(HomeWorkModel Model)
        {
            Authentication("HomIn");
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

                        Connection.GDsetHomeWork(Model.AssignmentDescription, SchoolId, Model.GradeId, Model.ClassId, filename, TeacherId, "1", "1", Model.SubjectId, Model.AssignmentNo, Model.DueDate, dueId, UserId, "Y");
                        Connection.SaveChanges();
                    }
                    }
                

                //return View();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return View();
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
            Authentication("HomIn");
            try
            {
                HomeWorkModel TModel = new HomeWorkModel();

                tblAssignmentHeader TCtable = Connection.tblAssignmentHeaders.SingleOrDefault(x => x.AssignmentNo == Code);
                TModel.SubjectId = TCtable.SubjectId;
                TModel.GradeId = TCtable.GradeId;
                TModel.ClassId = TCtable.ClassId;
                tblClass TClass = Connection.tblClasses.SingleOrDefault(x => x.ClassId == TCtable.ClassId && x.GradeId == TCtable.GradeId && x.SchoolId == SchoolId);
                TModel.Class = TClass.ClassName;
                tblGrade TGrade = Connection.tblGrades.SingleOrDefault(x => x.GradeId == TCtable.GradeId);
                TModel.Grade = TGrade.GradeName;
                tblSubject TSubject = Connection.tblSubjects.SingleOrDefault(x => x.SubjectId == TCtable.SubjectId);
                TModel.Subject = TSubject.SubjectName;
                TModel.stringDueDate = dates;
                TModel.AssignmentDescription = TCtable.AssignmentDescription;
                TModel.BatchNo = TCtable.BatchNo;
                TModel.BatchDescription = TCtable.BatchDescription;
                TModel.FilePath = TCtable.FilePath;
                TModel.AssignmentNo = TCtable.AssignmentNo;


                DropDownList(TCtable.GradeId);
                var Subject = Connection.GDgetGradeActiveSubject(TCtable.GradeId, "Y"); ;
                List<GDgetGradeActiveSubject_Result> Subjectlist = Subject.ToList();
                ViewBag.SubjectId = new SelectList(Subjectlist, "SubjectId", "SubjectName");
                ViewBag.SubjectIdtxt = TCtable.SubjectId;
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
        public ActionResult Edit(HomeWorkModel Model)
        {
            Authentication("HomIn");
            try
            {
                long dueId = 0;
                if (ModelState.IsValid)
                {
                    DateTime duedate = Convert.ToDateTime(Model.stringDueDate);
                    string _path = null;
                    if (Model.File != null)
                    {
                        if (Model.File.ContentLength > 0)
                        {


                            string _FileName = Path.GetFileName(Model.File.FileName); //;
                            string filename = TeacherId.ToString() + DateTime.Now.Date.ToString("yyyyMMddHHmmSS") + _FileName;
                            _path = Path.Combine(Server.MapPath("~/Uploads"), filename );
                            Model.File.SaveAs(_path);

                            Connection.GDModifyHomeWork(Model.AssignmentDescription, SchoolId, Model.GradeId, Model.ClassId, filename,"1", "1", Model.SubjectId, Model.AssignmentNo, duedate, dueId, UserId);                      
                            Connection.SaveChanges();
                        }
                    }
                    else {
                        Connection.GDModifyHomeWork(Model.AssignmentDescription, SchoolId, Model.GradeId, Model.ClassId, Model.FilePath, "1","1", Model.SubjectId, Model.AssignmentNo, duedate, dueId, UserId);
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
            Authentication("HomIn");
            HomeWorkModel TModel = new HomeWorkModel();
            TModel.AssignmentNo = Code;
            return PartialView("DeleteView", TModel);
        }

        //
        // POST: /TeacherCategory/Delete/5

        [HttpPost]
        public ActionResult Delete(HomeWorkModel Model)
        {
            Authentication("HomIn");
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
            Authentication("HomPa");
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
                    FromDate = DateTime.Now.AddMonths(-3).ToShortDateString();
                    ToDate = DateTime.Now.ToShortDateString();
                }
                ViewBag.FromDate = FromDate;
                ViewBag.ToDate = ToDate;
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
                        Class=x.ClassName,
                        Grade=x.GradeName,
                        TeacherId = x.TeacherId,
                        FilePath = x.FilePath,
                        BatchNo = x.BatchNo,
                        BatchDescription = x.BatchDescription,
                        SubjectId = x.SubjectId,
                        Subject=x.SubjectName,
                        CreatedBy = x.CreatedBy,
                        CreatedDate = x.CreatedDate,


                    }).ToList();
                    return View(tcmlist);
               

            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return View();
            }
        }


        public JsonResult getClass(string id)
        {
            SchoolId = USession.School_Id;
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

