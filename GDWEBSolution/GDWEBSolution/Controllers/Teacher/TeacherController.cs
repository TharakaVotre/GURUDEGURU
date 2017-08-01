using GDWEBSolution.Models;
using GDWEBSolution.Models.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Errorlog;
using GDWEBSolution.Util;
using System.Transactions;
using GDWEBSolution.Models.User;
using GDWEBSolution.Filters;
/*
 * Created Date : 2017/2/23
 * Author : Tharaka Madusanka
 */
namespace GDWEBSolution.Controllers.Teacher
{
    public class TeacherController : Controller
    {
        SchoolMGTEntitiesConnectionString Connection = new SchoolMGTEntitiesConnectionString();

        static string DECKey = System.Configuration.ConfigurationManager.AppSettings["DecKey"];
        string TeacherCatID = System.Configuration.ConfigurationManager.AppSettings["Teacher"];

        string Password = DECKey.Substring(10);

        UserSession _session = new UserSession();

        TeacherModel tcm = new TeacherModel();

        [UserFilter(Function_Id = "TINDX")]
        public ActionResult Index()
        {          
            var TeacherList = Connection.SMGTgetAllTeachers(_session.School_Id,"%","Y").ToList();
            List<TeacherModel> tcmlist = TeacherList.Select(x => new TeacherModel
            {
                TeacherCategoryId = x.TeacherCategoryId,
                TeacherCategoryName = x.TeacherCategoryName,
                UserId = x.UserId,
                Name = x.Name,
                Telephone = x.Telephone,
                Address1 = x.Address1,
                Address2 = x.Address2,
                Address3 = x.Address3,
                Gender = x.Gender,
                Description = x.Description,
                EmployeeNo = x.EmployeeNo,
                NIC = x.NIC,
                DateOfBirth = x.DateOfBirth,
                DrivingLicense = x.DrivingLicense,
                Passport = x.Passport,
                ImgUrl = x.ImgUrl,
                TeacherId = x.TeacherId,

                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate,
                IsActive = x.IsActive,
                ModifiedBy = x.ModifiedBy,
                ModifiedDate = x.ModifiedDate

            }).ToList();

            return View(tcmlist);
        }
        [UserFilter(Function_Id = "TCLAS")]
        public ActionResult Class()
        {
            ClassTeacherModel Model = new ClassTeacherModel();
            SubjctViewBags();
            var AY = Connection.tblAccadamicYears.Where(u => u.SchoolId == _session.School_Id).FirstOrDefault();
            var STQlist = Connection.SMGT_getSchoolClassTeachersList(_session.School_Id,
                                                                    AY.AccadamicYear).ToList();

            Model.ClassTeacherList = STQlist.Select(x => new ClassTeacherModel
            {
                SchoolId = x.SchoolId,
                TeacherId = x.TeacherId,
                TeacherName = x.Name,
                GradeId = x.GradeId,
                GradeName = x.GradeName,
                ClassId = x.ClassId,
                ClassName = x.ClassName,
                AccedamicYear = x.AccedamicYear,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate,
                ModifiedBy = x.ModifiedBy,
                ModifiedDate = x.ModifiedDate,
                IsActive = x.IsActive,
            }).ToList();
            return View(Model);
        }

        public ActionResult Details(long TeacherId)
        {
            ViewBag.TeacherDetailsID = TeacherId;
            return View();
        }

       [AllowAnonymous]
        public string IsUserNameExits(string input)
        {
            var count = Connection.tblUsers.Count(u => u.UserId == input);
            if (count != 0){return "Have";}
            else{return "NO";}
        }

       [AllowAnonymous]
       public string IsLoginEmailExits(string loginEmail)
       {
           var count = Connection.tblUsers.Count(u => u.LoginEmail == loginEmail);
           if (count != 0){return "Have";}
           else{return "NO";}
       }

        [UserFilter(Function_Id = "TCRET")]
        public ActionResult Create()
        {
            CQExCViewBags();
            SubjctViewBags();
            ViewBag._SeesionSchoolId = _session.School_Id;
            return View();
        }

