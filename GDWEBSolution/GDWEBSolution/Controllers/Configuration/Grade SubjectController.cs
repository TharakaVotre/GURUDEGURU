using GDWEBSolution.Models;
using GDWEBSolution.Models.Configuration;
using GDWEBSolution.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GDWEBSolution.Controllers.Configuration
{
    
    public class Grade_SubjectController : Controller
    {
       
        //
        // GET: /Grade Subject/
        SchoolMGTEntitiesConnectionString Connection = new SchoolMGTEntitiesConnectionString();
        UserSession USession = new UserSession();
        string UserId = null;
        string SchoolId = null;
        string Accyear = null;
        string gradeid = null;

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
               
            }
            else
            {
                // RedirectToAction();
                Response.Redirect("~/Home/Login");
            }
        }
        public ActionResult Index()
        {
            Authentication("GSub");
            ViewBag.Message = false;
            if (Session["ErrorMessage"] != null) { 
            string msg=Session["ErrorMessage"].ToString();
            if (msg == "True")
            {
                ViewBag.Message = true;
                Session["ErrorMessage"] = false;
            }
            else
            {
                ViewBag.Message = false;
            }
            }
            Dropdownlistdata("","");

            var Group = Connection.GDgetAllSubject("Y");
            List<GDgetAllSubject_Result> Grouplist = Group.ToList();

            Grade_SubjectModel tcm = new Grade_SubjectModel();

            List<Grade_SubjectModel> tcmlist = Grouplist.Select(x => new Grade_SubjectModel
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



            return PartialView(tcmlist);

           // return View();
        }

        private void Dropdownlistdata(string AcademicYear,string GradeId)
        {

            if (AcademicYear != null)
            {
                Session["Accyear"] = AcademicYear;
                Accyear = AcademicYear;
            }
            else
            {
                if (Session["Accyear"]!=null)
                {
                    Accyear = Session["Accyear"].ToString();
                }
            }
            if (GradeId != null)
            {
                Session["gradeId"] = GradeId;
                gradeid = GradeId;
            }
            else
            {
                if (Session["Accyear"] != null)
                {
                    gradeid = Session["gradeId"].ToString();
                }
            }
            List<SelectListItem> Optionallist = new List<SelectListItem>();
            Optionallist.Add(new SelectListItem
            {
                Text = "Compulsary",
                Value = "N"
            });
            Optionallist.Add(new SelectListItem
            {
                Text = "Optional",
                Value = "Y",
            });


            SchoolId = USession.School_Id;
            var Grade = Connection.GDgetSchoolGrade(SchoolId,"Y");
            List<GDgetSchoolGrade_Result> Gradelist = Grade.ToList();

            var SubjectCategory = Connection.GDgetAllSubjectCategory("Y");
            List<GDgetAllSubjectCategory_Result> SubjectCategorylist = SubjectCategory.ToList();

            ViewBag.AcademicYear=("2017");
            if (AcademicYear != "")
            {
                ViewBag.GradeId = AcademicYear;
            }
            ViewBag.SubjectCategoryId = new SelectList(SubjectCategorylist, "SubjectCategoryId", "SubjectCategoryName");
            ViewBag.GradeId = new SelectList(Gradelist, "GradeId", "GradeName");
            
           
            ViewBag.Optional = new SelectList(Optionallist, "Value", "Text");
        }

         [HttpPost]
        public ActionResult Create(int[] SubjectCategoryId, int[] selectedNames, string GradeId, string[] optional, string AcademicYear)
        {
            Authentication("GSuC");
            
            try
            {
                SchoolId = USession.School_Id;
                UserId = USession.User_Id;
                int valCounter = 0;
                int valCounter1 = 0;
                int lenth = 0;
                foreach (int SubCatId in SubjectCategoryId)
                {
                    if (SubCatId != 0) {
                        valCounter++;
                    }
                }
                foreach (string val in optional)
                {
                    if (val != "")
                    {
                        valCounter1++;
                    }
                }
                if (selectedNames != null)
                {
                    lenth = selectedNames.Length;
                }
                if (lenth != valCounter || valCounter1 != lenth || GradeId == "" || AcademicYear == "")
                {
                    Session["ErrorMessage"] = true;
                    return RedirectToAction("Index");
                   
                }
                else
                {
                    
                    ViewBag.Message = false;
                    Dropdownlistdata(AcademicYear, GradeId);
                    int count = -1;
                    foreach (int SubCatId in SubjectCategoryId)
                    {
                        if (SubCatId != 0)
                        {
                            count++;
                            int count1 = -1;
                            foreach (int SubId in selectedNames)
                            {
                                count1++;
                                if (count == count1)
                                {
                                    int count2 = -1;
                                    foreach (string optionalval in optional)
                                    {
                                        count2++;
                                        if (count2 == count1)
                                        {
                                            Connection.GDsetGradeSubject(AcademicYear,SchoolId, GradeId, SubId, SubCatId, optionalval, UserId, "Y");
                                        }
                                    }
                                }
                            }

                        }
                    }
                    var Group = Connection.GDgetAllGradeSubject(AcademicYear,SchoolId, GradeId, "Y");
                    List<GDgetAllGradeSubject_Result> Grouplist = Group.ToList();

                    Grade_SubjectModel tcm = new Grade_SubjectModel();

                    List<Grade_SubjectModel> tcmlist = Grouplist.Select(x => new Grade_SubjectModel
                    {
                        AcademicYear = x.AcademicYear,
                        GradeId = x.GradeId,
                        GradeName = x.GradeName,
                        SubjectId = x.SubjectId,
                        ShortName = x.ShortName,
                        SubjectName = x.SubjectName,
                        SubjectCategoryId = x.SubjectCategoryId,
                        SubjectCategoryName = x.SubjectCategoryName,
                        CreatedBy = x.CreatedBy,
                        CreatedDate = x.CreatedDate,
                        IsActive = x.IsActive,
                        Optional=x.Optional,
                        ModifiedBy = x.ModifiedBy,
                        ModifiedDate = x.ModifiedDate

                    }).ToList();

                    return View(tcmlist);
                }

            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return View();
            }
        }

         
         public ActionResult Detail(string AcademicYear,string GradeId)
         {
             try
             {
                 Authentication("GSubD");
                 Dropdownlistdata(AcademicYear, GradeId);
                 SchoolId = USession.School_Id;
                 var Group = Connection.GDgetAllGradeSubject(Accyear, SchoolId, gradeid, "Y");
                 List<GDgetAllGradeSubject_Result> Grouplist = Group.ToList();

                 Grade_SubjectModel tcm = new Grade_SubjectModel();

                 List<Grade_SubjectModel> tcmlist = Grouplist.Select(x => new Grade_SubjectModel
                 {
                     AcademicYear = x.AcademicYear,
                     GradeId = x.GradeId,
                     GradeName = x.GradeName,
                     SubjectId = x.SubjectId,
                     ShortName = x.ShortName,
                     SubjectName = x.SubjectName,
                     SubjectCategoryId = x.SubjectCategoryId,
                     SubjectCategoryName = x.SubjectCategoryName,
                     Optional = x.Optional,
                     CreatedBy = x.CreatedBy,
                     CreatedDate = x.CreatedDate,
                     IsActive = x.IsActive,
                     ModifiedBy = x.ModifiedBy,
                     ModifiedDate = x.ModifiedDate

                 }).ToList();

                 return View("Create", tcmlist);

             }
             catch (Exception ex) {
                 Errorlog.ErrorManager.LogError(ex);
                 return View();

             }
            

             // return View();
         }

         
         public ActionResult Edit(string AcademicYear, string GradeId, string SubjectId,string SubjectCategoryId,string Optional)
         {
             Authentication("GSuEV");
             Dropdownlistdata(AcademicYear, GradeId);
             int suid = Convert.ToInt32(SubjectId);
             Grade_SubjectModel TModel = new Grade_SubjectModel();
             tblSubject TCtable = Connection.tblSubjects.SingleOrDefault(x => x.SubjectId ==suid);
             TModel.AcademicYear = AcademicYear;
             TModel.GradeId = GradeId;
             TModel.SubjectName = TCtable.SubjectName;
             TModel.ShortName = TCtable.ShortName;
             TModel.SubjectId = Convert.ToInt32(SubjectId);
             TModel.SubjectCategoryId = Convert.ToInt32(SubjectCategoryId);
             TModel.Optional = Optional;


             tblGrade TGtable = Connection.tblGrades.SingleOrDefault(x => x.GradeId == GradeId);
             TModel.GradeName = TGtable.GradeName;
             return PartialView("Edit", TModel);
         }

         [HttpPost]
         public ActionResult Edit(Grade_SubjectModel Model)
         {
             Authentication("GSubE");
             try
             {
                 SchoolId = USession.School_Id;
                 UserId = USession.User_Id;
                 Connection.GDModifyGradeSubject(Model.AcademicYear,SchoolId, Model.GradeId, Model.SubjectId,Model.SubjectCategoryId,Model.Optional, UserId);
                 Connection.SaveChanges();
                 ViewBag.ShowDiv = false;
                // Detail(Model.AcademicYear, Model.GradeId);
                 return RedirectToAction("Detail", Model);
             }
             catch
             {
                 return View();
             }
         }



         public ActionResult Delete(string AcademicYear,string GradeId,string SubjectId)
         {
             Authentication("GSuDV");
             Grade_SubjectModel TModel = new Grade_SubjectModel();
             TModel.AcademicYear = AcademicYear;
             TModel.GradeId = GradeId;
             TModel.SubjectId = Convert.ToInt32(SubjectId);
             return PartialView("DeleteView", TModel);
         }

         //
         // POST: /TeacherCategory/Delete/5

         [HttpPost]
         public ActionResult Delete(Grade_SubjectModel Model)
         {
             Authentication("GSuDe");
             try
             {
                 SchoolId = USession.School_Id;
                UserId = USession.User_Id;
                 Connection.GDdeleteGradeSubject(Model.AcademicYear,SchoolId,Model.GradeId,Model.SubjectId,"N",UserId);
                 Connection.SaveChanges();

                 //return View();
                 return Json(true, JsonRequestBehavior.AllowGet);
                 //return RedirectToAction("Detail",Model);
             }
             catch
             {
                 return View();
             }
         }

    }
}
