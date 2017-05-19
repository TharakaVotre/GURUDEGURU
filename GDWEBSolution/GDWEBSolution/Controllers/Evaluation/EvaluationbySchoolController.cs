using GDWEBSolution.Models;
using GDWEBSolution.Models.Evaluation;
using GDWEBSolution.Models.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GDWEBSolution.Controllers.Evaluation
{
    public class EvaluationbySchoolController : Controller
    {
        //
        SchoolMGTEntitiesConnectionString Connection = new SchoolMGTEntitiesConnectionString();
        // GET: /EvaluationbySchool/

        public ActionResult Index()
        {
            List<tblSchool> Schoollist = Connection.tblSchools.ToList();
            ViewBag.SchoolIdList = new SelectList(Schoollist, "SchoolId", "SchoolName");
            List<tblGrade> Gradelist = Connection.tblGrades.ToList();
            ViewBag.GradeIdList = new SelectList(Gradelist, "GradeId", "GradeName");
            List<tblClass> Classlist = Connection.tblClasses.ToList();
            ViewBag.ClassIdList = new SelectList(Classlist, "ClassId", "ClassName");

            List<tblSubject> Subjlist = Connection.tblSubjects.ToList();
            ViewBag.SubjectIdDrpDown = new SelectList(Subjlist, "SubjectId", "SubjectName");


            return View();
        }

        //
        // GET: /EvaluationbySchool/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /EvaluationbySchool/Create

        public ActionResult Create()
        {
            dropdowns();



            return View();
        }

        private void dropdowns()
        {
            List<tblEvaluationHeader> evallist = Connection.tblEvaluationHeaders.ToList();

            ViewBag.EvaluationDrpDown = new SelectList(evallist, "EvaluationNo", "EvaluationDescription");


            
            String SchoolId = "11122221";

            List<tblStudent> Studentlist = Connection.tblStudents.ToList();
            ViewBag.StudentIdList = new SelectList(Studentlist, "", "");
            List<tblSubject> Subjlist = Connection.tblSubjects.ToList();
            ViewBag.SubjectIdDrpDown = new SelectList(Subjlist, "", "");
            List<tblSchool> Scllist = Connection.tblSchools.ToList();

            ViewBag.SchoolDrpDown = new SelectList(Scllist, "SchoolId", "SchoolName");

            String sclid;
            if (SchoolId == null)
            {
                sclid = "%";

            }
            else
            {

                sclid = SchoolId;
            }

            //  ViewBag.AcademicyearDrpDown
            var currentYear = DateTime.Today.Year;

            List<string> values = new List<string>();



            for (int i = 3; i >= -3; i--)
            {
                string year = (currentYear - i).ToString();


                values.Add(year);

                // Now just add an entry that's the current year minus the counter
                //   DropDownList2.Items.Add((currentYear - i).ToString());
            }

            ViewBag.AcademicYear = new List<SelectListItem> {                  
                 new SelectListItem { Text = values[0], Value = values[0],Selected=true},
                 new SelectListItem { Text = values[1], Value = values[1]}, 
                 new SelectListItem { Text = values[2], Value = values[2]}, 
                 new SelectListItem { Text = values[3], Value = values[3]}, 
                 new SelectListItem { Text = values[4], Value = values[4]}, 
                 new SelectListItem { Text = values[5], Value = values[5]}
               
             };
            var abc = new List<SelectListItem> {                  
                 new SelectListItem { Text = values[0], Value = values[0]},
                 new SelectListItem { Text = values[1], Value = values[1]}, 
                 new SelectListItem { Text = values[2], Value = values[2]}, 
                 new SelectListItem { Text = values[3], Value = values[3]}, 
                 new SelectListItem { Text = values[4], Value = values[4]}, 
                 new SelectListItem { Text = values[5], Value = values[5]}
               
             };
            ViewBag.AcademicYear = new SelectList(abc, "Value", "Text");

            ViewBag.Studentlist = new MultiSelectList(Studentlist, "StudentId", "StudentName");

            List<tblStudent> SAcdemicyrlist = abc.Select(x => new tblStudent
            {
                StudentId = x.Value,
                AcademicYear = x.Value,
                studentName = x.Text

            }).ToList();

            ViewBag.Academicyrxlist = new SelectList(SAcdemicyrlist, "AcademicYear", "studentName");




            List<tblEvaluationType> Scwwwllist = Connection.tblEvaluationTypes.ToList();

            ViewBag.EvaluationList = new SelectList(Scwwwllist, "EvaluationTypeCode", "EvaluationTypeDesc");

            //    ViewBag.AcademicyearDrpDown = values;


            var SchoolHouse = Connection.SMGTgetSchoolHouseadd(sclid).ToList();
            List<tblHouse> Shouselist = SchoolHouse.Select(x => new tblHouse
            {
                HouseId = x.HouseId,
                HouseName = x.HouseName,
                IsActive = x.IsActive

            }).ToList();


            var SchoolGrade = Connection.SMGTgetSchoolGrade(sclid).ToList();//Need to Pass a Session Schoolid
            List<tblGrade> SchoolGradeList = SchoolGrade.Select(x => new tblGrade
            {
                GradeId = x.GradeId,
                GradeName = x.GradeName,
                IsActive = x.IsActive

            }).ToList();

            var schoolclass = Connection.SMGTgetclassadd(sclid, "%").ToList();

            List<tblClass> SchoolclassList = schoolclass.Select(x => new tblClass
            {
                ClassName = x.ClassName,
                ClassId = x.ClassId

            }).ToList();




            // List<tblClass> Sclasslist = Connection.tblClasses.ToList();
            ViewBag.classDrpDown = new SelectList(SchoolclassList, "ClassId", "ClassName");


            ViewBag.SchoolGradeList = new SelectList(SchoolGradeList, "GradeId", "GradeName");

            ViewBag.HouseDrpDown = new SelectList(Shouselist, "HouseId", "HouseName");

            var Studentextra = Connection.SMGTgetStudentExtraCadd("%", "%").ToList();
            List<tblExtraCurricularActivity> excList = Studentextra.Select(x => new tblExtraCurricularActivity
            {
                ActivityCode = x.ActivityCode,
                ActivityName = x.ActivityName

            }).ToList();

            var StudentSextra = Connection.SMGTloadScholExtraCadd("%", "%").ToList();

            List<tblExtraCurricularActivity> excList2 = StudentSextra.Select(x => new tblExtraCurricularActivity
            {
                ActivityCode = x.ActivityCode,
                ActivityName = x.ActivityName

            }).ToList();

            ViewBag.excdropdown = new SelectList(excList2, "ActivityCode", "ActivityName");

        }

        //
        // POST: /EvaluationbySchool/Create

        [HttpPost]
        public ActionResult Create(EvaluationModel Model)
        {
            try
            {
                tblEvaluationHeader tbl = new tblEvaluationHeader();

                tbl.AccedamicYear = Model.AcademicYear;
                tbl.CreatedBy = "User1";
                tbl.CreatedDate = DateTime.Today;
                tbl.EvaluationDescription = Model.EvaluationDescription;
                tbl.EvaluationType = Model.EvaluationType;
                tbl.isActive = "Y";
                tbl.SchoolId = Model.SchoolId;
                Connection.tblEvaluationHeaders.Add(tbl);
                Connection.SaveChanges();


                dropdowns();

              

                return RedirectToAction("Create");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Addevaluationdataforclass(EvaluationModel Model, string[] chooseRecipient)
        {

            try
            {

                tblEvaluationDetail te = new tblEvaluationDetail();
                te.Class = Model.Class;
                te.CreatedBy = "User1";
                te.CreatedDate = DateTime.Now;
                te.Grade = Model.Grade;
                te.EvaluationNo = Model.EvaluationNo;
                te.ScheduledDate = Model.ScheduledDate;
                te.ScheduledTime = Model.ScheduledTime;
                te.SchoolId = Model.SchoolId;
                te.IsActive = "Y";
                Connection.tblEvaluationDetails.Add(te);
                Connection.SaveChanges();

                ModelState.Clear();


             




            }
            catch
            {



            }


            return View("Index");

        }

        [HttpPost]

        public ActionResult Addevaluationdataforclasse(EvaluationModel Model)
        { 

             try
            {
                tblEvaluationDetail tbl = new tblEvaluationDetail();



        }
            catch{
        
        
        
        }


             return View("Index");
        
        }

        //
        // GET: /EvaluationbySchool/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /EvaluationbySchool/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /EvaluationbySchool/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /EvaluationbySchool/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