        private void CQExCViewBags()
        {
            List<tblTeacherCategory> TCategorylist = Connection.tblTeacherCategories.ToList();
            ViewBag.TeacherCategoryDrpDown = new SelectList(TCategorylist,
                                                            "TeacherCategoryId",
                                                            "TeacherCategoryName");

            List<tblQualification> TQlist = Connection.tblQualifications.ToList();
            ViewBag.QualificationList = new SelectList(TQlist, "QualificationId", "QualificationName");

            List<tblSchool> TsList = Connection.tblSchools.ToList();
            ViewBag.TScholls = new SelectList(TsList, "SchoolId", "SchoolName");

            TeacherDrpList();

            List<SMGT_getSchoolExActivities_Result> Exlist = Connection.SMGT_getSchoolExActivities(
                                                             _session.School_Id).ToList();
            ViewBag.ExtraActivityList = new SelectList(Exlist, "ActivityCode", "ActivityName");
        }

        private void TeacherDrpList()
        {
            var SchoolTeacher = Connection.SMGTgetSchoolTeacher(_session.School_Id).ToList();//Need to Pass a Session Schoolid
            List<TeacherModel> SchooTList = SchoolTeacher.Select(x => new TeacherModel
            {
                TeacherId = x.TeacherId,
                Name = x.Name,
                UserId = x.UserId,
                IsActive = x.IsActive

            }).ToList();

            ViewBag.SchoolTeacher = new SelectList(SchooTList, "TeacherId", "Name");
          //List<tblTeacher> Tlist = Connection.tblTeachers.ToList(); //Not By school
            ViewBag.TeacherList = new SelectList(SchoolTeacher, "TeacherId", "Name");
            ViewBag.TeacherListEx = new SelectList(SchoolTeacher, "TeacherId", "Name");
        }

        private void SubjctViewBags()
        {
            var SchoolTeacher = Connection.SMGTgetSchoolTeacher(_session.School_Id).ToList();//Need to Pass a Session Schoolid
            List<TeacherModel> SchooTList = SchoolTeacher.Select(x => new TeacherModel
            {
                TeacherId = x.TeacherId,
                Name = x.Name,
                UserId = x.UserId,
                IsActive = x.IsActive

            }).ToList();

            ViewBag.SchoolTeacher = new SelectList(SchooTList, "TeacherId", "Name");

            var SchoolGrade = Connection.SMGTgetSchoolGrade(_session.School_Id).ToList();//Need to Pass a Session Schoolid
            List<tblGrade> SchoolGradeList = SchoolGrade.Select(x => new tblGrade
            {
                GradeId = x.GradeId,
                GradeName = x.GradeName,
                IsActive = x.IsActive

            }).ToList();
            ViewBag.SchoolGrades = new SelectList(SchoolGradeList, "GradeId", "GradeName");
        }

        public ActionResult ShowTeacherQualification(int TeacherId)
        {
            var STQlist = Connection.SMGTgetTeacherQualification(TeacherId).ToList();
            
            List<QualificationModel> List = STQlist.Select(x => new QualificationModel
            {
                Teacher_Id = x.TeacherId,
                SchoolId = x.SchoolId,
                QualificationName = x.QualificationName,
                IsActive = x.IsActive,
                QualificationId = x.QualificationId

            }).ToList();
            return PartialView("QualificationList", List);
        }

        public ActionResult ShowTeacherQualificationDetails(int TeacherId)
        {
            var STQlist = Connection.SMGTgetTeacherQualification(TeacherId).ToList();

            List<QualificationModel> List = STQlist.Select(x => new QualificationModel
            {
                Teacher_Id = x.TeacherId,
                SchoolId = x.SchoolId,
                QualificationName = x.QualificationName,
                IsActive = x.IsActive,
                QualificationId = x.QualificationId

            }).ToList();
            return PartialView("QualificationListDView", List);
        }

        public ActionResult ShowTeacherExActivity(int TeacherId)
        {
            var STQlist = Connection.SMGTgetTeacherExActivity(TeacherId).ToList();

            List<ExtraActivityModel> List = STQlist.Select(x => new ExtraActivityModel
            {
                TeacherID = x.TeacherId,
                SchoolId = x.SchoolId,
                ActivityCode = x.ActivityCode,
                ActivityName = x.ActivityName,
                IsActive = x.IsActive,

            }).ToList();
            return PartialView("ExtraCurricularActivityList", List);
        }

        public ActionResult ShowTeacherExActivityDetails(int TeacherId)
        {
            var STQlist = Connection.SMGTgetTeacherExActivity(TeacherId).ToList();

            List<ExtraActivityModel> List = STQlist.Select(x => new ExtraActivityModel
            {
                TeacherID = x.TeacherId,
                SchoolId = x.SchoolId,
                ActivityCode = x.ActivityCode,
                ActivityName = x.ActivityName,
                IsActive = x.IsActive,

            }).ToList();
            return PartialView("ExtraAcitvityDView", List);
        }

