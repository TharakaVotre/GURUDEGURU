using GDWEBSolution.Models;
using GDWEBSolution.Models.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Errorlog;

namespace GDWEBSolution.Controllers.Teacher
{
    public class TeacherController : Controller
    {
        SchoolMGTEntitiesConnectionString Connection = new SchoolMGTEntitiesConnectionString();
        //
        // GET: /Teacher/
        TeacherModel tcm = new TeacherModel();

        public ActionResult Index()
        {
            
            var TeacherList = Connection.SMGTgetAllTeachers("CKC","%","Y").ToList();

           

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

        //
        // GET: /Teacher/Details/5

        public ActionResult Class()
        {
            SubjctViewBags();
            return View();
        }

        public ActionResult Details(long TeacherId)
        {
            ViewBag.TeacherDetailsID = TeacherId;
            return View();
        }

        //
        // GET: /Teacher/Create
       [AllowAnonymous]
        public string IsUserNameExits(string input)
        {

            var count = Connection.tblTeachers.Count(u => u.UserId == input);
            if (count != 0)
            {
                return "Have";
            }
            else
            {
                return "NO";
            }
        }


        public ActionResult Create()
        {
            CQExCViewBags();


            SubjctViewBags();



            return View();
        }

        private void CQExCViewBags()
        {
            List<tblTeacherCategory> TCategorylist = Connection.tblTeacherCategories.ToList();
            ViewBag.TeacherCategoryDrpDown = new SelectList(TCategorylist, "TeacherCategoryId", "TeacherCategoryName");

            List<tblQualification> TQlist = Connection.tblQualifications.ToList();
            ViewBag.QualificationList = new SelectList(TQlist, "QualificationId", "QualificationName");

            TeacherDrpList();

            List<tblExtraCurricularActivity> Exlist = Connection.tblExtraCurricularActivities.ToList();
            ViewBag.ExtraActivityList = new SelectList(Exlist, "ActivityCode", "ActivityName");
        }

        private void TeacherDrpList()
        {
            List<tblTeacher> Tlist = Connection.tblTeachers.ToList(); //Not By school
            ViewBag.TeacherList = new SelectList(Tlist, "TeacherId", "Name");
            ViewBag.TeacherListEx = new SelectList(Tlist, "TeacherId", "Name");
        }

        private void SubjctViewBags()
        {
            var SchoolTeacher = Connection.SMGTgetSchoolTeacher("CKC").ToList();//Need to Pass a Session Schoolid
            List<TeacherModel> SchooTList = SchoolTeacher.Select(x => new TeacherModel
            {
                TeacherId = x.TeacherId,
                Name = x.Name,
                UserId = x.UserId,
                IsActive = x.IsActive

            }).ToList();

            ViewBag.SchoolTeacher = new SelectList(SchooTList, "TeacherId", "Name");

            var SchoolGrade = Connection.SMGTgetSchoolGrade("CKC").ToList();//Need to Pass a Session Schoolid
            List<tblGrade> SchoolGradeList = SchoolGrade.Select(x => new tblGrade
            {
                GradeId = x.GradeId,
                GradeName = x.GradeName,
                IsActive = x.IsActive

            }).ToList();
            ViewBag.SchoolGrades = new SelectList(SchoolGradeList, "GradeId", "GradeName");



            // TeacherModel tcm = new TeacherModel();
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


        public ActionResult ShowGradeClasses(string GradeId)
        {
            List<tblClass> ClassesList = Connection.tblClasses.Where(r => r.SchoolId == "CKC" && r.GradeId == GradeId).ToList();
            ViewBag.GradeClassse = new SelectList(ClassesList, "ClassId", "ClassName");


            var GradeSubject = Connection.SMGTgetGradeSubjects(GradeId).ToList();//Need to Pass a Session Schoolid
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
            List<tblClass> ClassesList = Connection.tblClasses.Where(r => r.SchoolId == "CKC" && r.GradeId == GradeId).ToList();
            ViewBag.GradeClassse = new SelectList(ClassesList, "ClassId", "ClassName");

            return PartialView("GradeClasses");
        }
        //
        // POST: /Teacher/Create

        [HttpPost]
        public JsonResult CreateTeacher(TeacherModel Model)
        {
            try
            {
                string result = "Error";
                var count = Connection.tblTeachers.Count(u => u.UserId == Model.UserId);

                if (count == 0)
                {
                    tblTeacher Newt = new tblTeacher();

                    Newt.CreatedBy = "ADMIN";
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
                    ts.SchoolId = "CKC";
                    ts.TeacherCategoryId = TID.TeacherCategoryId;
                    ts.TeacherId = TID.TeacherId;
                    ts.CreatedBy = "ADMIN";
                    ts.CreatedDate = DateTime.Now;
                    ts.IsActive = "Y";

                    Connection.tblTeacherSchools.Add(ts);
                    Connection.SaveChanges();

                    result = "Success";
                }
                else
                {
                    result = "UserExits";
                }
                //tblTeacherSchool Tschool = new tblTeacherSchool();

                return Json(result,JsonRequestBehavior.AllowGet);
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
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
                    }
                }
                throw raise;
            }
        }

        
        //
        // GET: /Teacher/Edit/5




