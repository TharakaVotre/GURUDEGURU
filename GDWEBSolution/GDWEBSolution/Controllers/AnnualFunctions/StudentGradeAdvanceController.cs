using GDWEBSolution.Models;
using GDWEBSolution.Models.AnnualFunctions;
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
        string UserId = "ADMIN";
        string SchoolId = "CKC";
        // GET: /StudentGradeAdvance/

        public ActionResult Index()
        {
            try
            {
                ViewBag.AcedamicYear = DateTime.Now.Year.ToString();
                Dropdownlistdata(SchoolId);
                List<StudentGradeAdvanceModel> tcmlist = getdataForTable("","","");

                return View(tcmlist);
            }
            catch (Exception ex)
            {
                //Errorlog.ErrorManager.LogError(ex);

                return View();  
            }
        }

        public ActionResult Detail(string GradeId, string ClassId, string AcedamicYear)
        {
            try
            {
                
                if (GradeId == null && Session["GroupId"] == null) {
                    return RedirectToAction("Index");
                }
                if (GradeId != null & ClassId!=null)
                {
                    Session["GroupId"]=GradeId;
                    Session["ClassId"] = ClassId;
                }
                else{
                    GradeId=Session["GroupId"].ToString();
                    ClassId = Session["classId"].ToString();
                }
                ViewBag.CurentYear = AcedamicYear;
                tblGrade TCtable = Connection.tblGrades.SingleOrDefault(x => x.GradeId == GradeId);
                ViewBag.CurentGrade = TCtable.GradeName;

                tblClass classtable = Connection.tblClasses.SingleOrDefault(x => x.ClassId == ClassId & x.GradeId == GradeId);
                ViewBag.CurentClass = classtable.ClassName;

                Dropdownlistdata(SchoolId);
                List<StudentGradeAdvanceModel> tcmlist = getdataForTable(AcedamicYear,GradeId, ClassId);
                if (tcmlist.Count==0)
                {
                return RedirectToAction("Index");
                }
                return View(tcmlist);
            }
            catch (Exception ex)
            {
                //Errorlog.ErrorManager.LogError(ex);

                return View();
            }
        }

        private List<StudentGradeAdvanceModel> getdataForTable(string AcedamicYear,string GradeId,string ClassId)
        {
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
                HouseName = x.HouseName,
                HouseId = x.HouseId,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate,
                IsActive = x.IsActive,
                ModifiedBy = x.ModifiedBy,
                ModifiedDate = x.ModifiedDate

            }).ToList();
            return tcmlist;
        }
        private void Dropdownlistdata(string SchoolId)
        {

            var Grade = Connection.GDgetAllGradeMaintenance("Y");
            List<GDgetAllGradeMaintenance_Result> Gradelist = Grade.ToList();

            ViewBag.GradeId = new SelectList(Gradelist, "GradeId", "GradeName");


            var Class = Connection.GDgetAllSchoolClass(SchoolId, "Y");
            List<GDgetAllSchoolClass_Result> Classlist = Class.ToList();


            ViewBag.ClassId = new SelectList(Classlist, "ClassId", "ClassName");
            
        }

       

        [HttpPost]
        public ActionResult Update(string[] selectedNames, string GradeId, string ClassId)
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
                            Connection.GDModifyStudentGradeAdvance(SchoolId, studentId, GradeId, ClassId, UserId);
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
    
    }
}
      
    

