using GDWEBSolution.Models;
using GDWEBSolution.Models.Evaluation;
using GDWEBSolution.Models.Schools;
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

        String SessionSchool = "Scl15241";
        //
        SchoolMGTEntitiesConnectionString Connection = new SchoolMGTEntitiesConnectionString();
        // GET: /EvaluationbySchool/

        public ActionResult Index()
        {
            List<tblSchool> Schoollist = Connection.tblSchools.Where(X=>X.IsActive=="Y").ToList();
            ViewBag.SchoolIdList = new SelectList(Schoollist, "SchoolId", "SchoolName");
            List<tblGrade> Gradelist = Connection.tblGrades.Where(X => X.IsActive == "Y").ToList();
            ViewBag.GradeIdList = new SelectList(Gradelist, "GradeId", "GradeName");
            List<tblClass> Classlist = Connection.tblClasses.Where(X => X.IsActive == "Y").ToList();
            ViewBag.ClassIdList = new SelectList(Classlist, "ClassId", "ClassName");

            List<tblSubject> Subjlist = Connection.tblSubjects.Where(X => X.IsActive == "Y").ToList();
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
            ViewBag.SchoolId1 = SessionSchool;
            dropdowns();



            return View();
        }

        private void dropdowns()
        {
            List<tblEvaluationHeader> evallist = Connection.tblEvaluationHeaders.Where(X => X.isActive == "Y" &&X.SchoolId==SessionSchool).ToList();

            ViewBag.EvaluationDrpDown = new SelectList(evallist, "EvaluationNo", "EvaluationDescription");



            String SchoolId = SessionSchool;

            List<tblStudent> Studentlist = Connection.tblStudents.Where(X => X.IsActive == "Y").ToList();
            ViewBag.StudentIdList = new SelectList(Studentlist, "", "");
            List<tblSubject> Subjlist = Connection.tblSubjects.Where(X => X.IsActive == "Y").ToList();
            ViewBag.SubjectIdDrpDown = new SelectList(Subjlist, "", "");
            List<tblSchool> Scllist = Connection.tblSchools.Where(X => X.IsActive == "Y").ToList();

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




            List<tblEvaluationType> Scwwwllist = Connection.tblEvaluationTypes.Where(X => X.IsActive == "Y").ToList();

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
        [AllowAnonymous]
        public JsonResult AddSchoolEvaluations(EvaluationModel Model)
        {
            try
            {
                string result = "Error";

                var count = Connection.tblEvaluationHeaders.Count(u => u.SchoolId == Model.SchoolId && u.EvaluationDescription == Model.EvaluationDescription);
                if (count == 0)
                {

                    tblEvaluationHeader newscg = new tblEvaluationHeader();
                    newscg.AccedamicYear = DateTime.Now.Year.ToString();
                    newscg.CreatedBy = "User1";
                    newscg.SchoolId = SessionSchool;
                    newscg.EvaluationDescription = Model.EvaluationDescription;
                    newscg.isActive = "Y";
                    newscg.CreatedDate = DateTime.Now;
                    newscg.EvaluationType = Model.EvaluationType;
                    newscg.TestPaperFee = Model.TestPaperFee;


                    Connection.tblEvaluationHeaders.Add(newscg);
                    Connection.SaveChanges();

                    result = SessionSchool;

                 //   ViewBag.SchoolId = Model.SchoolId;

                }
                else
                {
                    result = "Exits";
                }
                //ShowTeacherQualificatoin();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Errorlog.ErrorManager.LogError("Teacher Controller - AddQualification(QualificationModel Model)", Ex);
                return Json("Exception", JsonRequestBehavior.AllowGet);

            }
        }

        public ActionResult ShowEvaluation(string SchoolId)
        {
          

            var evallist = Connection.SMGTgetAllEvaluationHdetail(SchoolId).ToList();

           

       

            List<EvaluationModel> Liste = evallist.Select(x => new EvaluationModel
            {
                EvaluationNo = x.EvaluationNo,


                EvaluationType = x.EvaluationType,
                EvaluationDescription = x.EvaluationDescription,
                TestPaperFee = x.TestPaperFee.GetValueOrDefault(),
                EvaluationTypeDesc = x.EvaluationTypeDesc









            }).ToList();
            //return PartialView("GradeList", List);
            return PartialView("EvaluationList", Liste);
        }



        public ActionResult ShowEvaluationI()
        {
            string SchoolId = SessionSchool;

            var STQlist = Connection.SMGTgetSchoolGradeadd(SchoolId).ToList();
            var evallist = Connection.SMGTgetAllEvaluationHdetail(SchoolId).ToList();

            var tableevaluation = Connection.tblEvaluationHeaders.Where(X => X.isActive == "Y" && X.SchoolId == SessionSchool);



            List<EvaluationModel> Liste = evallist.Select(x => new EvaluationModel
            {
                EvaluationNo=x.EvaluationNo,

               
                EvaluationType = x.EvaluationType,
                EvaluationDescription = x.EvaluationDescription,
                TestPaperFee=x.TestPaperFee.GetValueOrDefault(),
                EvaluationTypeDesc=x.EvaluationTypeDesc
                








            }).ToList();
            //return PartialView("GradeList", List);
            return View("EvaluationList", Liste);
        }


        //
        // POST: /EvaluationbySchool/Create

        [HttpPost]
        public ActionResult Create(EvaluationModel Model)
        {
            try
            {
                tblEvaluationHeader tbl = new tblEvaluationHeader();

                tbl.AccedamicYear = DateTime.Now.Year.ToString();
                tbl.CreatedBy = "User1";
                tbl.CreatedDate = DateTime.Today;
                tbl.EvaluationDescription = Model.EvaluationDescription;
                tbl.EvaluationType = Model.EvaluationType;
                tbl.isActive = "Y";
                tbl.TestPaperFee = Model.TestPaperFee;
                tbl.SchoolId = SessionSchool;
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
            string b = "";
            string c = "";
            
            try
            {

              
                    var check1 = Model.ScheduledTimeStarts.Split('A');
                    //   string checkS = check1[1];
                    if (check1.Length < 2)
                    {

                        var a = Model.ScheduledTimeStarts.Split('P');
                        var pmbreak = a[0].Split(':');


                        int temp = Int32.Parse(pmbreak[0]) + 12;
                        b = temp + ":" + pmbreak[1];



                    }
                    else
                    {

                        b = check1[0];

                    }

                    var check2 = Model.ScheduledTimeEnds.Split('A');
                    //   string checkS = check1[1];
                    if (check1.Length < 2)
                    {

                        var a = Model.ScheduledTimeEnds.Split('P');
                        var pmbreak = a[0].Split(':');
                        int temp = Int32.Parse(pmbreak[0]) + 12;
                        c = temp + ":" + pmbreak[1];




                    }
                    else
                    {

                        c = check1[0];

                    }

                    for (int i = 0; i < chooseRecipient.Length; i++)
                    {

                        var a = chooseRecipient[i].Split('!');

                        string classname = a[0];
                        Model.Grade = a[1];



                        var count = Connection.tblEvaluationDetails.Count(u => u.Class == classname && u.EvaluationNo == Model.EvaluationNo && u.Grade == Model.Grade);

                        if (count == 0)
                        {


                            tblEvaluationDetail te = new tblEvaluationDetail();
                            te.ScheduledTimeStart = TimeSpan.Parse(b);
                            te.ScheduledTimeEnd = TimeSpan.Parse(c); ;
                            te.Class = classname;
                            te.CreatedBy = "User1";
                            te.CreatedDate = DateTime.Now;
                            te.Grade = Model.Grade;
                            te.EvaluationNo = Model.EvaluationNo;
                            //  te.ScheduledTimeStart=
                            te.ScheduledDate = Model.ScheduledDate;
                            //  te.ScheduledTimeStart = Model.ScheduledTimeStart;

                            te.SchoolId = SessionSchool;
                            te.IsActive = "Y";
                            Connection.tblEvaluationDetails.Add(te);
                            Connection.SaveChanges();

                            ModelState.Clear();

                        }

                    }
                


            }
            catch
            {



            }


            return View("Index");

        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Addevaluationdataforclassef(EvaluationModel Model, string[] chooseRecipient)
        {
            string b = "";
            string c = "";
            var result = "Exception";

            try
            {

                if (ModelState.IsValid == true)
                {
                     result = "error";

                    var check1 = Model.ScheduledTimeStarts.Split('A');
                    //   string checkS = check1[1];
                    if (check1.Length < 2)
                    {

                        var a = Model.ScheduledTimeStarts.Split('P');
                        var pmbreak = a[0].Split(':');
                        if (pmbreak[0] == "12")
                        {

                            pmbreak[0] = "0";
                        }
                        int temp = Int32.Parse(pmbreak[0]) + 12;
                        b = temp + ":" + pmbreak[1];



                    }
                    else
                    {

                        b = check1[0];

                    }

                    var check2 = Model.ScheduledTimeEnds.Split('A');
                    //   string checkS = check1[1];
                    if (check2.Length < 2)
                    {


                        var a = Model.ScheduledTimeEnds.Split('P');
                        var pmbreak = a[0].Split(':');
                        if (pmbreak[0] == "12")
                        {

                            pmbreak[0] = "0";
                        }
                        int temp = Int32.Parse(pmbreak[0]) + 12;
                        c = temp + ":" + pmbreak[1];



                    }
                    else
                    {

                        c = check2[0];

                    }

                    for (int i = 0; i < chooseRecipient.Length; i++)
                    {

                        var a = chooseRecipient[i].Split('!');

                        string classname = a[0];
                        Model.Grade = a[1];



                        var count = Connection.tblEvaluationDetails.Count(u => u.Class == classname && u.EvaluationNo == Model.EvaluationNo && u.Grade == Model.Grade);

                        if (count == 0)
                        {


                            tblEvaluationDetail te = new tblEvaluationDetail();
                            te.ScheduledTimeStart = TimeSpan.Parse(b);
                            te.ScheduledTimeEnd = TimeSpan.Parse(c); ;
                            te.Class = classname;
                            te.CreatedBy = "User1";
                            te.CreatedDate = DateTime.Now;
                            te.Grade = Model.Grade;
                            te.EvaluationNo = Model.EvaluationNo;
                            //  te.ScheduledTimeStart=
                            te.ScheduledDate = Model.ScheduledDate;
                            //  te.ScheduledTimeStart = Model.ScheduledTimeStart;

                            te.SchoolId = SessionSchool;
                            te.IsActive = "Y";
                            Connection.tblEvaluationDetails.Add(te);
                            Connection.SaveChanges();

                            result = Model.EvaluationNo.ToString();

                            ModelState.Clear();

                        }

                    }

                }     

            }
            catch
            {



            }


            return Json(result, JsonRequestBehavior.AllowGet);

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


        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetSchoolgrdclass(string SchoolId, string GradeId)
        {
            if (String.IsNullOrEmpty(SchoolId))
            {
                throw new ArgumentNullException("countryId");
            }
            //int id = 0;
            //bool isValid = Int32.TryParse(SchoolId, out id);

            var SchoolClass = Connection.SMGTgetGradeclassadd(SchoolId, GradeId).ToList();//Need to Pass a Session Schoolid





            //  var StudentSextra = Connection.SMGTloadScholExtraCadd(SchoolId, "%").ToList();

            var result2 = (from s in SchoolClass
                           select new
                           {
                               ClassId = s.ClassId+"!"+s.GradeId,
                               ClassName = s.ClassName,
                               GradeName=s.GradeName


                           }).ToList();



            //   ViewBag.excdropdown = new SelectList(excList2, "ActivityCode", "ActivityName");

            //var states = _repository.GetAllStatesByCountryId(id);
            //var result = (from s in states
            //              select new
            //              {
            //                  id = s.Id,
            //                  name = s.Name
            //              }).ToList();
            return Json(result2, JsonRequestBehavior.AllowGet);
        }





        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetEvaluation()
        {





          


            List<tblEvaluationHeader> Scwwwllist = Connection.tblEvaluationHeaders.Where(X => X.isActive== "Y" && X.SchoolId==SessionSchool).ToList();
            //int id = 0;
            //bool isValid = Int32.TryParse(SchoolId, out id);

            //Need to Pass a Session Schoolid





            //  var StudentSextra = Connection.SMGTloadScholExtraCadd(SchoolId, "%").ToList();

            var result2 = (from s in Scwwwllist
                           select new
                           {
                               EvaluationNo=s.EvaluationNo,
                               EvaluationDescription=s.EvaluationDescription,


                           }).ToList();



            //   ViewBag.excdropdown = new SelectList(excList2, "ActivityCode", "ActivityName");

            //var states = _repository.GetAllStatesByCountryId(id);
            //var result = (from s in states
            //              select new
            //              {
            //                  id = s.Id,
            //                  name = s.Name
            //              }).ToList();
            return Json(result2, JsonRequestBehavior.AllowGet);
        }



        public ActionResult DeleteEvaluationHeader(string EvaluationNo)
        {
            EvaluationModel Model = new EvaluationModel();
            Model.EvaluationNo = long.Parse(EvaluationNo);
            Model.SchoolId = SessionSchool;
            

            return PartialView("DeleteEvaluationHeader", Model);
        }


        public ActionResult DeleteEvaluationDetail(string EvaluationDetailSeqNo, string EvaluationNo)
        {
            EvaluationModel Model = new EvaluationModel();
            Model.EvaluationDetailSeqNo = long.Parse(EvaluationDetailSeqNo);
            Model.EvaluationNo = long.Parse(EvaluationNo);
            Model.SchoolId = SessionSchool;

            return PartialView("DeleteEvaluationDetail", Model);
        }


        [HttpPost]
        public ActionResult DeleteEvaluationHeader(EvaluationModel Model)
        {
            try
            {
                // Model.SchoolId="121127";
                string evalNo=Model.EvaluationNo.ToString();

                Connection.SMGTModifyEvaluationHeaderStatus(Model.SchoolId, evalNo);

                //  Connection.tblHouses.
                Connection.SaveChanges();

                return Json(Model.SchoolId, JsonRequestBehavior.AllowGet);



            }
            catch
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        public ActionResult DeleteEvaluationDetail(EvaluationModel Model)
        {
            try
            {
                // Model.SchoolId="121127";
                string evalNo = Model.EvaluationNo.ToString();
                string evaldseq = Model.EvaluationDetailSeqNo.ToString();

                Connection.SMGTModifyEvaluationDetailStatus(SessionSchool, evaldseq);

                //  Connection.tblHouses.
                Connection.SaveChanges();

                return Json(evalNo, JsonRequestBehavior.AllowGet);



            }
            catch
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult ShowEvaluationDetail(string EvluationNo)
        {
            string SchoolId = SessionSchool;


         //   var evallist = Connection.SMGTgetAllEvaluationHdetail(SchoolId).ToList();

            var evaldetailList = Connection.SMGTgetAllEvaluationdetailList(SchoolId, EvluationNo);



            List<EvaluationModel> Liste = evaldetailList.Select(x => new EvaluationModel
            {
                EvaluationNo = x.EvaluationNo,


                EvaluationType = x.EvaluationType,
                EvaluationDescription = x.EvaluationDescription,
                TestPaperFee = x.TestPaperFee.GetValueOrDefault(),
               GradeName=x.GradeName,
               ClassName=x.ClassName,
              ScheduledDates=x.ScheduledDate.ToShortDateString(),
              ScheduledTimeEndst=x.ScheduledTimeEnd.ToString(),
              ScheduledTimeStartst=x.ScheduledTimeStart.ToString(),
              EvaluationDetailSeqNo=x.EvaluationDetailSeqNo











            }).ToList();
            //return PartialView("GradeList", List);
            return PartialView("EvaluationDetaillist", Liste);
        }






    }
}