        [AllowAnonymous]
        public JsonResult AddQualification(QualificationModel Model)
        {
            try
            {
                string result = "Error";

                var count = Connection.tblTeacherQualifications.Count(u => u.TeacherId == Model.Teacher_Id && u.QualificationId == Model.QualificationId);
                if (count == 0)
                {

                    tblTeacherQualification NewQ = new tblTeacherQualification();

                    NewQ.CreatedBy = "ADMIN";
                    NewQ.CreatedDate = DateTime.Now;
                    NewQ.IsActive = "Y";
                    NewQ.QualificationId = Model.QualificationId;
                    NewQ.SchoolId = "CKC";
                    NewQ.TeacherId = Model.Teacher_Id;

                    Connection.tblTeacherQualifications.Add(NewQ);
                    Connection.SaveChanges();

                    result = Model.Teacher_Id.ToString();

                    ViewBag.TeacherId = Model.Teacher_Id.ToString();

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
                Errorlog.ErrorManager.LogError("Teacher Controller - AddQualification(QualificationModel Model)",Ex);
                return Json("Exception", JsonRequestBehavior.AllowGet);
                
            }
        }


        [AllowAnonymous]
        public JsonResult AddExtraActivity(ExtraActivityModel Model)
        {
            try
            {
                string result = "Error";

                var count = Connection.tblTeacherExtraCurricularActivities.Count(u => u.TeacherId == Model.TeacherID && u.ActivityCode == Model.ActivityCode);
                if (count == 0)
                {

                    tblTeacherExtraCurricularActivity NewQ = new tblTeacherExtraCurricularActivity();

                    NewQ.CreatedBy = "ADMIN";
                    NewQ.CreatedDate = DateTime.Now;
                    NewQ.IsActive = "Y";
                    NewQ.ActivityCode = Model.ActivityCode;
                    NewQ.SchoolId = "CKC";
                    NewQ.TeacherId = Model.TeacherID;

                    Connection.tblTeacherExtraCurricularActivities.Add(NewQ);
                    Connection.SaveChanges();

                    result = Model.TeacherID.ToString();

                    //ViewBag.TeacherId = Model.TeacherID.ToString();

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
                Errorlog.ErrorManager.LogError("Teacher Controller - AddExtraActivity(ExtraActivityModel Model)", Ex);
                return Json("Exception", JsonRequestBehavior.AllowGet);
            }
        }
        //
        // POST: /Teacher/Edit/5


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

                    tblTeacherSubject NewQ = new tblTeacherSubject();

                    NewQ.CreatedBy = "ADMIN";
                    NewQ.CreatedDate = DateTime.Now;
                    NewQ.IsActive = "Y";
                    NewQ.AcedemicYear = "2017";
                    NewQ.SchoolIds = "CKC";
                    NewQ.GradeId = Model.GradeId;
                    NewQ.ClassId = Model.ClassId;
                    NewQ.TeacherId = Model.Teacher_ID;
                    NewQ.SubjectId = Model.SubjectId;

                    Connection.tblTeacherSubjects.Add(NewQ);
                    Connection.SaveChanges();

                    result = Model.Teacher_ID.ToString();

                    //ViewBag.TeacherId = Model.TeacherID.ToString();

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

                var TCount = Connection.tblClassTeachers.Count(u => u.TeacherId == Model.TeacherId && u.AccedamicYear == Model.AccedamicYear && u.SchoolId == Model.SchoolId);

                var Ccount = Connection.tblClassTeachers.Count(u => u.ClassId == Model.ClassId 
                    && u.AccedamicYear == Model.AccedamicYear && u.GradeId == Model.GradeId && u.SchoolId == Model.SchoolId );

                if (TCount != 0)
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

                    NewQ.CreatedBy = "ADMIN";
                    NewQ.CreatedDate = DateTime.Now;
                    NewQ.IsActive = "Y";
                    NewQ.AccedamicYear = "2017";
                    NewQ.SchoolId = "CKC";
                    NewQ.GradeId = Model.GradeId;
                    NewQ.ClassId = Model.ClassId;
                    NewQ.TeacherId = Model.TeacherId;

                    Connection.tblClassTeachers.Add(NewQ);
                    Connection.SaveChanges();

                    result = "Success";
                }
                //ShowTeacherQualificatoin();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Errorlog.ErrorManager.LogError("Teacher Controller - AddClassTeacher(ClassTeacherModel Model)", Ex);
                return Json("Exception", JsonRequestBehavior.AllowGet);
            }
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
                tblTeacherExtraCurricularActivity Tble = Connection.tblTeacherExtraCurricularActivities.Find(Model.TeacherID, Model.SchoolId, Model.ActivityCode);
                Connection.tblTeacherExtraCurricularActivities.Remove(Tble);
                Connection.SaveChanges();


                return Json(Model.TeacherID, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("Index");
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
                tblTeacherQualification Tble = Connection.tblTeacherQualifications.Find(Model.Teacher_Id, Model.SchoolId, Model.QualificationId);
                Connection.tblTeacherQualifications.Remove(Tble);
                Connection.SaveChanges();


                return Json(Model.Teacher_Id, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("Index");
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
                //return RedirectToAction("Index");
            }
            catch
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ShowTeacherDetails(long TeacherId)
        {
            List<tblTeacherCategory> TCategorylist = Connection.tblTeacherCategories.ToList();
            ViewBag.TeacherCategoryDrpDown = new SelectList(TCategorylist, "TeacherCategoryId", "TeacherCategoryName");

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

            return PartialView("TeacherDetailsView", TModel);
        }
        public ActionResult ShowEditTeacher(long TeacherId)
        {
            List<tblTeacherCategory> TCategorylist = Connection.tblTeacherCategories.ToList();
            ViewBag.TeacherCategoryDrpDown = new SelectList(TCategorylist, "TeacherCategoryId", "TeacherCategoryName");

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

            return PartialView("EditTeacherDetailsView",TModel);
        }

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
            try
            {
                string result = "Error";

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

                Connection.SaveChanges();

                result = Model.TeacherId.ToString();
                //tblTeacherSchool Tschool = new tblTeacherSchool();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
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
                    }
                }
                throw raise;
            }
        }
        //
        // GET: /Teacher/Delete/5

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

        //
        // POST: /Teacher/Delete/5

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