        public ActionResult ShowTeacherSubjects(int TeacherId)
        {
            var STQlist = Connection.SMGTgetTeacherSubjects(TeacherId).ToList();

            List<TSubjectModel> List = STQlist.Select(x => new TSubjectModel
            {
                GradeName = x.GradeName,
                ClassId = x.ClassId,
                SubjectName = x.SubjectName,
                IsActive = x.IsActive,
                Teacher_ID = x.TeacherId,
                TeacherSubjectSeqNo = x.TeacherSubjectSeqNo

            }).ToList();
            return PartialView("TeacherSubjects", List);
        }

        public ActionResult ShowTeacherSubjectsDetails(int TeacherId)
        {
            var STQlist = Connection.SMGTgetTeacherSubjects(TeacherId).ToList();

            List<TSubjectModel> List = STQlist.Select(x => new TSubjectModel
            {
                GradeName = x.GradeName,
                ClassId = x.ClassId,
                SubjectName = x.SubjectName,
                IsActive = x.IsActive,
                Teacher_ID = x.TeacherId,
                TeacherSubjectSeqNo = x.TeacherSubjectSeqNo

            }).ToList();
            return PartialView("TeacherSubjectDetails", List);
        }

        public ActionResult ShowGradeClasses(string GradeId)
        {
            List<tblClass> ClassesList = Connection.tblClasses.Where(r => r.SchoolId == _session.School_Id 
                                                                    && r.GradeId == GradeId).ToList();
            ViewBag.GradeClassse = new SelectList(ClassesList, "ClassId", "ClassName");

            var GradeSubject = Connection.SMGTgetGradeSubjects(GradeId, _session.School_Id).ToList();
            List<tblSubject> GradeSubjectList = GradeSubject.Select(x => new tblSubject
            {
                SubjectId = x.SubjectId,
                ShortName = x.ShortName,
                SubjectName = x.SubjectName,
                IsActive = x.IsActive

            }).ToList();
            ViewBag.GradeSubjectList = new SelectList(GradeSubjectList, "SubjectId", "SubjectName");
            return PartialView("ClassNSubjects");
        }

        public ActionResult ShowGradeClassesForTC(string GradeId)
        {
            List<tblClass> ClassesList = Connection.tblClasses.Where(r => r.SchoolId == _session.School_Id 
                                                                    && r.GradeId == GradeId).ToList();
            ViewBag.GradeClassse = new SelectList(ClassesList, "ClassId", "ClassName");

            return PartialView("GradeClasses");
        }

