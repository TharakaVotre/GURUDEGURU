using GDWEBSolution.Models;
using GDWEBSolution.Models.Evaluation;
using GDWEBSolution.Models.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GDWEBSolution.Controllers.MarkSheet
{
    public class MarksheetController : Controller
    {


        SchoolMGTEntitiesConnectionString Connection = new SchoolMGTEntitiesConnectionString();



        [HttpGet]
        public ActionResult TableData(String GradeId, String SchoolId2, String ClassId2)
        {
            var stpop = Connection.SMGTgetStudentOptionalSubjects("%", "%");

            List<SMGTgetStudentOptionalSubjects_Result> studoption = stpop.ToList();


            List<OptionalSubjectModel> tcmlist = studoption.Select(x => new OptionalSubjectModel
            {


                SequanceNo = x.SeqNo,

                SchoolId = x.SchoolId,
                SchoolName = x.SchoolName,


                StudentId = x.StudentId,
                StudentName = x.studentName,
                GradeId = x.GradeId,
                GradeName = x.GradeName,
                ClassId = x.ClassId,
                ClassName = x.ClassName,
                SubjectId = x.StudentId,
                SubjectName = x.SubjectName






            }).ToList();

            return PartialView("TableData", tcmlist);
        }

        //
        // GET: /Marksheet/

        public ActionResult Index()
        {

            Academicyear();
            dropdowns();


            var marklis = Connection.SMGTGetStudentsAllSubjectMarks("%", "%", "%", "%", "%");

            List<MarkSheetModel> marklist = marklis.Select(x => new MarkSheetModel
          {

              AccedamicYear = x.AccedamicYear,
              Class = x.Class,
              Mark = x.Mark,
              SchoolId = x.SchoolId,
              StudentId = x.StudentId,
              studentName=x.studentName,
              SubjectName=x.SubjectName,
              
              EvaluationNo = x.EvaluationNo,
              SubjectId=x.SubjectId.ToString(),
              
              EvaluationType = x.EvaluationType,
              EvaluationTypeDesc = x.EvaluationTypeDesc,
              Grade = x.Grade

          }).ToList();
              

            
            
            







            return View(marklist);
        }

        private void dropdowns()
        {
            List<tblEvaluationType> evaluationtypelist = Connection.tblEvaluationTypes.ToList();
            ViewBag.evaluationtypeList = new SelectList(evaluationtypelist, "EvaluationTypeCode", "EvaluationTypeDesc");

            List<tblSchool> Schoollist = Connection.tblSchools.ToList();
            ViewBag.SchoolIdList = new SelectList(Schoollist, "SchoolId", "SchoolName");
            List<tblGrade> Gradelist = Connection.tblGrades.ToList();
            ViewBag.GradeIdList = new SelectList(Gradelist, "GradeId", "GradeName");
            List<tblClass> Classlist = Connection.tblClasses.ToList();
            ViewBag.ClassIdList = new SelectList(Classlist, "ClassId", "ClassName");

            List<tblSubject> Subjlist = Connection.tblSubjects.ToList();
            ViewBag.SubjectIdDrpDown = new SelectList(Subjlist, "SubjectId", "SubjectName");

            List<tblStudent> Stdlist = Connection.tblStudents.ToList();
            ViewBag.StudentDrpDown = new SelectList(Stdlist, "StudentId", "StudentName");
        }

        private void Academicyear()
        {
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
                 new SelectListItem { Text = values[0], Value = values[0]},
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
        }

        //
        // GET: /Marksheet/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Marksheet/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Marksheet/Create

        [HttpPost]
        public ActionResult Create(MarkSheetModel model)
        {
            try

            {

                String a = model.SchoolId;
                String b = model.Grade;
                string c = model.StudentId;
                string d = model.Class;
                String e = model.AccedamicYear;
                string f = model.EvaluationType.ToString();
                var marklis = Connection.SMGTGetStudentsAllSubjectMarks(a, c, b, e, f);

                List<MarkSheetModel> marklist = marklis.Select(x => new MarkSheetModel
                {

                    AccedamicYear = x.AccedamicYear,
                    Class = x.Class,
                    Mark = x.Mark,
                    SchoolId = x.SchoolId,
                    studentName = x.studentName,
                    SubjectName = x.SubjectName,
                    StudentId = x.StudentId,
                    SubjectId=x.SubjectId.ToString(),
                    EvaluationNo = x.EvaluationNo,
                    EvaluationType = x.EvaluationType,
                    EvaluationTypeDesc = x.EvaluationTypeDesc,
                    Grade = x.Grade

                }).ToList();


                Academicyear();
                dropdowns();

                return View("Index", marklist);






                // TODO: Add insert logic here

               // return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Marksheet/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Marksheet/Edit/5

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
        // GET: /Marksheet/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Marksheet/Delete/5

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
