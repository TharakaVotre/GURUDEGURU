using GDWEBSolution.Filters;
using GDWEBSolution.Models;
using GDWEBSolution.Models.Schools;
using GDWEBSolution.Models.Student;
using GDWEBSolution.Models.User;
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
        UserSession USession = new UserSession();

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
         [UserFilter(Function_Id = "STCF")]
        public ActionResult Index()
        {
           // Authentication("STCF");
            if (USession.User_Category == "SADMI")
            {

                var student = Connection.SMGTGetStudent(USession.School_Id);

                List<SMGTGetStudent_Result> Categorylist = student.ToList();
                StudentModel schl = new StudentModel();
                List<StudentModel> tcmlist = Categorylist.Select(x => new StudentModel
                {

                    SchoolId = x.SchoolId,
                    SchoolName = x.SchoolName,
                    DateOfBirth = x.DateofBirth,
                    CreatedDate = x.CreatedDate,
                    StudentId = x.StudentId,
                    StudentName = x.studentName,
                    ClassId = x.ClassName,
                    GradeId = x.GradeName,



                    IsActive = x.IsActive,
                    ModifiedBy = x.ModifiedBy,
                    ModifiedDate = x.ModifiedDate

                }).ToList();




                return View(tcmlist);


            }
            else
            {

                var student = Connection.SMGTGetStudent("%");

                List<SMGTGetStudent_Result> Categorylist = student.ToList();
                StudentModel schl = new StudentModel();
                List<StudentModel> tcmlist = Categorylist.Select(x => new StudentModel
                {

                    SchoolId = x.SchoolId,
                    SchoolName = x.SchoolName,
                    DateOfBirth = x.DateofBirth,
                    CreatedDate = x.CreatedDate,
                    StudentId = x.StudentId,
                    StudentName = x.studentName,
                    ClassId = x.ClassName,
                    GradeId = x.GradeName,



                    IsActive = x.IsActive,
                    ModifiedBy = x.ModifiedBy,
                    ModifiedDate = x.ModifiedDate

                }).ToList();




                return View(tcmlist);
            }
        }

        //
        // GET: /Student/Details/5
        [UserFilter(Function_Id = "STCF")]
        public ActionResult Details(string StudentId, string SchoolId)
        {

            Authentication("STCF");
            StudentModel TModel = new StudentModel();

            tblStudent TCtable = Connection.tblStudents.SingleOrDefault(x => x.StudentId == StudentId && x.SchoolId == SchoolId);


            //  TModel.IsActive = TCtable.IsActive;

            TModel.SchoolId = SchoolId;
            TModel.StudentId = TCtable.StudentId;
            TModel.StudentName = TCtable.studentName;




            TModel.DateOfBirth = TCtable.DateofBirth;

            if (TCtable.DateofBirth == null)
            {


            }
            else
            {

                string a = TCtable.DateofBirth.ToString();
                DateTime b = DateTime.Parse(a);
                TModel.datetimestring = b.ToShortDateString();
            }

            TModel.ClassId = TCtable.ClassId;
            TModel.Gender = TCtable.Gender;
            TModel.Gender = TCtable.Gender;
            TModel.ImagePathname = TCtable.ImgUrl;
            TModel.GradeId = TCtable.GradeId;
            TModel.HouseId = TCtable.HouseId;
            if (TCtable.ImgUrl == null||TCtable.ImgUrl =="")
            {
                TModel.ImagePathname = "~/Uploads/Student/Photo/no_image.jpg";

            }
            else
            {
                TModel.ImagePathname = TCtable.ImgUrl;
            }
            tblClass sclrank = Connection.tblClasses.SingleOrDefault(x =>  x.ClassId == TModel.ClassId &&x.GradeId==TModel.GradeId && x.SchoolId == SchoolId );
            TModel.ClassId = sclrank.ClassName;
            tblGrade sclgrade = Connection.tblGrades.SingleOrDefault(x => x.GradeId== TModel.GradeId);

            TModel.GradeId = sclgrade.GradeName;
            if (TModel.HouseId == null)
            {

            }
            else
            {
                tblHouse sclhouse = Connection.tblHouses.SingleOrDefault(x => x.SchoolId == SchoolId && x.HouseId == TModel.HouseId);
                TModel.HouseId = sclhouse.HouseName;
            }
            return View("Detail",TModel);
        }

        //
        // GET: /Student/Create
        [UserFilter(Function_Id = "STCF")]
        public ActionResult Create(String SchoolId)
        {
         //   Authentication("STCF");

            if (USession.User_Category == "SADMI")
            {

                List<tblSchool> Scllist = Connection.tblSchools.OrderBy(X=>X.SchoolName).Where(X => X.IsActive == "Y" && X.SchoolId==USession.School_Id).ToList();
                ViewBag.SchoolDrpDown = new SelectList(Scllist, "SchoolId", "SchoolName");

            }
            else {

                List<tblSchool> Scllist = Connection.tblSchools.OrderBy(X => X.SchoolName).Where(X => X.IsActive == "Y").ToList();
                ViewBag.SchoolDrpDown = new SelectList(Scllist, "SchoolId", "SchoolName");
            }
        
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
            
            }).Where(X=>X.IsActive=="Y").ToList();


           

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
        [UserFilter(Function_Id = "STCF")]
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
        [UserFilter(Function_Id = "STCF")]
        public ActionResult Create(StudentModel Model)
        {
         //   Authentication("STCF");

            string _path = "";
            string _pathL = "";

            String result = "error";



            // return View("SchoolCreate");

           
            try
            {
                if (ModelState.IsValid)
                {
                    if (Model.StudentImageFile == null)
                    {
                       
                       // ModelState.AddModelError("Email", "Email is not valid");

                    }
                    else
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
                    }
                 

                    //  ViewBag.Message = "File Uploaded Successfully!!";  

                    var counts = Connection.tblUsers.Count(u => u.UserId==Model.UserId);
                     var count = Connection.tblStudents.Count(u => u.SchoolId == Model.SchoolIdw && u.StudentId==Model.StudentId);
                     if (count == 0 && counts == 0)
                     {
                         Connection.SMGTsetStudent(Model.SchoolIdw, Model.StudentId, Model.StudentName, Model.DateOfBirth, Model.GradeId, Model.ClassId, Model.Gender, Model.UserId, Model.HouseId, _path, USession.User_Id, "Y");

                         //    Model.District, Model.Description, UserId, Model.Address1, Model.Address2, Model.Address3, Model.Email, Model.Fax, _path, Convert.ToInt16(Model.MinuteforPeriod), Model.Telephone, Model.SchoolCategory, Model.Province, Model.WebAddress, _pathL
                         //    );
                         Connection.SaveChanges();


                         //   string result = "Success";
                         ModelState.Clear();
                         result = "Success";


                         return Json(result, JsonRequestBehavior.AllowGet);
                         //return View();
                     }
                     else {

                        

                         result = "Exists";

                         if (counts != 0)
                         {

                             result = "UExists";
                         }

                         return Json(result, JsonRequestBehavior.AllowGet);
                     
                     }

                }
               

                return Json(result, JsonRequestBehavior.AllowGet);





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
        [UserFilter(Function_Id = "STCF")]
        public ActionResult Edit(string StudentId,string SchoolId)
        {
            Authentication("STCF");

            var StudentSextra = Connection.SMGTloadScholExtraCadd(SchoolId, "%").ToList();

            List<tblExtraCurricularActivity> excactivlist = StudentSextra.Select(x => new tblExtraCurricularActivity
            {
                ActivityCode = x.ActivityCode,
               ActivityName = x.ActivityName
            }).ToList();

            ViewBag.EXacDrpDown = new SelectList(excactivlist, "ActivityCode", "ActivityName");
            ViewBag.EditSchoolID = SchoolId;
            ViewBag.EditStudentID = StudentId;









         



            return View("Edit");






        }

        //
        // POST: /Student/Edit/5

        [HttpPost]
        [UserFilter(Function_Id = "STCF")]
        public ActionResult Edit(StudentModel Model)
        {
            Authentication("STCF");
            string _path = "";

          //  string _pathL = "";
            string _path1 = "";

            string _pathL2 = "";
            Model.SchoolName = "sad";
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add update logic here
                    if (Model.StudentImageFile != null)
                    {

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
                    
                }





                SchoolModel TModel = new SchoolModel();
                StudentModel stdmodel = new StudentModel();

                tblStudent tblstudnt = Connection.tblStudents.SingleOrDefault(x => x.StudentId == Model.StudentId);


                stdmodel.SchoolId = tblstudnt.SchoolId;
                stdmodel.StudentId = tblstudnt.StudentId;
                stdmodel.StudentName = tblstudnt.studentName;
                stdmodel.ModifiedBy = USession.User_Id;
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

                if (ModelState.IsValid)
                {
                    Connection.SMGTModifyStudent(Model.SchoolId, Model.StudentId, Model.StudentName, Model.DateOfBirth, Model.GradeId, Model.ClassId, Model.Gender, Model.UserId, Model.HouseId, _path, Model.ModifiedBy, "Y");
                    //Connection.DCISModifySchool(Model.SchoolId, Model.SchoolGroup, Model.SchoolName, Model.SchoolRank, "Y", Model.Division,
                    //   Model.District, Model.Description, UserId, Model.Address1, Model.Address2, Model.Address3, Model.Email, Model.Fax, _path1, Convert.ToInt16(Model.MinuteforPeriod), Model.Telephone, Model.SchoolCategory, Model.Province, Model.WebAddress, _pathL2
                    //   );
                    Connection.SaveChanges();
                }
                else{
                    return View();
                
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Student/Delete/5
        [UserFilter(Function_Id = "STCF")]
        public ActionResult Delete(String  StudentId)
        {
            StudentModel TModel = new StudentModel();
            TModel.StudentId = StudentId;
            TModel.IsActive = "N";


            return PartialView("Delete", TModel);

            
        }

        //
        // POST: /Student/Delete/5

        [HttpPost]
        [UserFilter(Function_Id = "STCF")]
        public ActionResult Delete(StudentModel TModel)
        {
            try
            {
                // TODO: Add delete logic here
               // Connection.SMGTDeleteSchool("N", TModel.SchoolId, UserId);
                Connection.SMGTDeleteStudent("N", TModel.StudentId, "Admin");

                Connection.SaveChanges();




                return View("Index");
            }
            catch
            {
                return View();
            }
        }


        [AllowAnonymous]
        [UserFilter(Function_Id = "STCF")]
        public JsonResult AddStudentExcActivity(StudentExtraCModel Model)
        {
            try
            {
                string result = "Error";
            //    var count2 = Connection.tblStudentExtraCurricularActivities.Count();
                // Model.HouseId = count2.ToString();
                if (Model.ActivityCode != null && Model.SchoolIdE != null && Model.StudentIdE != null)
                {

                    var count = Connection.tblStudentExtraCurricularActivities.Count(u => u.ActivityCode == Model.ActivityCode && u.SchoolId == Model.SchoolIdE && u.StudentId == Model.StudentIdE);
                    if (count == 0)
                    {
                        // Model.SchoolId = Schoold;
                        tblStudentExtraCurricularActivity newscg = new tblStudentExtraCurricularActivity();

                        newscg.CreatedBy = USession.User_Id;
                        newscg.CreatedDate = DateTime.Now;
                        newscg.SchoolId = Model.SchoolIdE;
                        newscg.StudentId = Model.StudentIdE;
                        newscg.ActivityCode = Model.ActivityCode;
                        newscg.IsActive = "Y";


                        Connection.tblStudentExtraCurricularActivities.Add(newscg);

                        Connection.SaveChanges();

                        result = "sessioncheck" + "!" + Model.SchoolIdE + "!" + Model.StudentIdE;

                        //  ViewBag.SchoolId = Model.SchoolId;

                    }
                    else
                    {
                        result = "Exits";
                    }
                }
                else {

                    result = "fill";
                
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


        [AllowAnonymous]
        [UserFilter(Function_Id = "STCF")]
        public JsonResult EAddStudentExcActivity(StudentExtraCModel Model)
        {
            try
            {
                string result = "Error";
                //    var count2 = Connection.tblStudentExtraCurricularActivities.Count();
                // Model.HouseId = count2.ToString();
                var count = Connection.tblStudentExtraCurricularActivities.Count(u => u.ActivityCode == Model.ActivityCode && u.SchoolId == Model.SchoolId && u.StudentId == Model.StudentId);
                if (count == 0)
                {
                    // Model.SchoolId = Schoold;
                    tblStudentExtraCurricularActivity newscg = new tblStudentExtraCurricularActivity();

                    newscg.CreatedBy = USession.User_Id;
                    newscg.CreatedDate = DateTime.Now;
                    newscg.SchoolId = Model.SchoolId;
                    newscg.StudentId = Model.StudentId;
                    newscg.ActivityCode = Model.ActivityCode;
                    newscg.IsActive = "Y";


                    Connection.tblStudentExtraCurricularActivities.Add(newscg);

                    Connection.SaveChanges();

                    result = "sessioncheck" + "!" + Model.SchoolId + "!" + Model.StudentId;

                    //  ViewBag.SchoolId = Model.SchoolId;

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

        [UserFilter(Function_Id = "STCF")]
        public ActionResult ShowstudentExactivty(string SchoolId, string StudentId)
        {


            List<StudentExtraCModel> List = loadSclEXtralist(SchoolId, StudentId);
          
            //List<tblSubjectCategory> sclSubcatlist = Connection.tblSubjectCategories.Where(X => X.IsActive == "Y").ToList();
            //ViewBag.SubcatscldrpList = new SelectList(sclSubcatlist, "SubjectCategoryId", "SubjectCategoryName");
            //List<tblSchoolCategory> SCategorylist = Connection.tblSchoolCategories.Where(X => X.IsActive == "Y").ToList();
            //ViewBag.SchoolCategoryDrpDown = new SelectList(SCategorylist, "SchoolCategoryId", "SchoolCategoryName");
            //List<tblProvince> provincelist = Connection.tblProvinces.Where(X => X.IsActive == "Y").ToList();
            //ViewBag.ProvinceDrpDown = new SelectList(provincelist, "ProvinceId", "ProvinceName");
            //List<tblSchoolGroup> schoolgrps = Connection.tblSchoolGroups.Where(X => X.IsActive == "Y").ToList();
            //ViewBag.SGroupDrpDown = new SelectList(schoolgrps, "GroupId", "GroupName");
            //List<tblDistrict> districtlist = Connection.tblDistricts.Where(X => X.IsActive == "Y").ToList();
            //ViewBag.DistrictDrpDown = new SelectList(districtlist, "DistrictId", "DistrictName");
            //List<tblDivision> divisionlist = Connection.tblDivisions.Where(X => X.IsActive == "Y").ToList();
            //ViewBag.DivisionDrpDown = new SelectList(divisionlist, "DivisionId", "DivisionName");
            //List<tblSchoolRank> Ranklist = Connection.tblSchoolRanks.Where(X => X.IsActive == "Y").ToList();
            //ViewBag.RankDrpDown = new SelectList(Ranklist, "SchoolRankId", "SchoolRankName");
            //List<tblSubject> sclSublist = Connection.tblSubjects.Where(X => X.IsActive == "Y").ToList();
            //ViewBag.SubjectscldrpList = new SelectList(sclSublist, "SubjectId", "SubjectName");
            //List<tblExtraCurricularActivity> excatlist = Connection.tblExtraCurricularActivities.Where(X => X.IsActive == "Y").ToList();
            //ViewBag.ActivitydrpList = new SelectList(excatlist, "ActivityCode", "ActivityName");
                    //   string GradeId = ""; ;
          
            //    string username = result.Consignor.Split('<')[0];
                       return PartialView("Stdexclist", List);
        }

        private List<StudentExtraCModel> loadSclEXtralist(string SchoolId, string StudentId)
        {

            var STQlist = Connection.SMGTgetStudentExtraCadd(SchoolId,StudentId).ToList();

            List<StudentExtraCModel> List = STQlist.Select(x => new StudentExtraCModel
            {

                ActivityCode = x.ActivityCode,
                ActivityName = x.ActivityName,
                SchoolId = x.SchoolId,
               StudentId=x.StudentId

                //GradeId = x.GradeId,
                //SchoolName = x.SchoolName,
                //GradeName = x.GradeName,

             


            }).ToList();
            return List;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        [UserFilter(Function_Id = "STCF")]
        public ActionResult HouseDropdown(string SchoolId)
        {
            var STQlist = Connection.SMGTgetSchoolHouseadd(SchoolId).ToList();
            var result2 = (from s in STQlist
                           select new
                           {
                               HouseId = s.HouseId,
                               HouseName = s.HouseName,

                           }).ToList();

            return Json(result2, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Deletescexc(string SchoolId,string StudentId, string ActivityCode)
        {
            StudentExtraCModel Model = new StudentExtraCModel();

            Model.ActivityCode = ActivityCode;
            Model.SchoolId = SchoolId;
            Model.StudentId = StudentId;


            return PartialView("DeleteStudentEXactvity", Model);
        }


        [HttpPost]
        [UserFilter(Function_Id = "STCF")]
        public ActionResult DeleteStudentexcactivities(StudentExtraCModel Model)
        {
            try
            {

                tblStudentExtraCurricularActivity Tble = Connection.tblStudentExtraCurricularActivities.Find(Model.StudentId, Model.SchoolId, Model.ActivityCode);
                Connection.tblStudentExtraCurricularActivities.Remove(Tble);
                Connection.SaveChanges();


                var item = Connection.tblStudentExtraCurricularActivities.FirstOrDefault(i => i.SchoolId == Model.SchoolId && i.ActivityCode == Model.ActivityCode);

                //tblStudentExtraCurricularActivity Tblew = Connection.tblStudentExtraCurricularActivities.Find(item.ActivityCode);

                //Connection.tblStudentExtraCurricularActivities.Remove(Tblew);
                //  Connection.SaveChanges();

                string s = Model.SchoolId + "!" + Model.StudentId;
                return Json(s, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("Index");
            }
            catch
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }



        public ActionResult ShowEditStudent(string StudentId, string SchoolId)
        {






            StudentModel TModel = new StudentModel();

            tblStudent TCtable = Connection.tblStudents.SingleOrDefault(x => x.StudentId == StudentId && x.SchoolId == SchoolId);

           tblSchool schl  = Connection.tblSchools.SingleOrDefault(x => x.SchoolId == SchoolId);
            //  TModel.IsActive = TCtable.IsActive;

          string SchoolName = schl.SchoolName;
            TModel.StudentId = TCtable.StudentId;
            TModel.StudentName = TCtable.studentName;
            TModel.SchoolName = SchoolName;
            TModel.DateOfBirth = TCtable.DateofBirth;
            TModel.ClassId = TCtable.ClassId;
            TModel.Gender = TCtable.Gender;
            TModel.UserId = TCtable.UserId;

            List<tblSchool> Scllist = Connection.tblSchools.Where(X => X.IsActive == "Y").ToList();

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



            List<tblClass> Sclasslist = Connection.tblClasses.Where(X => X.IsActive == "Y").ToList();
            ViewBag.classDrpDown = new SelectList(Sclasslist, "ClassId", "ClassName");


            ViewBag.SGradeDrpDown = new SelectList(SchoolGradeList, "GradeId", "GradeName");


            ViewBag.HouseDrpDown = new SelectList(Shouselist, "HouseId", "HouseName");

            ViewBag.SchoolDrpDown = new SelectList(Scllist, "SchoolId", "SchoolName");


            List<tblSchoolCategory> SCategorylist = Connection.tblSchoolCategories.Where(X => X.IsActive == "Y").ToList();
            ViewBag.SchoolCategoryDrpDown = new SelectList(SCategorylist, "SchoolCategoryId", "SchoolCategoryName");
            //  ViewBag.SchoolCategoryDrpDown = TCtable.SchoolCategory;
            List<tblProvince> provincelist = Connection.tblProvinces.Where(X => X.IsActive == "Y").ToList();
            ViewBag.ProvinceDrpDown = new SelectList(provincelist, "ProvinceId", "ProvinceName");
            List<tblSchoolGroup> schoolgrps = Connection.tblSchoolGroups.Where(X => X.IsActive == "Y").ToList();
            ViewBag.SGroupDrpDown = new SelectList(schoolgrps, "GroupId", "GroupName");
            List<tblDistrict> districtlist = Connection.tblDistricts.Where(X => X.IsActive == "Y").ToList();
            ViewBag.DistrictDrpDown = new SelectList(districtlist, "DistrictId", "DistrictName");
            List<tblDivision> divisionlist = Connection.tblDivisions.Where(X => X.IsActive == "Y").ToList();
            ViewBag.DivisionDrpDown = new SelectList(divisionlist, "DivisionId", "DivisionName");
            List<tblSchoolRank> Ranklist = Connection.tblSchoolRanks.Where(X => X.IsActive == "Y").ToList();
            ViewBag.RankDrpDown = new SelectList(Ranklist, "SchoolRankId", "SchoolRankName");
            List<tblSubject> sclSublist = Connection.tblSubjects.Where(X => X.IsActive == "Y").ToList();
            ViewBag.SubjectscldrpList = new SelectList(sclSublist, "SubjectId", "SubjectName");
            List<tblExtraCurricularActivity> excatlist = Connection.tblExtraCurricularActivities.Where(X => X.IsActive == "Y").ToList();
            ViewBag.ActivitydrpList = new SelectList(excatlist, "ActivityCode", "ActivityName");
            List<tblSubjectCategory> sclSubcatlist = Connection.tblSubjectCategories.Where(X => X.IsActive == "Y").ToList();
            ViewBag.SubcatscldrpList = new SelectList(sclSubcatlist, "SubjectCategoryId", "SubjectCategoryName");
            TModel.HouseId = TCtable.HouseId;
           TModel.GradeId = TCtable.GradeId;
            TModel.SchoolId = TCtable.SchoolId;
            TModel.SchoolIdw = TCtable.SchoolId;


            var SchoolClass = Connection.SMGTgetclassadd(SchoolId, TModel.GradeId).OrderBy(X => X.ClassName).ToList();

            List<tblClass> SchoolGradeclasseList = SchoolClass.Select(x => new tblClass
            {
              ClassId=x.ClassId,
              ClassName=x.ClassName

            }).ToList();

            ViewBag.SelectedschoolList = new SelectList(SchoolGradeclasseList, "ClassId", "ClassName");









            return PartialView("StudentEditView", TModel);
        }



        public ActionResult ShowEditExtra(string StudentId, string SchoolId)
        {






            StudentExtraCModel TModel = new StudentExtraCModel();

            tblStudent TCtable = Connection.tblStudents.SingleOrDefault(x => x.StudentId == StudentId && x.SchoolId == SchoolId);

            tblSchool schl = Connection.tblSchools.SingleOrDefault(x => x.SchoolId == SchoolId);
            //  TModel.IsActive = TCtable.IsActive;

            string SchoolName = schl.SchoolName;
            TModel.StudentId = TCtable.StudentId;
            TModel.StudentName = TCtable.studentName;
            TModel.SchoolName = SchoolName;


            var StudentSextra = Connection.SMGTloadScholExtraCadd(SchoolId, "%").ToList();

          

            List<tblExtraCurricularActivity> result = StudentSextra.Select(x => new tblExtraCurricularActivity
            {
                ActivityCode = x.ActivityCode,
                ActivityName = x.ActivityName

            }).ToList();
            ViewBag.studentextrac = new SelectList(StudentSextra, "ActivityCode", "ActivityName");

       


          



        
           


           


        
         
            TModel.SchoolId = TCtable.SchoolId;









            return PartialView("EditStudentEXtra", TModel);
        }



    }
}
