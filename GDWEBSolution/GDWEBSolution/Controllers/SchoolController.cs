using GDWEBSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GDWEBSolution.Models.Schools;
using System.IO;
using GDWEBSolution.Models.Teacher;
using GDWEBSolution.Models.Student;
using GDWEBSolution.Util;
using GDWEBSolution.Models.User;
using System.Transactions;
using GDWEBSolution.Filters;
//DBEntityModel.edmx
//SchoolMGTEntitiesConnectionString
namespace GDWEBSolution.Controllers
{
    public class SchoolController : Controller
    {

        static string DECKey = System.Configuration.ConfigurationManager.AppSettings["DecKey"];
        string Password = DECKey.Substring(10);

        String Schoold = "121127";
        string UserId = "User1";
        //
        // GET: /School/
        UserSession USession = new UserSession();


        SchoolMGTEntitiesConnectionString Connection = new SchoolMGTEntitiesConnectionString();

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

        public ActionResult Index()
        {
            Authentication("SCF");
            ViewBag.BtnStatus = true;
            if (USession.User_Category == "SADMI")
            {
                ViewBag.BtnStatus = false;
                var school2 = Connection.DCISgetSchoolAdmin(USession.School_Id);

                List<DCISgetSchoolAdmin_Result> Categorylist = school2.ToList();
                SchoolModel schl = new SchoolModel();
                List<SchoolModel> tcmlist = Categorylist.Select(x => new SchoolModel
                {

                    SchoolId = x.SchoolId,
                    SchoolName = x.SchoolName,
                    SchoolGroup = x.SchoolGroup,
                    Address1 = x.Address1,
                    Address2 = x.Address2,
                    Address3 = x.Address3,
                    SchoolCategory = x.SchoolCategory,
                    MinuteforPeriod = x.MinuteforPeriod.ToString(),
                    SchoolCategoryName = x.SchoolCategoryName,
                    Telephone = x.Telephone,

                    CreatedDate = x.CreatedDate,
                    WebAddress = x.WebUrl,



                    IsActive = x.IsActive,
                    ModifiedBy = x.ModifiedBy,
                    ModifiedDate = x.ModifiedDate

                }).ToList();



                return View(tcmlist);


            }
            else
            {


                var school = Connection.DCISgetSchool();




                List<DCISgetSchool_Result> Categorylist = school.ToList();
                SchoolModel schl = new SchoolModel();
                List<SchoolModel> tcmlist = Categorylist.Select(x => new SchoolModel
                {

                    SchoolId = x.SchoolId,
                    SchoolName = x.SchoolName,
                    SchoolGroup = x.SchoolGroup,
                    Address1 = x.Address1,
                    Address2 = x.Address2,
                    Address3 = x.Address3,
                    SchoolCategory = x.SchoolCategory,
                    MinuteforPeriod = x.MinuteforPeriod.ToString(),
                    SchoolCategoryName = x.SchoolCategoryName,
                    Telephone = x.Telephone,

                    CreatedDate = x.CreatedDate,
                    WebAddress = x.WebUrl,



                    IsActive = x.IsActive,
                    ModifiedBy = x.ModifiedBy,
                    ModifiedDate = x.ModifiedDate

                }).ToList();



                return View(tcmlist);
            }
        }

        //
        // GET: /School/Details/5

        public ActionResult Detail(String SchoolId)
        {
            Authentication("SCF");

            ViewBag.EditSChoolID = SchoolId;

            SchoolModel TModel = new SchoolModel();

            tblSchool TCtable = Connection.tblSchools.SingleOrDefault(x => x.SchoolId == SchoolId);
            //  TModel.IsActive = TCtable.IsActive;

            TModel.SchoolId = TCtable.SchoolId;
            TModel.SchoolName = TCtable.SchoolName;
            TModel.Address1 = TCtable.Address1;
            TModel.Description = TCtable.Description;
            TModel.Address1 = TCtable.Address1;
            TModel.Address2 = TCtable.Address2;
            TModel.Address3 = TCtable.Address3;
            TModel.Email = TCtable.Email;
            TModel.WebAddress = TCtable.WebUrl;
            TModel.MinuteforPeriod = TCtable.MinuteforPeriod.ToString();
            TModel.Telephone = TCtable.Telephone;

            TModel.SchoolCategory = TCtable.SchoolCategory;
            if (TCtable.SchoolGroup == null)
            {
                TModel.SchoolGroup = 0;

            }
            else
            {
                TModel.SchoolGroup = TCtable.SchoolGroup;
            }
            TModel.SchoolRank = TCtable.SchoolRank;
            TModel.District = TCtable.District;
            TModel.Division = TCtable.Division;
            TModel.Province = TCtable.Province;
            TModel.ImagePath = TCtable.ImagePath;
            TModel.LogoPath = TCtable.LogoPath;
            if (TCtable.ImagePath == null || TCtable.ImagePath=="")
            {

                TModel.ImagePath = "~/Uploads/Schools/Logo/no_image.jpg";
            }
            if (TCtable.LogoPath == null || TCtable.LogoPath=="")
            {

                TModel.LogoPath = "~/Uploads/Schools/Logo/no_image.jpg";
            }
            academicyear();
            
            tblSchoolCategory cattbl = Connection.tblSchoolCategories.SingleOrDefault(x => x.SchoolCategoryId == TModel.SchoolCategory);
            TModel.SchoolCategoryName = cattbl.SchoolCategoryName;

            if (TCtable.SchoolGroup == null)
            {
                TModel.SchoolGroup = 0;


                TModel.GroupName = "";

            }
            else {

                tblSchoolGroup sclgrp = Connection.tblSchoolGroups.SingleOrDefault(x => x.GroupId == TModel.SchoolGroup);

                TModel.GroupName = sclgrp.GroupName;
            
            }


            if (TModel.SchoolRank == null)
            {
                TModel.SchoolRank = 0;


                TModel.SchoolRankName = "";

            }
            else
            {

                tblSchoolRank sclrank = Connection.tblSchoolRanks.SingleOrDefault(x => x.SchoolRankId == TModel.SchoolRank);
                TModel.SchoolRankName = sclrank.SchoolRankName;

            }

            if (TModel.District == null)
            {
                TModel.District = 0;


                TModel.DistrictName = "";

            }
            else
            {

                tblDistrict scldr = Connection.tblDistricts.SingleOrDefault(x => x.DistrictId == TModel.District);
                TModel.DistrictName = scldr.DistrictName;

            }

            if (TModel.Division == null)
            {
                TModel.Division = 0;


                TModel.DivisionName = "";

            }
            else
            {

                tblDivision scldivisions = Connection.tblDivisions.SingleOrDefault(x => x.DivisionId == TModel.Division);
                TModel.DivisionName = scldivisions.DivisionName;

            }

            if (TModel.Province == null)
            {
                TModel.Province = 0;


                TModel.ProvinceName = "";

            }
            else
            {

                tblProvince Province = Connection.tblProvinces.SingleOrDefault(x => x.ProvinceId == TModel.Province);
                TModel.ProvinceName = Province.ProvinceName;

            }
            
           
          
           
            


            return View("Detail", TModel);



        }

        //
        // GET: /School/Create

        public ActionResult Create(SchoolHouseModel hsmodel)
        
       {

           Authentication("SCF");

           academicyear();


           ViewBag.SchoolId = Schoold;

           hsmodel.SchoolId = Schoold;
           List<tblSchoolCategory> SCategorylist = Connection.tblSchoolCategories.Where(X => X.IsActive == "Y").ToList();
            ViewBag.SchoolCategoryDrpDown = new SelectList(SCategorylist, "SchoolCategoryId", "SchoolCategoryName");
            List<tblProvince> provincelist = Connection.tblProvinces.Where(X => X.IsActive == "Y").ToList();
            ViewBag.ProvinceDrpDown = new SelectList(provincelist, "ProvinceId", "ProvinceName");
            List<tblSchoolGroup> schoolgrps = Connection.tblSchoolGroups.Where(X => X.IsActive == "Y").ToList();
            ViewBag.SGroupDrpDown = new SelectList(schoolgrps , "GroupId", "GroupName");
            List<tblDistrict> districtlist = Connection.tblDistricts.Where(X => X.IsActive == "Y").ToList();
            ViewBag.DistrictDrpDown = new SelectList(districtlist, "DistrictId", "DistrictName");
            List<tblDivision> divisionlist = Connection.tblDivisions.Where(X => X.IsActive == "Y").ToList();
            ViewBag.DivisionDrpDown = new SelectList(divisionlist, "DivisionId", "DivisionName");
            List<tblSchoolRank> Ranklist = Connection.tblSchoolRanks.Where(X => X.IsActive == "Y").ToList();
            ViewBag.RankDrpDown = new SelectList(Ranklist, "SchoolRankId", "SchoolRankName");

            List<tblSubject> sclSublist = Connection.tblSubjects.Where(X => X.IsActive == "Y").ToList();
            ViewBag.SubjectscldrpList = new SelectList(sclSublist, "SubjectId", "SubjectName");


            List<tblSubjectCategory> sclSubcatlist = Connection.tblSubjectCategories.Where(X => X.IsActive == "Y").ToList();
            ViewBag.SubcatscldrpList = new SelectList(sclSubcatlist, "SubjectCategoryId", "SubjectCategoryName");

            SchoolGradeDrpList();
            SchoolHouseDrpListe(Schoold);
            List<tblExtraCurricularActivity> excatlist = Connection.tblExtraCurricularActivities.Where(X => X.IsActive == "Y").ToList();
            ViewBag.ActivitydrpList = new SelectList(excatlist, "ActivityCode", "ActivityName");
          
            return View("SchoolCreate");
        }