        [HttpPost]
        public JsonResult CreateTeacher(TeacherModel Model)
        {
            string result = "Error";
            using (SchoolMGTEntitiesConnectionString Connection = new SchoolMGTEntitiesConnectionString())
            {
                using (var scope = new TransactionScope())
                {
                    try
                    {             
                        tblTeacher Newt = new tblTeacher();

                        Newt.CreatedBy = _session.User_Id;
                        Newt.CreatedDate = DateTime.Now;
                        Newt.TeacherCategoryId = 0;
                        Newt.TeacherCategoryId = Model.TeacherCategoryId;
                        Newt.DateOfBirth = Model.DateOfBirth;
                        Newt.Address1 = Model.Address1;
                        Newt.Address2 = Model.Address2;
                        Newt.Address3 = Model.Address3;
                        Newt.Telephone = Model.Telephone;
                        Newt.Gender = Model.Gender;
                        Newt.Description = Model.Description;
                        Newt.EmployeeNo = Model.EmployeeNo;
                        Newt.DrivingLicense = Model.DrivingLicense;
                        Newt.NIC = Model.NIC;
                        Newt.Name = Model.Name;
                        Newt.Passport = Model.Passport;
                        Newt.UserId = Model.UserId;
                        Newt.IsActive = "Y";

                        Connection.tblTeachers.Add(Newt);
                        Connection.SaveChanges();

                        var TID = Connection.tblTeachers.Where(b => b.UserId == Model.UserId).FirstOrDefault();

                        tblTeacherSchool ts = new tblTeacherSchool();
                        ts.SchoolId = Model.SchoolID;
                        ts.TeacherCategoryId = TID.TeacherCategoryId;
                        ts.TeacherId = TID.TeacherId;
                        ts.CreatedBy = _session.User_Id;
                        ts.CreatedDate = DateTime.Now;
                        ts.IsActive = "Y";

                        tblUser user = new tblUser();

                        string pass = Encrypt_Decrypt.Encrypt(Model.Password, Password);

                        user.PersonName = Model.Name;
                        user.CreatedBy = _session.User_Id;
                        user.CreatedDate = DateTime.Now;
                        user.IsActive = "Y";
                        user.JobDescription = "School Teacher";
                        user.LoginEmail = Model.LoginEmail;
                        user.Mobile = Model.Telephone;
                        user.Password = pass;
                        user.UserCategory = TeacherCatID;
                        user.UserId = Model.UserId;
                        user.SchoolId = Model.SchoolID;

                        Connection.tblUsers.Add(user);
                        Connection.tblTeacherSchools.Add(ts);

                        Connection.SaveChanges();

                        result = "Success";
                        scope.Complete();
                     
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                    {
                        //Exception raise = dbEx;
                        //foreach (var validationErrors in dbEx.EntityValidationErrors)
                        //{
                        //    foreach (var validationError in validationErrors.ValidationErrors)
                        //    {
                        //        string message = string.Format("{0}:{1}",
                        //            validationErrors.Entry.Entity.ToString(),
                        //            validationError.ErrorMessage);
                        //        // raise a new exception nesting  
                        //        // the current instance as InnerException  
                        //        raise = new InvalidOperationException(message, raise);
                        //    }
                        //}
                        Errorlog.ErrorManager.LogError("public JsonResult CreateTeacher(TeacherModel Model) @TeacherController", dbEx);
                       // throw raise;                      
                    }                 
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public JsonResult AddQualification(QualificationModel Model)
        {
            try
            {
                var count = Connection.tblTeacherQualifications.Count(u => u.TeacherId == Model.Teacher_Id && 
                            u.QualificationId == Model.QualificationId);
                if (count == 0)
                {
                    tblTeacherQualification NewQ = new tblTeacherQualification();
                    NewQ.CreatedBy = _session.User_Id;
                    NewQ.CreatedDate = DateTime.Now;
                    NewQ.IsActive = "Y";
                    NewQ.QualificationId = Model.QualificationId;
                    NewQ.SchoolId = _session.School_Id;
                    NewQ.TeacherId = Model.Teacher_Id;

                    Connection.tblTeacherQualifications.Add(NewQ);
                    Connection.SaveChanges();

                    //result = Model.Teacher_Id.ToString();

                    ViewBag.TeacherId = Model.Teacher_Id.ToString();
                    var result = new { Teacher_Id = Model.Teacher_Id, msg = "Success" };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var result = new { Teacher_Id = Model.Teacher_Id, msg = "Exits" };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
               
            }
            catch (Exception Ex)
            {
                Errorlog.ErrorManager.LogError("Teacher Controller - AddQualification(QualificationModel Model)",Ex);
                var result = new { Teacher_Id = Model.Teacher_Id, msg = "Exception" };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [AllowAnonymous]
        public JsonResult AddExtraActivity(ExtraActivityModel Model)
        {
            try
            {
                var count = Connection.tblTeacherExtraCurricularActivities.Count(
                                            u => u.TeacherId == Model.TeacherID && 
                                            u.ActivityCode == Model.ActivityCode);
                if (count == 0)
                {

                    tblTeacherExtraCurricularActivity NewQ = new tblTeacherExtraCurricularActivity();

                    NewQ.CreatedBy = _session.User_Id;
                    NewQ.CreatedDate = DateTime.Now;
                    NewQ.IsActive = "Y";
                    NewQ.ActivityCode = Model.ActivityCode;
                    NewQ.SchoolId = _session.School_Id;
                    NewQ.TeacherId = Model.TeacherID;

                    Connection.tblTeacherExtraCurricularActivities.Add(NewQ);
                    Connection.SaveChanges();

                    var result = new { Teacher_Id = Model.TeacherID, msg = "Success" };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var result = new { Teacher_Id = Model.TeacherID, msg = "Exits" };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception Ex)
            {
                Errorlog.ErrorManager.LogError("Teacher Controller - AddExtraActivity(ExtraActivityModel Model)", Ex);
                var result = new { Teacher_Id = Model.TeacherID, msg = "Exception" };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [AllowAnonymous]
        public JsonResult AddTeacherSubject(TSubjectModel Model)
        {
            try
            {
                string result = "Error";
                var count = Connection.tblTeacherSubjects.Count(
                                        u => u.ClassId == Model.ClassId &&
                                        u.GradeId == Model.GradeId && u.SubjectId == Model.SubjectId);
                if (count == 0)
                {
                    var AY = Connection.tblAccadamicYears.Where(u => u.SchoolId == _session.School_Id).FirstOrDefault();
                    tblTeacherSubject NewQ = new tblTeacherSubject();

                    NewQ.CreatedBy = _session.User_Id;
                    NewQ.CreatedDate = DateTime.Now;
                    NewQ.IsActive = "Y";
                    NewQ.AcedemicYear = AY.AccadamicYear;
                    NewQ.SchoolIds = _session.School_Id;
                    NewQ.GradeId = Model.GradeId;
                    NewQ.ClassId = Model.ClassId;
                    NewQ.TeacherId = Model.Teacher_ID;
                    NewQ.SubjectId = Model.SubjectId;

                    Connection.tblTeacherSubjects.Add(NewQ);
                    Connection.SaveChanges();

                    result = Model.Teacher_ID.ToString();
                }
                else
                {
                    result = "Exits";
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Errorlog.ErrorManager.LogError("Teacher Controller - AddTeacherSubject(TSubjectModel Model)", Ex);
                return Json("Exception", JsonRequestBehavior.AllowGet);
            }
        }

        [AllowAnonymous]
        public JsonResult AddClassTeacher(ClassTeacherModel Model)
        {
            try
            {
                string result = "Error";
                var AY = Connection.tblAccadamicYears.Where(u => u.SchoolId == _session.School_Id).FirstOrDefault();
               // List<tblClass> ClassesList = Connection.tblClasses.Where(r => r.SchoolId == "CKC" && r.GradeId == GradeId).ToList();
                int countt = Connection.tblClassTeachers.Count(u => u.TeacherId == Model.TeacherId 
                                                                && u.AccedamicYear == AY.AccadamicYear 
                                                                && u.IsActive == "Y");

                int Ccount = Connection.tblClassTeachers.Count(u => u.ClassId == Model.ClassId
                    && u.AccedamicYear == AY.AccadamicYear && u.GradeId == Model.GradeId && u.SchoolId == _session.School_Id && u.IsActive == "Y");

                if (countt != 0)
                {
                    result = "TExits";
                    //ViewBag.TeacherId = Model.TeacherID.ToString();
                }
                else if (Ccount != 0)
                {
                    result = "CExits";
                }
                else
                {
                    tblClassTeacher NewQ = new tblClassTeacher();

                    NewQ.CreatedBy = _session.User_Id;
                    NewQ.CreatedDate = DateTime.Now;
                    NewQ.IsActive = "Y";
                    NewQ.AccedamicYear = AY.AccadamicYear;
                    NewQ.SchoolId = AY.SchoolId;
                    NewQ.GradeId = Model.GradeId;
                    NewQ.ClassId = Model.ClassId;
                    NewQ.TeacherId = Model.TeacherId;

                    Connection.tblClassTeachers.Add(NewQ);
                    Connection.SaveChanges();

                    result = "Success";
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Errorlog.ErrorManager.LogError("Teacher Controller - AddClassTeacher(ClassTeacherModel Model)", Ex);
                return Json("Exception", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ShowClassTeachers()
        {
            var AY = Connection.tblAccadamicYears.Where(u => u.SchoolId == _session.School_Id).FirstOrDefault();
            var STQlist = Connection.SMGT_getSchoolClassTeachersList(_session.School_Id,AY.AccadamicYear).ToList();

            List<ClassTeacherModel> List = STQlist.Select(x => new ClassTeacherModel
            {
                SchoolId = x.SchoolId,
                TeacherId = x.TeacherId,
                TeacherName = x.Name,
                GradeId = x.GradeId,
                GradeName = x.GradeName,
                ClassId = x.ClassId,
                ClassName = x.ClassName,
                AccedamicYear = x.AccedamicYear,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate,
                ModifiedBy = x.ModifiedBy,
                ModifiedDate = x.ModifiedDate,
                IsActive = x.IsActive,
            }).ToList();
            return PartialView("ClassTeacherView", List);
        }

        public ActionResult DeleteExActivity(int Teacherid,string Schoolid, string Activitycode)
        {
            ExtraActivityModel TModel = new ExtraActivityModel();
            TModel.TeacherID = Teacherid;
            TModel.SchoolId = Schoolid;
            TModel.ActivityCode = Activitycode;
            return PartialView("DeleteTeacherExActivity", TModel);
        }

        [HttpPost]
        public ActionResult DeleteExActivity(ExtraActivityModel Model)
        {
            try
            {
                tblTeacherExtraCurricularActivity Tble = Connection.tblTeacherExtraCurricularActivities.Find
                                                         (Model.TeacherID,
                                                          Model.SchoolId, 
                                                          Model.ActivityCode);
                Connection.tblTeacherExtraCurricularActivities.Remove(Tble);
                Connection.SaveChanges();
                return Json(Model.TeacherID, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteQualification(int Teacherid, string Schoolid, int Qualificationid)
        {
            QualificationModel TModel = new QualificationModel();
            TModel.Teacher_Id = Teacherid;
            TModel.SchoolId = Schoolid;
            TModel.QualificationId = Qualificationid;
            return PartialView("DeleteTeacherQalification", TModel);
        }

        [HttpPost]
        public ActionResult DeleteQualification(QualificationModel Model)
        {
            try
            {
                tblTeacherQualification Tble = Connection.tblTeacherQualifications.Find
                                               (Model.Teacher_Id, 
                                                Model.SchoolId, 
                                                Model.QualificationId);
                Connection.tblTeacherQualifications.Remove(Tble);
                Connection.SaveChanges();
                return Json(Model.Teacher_Id, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult DeleteTeacherSubject(long TSubjectId,long TeacherId)
        {
            TSubjectModel TModel = new TSubjectModel();
            TModel.TeacherSubjectSeqNo = TSubjectId;
            TModel.Teacher_ID = TeacherId;
            return PartialView("DeleteTeacherSubject", TModel);
        }

        [HttpPost]
        public ActionResult DeleteTeacherSubject(TSubjectModel Model)
        {
            try
            {
                tblTeacherSubject Tble = Connection.tblTeacherSubjects.Find(Model.TeacherSubjectSeqNo);
                Connection.tblTeacherSubjects.Remove(Tble);
                Connection.SaveChanges();
                return Json(Model.Teacher_ID, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ShowTeacherDetails(long TeacherId)
        {
            TeacherModel TModel = new TeacherModel();

            var TCtable = Connection.SMGTgetTeacher(TeacherId).First();
            TModel.TeacherCategoryName = TCtable.TeacherCategoryName;
            TModel.IsActive = TCtable.IsActive;
            TModel.Address1 = TCtable.Address1;
            TModel.Address2 = TCtable.Address2;
            TModel.Address3 = TCtable.Address3;
            TModel.DateOfBirth = TCtable.DateOfBirth;
            TModel.Description = TCtable.Description;
            TModel.DrivingLicense = TCtable.DrivingLicense;
            TModel.EmployeeNo = TCtable.EmployeeNo;
            TModel.Gender = TCtable.Gender;
            TModel.UserId = TCtable.UserId;
            TModel.Name = TCtable.Name;
            TModel.Telephone = TCtable.Telephone;
            TModel.NIC = TCtable.NIC;
            TModel.Passport = TCtable.Passport;
            TModel.DrivingLicense = TCtable.DrivingLicense;
            TModel.TeacherId = TCtable.TeacherId;
            TModel.TeacherCategoryId = TCtable.TeacherCategoryId;

            return PartialView("TeacherDetailsView", TModel);
        }
        public ActionResult ShowEditTeacher(long TeacherId)
        {
            List<tblTeacherCategory> TCategorylist = Connection.tblTeacherCategories.ToList();
            ViewBag.TeacherCategoryDrpDown = new SelectList(TCategorylist, 
                                                            "TeacherCategoryId", 
                                                            "TeacherCategoryName");

            List<tblSchool> TSlist = Connection.tblSchools.ToList();
            ViewBag.ETSchools = new SelectList(TSlist, "SchoolId", "SchoolName");

            TeacherModel TModel = new TeacherModel();

            tblTeacher TCtable = Connection.tblTeachers.SingleOrDefault(x => x.TeacherId == TeacherId);
            TModel.IsActive = TCtable.IsActive;
            TModel.Address1 = TCtable.Address1;
            TModel.Address2 = TCtable.Address2;
            TModel.Address3 = TCtable.Address3;
            TModel.DateOfBirth = TCtable.DateOfBirth;
            TModel.Description = TCtable.Description;
            TModel.DrivingLicense = TCtable.DrivingLicense;
            TModel.EmployeeNo = TCtable.EmployeeNo;
            TModel.Gender = TCtable.Gender;
            TModel.UserId = TCtable.UserId;
            TModel.Name = TCtable.Name;
            TModel.Telephone = TCtable.Telephone;
            TModel.NIC = TCtable.NIC;
            TModel.Passport = TCtable.Passport;
            TModel.DrivingLicense = TCtable.DrivingLicense;
            TModel.TeacherId = TCtable.TeacherId;
            TModel.TeacherCategoryId = TCtable.TeacherCategoryId;

            tblTeacherSchool Tstbl = Connection.tblTeacherSchools.SingleOrDefault(
                                        x => x.TeacherId == TeacherId);
            TModel.SchoolID = Tstbl.SchoolId;

            return PartialView("EditTeacherDetailsView",TModel);
        }

        [UserFilter(Function_Id = "TEDIT")]
        public ActionResult EditTeacher(long TeacherId,string Name)
        {
            ViewBag.EditTeacherID = TeacherId;
            ViewBag.EditTeacherName = Name;
            CQExCViewBags();
            SubjctViewBags();
            return View();
        }

        [HttpPost]
        public JsonResult EditTeacherDetails(TeacherModel Model)
        {
            string result = "Error";
            try
            {
                tblTeacher Newt = Connection.tblTeachers.SingleOrDefault(x => x.TeacherId == Model.TeacherId);
                Newt.ModifiedBy = "ADMIN";
                Newt.ModifiedDate = DateTime.Now;
                Newt.TeacherCategoryId = Model.TeacherCategoryId;
                Newt.DateOfBirth = Model.DateOfBirth;
                Newt.Address1 = Model.Address1;
                Newt.Address2 = Model.Address2;
                Newt.Address3 = Model.Address3;
                Newt.Telephone = Model.Telephone;
                Newt.Gender = Model.Gender;
                Newt.Description = Model.Description;
                Newt.EmployeeNo = Model.EmployeeNo;
                Newt.DrivingLicense = Model.DrivingLicense;
                Newt.NIC = Model.NIC;
                Newt.Name = Model.Name;
                Newt.Passport = Model.Passport;

                tblTeacherSchool Tstbl = Connection.tblTeacherSchools.SingleOrDefault(
                                         x => x.TeacherId == Model.TeacherId);
                Tstbl.SchoolId = Model.SchoolID;

                Connection.SaveChanges();

                result = Model.TeacherId.ToString(); 
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Errorlog.ErrorManager.LogError("Teacher Controller @EditTeacherDetails", dbEx);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteTeacher(long TeacherId)
        {
            TeacherModel TModel = new TeacherModel();
            TModel.TeacherId = TeacherId;
            return PartialView("DeleteTeacher", TModel);
        }

        [HttpPost]
        public ActionResult DeleteTeacher(TeacherModel Model)
        {
            try
            {
                tblTeacher TCtable = Connection.tblTeachers.SingleOrDefault(x => x.TeacherId == Model.TeacherId);
                TCtable.IsActive = "N";
                Connection.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult DeleteClassTeacher(string tid, string gid, string classid, string Accyear, string sid)
        {
            ClassTeacherModel TModel = new ClassTeacherModel();
            TModel.TeacherId = tid;
            TModel.GradeId = gid;
            TModel.ClassId = classid;
            TModel.AccedamicYear = Accyear;
            TModel.SchoolId = sid;
            return PartialView("DeleteClassTeacher", TModel);
        }

        [HttpPost]
        public JsonResult DeleteClassTeacher(ClassTeacherModel Model)
        {
            try
            {
                tblClassTeacher TCtable = Connection.tblClassTeachers.SingleOrDefault(
                                            x => x.TeacherId == Model.TeacherId && 
                                            x.GradeId == Model.GradeId && 
                                            x.ClassId == Model.ClassId && 
                                            x.AccedamicYear == Model.AccedamicYear && 
                                            x.SchoolId == Model.SchoolId);

                TCtable.IsActive = "D";
                Connection.SaveChanges();

                return Json("Sucses", JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }


    }
}
