using GDWEBSolution.Models;
using GDWEBSolution.Models.AnnualFunctions;
using GDWEBSolution.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GDWEBSolution.Controllers.AnnualFunctions
{
    public class StudentGradeAdvanceController : Controller
    {
        //
        SchoolMGTEntitiesConnectionString Connection = new SchoolMGTEntitiesConnectionString();
        UserSession USession = new UserSession();
        string UserId =null;
        string SchoolId = null;        // GET: /StudentGradeAdvance/
        
        public ActionResult Index()
        {

            Authentication("GAdv");
            try
            {
                tblParameter TCtable = Connection.tblParameters.SingleOrDefault(x => x.ParameterId == "AY");
               
                ViewBag.AcedamicYear = TCtable.ParameterValue;
                Dropdownlistdata(SchoolId);
                List<StudentGradeAdvanceModel> tcmlist = getdataForTable("","","");

                return View(tcmlist);
            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);

                return View();  
            }
        }

        private void Authentication(string ControlerName)
        {
           
            if (USession.User_Id != "")
            {
                string CategoryId = USession.User_Category;
                tblUserCategoryFunction AccessControl = Connection.tblUserCategoryFunctions.SingleOrDefault(a => a.FunctionId == ControlerName && a.CategoryId==CategoryId && a.IsActive=="Y");

                if (AccessControl ==null)
                {
                    //RedirectToAction("~/Prohibited");
                    Response.Redirect("~/Prohibited");
                }
                else
                {
                    UserId = USession.User_Id;
                    SchoolId = USession.School_Id;
                }
            }
            else
            {
                // RedirectToAction();
                Response.Redirect("~/Home/Login");
            }
        }

        public ActionResult Detail(string GradeId, string ClassId, string AcedamicYear)
        {
            Authentication("GAdDt");
            try
            {
                tblAccadamicYear Ttable = Connection.tblAccadamicYears.SingleOrDefault(x => x.SchoolId == SchoolId);
                
                Dropdownlistdata(SchoolId);
               
                if (GradeId == null && Session["GroupId"] == null) {
                    return RedirectToAction("Index");
                }
                if (GradeId != null & ClassId!=null)
                {
                    Session["GroupId"]=GradeId;
                    Session["ClassId"] = ClassId;
                    Session["AccYear"] = AcedamicYear;// = Ttable.ParameterValue;
                }
                else{
                    GradeId=Session["GroupId"].ToString();
                    ClassId = Session["classId"].ToString();
                    AcedamicYear = Session["AccYear"].ToString();
                }
                ViewBag.CurentYear = AcedamicYear;
                tblGrade TCtable = Connection.tblGrades.SingleOrDefault(x => x.GradeId == GradeId);
                ViewBag.CurentGrade = TCtable.GradeName;

                tblClass classtable = Connection.tblClasses.SingleOrDefault(x => x.ClassId == ClassId && x.GradeId==GradeId);
                ViewBag.CurentClass = classtable.ClassName;

               
                ViewBag.ParameterAcedamicYear = Ttable.AccadamicYear;
                if (Ttable.AccadamicYear != AcedamicYear)
                {
                    ViewBag.ErrorMsg = false;
                }
                else
                {
                    ViewBag.ErrorMsg = true;
                }
                List<StudentGradeAdvanceModel> tcmlist = getdataForTable(AcedamicYear,GradeId, ClassId);
                if (tcmlist.Count==0)
                {
                return RedirectToAction("Index");
                }
                return View(tcmlist);
               
               
            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);

                return View();
            }
        }

        private List<StudentGradeAdvanceModel> getdataForTable(string AcedamicYear,string GradeId,string ClassId)
        {
           
            try
            {
                SchoolId = USession.School_Id;
                var Group = Connection.GDgetAllStudentInGrade(AcedamicYear, SchoolId, GradeId, ClassId, "Y");
                List<GDgetAllStudentInGrade_Result> Grouplist = Group.ToList();

                StudentGradeAdvanceModel tcm = new StudentGradeAdvanceModel();

                List<StudentGradeAdvanceModel> tcmlist = Grouplist.Select(x => new StudentGradeAdvanceModel
                {

                    GradeId = x.GradeId,
                    GradeName = x.GradeName,
                    StudentId = x.StudentId,
                    StudentName = x.studentName,
                    ClassId = x.ClassId,
                    ClassName = x.ClassName,

                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate,
                    IsActive = x.IsActive,
                    ModifiedBy = x.ModifiedBy,
                    ModifiedDate = x.ModifiedDate

                }).ToList();
                return tcmlist;
            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return null;
            }
        }
        private void Dropdownlistdata(string SchoolId)
        {
            SchoolId = USession.School_Id;
            var Grade = Connection.GDgetSchoolGrade(SchoolId,"Y");
            List<GDgetSchoolGrade_Result> Gradelist = Grade.ToList();

            ViewBag.GradeId = new SelectList(Gradelist, "GradeId", "GradeName");


            var Class = Connection.GDgetAllSchoolClass(SchoolId, "Y");
            List<GDgetAllSchoolClass_Result> Classlist = Class.ToList();


            ViewBag.ClassId = new SelectList(Classlist, "ClassId", "ClassName");
            
        }

       

        [HttpPost]
        public ActionResult Update(string[] selectedNames, string GradeId, string ClassId, string ParameterAcedamicYear)
        {
            Authentication("GAdUp");
            try
            {
                if (GradeId!="")
                {
                if (selectedNames != null)
                {
                    if (ClassId=="")
                    {
                        foreach (string studentId in selectedNames)
                        {
                            Connection.GDsetStudentHistory(studentId);
                            Connection.GDModifyStudentLeaver(SchoolId, studentId, UserId, "L");
                        }
                    }else{
                        foreach (string studentId in selectedNames)
                        {
                            Connection.GDsetStudentHistory(studentId);
                            Connection.GDModifyStudentGradeAdvance(SchoolId, studentId, GradeId, ClassId, ParameterAcedamicYear, UserId);
                        }
                    }
                   
                    Connection.SaveChanges();
                }
                }
                //return View();

                return RedirectToAction("Detail");
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Errorlog.ErrorManager.LogError(dbEx);
                return RedirectToAction("Index");
            }
        }




        public JsonResult getstate(string id)
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



        public ActionResult UpdateAccedemicYear()
        {
            Authentication("UpYer");
            try
            {
                tblAccadamicYear Ttable = Connection.tblAccadamicYears.SingleOrDefault(x => x.SchoolId == SchoolId);
                ViewBag.AccYear = Ttable.AccadamicYear;
                return View();
            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return View();
            }
        }
        [HttpPost]

        public ActionResult UpdateAccYear(string AccYear)
        {
            Authentication("UpYrP");
            try
            {
                Connection.GDModifySchoolAccademicYear(SchoolId,AccYear);
                return RedirectToAction("UpdateAccedemicYear");
            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return View();
            }
        }


    }
}
      
    