        private void academicyear()
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

        public ActionResult Createe(string schoolid)
        {

            Authentication("SCF"); ;
            academicyear();
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

            List<tblExtraCurricularActivity> excatlist = Connection.tblExtraCurricularActivities.ToList();
            ViewBag.ActivitydrpList = new SelectList(excatlist, "ActivityCode", "ActivityName");

            SchoolGradeDrpList();
            SchoolHouseDrpListe(Schoold);
            return PartialView("House");
        }

        public ActionResult Create2()
        {


            return View("createdef");
        }


         [AllowAnonymous]
         [UserFilter(Function_Id = "SCF")]
        public JsonResult AddSchoolGrade(SchoolGradeModel Model)
        {

          //  Authentication("SCF");
            try


            {
                string result = "Error";

                if (Model.SchoolId != null && Model.GradeId1 != null)
                {



                    var count = Connection.tblSchoolGrades.Count(u => u.SchoolId == Model.SchoolId && u.GradeId == Model.GradeId1);
                    if (count == 0)
                    {

                        tblSchoolGrade newscg = new tblSchoolGrade();

                        newscg.CreatedBy = "User1";
                        newscg.SchoolId = Model.SchoolId;
                        newscg.GradeId = Model.GradeId1;
                        newscg.IsActive = "Y";
                        newscg.CreatedDate = DateTime.Now;


                        Connection.tblSchoolGrades.Add(newscg);
                        Connection.SaveChanges();

                        result = "sessioncheck"+"!"+Model.SchoolId;

                        ViewBag.SchoolId = Model.SchoolId.ToString();

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
         public JsonResult AddSchoolClass(SchoolModel Model)
         {
             Authentication("SCF");
             try
             {
                 string result = "Error";

                 if (Model.SchoolId3 != null && Model.GradeId != null && Model.ClassId != null && Model.ClassName != null)
                 {





                     var count = Connection.tblClasses.Count(u => u.SchoolId == Model.SchoolId3 && u.GradeId == Model.GradeId && u.ClassId == Model.ClassId);
                     if (count == 0)
                     {

                         tblClass cls = new tblClass();

                         cls.CreatedBy = "User1";
                         cls.SchoolId = Model.SchoolId3;
                         cls.GradeId = Model.GradeId;
                         cls.IsActive = "Y";
                         cls.CreatedDate = DateTime.Now;
                         cls.ClassId = Model.ClassId;
                         cls.ClassName = Model.ClassName;


                         Connection.tblClasses.Add(cls);
                         Connection.SaveChanges();

                         result = "sessioncheck" + "!" + Model.SchoolId3 + "!" + Model.GradeId;

                         ViewBag.SchoolId = Model.SchoolId3.ToString();

                     }
                     else
                     {
                         result = "Exits";
                     }
                 }


                 else
                 {
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
         public JsonResult AddSchoolAdmin(SchoolAdminModel Model)
         {
             Authentication("SCF");
             try
             {
                 string result = "Error";
                 if (Model.SchoolId4 == null || Model.AdminUserId == null || Model.Password == null || Model.AdminPersonalEmail == null)
                 {

                     result = "notfilled";

                 }
                 else
                 {

                     var count = Connection.tblUsers.Count(u => u.SchoolId == Model.SchoolId4 && u.UserId == Model.AdminUserId);
                     if (count == 0)
                     {

                         tblUser usr = new tblUser();

                         usr.CreatedBy = UserId;
                         usr.CreatedDate = DateTime.Now;
                         usr.SchoolId = Model.SchoolId4;
                         usr.UserId = Model.AdminUserId;
                         usr.IsActive = "Y";
                         usr.Mobile = Model.PersonalMobile;
                         usr.UserCategory = "SADMI";
                         usr.PersonName = Model.AdminName;
                         usr.UserId = Model.AdminUserId;
                              string pass = Encrypt_Decrypt.Encrypt(Model.Password, Password);

                              usr.Password = pass;
                         usr.LoginEmail = Model.AdminPersonalEmail;


                         Connection.tblUsers.Add(usr);
                         Connection.SaveChanges();

                         result = "sessioncheck" + "!" + Model.SchoolId4 ;



                     }
                     else
                     {
                         result = "Exits";
                     }
                     //ShowTeacherQualificatoin();
                 }
                 return Json(result, JsonRequestBehavior.AllowGet);
             }
             catch (Exception Ex)
             {
                 Errorlog.ErrorManager.LogError("Teacher Controller - AddQualification(QualificationModel Model)", Ex);
                 return Json("Exception", JsonRequestBehavior.AllowGet);

             }
         }




         [AllowAnonymous]
         [UserFilter(Function_Id = "SCF")]
         public JsonResult EAddSchoolClass(SchoolModel Model)
         {
           //  Authentication("SCF");
             try
             {
                 string result = "Error";

                 var count = Connection.tblClasses.Count(u => u.SchoolId == Model.SchoolId && u.GradeId == Model.GradeId && u.ClassId == Model.ClassId);
                 if (count == 0)
                 {

                     tblClass cls = new tblClass();

                     cls.CreatedBy = "User1";
                     cls.SchoolId = Model.SchoolId;
                     cls.GradeId = Model.GradeId;
                     cls.IsActive = "Y";
                     cls.CreatedDate = DateTime.Now;
                     cls.ClassId = Model.ClassId;
                     cls.ClassName = Model.ClassName;


                     Connection.tblClasses.Add(cls);
                     Connection.SaveChanges();

                     result = "sessioncheck" + "!" + Model.SchoolId + "!" + Model.GradeId;

                     ViewBag.SchoolId = Model.SchoolId.ToString();

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


         public ActionResult ShowSchooClasses(string SchoolIdGradeId)
         {
             if (SchoolIdGradeId == null) {

                 SchoolIdGradeId = "awq!Bss";
             }

             string scid  = SchoolIdGradeId.Split('!')[0];
             string gdid = SchoolIdGradeId.Split('!')[1];

             academicyear();
             List<tblSubjectCategory> sclSubcatlist = Connection.tblSubjectCategories.Where(X => X.IsActive == "Y").ToList();
             ViewBag.SubcatscldrpList = new SelectList(sclSubcatlist, "SubjectCategoryId", "SubjectCategoryName");
             List<tblSchoolCategory> SCategorylist = Connection.tblSchoolCategories.Where(X => X.IsActive == "Y").ToList();
             ViewBag.SchoolCategoryDrpDown = new SelectList(SCategorylist, "SchoolCategoryId", "SchoolCategoryName");
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
             SchoolGradeDrpList();
             SchoolHouseDrpListe(Schoold);
            // string GradeId = ""; ;
             List<SchoolModel> List = LoadClasses(scid, gdid);
         //    string username = result.Consignor.Split('<')[0];
             return PartialView("ClassesList", List);
         }


         public ActionResult DeleteClass(String SchoolId, String GradeId, String ClassId)
         {
             SchoolModel Model = new SchoolModel();
             Model.GradeId = GradeId;
             Model.SchoolId = SchoolId;
             Model.ClassId = ClassId;
             return PartialView("DeleteClass", Model);
         }

         [HttpPost]
         public ActionResult DeleteSchoolclasses(SchoolModel Model)
         {
             try
             {

                 //tblClass Tble = Connection.tblClasses.Find(Model.ClassId, Model.GradeId, Model.SchoolId);
                 //Connection.tblClasses.Remove(Tble);
                 Connection.SMGTModifyClassStatus(Model.SchoolId, Model.ClassId, Model.GradeId);
                 Connection.SaveChanges();
                 string resultt = Model.SchoolId + "!" + Model.GradeId;

                 return Json(resultt, JsonRequestBehavior.AllowGet);
                 //return RedirectToAction("Index");
             }
             catch
             {
                 return Json("Error", JsonRequestBehavior.AllowGet);
             }
         }


        [AllowAnonymous]
        [UserFilter(Function_Id = "SCF")]
         public JsonResult AddSchoolHouse(SchoolHouseModel Model)
         {
          
             try
             {
                 string result = "Error";
                 if (Model.SchoolId1 != null && Model.HouseName != null)
                 {


                    
                     var count2 = Connection.tblHouses.Count();
                     int a = count2 + 10;
                     Model.HouseId = a.ToString();
                     var count = Connection.tblHouses.Count(u => u.SchoolId == Model.SchoolId1 && u.HouseName == Model.HouseName);
                     if (count == 0)
                     {
                         //    Model.SchoolId = Schoold;
                         tblHouse newscg = new tblHouse();

                         newscg.CreatedBy = "User1";
                         newscg.CreatedDate = DateTime.Now;
                         newscg.SchoolId = Model.SchoolId1;
                         newscg.HouseId = Model.HouseId;
                         newscg.HouseName = Model.HouseName;
                         newscg.IsActive = "Y";
                         newscg.HouseInchargeId = Model.HouseInchargeId.ToString();

                         Connection.tblHouses.Add(newscg);

                         Connection.SaveChanges();

                         result = "sessioncheck" + "!" + Model.SchoolId1;

                         ViewBag.SchoolId = Model.SchoolId1;

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
        [UserFilter(Function_Id = "SCF")]
        public JsonResult AddSchoolSubjects(SchoolSubjectModel Model)
        {
           // Authentication("SCF");
            try
            {
                string result = "Error";

                if (Model.SchoolIds != null && Model.AcademicYear != null && Model.SubjectId != null && Model.SubjectCategoryId != null)
                {
                    int subid = Int32.Parse(Model.SubjectId);


                    var count = Connection.tblSchoolSubjects.Count(u => u.SchoolId == Model.SchoolIds && u.AcademicYear == Model.AcademicYear && u.SubjectId == subid);
                    if (count == 0)
                    {
                        //  Model.SchoolId = Schoold;
                        tblSchoolSubject newscg = new tblSchoolSubject();

                        newscg.CreatedBy = USession.User_Id;
                        newscg.CreatedDate = DateTime.Now;
                        newscg.SchoolId = Model.SchoolIds;
                        newscg.Optional = "Y";

                        newscg.SubjectId = Int32.Parse(Model.SubjectId);
                        newscg.SubjectCategoryId = Int32.Parse(Model.SubjectCategoryId);
                        newscg.AcademicYear = Model.AcademicYear;
                        newscg.IsActive = "Y";


                        Connection.tblSchoolSubjects.Add(newscg);

                        Connection.SaveChanges();

                        result = "sessioncheck" + "!" + Model.AcademicYear + "!" + Model.SchoolIds;

                        ViewBag.SchoolId = Model.SchoolIds;

                    }
                    else
                    {
                        result = "Exits";
                    }
                    //ShowTeacherQualificatoin();
                }
                else {
                    result = "fill";
                
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Errorlog.ErrorManager.LogError("Teacher Controller - AddQualification(QualificationModel Model)", Ex);
                return Json("Exception", JsonRequestBehavior.AllowGet);

            }
        }

        [AllowAnonymous]
        [UserFilter(Function_Id = "SCF")]

        public JsonResult AddSchoolExcActivity(SchoolExtraModel Model)
        {
          //  Authentication("SCF");
            try
            {
                string result = "Error";

                if (Model.ActivityCode != null && Model.SchoolIdEx != null)
                {
                    // Model.HouseId = count2.ToString();
                    var count = Connection.tblSchoolExtraCurricularActivities.Count(u => u.ActivityCode == Model.ActivityCode && u.SchoolId == Model.SchoolIdEx);
                    if (count == 0)
                    {
                        // Model.SchoolId = Schoold;
                        tblSchoolExtraCurricularActivity newscg = new tblSchoolExtraCurricularActivity();

                        newscg.CreatedBy = USession.User_Id;
                        newscg.CreatedDate = DateTime.Now;
                        newscg.SchoolId = Model.SchoolIdEx;
                        newscg.ActivityCode = Model.ActivityCode;
                        newscg.IsActive = "Y";


                        Connection.tblSchoolExtraCurricularActivities.Add(newscg);

                        Connection.SaveChanges();

                        result = "sessioncheck" + "!" + Model.SchoolIdEx;

                        ViewBag.SchoolId = Model.SchoolIdEx;

                    }
                    else
                    {
                        result = "Exits";
                    }
                    //ShowTeacherQualificatoin();
                }
                else {

                    result = "fill";
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Errorlog.ErrorManager.LogError("Teacher Controller - AddQualification(QualificationModel Model)", Ex);
                return Json("Exception", JsonRequestBehavior.AllowGet);

            }
        }

        [AllowAnonymous]
        [UserFilter(Function_Id = "SCF")]
        public JsonResult AddSchoolExcActivityEdit(SchoolExtraModel Model)
        {
            //  Authentication("SCF");
            try
            {
                string result = "Error";

                if (Model.ActivityCode != null && Model.SchoolId != null)
                {
                    // Model.HouseId = count2.ToString();
                    var count = Connection.tblSchoolExtraCurricularActivities.Count(u => u.ActivityCode == Model.ActivityCode && u.SchoolId == Model.SchoolId);
                    if (count == 0)
                    {
                        // Model.SchoolId = Schoold;
                        tblSchoolExtraCurricularActivity newscg = new tblSchoolExtraCurricularActivity();

                        newscg.CreatedBy = USession.User_Id;
                        newscg.CreatedDate = DateTime.Now;
                        newscg.SchoolId = Model.SchoolId;
                        newscg.ActivityCode = Model.ActivityCode;
                        newscg.IsActive = "Y";


                        Connection.tblSchoolExtraCurricularActivities.Add(newscg);

                        Connection.SaveChanges();

                        result = "sessioncheck" + "!" + Model.SchoolId;

                        ViewBag.SchoolId = Model.SchoolId;

                    }
                    else
                    {
                        result = "Exits";
                    }
                    //ShowTeacherQualificatoin();
                }
                else
                {

                    result = "fill";
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Errorlog.ErrorManager.LogError("Teacher Controller - AddQualification(QualificationModel Model)", Ex);
                return Json("Exception", JsonRequestBehavior.AllowGet);

            }
        }

         [UserFilter(Function_Id = "SCF")]
         public ActionResult ShowSchoolGrade(string SchoolId)
         {
             var STQlist = Connection.SMGTgetSchoolGradeadd(SchoolId).ToList();

             List<SchoolGradeModel> List = STQlist.Select(x => new SchoolGradeModel
             {
                 
                 SchoolId = x.SchoolId,
                 GradeId=x.GradeId,
                SchoolName=x.SchoolName,
                GradeName=x.GradeName,
             
                 IsActive = x.IsActive,
                 

             }).ToList();
             return PartialView("GradeList", List);
         }

         public ActionResult ShowSchoolGradeDetails(string SchoolId)
         {
             var STQlist = Connection.SMGTgetSchoolGradeadd(SchoolId).ToList();

             List<SchoolGradeModel> List = STQlist.Select(x => new SchoolGradeModel
             {

                 SchoolId = x.SchoolId,
                 GradeId = x.GradeId,
                 SchoolName = x.SchoolName,
                 GradeName = x.GradeName,

                 IsActive = x.IsActive,


             }).ToList();
             return PartialView("DGradeList", List);
         }



         public ActionResult ShowSchoolAdmins(string SchoolId)
         {
            // var STQlist = Connection.SMGTgetSchoolGradeadd(SchoolId).ToList();

             List<tblUser> schoolList = Connection.tblUsers.Where(X => X.IsActive == "Y" && X.SchoolId == SchoolId && X.UserCategory == "SADMI").ToList();

             List<SchoolAdminModel> List = schoolList.Select(x => new SchoolAdminModel
             {

                 SchoolId = x.SchoolId,
                 AdminUserId=x.UserId,
                 AdminName=x.PersonName
                


             }).ToList();
             return PartialView("AdminList", List);
         }


         public ActionResult ShowSchoolAdminsDetails(string SchoolId)
         {
             // var STQlist = Connection.SMGTgetSchoolGradeadd(SchoolId).ToList();

             List<tblUser> schoolList = Connection.tblUsers.Where(X => X.IsActive == "Y" && X.SchoolId == SchoolId && X.UserCategory == "SADMI").ToList();

             List<SchoolAdminModel> List = schoolList.Select(x => new SchoolAdminModel
             {

                 SchoolId = x.SchoolId,
                 AdminUserId = x.UserId,
                 AdminName = x.PersonName



             }).ToList();
             return PartialView("DAdminList", List);
         }


         public ActionResult ShowSchoolSubjects(string AcademicYear, string SchoolId)
         {
             var STQlist = Connection.SMGTgetSchoolSubadd(SchoolId, AcademicYear).ToList();
             if (SchoolId == null||SchoolId =="")
             {
                 SchoolId = Schoold;

             }
             List<SchoolSubjectModel> List = STQlist.Select(x => new SchoolSubjectModel
             {
                
                 
                 SchoolId = x.SchoolId,
                 SubjectName=x.SubjectName,
                SubjectId= x.SubjectId.ToString(),
                AcademicYear=AcademicYear,

                
             
                 IsActive = x.IsActive,
                 

             }).ToList();
             return PartialView("SclSubList", List);
         }

         public ActionResult ShowSchoolSubjectsload(string AcademicYear, string SchoolId)
         {
             if (AcademicYear == "%")
             {

                 int count = Connection.tblAccadamicYears.Count(X => X.SchoolId == SchoolId);
                 if (count > 0)
                 {

                     tblAccadamicYear tbl = Connection.tblAccadamicYears.FirstOrDefault(X => X.SchoolId == SchoolId);

                     AcademicYear = tbl.AccadamicYear;





                 }
                 else {
                     AcademicYear = DateTime.Now.Year.ToString();
                 
                 
                 }
             }
            
                  var STQlist = Connection.SMGTgetSchoolSubadd(SchoolId, AcademicYear).ToList();
                  if (SchoolId == null || SchoolId == "")

                  {
                      SchoolId = Schoold;

                  }
                  List<SchoolSubjectModel> List = STQlist.Select(x => new SchoolSubjectModel
                  {

                      SubjectCategoryId=x.SubjectCategoryName,
                      SchoolId = x.SchoolId,
                      SubjectName = x.SubjectName,
                      SubjectId = x.SubjectId.ToString(),
                      AcademicYear = AcademicYear,
                     SchoolName=x.SchoolName,



                      IsActive = x.IsActive,


                  }).ToList();
                  return PartialView("academicyrSubjects", List);

              
         }


         public ActionResult ShowSchooHouse(string SchoolId)
         {
             academicyear();
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
             List<tblExtraCurricularActivity> excatlist = Connection.tblExtraCurricularActivities.ToList();
             ViewBag.ActivitydrpList = new SelectList(excatlist, "ActivityCode", "ActivityName");
             List<tblSubject> sclSublist = Connection.tblSubjects.ToList();
             ViewBag.SubjectscldrpList = new SelectList(sclSublist, "SubjectId", "SubjectName");
             SchoolGradeDrpList();
             SchoolHouseDrpListe(Schoold);

             string listid = "";
             if (SchoolId == null || SchoolId == "")
             {
                 listid = Schoold;
             }
             else
             {

                 listid = SchoolId;

             }

             List<SchoolHouseModel> Listl = loadhouselist(listid);
             

             
             
             
             return PartialView("HouseList", Listl);
         }


         public ActionResult ShowSchooHouseDetails(string SchoolId)
         {
             
         

             string listid = "";
             if (SchoolId == null || SchoolId == "")
             {
                 listid = Schoold;
             }
             else
             {

                 listid = SchoolId;

             }

             List<SchoolHouseModel> Listl = loadhouselist(listid);





             return PartialView("DHouseList", Listl);
         }






         private List<SchoolHouseModel> loadhouselist(string SchoolId)
         {

             var STQlist = Connection.SMGTgetSchoolHouseadd(SchoolId).ToList();

             List<SchoolHouseModel> List = STQlist.Select(x => new SchoolHouseModel
             {

                 HouseId = x.HouseId,
                 HouseName = x.HouseName,
                 SchoolId2=SchoolId,

                 //GradeId = x.GradeId,
                 //SchoolName = x.SchoolName,
                 //GradeName = x.GradeName,

                 IsActive = x.IsActive


             }).ToList();
             return List;
         }


         public ActionResult ShowSchooEXActivity(string SchoolId)
         {
             academicyear();
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
             ViewBag.ActivitydrpList = new SelectList(excatlist, "ActivityCode", "ActivityName");
             SchoolGradeDrpList();
             SchoolHouseDrpListe(Schoold);
             if (SchoolId == null) {

                 SchoolId = "";
             }

             List<SchoolExtraModel> List = loadSclEXtralist(SchoolId);

             return PartialView("ExcList", List);
         }


         public ActionResult ShowSchooEXActivityDetails(string SchoolId)
         {
             academicyear();
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
             ViewBag.ActivitydrpList = new SelectList(excatlist, "ActivityCode", "ActivityName");
             SchoolGradeDrpList();
             SchoolHouseDrpListe(Schoold);
             if (SchoolId == null)
             {

                 SchoolId = "";
             }

             List<SchoolExtraModel> List = loadSclEXtralist(SchoolId);

             return PartialView("DExcList", List);
         }
        
  
         public ActionResult ShowSchooSubject(string SchoolId)
         {
             academicyear();
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
             ViewBag.ActivitydrpList = new SelectList(excatlist, "ActivityCode", "ActivityName");

             List<tblSubjectCategory> sclSubcatlist = Connection.tblSubjectCategories.ToList();
             ViewBag.SubcatscldrpList = new SelectList(sclSubcatlist, "SubjectCategoryId", "SubjectCategoryName");
             SchoolGradeDrpList();
             SchoolHouseDrpListe(Schoold);

             List<SchoolExtraModel> List = loadSclEXtralist(Schoold);

             return PartialView("ShowSchoolsubjects", List);
         }


         private List<SchoolModel> LoadClasses(string SchoolId, string GradeId)
         {

             var STQlist = Connection.SMGTgetSchoolClasses(SchoolId,GradeId).ToList();

             List<SchoolModel> List = STQlist.Select(x => new SchoolModel
             {
                 GradeName=x.GradeName,

                ClassId=x.ClassId,
                ClassName=x.ClassName,
                SchoolId=SchoolId,
                GradeId = GradeId
                 //GradeId = x.GradeId,
                 //SchoolName = x.SchoolName,
                 //GradeName = x.GradeName,

            

             }).ToList();
             return List;
         }

         private List<SchoolExtraModel> loadSclEXtralist(string SchoolId)
         {

             var STQlist = Connection.SMGTgetSchoolExtraCadd(SchoolId).ToList();

             List<SchoolExtraModel> List = STQlist.Select(x => new SchoolExtraModel
             {

                ActivityCode=x.ActivityCode,
                ActivityName=x.ActivityName,
                SchoolId=x.SchoolId,
                SchoolName=x.SchoolName,

                 //GradeId = x.GradeId,
                 //SchoolName = x.SchoolName,
                 //GradeName = x.GradeName,

                 IsActive = x.IsActive


             }).ToList();
             return List;
         }


         public ActionResult DeleteGrade(string SchoolId, string GradeId)
         {
             SchoolGradeModel Model = new SchoolGradeModel();
             Model.GradeId = GradeId;
             Model.SchoolId = SchoolId;
             return PartialView("DeleteSchoolGrade", Model);
         }



         public ActionResult DeleteSchoolSubjects(string SchoolId, string SubjectId,string AcademicYear)
         {
             SchoolSubjectModel Model = new SchoolSubjectModel();
             Model.SubjectId= SubjectId;
             Model.AcademicYear = AcademicYear;
             Model.SchoolId = SchoolId;
             return PartialView("DeleteSchoolSubjects", Model);
         }
         public ActionResult Deletescexc(string SchoolId, string ActivityCode)
         {
             SchoolExtraModel Model = new SchoolExtraModel();
             Model.ActivityCode = ActivityCode;
             Model.SchoolId = SchoolId;
         

             return PartialView("DeleteSchoolEXactvity", Model);
         }
         public ActionResult DeleteHouse(string HouseId, string SchoolId)
         {
             SchoolHouseModel Model = new SchoolHouseModel();
             Model.HouseId = HouseId;
             Model.SchoolId2 = SchoolId;

             return PartialView("DeleteSchoolHouse", Model);
         }

         [HttpPost]
         public ActionResult DeleteSchoolgrades(SchoolGradeModel Model)
         {
             try
             {
                
                 tblSchoolGrade Tble = Connection.tblSchoolGrades.Find(Model.SchoolId, Model.GradeId);
                 Connection.tblSchoolGrades.Remove(Tble);
                 Connection.SaveChanges();
                 Connection.SMGTModifyClassStatus(Model.SchoolId, "%", Model.GradeId);
                 Connection.SaveChanges();

                 return Json(Model.SchoolId, JsonRequestBehavior.AllowGet);
                 //return RedirectToAction("Index");
             }
             catch
             {
                 return Json("Error", JsonRequestBehavior.AllowGet);
             }
         }

         [HttpPost]
         public ActionResult DeleteSchoolSubjects(SchoolSubjectModel Model)
         {
             try
             {
                 int a = Int32.Parse(Model.SubjectId);
                 tblSchoolSubject Tble = Connection.tblSchoolSubjects.Find(Model.AcademicYear, Model.SchoolId, a);
                 Connection.tblSchoolSubjects.Remove(Tble);
                 Connection.SaveChanges();


                 return Json(Model.AcademicYear+"!"+Model.SchoolId, JsonRequestBehavior.AllowGet);
                 //return RedirectToAction("Index");
             }
             catch
             {
                 return Json("Error", JsonRequestBehavior.AllowGet);
             }
         }
        

                [HttpPost]
         public ActionResult DeleteSchoolexcactivities(SchoolExtraModel Model)
         {
             try
             {

                 tblSchoolExtraCurricularActivity Tble = Connection.tblSchoolExtraCurricularActivities.Find(Model.SchoolId, Model.ActivityCode);
                 Connection.tblSchoolExtraCurricularActivities.Remove(Tble);
                 Connection.SaveChanges();
                 Connection.SMGTdeleteextraCstudent(Model.SchoolId, Model.ActivityCode);
                 Connection.SaveChanges();

                 var item = Connection.tblStudentExtraCurricularActivities.FirstOrDefault(i => i.SchoolId == Model.SchoolId && i.ActivityCode == Model.ActivityCode);

                 //tblStudentExtraCurricularActivity Tblew = Connection.tblStudentExtraCurricularActivities.Find(item.ActivityCode);

                 //Connection.tblStudentExtraCurricularActivities.Remove(Tblew);
               //  Connection.SaveChanges();


                 return Json(Model.SchoolId, JsonRequestBehavior.AllowGet);
                 //return RedirectToAction("Index");
             }
             catch
             {
                 return Json("Error", JsonRequestBehavior.AllowGet);
             }
         }

         [HttpPost]
         public ActionResult DeleteSchoolHouses(SchoolHouseModel Model)
         {
             try
             {
                // Model.SchoolId="121127";

                 tblHouse Tble = Connection.tblHouses.Find( Model.SchoolId2,Model.HouseId);
                 Connection.SMGTModifySchoolHouseStatus(Model.SchoolId2, Model.HouseId);
             
               //  Connection.tblHouses.
                 Connection.SaveChanges();

                 return Json(Model.SchoolId2, JsonRequestBehavior.AllowGet);


                 //academicyear(); 
                 //List<tblSubjectCategory> sclSubcatlist = Connection.tblSubjectCategories.ToList();
                 //ViewBag.SubcatscldrpList = new SelectList(sclSubcatlist, "SubjectCategoryId", "SubjectCategoryName");
                 //List<tblSchoolCategory> SCategorylist = Connection.tblSchoolCategories.ToList();
                 //ViewBag.SchoolCategoryDrpDown = new SelectList(SCategorylist, "SchoolCategoryId", "SchoolCategoryName");
                 //List<tblProvince> provincelist = Connection.tblProvinces.ToList();
                 //ViewBag.ProvinceDrpDown = new SelectList(provincelist, "ProvinceId", "ProvinceName");
                 //List<tblSchoolGroup> schoolgrps = Connection.tblSchoolGroups.ToList();
                 //ViewBag.SGroupDrpDown = new SelectList(schoolgrps, "GroupId", "GroupName");
                 //List<tblDistrict> districtlist = Connection.tblDistricts.ToList();
                 //ViewBag.DistrictDrpDown = new SelectList(districtlist, "DistrictId", "DistrictName");
                 //List<tblDivision> divisionlist = Connection.tblDivisions.ToList();
                 //ViewBag.DivisionDrpDown = new SelectList(divisionlist, "DivisionId", "DivisionName");
                 //List<tblSchoolRank> Ranklist = Connection.tblSchoolRanks.ToList();
                 //ViewBag.RankDrpDown = new SelectList(Ranklist, "SchoolRankId", "SchoolRankName");
                 //List<tblSubject> sclSublist = Connection.tblSubjects.ToList();
                 //ViewBag.SubjectscldrpList = new SelectList(sclSublist, "SubjectId", "SubjectName");
                 //List<tblExtraCurricularActivity> excatlist = Connection.tblExtraCurricularActivities.ToList();
                 //ViewBag.ActivitydrpList = new SelectList(excatlist, "ActivityCode", "ActivityName");
                 //SchoolGradeDrpList();
                 //SchoolHouseDrpListe(Schoold);

                 //List<SchoolHouseModel> List = loadhouselist(Model.SchoolId);

                 //return PartialView("HouseList", List);



               //  return Json(Model.SchoolId, JsonRequestBehavior.AllowGet);
                 //return RedirectToAction("Index");
             }
             catch
             {
                 return Json("Error", JsonRequestBehavior.AllowGet);
             }
         }


        private void SchoolgradeViewBags()
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


        private void SchoolGradeDrpList()
        {
            if (USession.User_Category == "SADMI")
            {

                List<tblSchool> schoolList = Connection.tblSchools.OrderBy(X => X.SchoolName).Where(X => X.IsActive == "Y" && X.SchoolId == USession.School_Id).ToList();
                ViewBag.SchoolgrddrpList = new SelectList(schoolList, "SchoolId", "SchoolName");
            }
            else
            {
                List<tblSchool> schoolList = Connection.tblSchools.OrderBy(X => X.SchoolName).Where(X => X.IsActive == "Y").ToList();
                ViewBag.SchoolgrddrpList = new SelectList(schoolList, "SchoolId", "SchoolName");
            }

           

            List<tblGrade> GradeList = Connection.tblGrades.OrderBy(X => X.GradeName).Where(x => x.IsActive == "Y").ToList();
            ViewBag.grdschooldrpList = new SelectList(GradeList, "GradeId", "GradeName");
           
        }

        private void SchoolHouseDrpList()
        {
            
            var liste = Connection.SMGTgetTeacherfromSchool("%").ToList();


            List<SMGTgetTeacherfromSchool_Result> Categorylist = liste.ToList();
            SchoolHouseModel schl = new SchoolHouseModel();
            List<SchoolHouseModel> tcmlist = Categorylist.Select(x => new SchoolHouseModel
            {
                SchoolId = x.SchoolId,
                HouseInchargeId = x.TeacherId,
                HouseInchargeName=x.Name
                
                

                

            }).ToList();
            ViewBag.houseteacherdrpList = new SelectList(tcmlist, "HouseInchargeId", "HouseInchargeName");

            List<tblSchool> schoolList = Connection.tblSchools.Where(X => X.IsActive == "Y").ToList();
            ViewBag.SchoolfrhdrpList = new SelectList(schoolList, "SchoolId", "SchoolName");

            //List<tblGrade> GradeList = Connection.tblGrades.ToList();
            //ViewBag.grdschooldrpList = new SelectList(GradeList, "GradeId", "GradeName");

        }

        private void SchoolHouseDrpListe(String schoolid)
        {

            var liste = Connection.SMGTgetTeacherfromSchool(schoolid).ToList();


            List<SMGTgetTeacherfromSchool_Result> Categorylist = liste.ToList();
            SchoolHouseModel schl = new SchoolHouseModel();
            List<SchoolHouseModel> tcmlist = Categorylist.Select(x => new SchoolHouseModel
            {
                SchoolId = x.SchoolId,
                HouseInchargeId = x.TeacherId,
                HouseInchargeName = x.Name





            }).ToList();
            ViewBag.houseteacherdrpList = new SelectList(tcmlist, "HouseInchargeId", "HouseInchargeName");

            List<tblSchool> schoolList = Connection.tblSchools.Where(X => X.IsActive == "Y").ToList();
            ViewBag.SchoolfrhdrpList = new SelectList(schoolList, "SchoolId", "SchoolName");
            //ViewBag.SchoolfrhdrpList.SelectedValue= "121127";
            //List<tblGrade> GradeList = Connection.tblGrades.ToList();
            //ViewBag.grdschooldrpList = new SelectList(GradeList, "GradeId", "GradeName");

        }
           
        //
        // POST: /School/Create

        [HttpPost]
        [UserFilter(Function_Id = "SCF")]
        public ActionResult Create(SchoolModel Model)
        {

           // Authentication("SCF");

            string _path="";
            string _pathL = "";
              string _path1="";
            string _pathL2 = "";
            String result = "error";
       

           // return View("SchoolCreate");

            Model.SchoolId = Schoold;

            try
            {
                if (ModelState.IsValid)
                {
                    if (Model.File == null)
                    {

                    }
                    else
                    {
                        if (Model.File.ContentLength > 0)
                        {


                            string fnm = DateTime.Now.ToString("");
                            string nwString22 = fnm.Replace("-", ".");
                            string nwString = nwString22.Replace("/", ".");
                            string nwString2 = nwString.Replace(" ", ".");
                            string time = nwString2.Replace(":", ".");

                            string _FileName = time + "_" + Path.GetFileName(Model.File.FileName);
                            _path = Path.Combine(Server.MapPath("~/Uploads/Schools/Images"), _FileName);
                            _path1 = "~/Uploads/Schools/Images/" + _FileName;
                            Model.File.SaveAs(_path);
                        }
                    }
                    if (Model.LogoFile == null)
                    {

                    }
                    else
                    {
                        if (Model.LogoFile.ContentLength > 0)
                        {
                            string fnm = DateTime.Now.ToString("");
                            string nwString22 = fnm.Replace("-", ".");
                            string nwString = nwString22.Replace("/", ".");
                            string nwString2 = nwString.Replace(" ", ".");
                            string time = nwString2.Replace(":", ".");

                            string _FileName = time + "_" + Path.GetFileName(Model.LogoFile.FileName);
                            _pathL = Path.Combine(Server.MapPath("~/Uploads/Schools/Logo"), _FileName);
                            _pathL2 = "~/Uploads/Schools/Logo/" + _FileName;
                            Model.LogoFile.SaveAs(_pathL);
                        }
                    }
                    //  ViewBag.Message = "File Uploaded Successfully!!";  

                    var schoolcount = Connection.SMGTSchoolCount().FirstOrDefault();
                    string SchoolId = "Scl" + Model.Division.ToString() + Model.District.ToString() + Model.Province.ToString() + Model.SchoolGroup.ToString() + schoolcount.ToString();

                    //using (var scope = new TransactionScope())
                    //{
                        Connection.DCISsetSchool(SchoolId, Model.SchoolGroup, Model.SchoolName, Model.SchoolRank, "Y", Model.Division,
                            Model.District, Model.Description, UserId, Model.Address1, Model.Address2, Model.Address3, Model.Email, Model.Fax, _path1, Convert.ToInt16(Model.MinuteforPeriod), Model.Telephone, Model.SchoolCategory, Model.Province, Model.WebAddress, _pathL2
                            );
                        Connection.SaveChanges();

                        tblAccadamicYear tblac = new tblAccadamicYear();
                        tblac.SchoolId = SchoolId;
                        tblac.AccadamicYear = DateTime.Now.Year.ToString();
                        tblac.CreatedBy = USession.User_Id;
                        tblac.CreatedDate = DateTime.Now;
                        Connection.tblAccadamicYears.Add(tblac);
                        Connection.SaveChanges();

                       // scope.Complete();

                  //  }

                        result = "sessioncheck" + "!" + SchoolId;
                    ModelState.Clear();
             
                    //return View();
                    return Json(result, JsonRequestBehavior.AllowGet);

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
            return Json(result, JsonRequestBehavior.AllowGet);

               



            //try
            //{
            //    // TODO: Add insert logic here

            //    return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}
        }

        //
        // GET: /School/Edit/5

        public ActionResult Edit(string SchoolId)
        {

            Authentication("SCF");

            SchoolHouseDrpListe(SchoolId);

            academicyear();


            var SchoolGrade = Connection.SMGTgetSchoolGrade(SchoolId).OrderBy(X=>X.GradeName).ToList();//Need to Pass a Session Schoolid





            //  var StudentSextra = Connection.SMGTloadScholExtraCadd(SchoolId, "%").ToList();

            List<tblGrade> result = SchoolGrade.Select(x => new tblGrade
            {
                GradeId = x.GradeId,
                GradeName = x.GradeName
                
            }).OrderBy(X=>X.GradeName).ToList();


           // List<tblGrade> GradeList = Connection.tblGrades.Where(x => x.IsActive == "Y" ).ToList();
            ViewBag.grdschooldrpListl = new SelectList(result, "GradeId", "GradeName");

            List<tblExtraCurricularActivity> excatlist = Connection.tblExtraCurricularActivities.Where(X => X.IsActive == "Y").ToList();
            ViewBag.ActivitydrpList = new SelectList(excatlist, "ActivityCode", "ActivityName");


            ViewBag.EditSChoolID = SchoolId;

            SchoolGradeDrpList();



            SchoolModel TModel = new SchoolModel();

            tblSchool TCtable = Connection.tblSchools.SingleOrDefault(x => x.SchoolId == SchoolId);
            //  TModel.IsActive = TCtable.IsActive;

            TModel.SchoolId = TCtable.SchoolId;
            TModel.SchoolName = TCtable.SchoolName;

            ViewBag.EditSchoolName = TCtable.SchoolName;
            TModel.Address1 = TCtable.Address1;
            TModel.Description = TCtable.Description;
            TModel.Address1 = TCtable.Address1;
            TModel.Address2 = TCtable.Address2;
            TModel.Address3 = TCtable.Address3;
            TModel.Email = TCtable.Email;
            TModel.WebAddress = TCtable.WebUrl;
            TModel.MinuteforPeriod = TCtable.MinuteforPeriod.ToString();
            TModel.Telephone = TCtable.Telephone;

            academicyear();
            List<tblSubjectCategory> sclSubcatlist = Connection.tblSubjectCategories.Where(X => X.IsActive == "Y").ToList();
            ViewBag.SubcatscldrpList = new SelectList(sclSubcatlist, "SubjectCategoryId", "SubjectCategoryName");
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
            List<tblExtraCurricularActivity> excatliste = Connection.tblExtraCurricularActivities.Where(X => X.IsActive == "Y").ToList();
            ViewBag.ActivitydrpList = new SelectList(excatliste, "ActivityCode", "ActivityName");

            TModel.SchoolCategory = TCtable.SchoolCategory;
            TModel.SchoolGroup = TCtable.SchoolGroup;
            TModel.SchoolRank = TCtable.SchoolRank;
            TModel.District = TCtable.District;
            TModel.Division = TCtable.Division;
            TModel.Province = TCtable.Province;


            return View("Edit");





        }





        //
        // POST: /School/Edit/5

        [HttpPost]
        public ActionResult Edit(SchoolModel Model)
        {
            Authentication("SCF");

            string _path = "";

            string _pathL = "";
            string _path1 = "";

            string _pathL2 = "";
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add update logic here
                    if (Model.File == null)
                    {

                    }
                    else
                    {


                        if (Model.File.ContentLength > 0)
                        {

                            string fnm = DateTime.Now.ToString("");
                            string nwString22 = fnm.Replace("-", ".");
                            string nwString = nwString22.Replace("/", ".");
                            string nwString2 = nwString.Replace(" ", ".");
                            string time = nwString2.Replace(":", ".");

                            string _FileName = time + "_" + Path.GetFileName(Model.File.FileName);




                            _path = Path.Combine(Server.MapPath("~/Uploads/Schools/Images"), _FileName);
                            _path1 = "~/Uploads/Schools/Images/" + _FileName;
                            Model.File.SaveAs(_path);
                        }
                    }
                    if (Model.LogoFile == null) { }

                    else
                    {

                        if (Model.LogoFile.ContentLength > 0)
                        {
                            string fnm = DateTime.Now.ToString("");
                            string nwString22 = fnm.Replace("-", ".");
                            string nwString = nwString22.Replace("/", ".");
                            string nwString2 = nwString.Replace(" ", ".");
                            string time = nwString2.Replace(":", ".");

                            string _FileName = time + "_" + Path.GetFileName(Model.LogoFile.FileName);


                            _pathL = Path.Combine(Server.MapPath("~/Uploads/Schools/Logo"), _FileName);
                            _pathL2 = "~/Uploads/Schools/Logo/" + _FileName;
                            Model.LogoFile.SaveAs(_pathL);
                        }
                    }
                }





                SchoolModel TModel = new SchoolModel();

                tblSchool TCtable = Connection.tblSchools.SingleOrDefault(x => x.SchoolId == Model.SchoolId);
                //  TModel.IsActive = TCtable.IsActive;

                TModel.SchoolId = TCtable.SchoolId;
                TModel.SchoolName = TCtable.SchoolName;
                TModel.Address1 = TCtable.Address1;
                TModel.Description = TCtable.Description;
                TModel.Address1 = TCtable.Address1 + " " + TCtable.Address2 + " " + TCtable.Address3;
                TModel.Email = TCtable.Email;
                TModel.WebAddress = TCtable.WebUrl;
                TModel.MinuteforPeriod = TCtable.MinuteforPeriod.ToString();
                TModel.Telephone = TCtable.Telephone;

                TModel.SchoolCategory = TCtable.SchoolCategory;
                TModel.SchoolGroup = TCtable.SchoolGroup;
                TModel.SchoolRank = TCtable.SchoolRank;
                TModel.District = TCtable.District;
                TModel.Division = TCtable.Division;
                TModel.Province = TCtable.Province;
                TModel.ImagePath = TCtable.ImagePath;
                TModel.LogoPath = TCtable.LogoPath;

                if (Model.SchoolName == null)
                {
                    Model.SchoolName = TModel.SchoolName;

                }


                if (Model.Email == null)
                {
                    Model.Email = TModel.Email;

                }
                if (Model.MinuteforPeriod == null)
                {
                    Model.MinuteforPeriod = TModel.MinuteforPeriod;

                }

                if (Model.Telephone == null)
                {
                    Model.Telephone = TModel.Telephone;

                }

                if (_path1 == "")
                {

                    _path1 = TModel.ImagePath;

                }
                if (_pathL2 == "")
                {

                    _pathL2 = TModel.LogoPath;

                }







                Connection.DCISModifySchool(Model.SchoolId, Model.SchoolGroup, Model.SchoolName, Model.SchoolRank, "Y", Model.Division,
                   Model.District, Model.Description, UserId, Model.Address1, Model.Address2, Model.Address3, Model.Email, Model.Fax, _path1, Convert.ToInt16(Model.MinuteforPeriod), Model.Telephone, Model.SchoolCategory, Model.Province, Model.WebAddress, _pathL2
                   );
                Connection.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /School/Delete/5

        public ActionResult Delete(string SchoolId)
        {
            SchoolModel TModel = new SchoolModel();
            TModel.SchoolId = SchoolId;
            TModel.IsActive = "N";


            return PartialView("Delete", TModel);
        }

        //
        // POST: /School/Delete/5

        [HttpPost]
        public ActionResult Delete(SchoolModel TModel)
        {
            Authentication("SCF");
            try
            {
                // TODO: Add delete logic here
                Connection.SMGTDeleteSchool("N", TModel.SchoolId, UserId);
                  
                Connection.SaveChanges();




                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


         [HttpGet]  
        public ActionResult UploadFile()  
        {  
            return View();  
        }  
        [HttpPost]  
        public ActionResult UploadFile(HttpPostedFileBase file)  
        {  
            try  
            {  
                if (file.ContentLength > 0)  
                {  
                    string _FileName = Path.GetFileName(file.FileName);  
                    string _path = Path.Combine(Server.MapPath("~/Uploads"), _FileName);  
                    file.SaveAs(_path);  
                }  
                ViewBag.Message = "File Uploaded Successfully!!";  
                return PartialView("SchoolCreate");  
            }  
            catch  
            {  
                ViewBag.Message = "File upload failed!!";  
                return PartialView("SchoolCreate");  
            }  
        }


        [HttpPost]
        public ActionResult FileUpload(RegistrationModel mRegister)
        {
            //Check server side validation using data annotation
            if (ModelState.IsValid)
            {

                string filesadd = mRegister.Address;
                //TO:DO
                var fileName = Path.GetFileName(mRegister.file.FileName);
                var path = Path.Combine(Server.MapPath("~/Uploads"), fileName);
                mRegister.file.SaveAs(path);
                ViewBag.Message = "File has been uploaded successfully";
                ModelState.Clear();
            }
            return View("Index");
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetHouseIncharge(string SchoolId)
        {
            if (String.IsNullOrEmpty(SchoolId))
            {
                throw new ArgumentNullException("countryId");
            }
            var houseincharge = Connection.SMGTgetSchoolteachers(SchoolId).ToList();




            var StudentSextra = Connection.SMGTloadScholExtraCadd(SchoolId, "%").ToList();

            var result2 = (from s in houseincharge
                           select new
                           {
                               HouseInchargeId = s.TeacherId,
                               HouseInchargeName = s.Name
                           }).ToList();

            List<tblTeacher> result = houseincharge.Select(x => new tblTeacher
            {
                TeacherId = x.TeacherId,
                Name = x.Name
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
        public ActionResult GetProvince(string ProvinceId)
        {
            if (String.IsNullOrEmpty(ProvinceId))
            {
                throw new ArgumentNullException("countryId");



            }
            int prv = Int16.Parse(ProvinceId);



            List<tblDistrict> sclSubcatlist = Connection.tblDistricts.Where(X => X.ProvinceId == prv).ToList();





            var result2 = (from s in sclSubcatlist
                           select new
                           {
                               DistrictId = s.DistrictId,
                               DistrictName = s.DistrictName
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
        public ActionResult Getloadschool()
        {

            if (USession.User_Category == "SADMI")
            {
                List<tblSchool> scldatalist = Connection.tblSchools.OrderBy(X=>X.SchoolName).Where(X => X.SchoolId == USession.School_Id && X.IsActive=="Y").ToList();
                var result2 = (from s in scldatalist
                               select new
                               {
                                   SchoolId = s.SchoolId,
                                   SchoolName = s.SchoolName
                               }).ToList();


                return Json(result2, JsonRequestBehavior.AllowGet);

            }
            else {

                List<tblSchool> scldatalist = Connection.tblSchools.OrderBy(X => X.SchoolName).Where(X => X.IsActive == "Y").ToList();

                var result2 = (from s in scldatalist
                               select new
                               {
                                   SchoolId = s.SchoolId,
                                   SchoolName = s.SchoolName
                               }).ToList();


                return Json(result2, JsonRequestBehavior.AllowGet);
            
            
            }






        }



        //edit Start


        public ActionResult ShowEditGrade(string SchoolId)
        {

            List<tblGrade> SRlist = Connection.tblGrades.OrderBy(X=>X.GradeName).Where(X => X.IsActive == "Y").ToList();
            ViewBag.GradeList = new SelectList(SRlist, "GradeId", "GradeName");


            var STQlist = Connection.SMGTgetSchoolGradeadd(SchoolId).ToList();

            List<SchoolGradeModel> List = STQlist.Select(x => new SchoolGradeModel
            {

                SchoolId = x.SchoolId,
                GradeId = x.GradeId,
                SchoolName = x.SchoolName,
                GradeName = x.GradeName,

                IsActive = x.IsActive,


            }).ToList();



            SchoolGradeModel TModel = new SchoolGradeModel();


            var TCtable = Connection.tblSchoolGrades.Where(x => x.SchoolId == SchoolId).ToList();


            //  tblParent TCtable = Connection.tblParents.SingleOrDefault(x => x.ParentId == pid);
            //  TModel.IsActive = TCtable.IsActive;
            //   TModel.ParentId = TCtable.ParentId.ToString();




            return PartialView("EditParentDetailsView", TModel);
        }



        public ActionResult ShowEditSchool(string SchoolId)
        {
            SchoolGradeDrpList();



            SchoolModel TModel = new SchoolModel();

            tblSchool TCtable = Connection.tblSchools.SingleOrDefault(x => x.SchoolId == SchoolId);
            //  TModel.IsActive = TCtable.IsActive;

            TModel.SchoolId = TCtable.SchoolId;
            TModel.SchoolName = TCtable.SchoolName;
            TModel.Address1 = TCtable.Address1;
            TModel.Description = TCtable.Description;
            TModel.Address1 = TCtable.Address1;
            TModel.Address2 = TCtable.Address2;
            TModel.Address3 = TCtable.Address3;
            TModel.Email = TCtable.Email;
            TModel.WebAddress = TCtable.WebUrl;
            TModel.MinuteforPeriod = TCtable.MinuteforPeriod.ToString();
            TModel.Telephone = TCtable.Telephone;

            academicyear();
            List<tblSubjectCategory> sclSubcatlist = Connection.tblSubjectCategories.Where(X => X.IsActive == "Y").ToList();
            ViewBag.SubcatscldrpList = new SelectList(sclSubcatlist, "SubjectCategoryId", "SubjectCategoryName");
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

            TModel.SchoolCategory = TCtable.SchoolCategory;
            TModel.SchoolGroup = TCtable.SchoolGroup;
            TModel.SchoolRank = TCtable.SchoolRank;
            TModel.District = TCtable.District;
            TModel.Division = TCtable.Division;
            TModel.Province = TCtable.Province;





            return PartialView("EditSchoolView", TModel);
        }


        [AllowAnonymous]
        public JsonResult EAddSchoolHouse(SchoolHouseModel Model)
        {
             Authentication("SCF");
            try
            {
                string result = "Error";
                var count2 = Connection.tblHouses.Count();
                int a = count2 + 10;
                Model.HouseId = a.ToString();
                var count = Connection.tblHouses.Count(u => u.SchoolId == Model.SchoolId && u.HouseName == Model.HouseName);
                if (count == 0)
                {
                    //    Model.SchoolId = Schoold;
                    tblHouse newscg = new tblHouse();

                    newscg.CreatedBy = "User1";
                    newscg.CreatedDate = DateTime.Now;
                    newscg.SchoolId = Model.SchoolId;
                    newscg.HouseId = Model.HouseId;
                    newscg.HouseName = Model.HouseName;
                    newscg.IsActive = "Y";
                    newscg.HouseInchargeId = Model.HouseInchargeId.ToString();

                    Connection.tblHouses.Add(newscg);

                    Connection.SaveChanges();

                    result = "sessioncheck" + "!" + Model.SchoolId;

                    ViewBag.SchoolId = Model.SchoolId;

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


        public ActionResult ShowEditSchoolSubjects( string SchoolId)
        {
            String AcademicYear;
           var Academicyr = Connection.tblAccadamicYears.SingleOrDefault(x => x.SchoolId == SchoolId);

           if (Academicyr == null)
           {
               AcademicYear = DateTime.Now.Year.ToString();
           }
           else {

               AcademicYear = Academicyr.AccadamicYear;
           }
            var STQlist = Connection.SMGTgetSchoolSubadd(SchoolId, AcademicYear).ToList();
            if (SchoolId == null || SchoolId == "")
            {
                SchoolId = Schoold;

            }
            List<SchoolSubjectModel> List = STQlist.Select(x => new SchoolSubjectModel
            {


                SchoolId = x.SchoolId,
                SubjectName = x.SubjectName,
                SubjectId = x.SubjectId.ToString(),
                AcademicYear=AcademicYear,


                IsActive = x.IsActive,


            }).ToList();
            return PartialView("SclSubList", List);
        }


        public ActionResult ShowEditSchoolSubjectsDetails(string SchoolId)
        {
            String AcademicYear;
            var Academicyr = Connection.tblAccadamicYears.SingleOrDefault(x => x.SchoolId == SchoolId);

            if (Academicyr == null)
            {
                AcademicYear = DateTime.Now.Year.ToString();
            }
            else
            {

                AcademicYear = Academicyr.AccadamicYear;
            }
            var STQlist = Connection.SMGTgetSchoolSubadd(SchoolId, AcademicYear).ToList();
            if (SchoolId == null || SchoolId == "")
            {
                SchoolId = Schoold;

            }
            List<SchoolSubjectModel> List = STQlist.Select(x => new SchoolSubjectModel
            {


                SchoolId = x.SchoolId,
                SubjectName = x.SubjectName,
                SubjectId = x.SubjectId.ToString(),
                AcademicYear = AcademicYear,


                IsActive = x.IsActive,


            }).ToList();
            return PartialView("DSclSubList", List);
        }



        [AllowAnonymous]
        public JsonResult EAddSchoolSubjects(SchoolSubjectModel Model)
        {
            Authentication("SCF");
            try

            {

                string Academicyear;
                var academ = Connection.tblAccadamicYears.SingleOrDefault(X => X.SchoolId == Model.SchoolId);
                if (academ == null)
                {

                    Academicyear = DateTime.Now.Year.ToString();
                }
                else {

                    Academicyear = academ.AccadamicYear;
                }
             
                string result = "Error";
                int subid = Int32.Parse(Model.SubjectId);



                var count = Connection.tblSchoolSubjects.Count(u => u.SchoolId == Model.SchoolId && u.AcademicYear == Academicyear && u.SubjectId == subid);
                if (count == 0)
                {
                    //  Model.SchoolId = Schoold;
                    tblSchoolSubject newscg = new tblSchoolSubject();

                    newscg.CreatedBy = "User1";
                    newscg.CreatedDate = DateTime.Now;
                    newscg.SchoolId = Model.SchoolId;
                    newscg.Optional = "Y";

                    newscg.SubjectId = Int32.Parse(Model.SubjectId);
                    newscg.SubjectCategoryId = Int32.Parse(Model.SubjectCategoryId);
                    newscg.AcademicYear = Academicyear;
                    newscg.IsActive = "Y";


                    Connection.tblSchoolSubjects.Add(newscg);

                    Connection.SaveChanges();

                    result = "sessioncheck" + "!" + Model.SchoolId;

                    ViewBag.SchoolId =  Model.SchoolId;

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


        public ActionResult EShowSchooClasses(string SchoolIdGradeId)
        {
            if (SchoolIdGradeId == null)
            {

                SchoolIdGradeId = "awq!Bss";
            }

            string scid = SchoolIdGradeId.Split('!')[0];
            string gdid = SchoolIdGradeId.Split('!')[1];

          ;
            // string GradeId = ""; ;
            List<SchoolModel> List = LoadClasses(scid, gdid);
            //    string username = result.Consignor.Split('<')[0];
            return PartialView("ClassesList", List);
        }



        public ActionResult ShowEditSchooladmin(string SchoolId)
        {
            List<tblUser> usr = Connection.tblUsers.Where(X => X.IsActive == "Y" && X.SchoolId == SchoolId && X.UserCategory == "SADMI").ToList();


            ViewBag.UseradminList = new SelectList(usr, "UserId", "PersonName");






            return PartialView("EditSchoolAdminC");
        }


        public ActionResult Admindetails(string AdminUserId)
        {








            tblUser sclgrp = Connection.tblUsers.SingleOrDefault(x => x.UserId == AdminUserId);
            string adminname = sclgrp.PersonName;
            string email = sclgrp.LoginEmail;
            string telephone = sclgrp.Mobile;




            var details = new { Name = adminname, Email = email, Contact = telephone };






            return Json(details, JsonRequestBehavior.AllowGet);
            //Hard coded for demo. You may replace it with data from db.

        }


        [AllowAnonymous]
        [UserFilter(Function_Id = "SCF")]
        public JsonResult EditSchoolAdmin(SchoolAdminModel Model)
        {
            try
            {
                string result = "Error";
                if (Model.SchoolId == null || Model.AdminName == null || Model.Password == null || Model.AdminPersonalEmail == null)
                {

                    result = "notfilled";

                }
                else
                {



                    tblUser usr = new tblUser();


                    usr.SchoolId = Model.SchoolId;
                    usr.UserId = Model.AdminUserId;

                    usr.Mobile = Model.PersonalMobile;
                    usr.ModifiedBy = UserId;

                    usr.PersonName = Model.AdminName;
                    usr.UserId = Model.AdminUserId;
                    string pass = Encrypt_Decrypt.Encrypt(Model.Password, Password);

                    usr.Password = pass;
                    usr.LoginEmail = Model.AdminPersonalEmail;
                    Connection.SMGTModifyAdminUser(usr.SchoolId, usr.UserId, usr.PersonName, usr.Mobile, usr.Password, usr.LoginEmail,usr.ModifiedBy);



                    Connection.SaveChanges();

                    result = "sessioncheck" + "!" + Model.SchoolId;




                    //ShowTeacherQualificatoin();
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Errorlog.ErrorManager.LogError("Teacher Controller - AddQualification(QualificationModel Model)", Ex);
                return Json("Exception", JsonRequestBehavior.AllowGet);

            }
        }


        public ActionResult ShowSchooClassesnGrades(string SchoolId)
        {

            List<SchoolModel> List = LoadClasses(SchoolId, "%");
            //    string username = result.Consignor.Split('<')[0];
            return PartialView("gradeclassview", List);
        }



    }
}
