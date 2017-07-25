using GDWEBSolution.Filters;
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
        [UserFilter(Function_Id = "GAdv")]
        public ActionResult Index()
        {
            
          
            try
            {
                SchoolId = USession.School_Id;
                tblAccadamicYear TCtable = Connection.tblAccadamicYears.SingleOrDefault(x => x.SchoolId == SchoolId);

                ViewBag.AcedamicYear = TCtable.AccadamicYear;
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


       [UserFilter(Function_Id = "GAdv")]
        public ActionResult Detail(string GradeId, string ClassId, string AcedamicYear)
        {
           
            try
            {
                SchoolId =USession.School_Id;
                tblAccadamicYear Ttable = Connection.tblAccadamicYears.SingleOrDefault(x => x.SchoolId == SchoolId);
                
                Dropdownlistdata(SchoolId);
               
                if (GradeId == null) {
                    return RedirectToAction("Index");
                }
               
              
                ViewBag.CurentYear = AcedamicYear;
                tblGrade TCtable = Connection.tblGrades.SingleOrDefault(x => x.GradeId == GradeId);
                ViewBag.CurentGrade = TCtable.GradeName;

                tblClass classtable = Connection.tblClasses.SingleOrDefault(x => x.ClassId == ClassId && x.GradeId==GradeId && x.SchoolId==SchoolId);
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
         [UserFilter(Function_Id = "GAdv")]
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
         [UserFilter(Function_Id = "GAdv")]
        private void Dropdownlistdata(string SchoolId)
        {
            SchoolId =USession.School_Id;
            var Grade = Connection.GDgetSchoolGrade(SchoolId,"Y");
            List<GDgetSchoolGrade_Result> Gradelist = Grade.ToList();

            ViewBag.GradeId = new SelectList(Gradelist, "GradeId", "GradeName");


            var Class = Connection.GDgetAllSchoolClass(SchoolId, "Y");
            List<GDgetAllSchoolClass_Result> Classlist = Class.ToList();


            ViewBag.ClassId = new SelectList(Classlist, "ClassId", "ClassName");
            
        }


         [UserFilter(Function_Id = "GAdv")]
        [HttpPost]
        public ActionResult Update(string[] selectedNames, string GradeId, string ClassId, string ParameterAcedamicYear)
        {
            
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
            SchoolId =USession.School_Id;
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


         [UserFilter(Function_Id = "GAdv")]
        public ActionResult UpdateAccedemicYear()
        {
            
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

         [UserFilter(Function_Id = "GAdv")]
        [HttpPost]

        public ActionResult UpdateAccYear(string AccYear)
        {
            
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
      
    

