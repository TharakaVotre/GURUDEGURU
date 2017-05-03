﻿using GDWEBSolution.Models;
using GDWEBSolution.Models.Schools;
using GDWEBSolution.Models.Student;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GDWEBSolution.Controllers.Student
{
    public class StudentController : Controller
    {
        //
        // GET: /Student/

        SchoolMGTEntitiesConnectionString Connection = new SchoolMGTEntitiesConnectionString();

        public ActionResult Index()
        {
            var student = Connection.SMGTGetStudent();

            List<SMGTGetStudent_Result> Categorylist = student.ToList();
            StudentModel schl = new StudentModel();
            List<StudentModel> tcmlist = Categorylist.Select(x => new StudentModel
            {

                SchoolId = x.SchoolId,
                SchoolName = x.SchoolName,
                DateOfBirth=x.DateofBirth,
                CreatedDate=x.CreatedDate,
                StudentId=x.StudentId,
                StudentName=x.studentName,
               



                IsActive = x.IsActive,
                ModifiedBy = x.ModifiedBy,
                ModifiedDate = x.ModifiedDate

            }).ToList();




            return View(tcmlist);
        }

        //
        // GET: /Student/Details/5

        public ActionResult Details(string StudentId, string SchoolId)
        {
            StudentModel TModel = new StudentModel();

            tblStudent TCtable = Connection.tblStudents.SingleOrDefault(x => x.StudentId == StudentId && x.SchoolId == SchoolId);


            //  TModel.IsActive = TCtable.IsActive;


            TModel.StudentId = TCtable.StudentId;
            TModel.StudentName = TCtable.studentName;
            TModel.DateOfBirth = TCtable.DateofBirth;
            TModel.ClassId = TCtable.ClassId;
            TModel.Gender = TCtable.Gender;
            TModel.Gender = TCtable.Gender;
            TModel.ImagePathname = TCtable.ImgUrl;
            TModel.GradeId = TCtable.GradeId;
            TModel.HouseId = TCtable.HouseId;

            tblClass sclrank = Connection.tblClasses.SingleOrDefault(x =>  x.ClassId == TModel.ClassId &&x.GradeId==TModel.GradeId && x.SchoolId == SchoolId );
            TModel.ClassId = sclrank.ClassName;
            tblGrade sclgrade = Connection.tblGrades.SingleOrDefault(x => x.GradeId== TModel.GradeId);

            TModel.GradeId = sclgrade.GradeName;
            tblHouse sclhouse = Connection.tblHouses.SingleOrDefault(x => x.SchoolId == SchoolId && x.HouseId == TModel.HouseId);
            TModel.HouseId = sclhouse.HouseName;
            return View("Detail",TModel);
        }

        //
        // GET: /Student/Create

        public ActionResult Create(String SchoolId)
        {
        

            List<tblSchool> Scllist = Connection.tblSchools.ToList();
        
            String sclid;
            if (SchoolId == null)
            {
                sclid = "%";

            }
            else {

                sclid = SchoolId;
            }


            var SchoolHouse = Connection.SMGTgetSchoolHouseadd(sclid).ToList();
            List<tblHouse> Shouselist = SchoolHouse.Select(x => new tblHouse
            {
                HouseId=x.HouseId,
                HouseName=x.HouseName,
                IsActive = x.IsActive

            }).ToList();


            var SchoolGrade = Connection.SMGTgetSchoolGrade(sclid).ToList();//Need to Pass a Session Schoolid
            List<tblGrade> SchoolGradeList = SchoolGrade.Select(x => new tblGrade
            {
                GradeId = x.GradeId,
                GradeName = x.GradeName,
                IsActive = x.IsActive

            }).ToList();

            var schoolclass = Connection.SMGTgetclassadd(sclid,"%").ToList();

            List<tblClass> SchoolclassList = schoolclass.Select(x => new tblClass
            {
            ClassName=x.ClassName,  
            ClassId=x.ClassId
            
            }).ToList();


            ViewBag.SchoolDrpDown = new SelectList(Scllist, "SchoolId", "SchoolName");

            // List<tblClass> Sclasslist = Connection.tblClasses.ToList();
             ViewBag.classDrpDown = new SelectList(SchoolclassList, "ClassId", "ClassName");

         
             ViewBag.SGradeDrpDown = new SelectList(SchoolGradeList, "GradeId", "GradeName");
            
             ViewBag.HouseDrpDown = new SelectList(Shouselist, "HouseId", "HouseName");

             var Studentextra = Connection.SMGTgetStudentExtraCadd("%", "%").ToList();
             List<tblExtraCurricularActivity> excList = Studentextra.Select(x => new tblExtraCurricularActivity
             {
                ActivityCode=x.ActivityCode,
                ActivityName=x.ActivityName

             }).ToList();

             var StudentSextra = Connection.SMGTloadScholExtraCadd("%", "%").ToList();

             List<tblExtraCurricularActivity> excList2 = StudentSextra.Select(x => new tblExtraCurricularActivity
             {
                 ActivityCode = x.ActivityCode,
                 ActivityName = x.ActivityName

             }).ToList();

             ViewBag.excdropdown = new SelectList(excList2, "ActivityCode", "ActivityName");

            return View();
        }



        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetSchoolextracuricluar(string SchoolId)
        {
            if (String.IsNullOrEmpty(SchoolId))
            {
                throw new ArgumentNullException("countryId");
            }
            int id = 0;
            bool isValid = Int32.TryParse(SchoolId, out id);


            var StudentSextra = Connection.SMGTloadScholExtraCadd(SchoolId, "%").ToList();

            var result2 = (from s in StudentSextra 
                          select new
                          {
                              ActivityCode = s.ActivityCode,
                              ActivityName = s.ActivityName
                          }).ToList();

            List<tblExtraCurricularActivity> result = StudentSextra.Select(x => new tblExtraCurricularActivity
            {
                ActivityCode = x.ActivityCode,
                ActivityName = x.ActivityName

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




        //
        // POST: /Student/Create

        [HttpPost]
        public ActionResult Create(StudentModel Model)
        {
            string _path = "";
            string _pathL = "";



            // return View("SchoolCreate");

           
            try
            {
                if (ModelState.IsValid)
                {
                    if (Model.StudentImageFile.ContentLength > 0)
                    {


                       
                       
                       

                        string fnm = DateTime.Now.ToString("");
                        string nwString22 = fnm.Replace("-", ".");
                        string nwString = nwString22.Replace("/", ".");
                        string nwString2 = nwString.Replace(" ", ".");
                        string time = nwString2.Replace(":", ".");

                        string _FileName = time + "_" + Path.GetFileName(Model.StudentImageFile.FileName);
                        _pathL = Path.Combine(Server.MapPath("~/Uploads/Student/Photo"), _FileName);
                        _path = "~/Uploads/Student/Photo/" + _FileName;
                        Model.StudentImageFile.SaveAs(_pathL);



                    }

                 

                    //  ViewBag.Message = "File Uploaded Successfully!!";  



                    Connection.SMGTsetStudent(Model.SchoolId, Model.StudentId, Model.StudentName, Model.DateOfBirth, Model.GradeId, Model.ClassId, "M", "User1", Model.HouseId, _path, "User1", "Y");

                    //    Model.District, Model.Description, UserId, Model.Address1, Model.Address2, Model.Address3, Model.Email, Model.Fax, _path, Convert.ToInt16(Model.MinuteforPeriod), Model.Telephone, Model.SchoolCategory, Model.Province, Model.WebAddress, _pathL
                    //    );
                    Connection.SaveChanges();


                    string result = "Success";
                    ModelState.Clear();
                 
                    //return View();


                }
                List<tblSubjectCategory> sclSubcatlist = Connection.tblSubjectCategories.ToList();
                ViewBag.SubcatscldrpList = new SelectList(sclSubcatlist, "SubjectCategoryId", "SubjectCategoryName");

                List<tblSchoolCategory> SCategorylist = Connection.tblSchoolCategories.ToList();
                ViewBag.SchoolCategoryDrpDown = new SelectList(SCategorylist, "SchoolCategoryId", "SchoolCategoryName");
                List<tblProvince> provincelist = Connection.tblProvinces.ToList();
                ViewBag.ProvinceDrpDown = new SelectList(provincelist, "ProvinceId", "ProvinceName");
                List<tblSchoolGroup> schoolgrps = Connection.tblSchoolGroups.ToList();
                ViewBag.SGroupDrpDown = new SelectList(schoolgrps, "GroupId", "GroupName");
                List<tblDistrict> districtlist = Connection.tblDistricts.ToList();
                ViewBag.DistrictDrpDown = new SelectList(districtlist, "DistrictId", "DistrictName");
                List<tblDivision> divisionlist = Connection.tblDivisions.ToList();
                ViewBag.DivisionDrpDown = new SelectList(divisionlist, "DivisionId", "DivisionName");
                List<tblSchoolRank> Ranklist = Connection.tblSchoolRanks.ToList();
                ViewBag.RankDrpDown = new SelectList(Ranklist, "SchoolRankId", "SchoolRankName");

                List<tblSubject> sclSublist = Connection.tblSubjects.ToList();
                ViewBag.SubjectscldrpList = new SelectList(sclSublist, "SubjectId", "SubjectName");

                List<tblExtraCurricularActivity> excatlist = Connection.tblExtraCurricularActivities.ToList();



                String SchoolId = Model.StudentId;

                List<tblSchool> Scllist = Connection.tblSchools.ToList();

                String sclid;
                if (SchoolId == null)
                {
                    sclid = "%";

                }
                else
                {

                    sclid = SchoolId;
                }


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


                ViewBag.SchoolDrpDown = new SelectList(Scllist, "SchoolId", "SchoolName");

                List<tblClass> Sclasslist = Connection.tblClasses.ToList();
                ViewBag.classDrpDown = new SelectList(Sclasslist, "ClassId", "ClassName");


                ViewBag.SGradeDrpDown = new SelectList(SchoolGradeList, "GradeId", "GradeName");

                ViewBag.HouseDrpDown = new SelectList(Shouselist, "HouseId", "HouseName");




                ViewBag.ActivitydrpList = new SelectList(excatlist, "ActivityCode", "ActivityName");
                var StudentSextra = Connection.SMGTloadScholExtraCadd("%", "%").ToList();

                List<tblExtraCurricularActivity> excList2 = StudentSextra.Select(x => new tblExtraCurricularActivity
                {
                    ActivityCode = x.ActivityCode,
                    ActivityName = x.ActivityName

                }).ToList();

                ViewBag.excdropdown = new SelectList(excList2, "ActivityCode", "ActivityName");

                return View("Create");





                //   return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                ViewBag.Message = "File upload failed!!";
                //  return PartialView("SchoolCreate");


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

                        Errorlog.ErrorManager.LogError(dbEx);

                    }
                }
                throw raise;
            }
        }

        //
        // GET: /Student/Edit/5

        public ActionResult Edit(string StudentId,string SchoolId)
        {
            StudentModel TModel = new StudentModel();

            tblStudent TCtable = Connection.tblStudents.SingleOrDefault(x => x.StudentId == StudentId && x.SchoolId == SchoolId);

             
            //  TModel.IsActive = TCtable.IsActive;

            
            TModel.StudentId = TCtable.StudentId;
            TModel.StudentName = TCtable.studentName;
            TModel.DateOfBirth = TCtable.DateofBirth;
            TModel.ClassId = TCtable.ClassId;
            TModel.Gender = TCtable.Gender;

            List<tblSchool> Scllist = Connection.tblSchools.ToList();

            String sclid;
            if (SchoolId == null)
            {
                sclid = "%";

            }
            else
            {

                sclid = SchoolId;
            }

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



            List<tblClass> Sclasslist = Connection.tblClasses.ToList();
            ViewBag.classDrpDown = new SelectList(Sclasslist, "ClassId", "ClassName");


            ViewBag.SGradeDrpDown = new SelectList(SchoolGradeList, "GradeId", "GradeName");


            ViewBag.HouseDrpDown = new SelectList(Shouselist, "HouseId", "HouseName");

            ViewBag.SchoolDrpDown = new SelectList(Scllist, "SchoolId", "SchoolName");


            List<tblSchoolCategory> SCategorylist = Connection.tblSchoolCategories.ToList();
            ViewBag.SchoolCategoryDrpDown = new SelectList(SCategorylist, "SchoolCategoryId", "SchoolCategoryName");
            //  ViewBag.SchoolCategoryDrpDown = TCtable.SchoolCategory;
            List<tblProvince> provincelist = Connection.tblProvinces.ToList();
            ViewBag.ProvinceDrpDown = new SelectList(provincelist, "ProvinceId", "ProvinceName");
            List<tblSchoolGroup> schoolgrps = Connection.tblSchoolGroups.ToList();
            ViewBag.SGroupDrpDown = new SelectList(schoolgrps, "GroupId", "GroupName");
            List<tblDistrict> districtlist = Connection.tblDistricts.ToList();
            ViewBag.DistrictDrpDown = new SelectList(districtlist, "DistrictId", "DistrictName");
            List<tblDivision> divisionlist = Connection.tblDivisions.ToList();
            ViewBag.DivisionDrpDown = new SelectList(divisionlist, "DivisionId", "DivisionName");
            List<tblSchoolRank> Ranklist = Connection.tblSchoolRanks.ToList();
            ViewBag.RankDrpDown = new SelectList(Ranklist, "SchoolRankId", "SchoolRankName");
            List<tblSubject> sclSublist = Connection.tblSubjects.ToList();
            ViewBag.SubjectscldrpList = new SelectList(sclSublist, "SubjectId", "SubjectName");
            List<tblExtraCurricularActivity> excatlist = Connection.tblExtraCurricularActivities.ToList();
            ViewBag.ActivitydrpList = new SelectList(excatlist, "ActivityCode", "ActivityName");
            List<tblSubjectCategory> sclSubcatlist = Connection.tblSubjectCategories.ToList();
            ViewBag.SubcatscldrpList = new SelectList(sclSubcatlist, "SubjectCategoryId", "SubjectCategoryName");
            TModel.HouseId = TCtable.HouseId;
            TModel.GradeId = TCtable.GradeId;
            TModel.SchoolId = TCtable.SchoolId;
         



            return View("Edit", TModel);






        }

        //
        // POST: /Student/Edit/5

        [HttpPost]
        public ActionResult Edit(StudentModel Model)
        {
            string _path = "";

            string _pathL = "";
            string _path1 = "";

            string _pathL2 = "";
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add update logic here
                    if (Model.StudentImageFile.ContentLength > 0)
                    {

                        string fnm = DateTime.Now.ToString("");
                        string nwString22 = fnm.Replace("-", ".");
                        string nwString = nwString22.Replace("/", ".");
                        string nwString2 = nwString.Replace(" ", ".");
                        string time = nwString2.Replace(":", ".");

                        string _FileName = time + "_" + Path.GetFileName(Model.StudentImageFile.FileName);




                        _path = Path.Combine(Server.MapPath("~/Uploads/Student/Photo"), _FileName);
                        _path1 = "~/Uploads/Student/Photo/" + _FileName;
                        Model.StudentImageFile.SaveAs(_path);
                    }

                    
                }





                SchoolModel TModel = new SchoolModel();
                StudentModel stdmodel = new StudentModel();

                tblStudent tblstudnt = Connection.tblStudents.SingleOrDefault(x => x.StudentId == Model.StudentId);


                stdmodel.SchoolId = tblstudnt.SchoolId;
                stdmodel.StudentId = tblstudnt.StudentId;
                stdmodel.StudentName = tblstudnt.studentName;
                stdmodel.ModifiedBy = "User1";
                stdmodel.ModifiedDate = DateTime.Now;
                stdmodel.GradeId = tblstudnt.GradeId;
                stdmodel.HouseId = tblstudnt.HouseId;
                stdmodel.ClassId = tblstudnt.ClassId;
                String tblpath = tblstudnt.ImgUrl;
                stdmodel.DateOfBirth = tblstudnt.DateofBirth;
                stdmodel.Gender = tblstudnt.Gender;

                tblSchool TCtable = Connection.tblSchools.SingleOrDefault(x => x.SchoolId == Model.SchoolId);
                //  TModel.IsActive = TCtable.IsActive;

       

                if (Model.Gender == null)
                {
                    Model.Gender = stdmodel.Gender;

                }

                if (Model.SchoolId == null) {

                    Model.SchoolId = stdmodel.SchoolId;
                
                }

                if (Model.StudentName == null)
                {

                    Model.StudentName = stdmodel.StudentName;

                }

                if (Model.GradeId == null)
                {

                    Model.GradeId = stdmodel.GradeId;

                }

                if (Model.HouseId == null)
                {

                    Model.HouseId = stdmodel.HouseId;

                }

                if (_path == "") {


                    _path = tblstudnt.ImgUrl;
                }







                //if (Model.Email == null)
                //{
                //    Model.Email = TModel.Email;

                //}
                //if (Model.MinuteforPeriod == null)
                //{
                //    Model.MinuteforPeriod = TModel.MinuteforPeriod;

                //}

                //if (Model.Telephone == null)
                //{
                //    Model.Telephone = TModel.Telephone;

                //}

                if (_path1 == "")
                {

                    _path1 = TModel.ImagePath;

                }
                if (_pathL2 == "")
                {

                    _pathL2 = TModel.LogoPath;

                }

             //   ModelState.


                Model.UserId = "User1";
                Model.ModifiedBy = "User1";
                Model.ModifiedDate = DateTime.Now;


                Connection.SMGTModifyStudent(Model.SchoolId, Model.StudentId, Model.StudentName, Model.DateOfBirth, Model.GradeId, Model.ClassId, Model.Gender, Model.UserId, Model.HouseId, _path, Model.ModifiedBy, "Y");
                //Connection.DCISModifySchool(Model.SchoolId, Model.SchoolGroup, Model.SchoolName, Model.SchoolRank, "Y", Model.Division,
                //   Model.District, Model.Description, UserId, Model.Address1, Model.Address2, Model.Address3, Model.Email, Model.Fax, _path1, Convert.ToInt16(Model.MinuteforPeriod), Model.Telephone, Model.SchoolCategory, Model.Province, Model.WebAddress, _pathL2
                //   );
                Connection.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Student/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Student/Delete/5

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
