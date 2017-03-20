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

                Dropdownlistdata(SchoolId);
                List<StudentGradeAdvanceModel> tcmlist = getdataForTable("");

                return View(tcmlist);
            }
            catch (Exception ex)
            {
                //Errorlog.ErrorManager.LogError(ex);

                return View();
            }
        }

        public ActionResult Detail(string GradeId)
        {
            try
            {
                if (GradeId == null && Session["GroutId"] == null)
                {
                    return RedirectToAction("Index");
                }
                if (GradeId != null)
                {
                    Session["GroutId"] = GradeId;
                }
                else
                {
                    GradeId = Session["GroutId"].ToString();
                }
                tblGrade TCtable = Connection.tblGrades.SingleOrDefault(x => x.GradeId == GradeId);
                ViewBag.CurentGroup = TCtable.GradeName;
                Dropdownlistdata(SchoolId);
                List<StudentGradeAdvanceModel> tcmlist = getdataForTable(GradeId);
                if (tcmlist.Count == 0)
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

        private List<StudentGradeAdvanceModel> getdataForTable(string GradeId)
        {
            var Group = Connection.GDgetAllStudentInGrade(SchoolId, GradeId, "Y");
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
            
        }



        [HttpPost]
        public ActionResult Update(string[] selectedNames, string GradeId)
        {

            try
            {
                if (selectedNames != null)
                {
                    foreach (string studentId in selectedNames)
                        Connection.GDModifyStudentGradeAdvance(SchoolId, studentId, GradeId, UserId);
                    Connection.SaveChanges();
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


    }
}
      
    

