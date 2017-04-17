using GDWEBSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GDWEBSolution.Models.Schools;
using System.IO;
using GDWEBSolution.Models.Teacher;

namespace GDWEBSolution.Controllers
{
    public class SchoolController : Controller
    {
        String Schoold = "121127";
        string UserId = "User1";
        //
        // GET: /School/
        SchoolMGTEntitiesConnectionString Connection = new SchoolMGTEntitiesConnectionString();
        public ActionResult Index()
        {
          
          
            var school = Connection.DCISgetSchool();
        
            List<DCISgetSchool_Result> Categorylist = school.ToList();
            SchoolModel schl = new SchoolModel();
            List<SchoolModel> tcmlist = Categorylist.Select(x => new SchoolModel
            {
               
                SchoolId=x.SchoolId,
                SchoolName=x.SchoolName,
                SchoolGroup=x.SchoolGroup,
                Address1=x.Address1,
                Address2=x.Address2,
                Address3=x.Address3,
                SchoolCategory=x.SchoolCategory,
                MinuteforPeriod=x.MinuteforPeriod.ToString(),
               SchoolCategoryName= x.SchoolCategoryName,
               GroupName=x.GroupName,
               CreatedDate=x.CreatedDate,
       
          
                
                IsActive = x.IsActive,
                ModifiedBy = x.ModifiedBy,
                ModifiedDate = x.ModifiedDate

            }).ToList();



            return View(tcmlist);
        }

        //
        // GET: /School/Details/5

        public ActionResult Detail(String SchoolId)
        {


            SchoolModel TModel = new SchoolModel();

            tblSchool TCtable = Connection.tblSchools.SingleOrDefault(x => x.SchoolId == SchoolId);
            //  TModel.IsActive = TCtable.IsActive;

            TModel.SchoolId = TCtable.SchoolId;
            TModel.SchoolName = TCtable.SchoolName;
            TModel.Address1 = TCtable.Address1;
            TModel.Description = TCtable.Description;
            TModel.Address1 = TCtable.Address1 +" "+ TCtable.Address2 + " "+TCtable.Address3;
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
            if (TCtable.ImagePath == null ) { 
            
            TModel.ImagePath="~/Uploads/Schools/Logo/logo.png";
            }
            if (TCtable.LogoPath == null)
            {

                TModel.LogoPath = "~/Uploads/Schools/Logo/logo.png";
            }

            
            tblSchoolCategory cattbl = Connection.tblSchoolCategories.SingleOrDefault(x => x.SchoolCategoryId == TModel.SchoolCategory);
            TModel.SchoolCategoryName = cattbl.SchoolCategoryName;
            tblSchoolGroup sclgrp = Connection.tblSchoolGroups.SingleOrDefault(x => x.GroupId == TModel.SchoolGroup);
            TModel.GroupName = sclgrp.GroupName;
            tblSchoolRank sclrank = Connection.tblSchoolRanks.SingleOrDefault(x => x.SchoolRankId == TModel.SchoolRank);
            TModel.SchoolRankName = sclrank.SchoolRankName;
            tblDistrict scldr = Connection.tblDistricts.SingleOrDefault(x => x.DistrictId == TModel.District);
            TModel.DistrictName = scldr.DistrictName;
            tblDivision scldivisions = Connection.tblDivisions.SingleOrDefault(x => x.DivisionId == TModel.Division);
            TModel.DivisionName = scldivisions.DivisionName;
            tblProvince Province = Connection.tblProvinces.SingleOrDefault(x => x.ProvinceId == TModel.Province);
            TModel.ProvinceName = Province.ProvinceName;


            return View("Detail", TModel);



        }

        //
        // GET: /School/Create

        public ActionResult Create(SchoolHouseModel hsmodel)
        
       {

           ViewBag.SchoolId = Schoold;

           hsmodel.SchoolId = Schoold;
            List<tblSchoolCategory> SCategorylist = Connection.tblSchoolCategories.ToList();
            ViewBag.SchoolCategoryDrpDown = new SelectList(SCategorylist, "SchoolCategoryId", "SchoolCategoryName");
            List<tblProvince> provincelist = Connection.tblProvinces.ToList();
            ViewBag.ProvinceDrpDown = new SelectList(provincelist, "ProvinceId", "ProvinceName");
            List<tblSchoolGroup> schoolgrps = Connection.tblSchoolGroups.ToList();
            ViewBag.SGroupDrpDown = new SelectList(schoolgrps , "GroupId", "GroupName");
            List<tblDistrict> districtlist = Connection.tblDistricts.ToList();
            ViewBag.DistrictDrpDown = new SelectList(districtlist, "DistrictId", "DistrictName");
            List<tblDivision> divisionlist = Connection.tblDivisions.ToList();
            ViewBag.DivisionDrpDown = new SelectList(divisionlist, "DivisionId", "DivisionName");
            List<tblSchoolRank> Ranklist = Connection.tblSchoolRanks.ToList();
            ViewBag.RankDrpDown = new SelectList(Ranklist, "SchoolRankId", "SchoolRankName");

            List<tblSubject> sclSublist = Connection.tblSubjects.ToList();
            ViewBag.SubjectscldrpList = new SelectList(sclSublist, "SubjectId", "SubjectName");


            List<tblSubjectCategory> sclSubcatlist = Connection.tblSubjectCategories.ToList();
            ViewBag.SubcatscldrpList = new SelectList(sclSubcatlist, "SubjectCategoryId", "SubjectCategoryName");

            SchoolGradeDrpList();
            SchoolHouseDrpListe(Schoold);
            List<tblExtraCurricularActivity> excatlist = Connection.tblExtraCurricularActivities.ToList();
            ViewBag.ActivitydrpList = new SelectList(excatlist, "ActivityCode", "ActivityName");
          
            return View("SchoolCreate");
        }

        public ActionResult Createe(string schoolid)
        {
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
        public JsonResult AddSchoolGrade(SchoolGradeModel Model)
        {
            try
            {
                string result = "Error";

                var count = Connection.tblSchoolGrades.Count(u => u.SchoolId == Model.SchoolId && u.GradeId == Model.GradeId);
                if (count == 0)
                {

                    tblSchoolGrade newscg = new tblSchoolGrade();

                    newscg.CreatedBy = "User1";
                    newscg.SchoolId = Model.SchoolId;
                    newscg.GradeId = Model.GradeId;
                    newscg.IsActive = "Y";
                    newscg.CreatedDate = DateTime.Now;
                   

                    Connection.tblSchoolGrades.Add(newscg);
                    Connection.SaveChanges();

                    result = Model.SchoolId.ToString();

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

        [AllowAnonymous]
         public JsonResult AddSchoolHouse(SchoolHouseModel Model)
         {
             try
             {
                 string result = "Error";
                 var count2 = Connection.tblHouses.Count();
                 int a = count2 + 10;
                 Model.HouseId = a.ToString();
                 var count = Connection.tblHouses.Count(u => u.SchoolId == Schoold && u.HouseName == Model.HouseName);
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

                     result = Model.SchoolId.ToString();

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


        [AllowAnonymous]
        public JsonResult AddSchoolSubjects(SchoolSubjectModel Model)
        {
            try
            {
                string result = "Error";
                int subid = Int32.Parse(Model.SubjectId);


                var count = Connection.tblSchoolSubjects.Count(u => u.SchoolId == Schoold && u.AcademicYear == Model.AcademicYear && u.SubjectId == subid);
                if (count == 0)
                {
                    Model.SchoolId = Schoold;
                    tblSchoolSubject newscg = new tblSchoolSubject();

                    newscg.CreatedBy = "User1";
                    newscg.CreatedDate = DateTime.Now;
                    newscg.SchoolId = Schoold;
                    newscg.Optional = "Y";

                    newscg.SubjectId = Int32.Parse(Model.SubjectId);
                    newscg.SubjectCategoryId =Int32.Parse( Model.SubjectCategoryId);
                    newscg.AcademicYear = Model.AcademicYear;
                    newscg.IsActive = "Y";
                   

                    Connection.tblSchoolSubjects.Add(newscg);

                    Connection.SaveChanges();

                    result = Model.SchoolId.ToString();

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

        [AllowAnonymous]
        public JsonResult AddSchoolExcActivity(SchoolExtraModel Model)
        {
            try
            {
                string result = "Error";
                var count2 = Connection.tblSchoolExtraCurricularActivities.Count();
               // Model.HouseId = count2.ToString();
                var count = Connection.tblSchoolExtraCurricularActivities.Count(u => u.ActivityCode == Model.ActivityCode && u.SchoolId == Model.SchoolId);
                if (count == 0)
                {
                    Model.SchoolId = Schoold;
                    tblSchoolExtraCurricularActivity newscg = new tblSchoolExtraCurricularActivity();

                    newscg.CreatedBy = "User1";
                    newscg.CreatedDate = DateTime.Now;
                    newscg.SchoolId = Schoold;
                    newscg.ActivityCode = Model.ActivityCode;              
                    newscg.IsActive = "Y";
                 

                    Connection.tblSchoolExtraCurricularActivities.Add(newscg);

                    Connection.SaveChanges();

                    result = Model.SchoolId.ToString();

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


         public ActionResult ShowSchoolSubjects(string AcademicYear, string SchoolId)
         {
             var STQlist = Connection.SMGTgetSchoolSubadd(Schoold, AcademicYear).ToList();
             if (SchoolId == null||SchoolId =="")
             {
                 SchoolId = Schoold;

             }
             List<SchoolSubjectModel> List = STQlist.Select(x => new SchoolSubjectModel
             {
                
                 
                 SchoolId = x.SchoolId,
                 SubjectName=x.SubjectName,
                SubjectId= x.SubjectId.ToString(),

                
             
                 IsActive = x.IsActive,
                 

             }).ToList();
             return PartialView("SclSubList", List);
         }
         public ActionResult ShowSchooHouse(string SchoolId)
         {

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

             List<SchoolHouseModel> List = loadhouselist(listid);
             return PartialView("HouseList", List);
         }






         private List<SchoolHouseModel> loadhouselist(string SchoolId)
         {

             var STQlist = Connection.SMGTgetSchoolHouseadd(SchoolId).ToList();

             List<SchoolHouseModel> List = STQlist.Select(x => new SchoolHouseModel
             {

                 HouseId = x.HouseId,
                 HouseName = x.HouseName,

                 //GradeId = x.GradeId,
                 //SchoolName = x.SchoolName,
                 //GradeName = x.GradeName,

                 IsActive = x.IsActive


             }).ToList();
             return List;
         }


         public ActionResult ShowSchooEXActivity(string SchoolId)
         {

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

             List<SchoolExtraModel> List = loadSclEXtralist(Schoold);

             return PartialView("ExcList", List);
         }
         public ActionResult ShowSchooSubject(string SchoolId)
         {

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
         public ActionResult Deletescexc(string SchoolId, string ActivityCode,string ActivityName)
         {
             SchoolExtraModel Model = new SchoolExtraModel();
             Model.ActivityCode = ActivityCode;
             Model.SchoolId = SchoolId;
             Model.ActivityName = ActivityName;
             return PartialView("DeleteSchoolEXactvity", Model);
         }
         public ActionResult DeleteHouse(string HouseId, string HouseName, string SchoolId)
         {
             SchoolHouseModel Model = new SchoolHouseModel();
             Model.HouseId = HouseId;

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

                 tblSchoolSubject Tble = Connection.tblSchoolSubjects.Find(Model.AcademicYear,Model.SchoolId, Model.SubjectId);
                 Connection.tblSchoolSubjects.Remove(Tble);
                 Connection.SaveChanges();


                 return Json(Model.AcademicYear, JsonRequestBehavior.AllowGet);
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
                 Model.SchoolId="121127";

                 tblHouse Tble = Connection.tblHouses.Find( Model.SchoolId,Model.HouseId);
                 Connection.tblHouses.Remove(Tble);
                 Connection.SaveChanges();



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

                 List<SchoolHouseModel> List = loadhouselist(Model.SchoolId);

                 return PartialView("HouseList", List);



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
           
            List<tblSchool> schoolList = Connection.tblSchools.ToList();
            ViewBag.SchoolgrddrpList = new SelectList(schoolList, "SchoolId", "SchoolName");

            List<tblGrade> GradeList = Connection.tblGrades.ToList();
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

            List<tblSchool> schoolList = Connection.tblSchools.ToList();
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

            List<tblSchool> schoolList = Connection.tblSchools.ToList();
            ViewBag.SchoolfrhdrpList = new SelectList(schoolList, "SchoolId", "SchoolName");
            //ViewBag.SchoolfrhdrpList.SelectedValue= "121127";
            //List<tblGrade> GradeList = Connection.tblGrades.ToList();
            //ViewBag.grdschooldrpList = new SelectList(GradeList, "GradeId", "GradeName");

        }

        //
        // POST: /School/Create

        [HttpPost]
        public ActionResult Create(SchoolModel Model)
        {
            string _path="";
            string _pathL = "";
              string _path1="";
            string _pathL2 = "";
       

           // return View("SchoolCreate");

            Model.SchoolId = Schoold;

            try
            {
                if (ModelState.IsValid)
                {
                    if (Model.File.ContentLength > 0)
                    {


                        string fnm = DateTime.Now.ToString("");
                        string nwString22 = fnm.Replace("-", ".");
                        string nwString = nwString22.Replace("/", ".");
                        string nwString2 = nwString.Replace(" ", ".");
                        string time = nwString2.Replace(":", ".");

                        string _FileName = time+"_"+Path.GetFileName(Model.File.FileName);
                        _path = Path.Combine(Server.MapPath("~/Uploads/Schools/Images"), _FileName);
                        _path1 = "~/Uploads/Schools/Images/" + _FileName;
                        Model.File.SaveAs(_path);
                    }

                    if (Model.LogoFile.ContentLength > 0)
                    {
                        string fnm = DateTime.Now.ToString("");
                         string nwString22 = fnm.Replace("-", ".");
                         string nwString = nwString22.Replace("/", ".");
                        string nwString2 = nwString.Replace(" ", ".");
                        string time = nwString2.Replace(":", ".");
                   
                        string _FileName =time+"_" +Path.GetFileName(Model.LogoFile.FileName);
                        _pathL = Path.Combine(Server.MapPath("~/Uploads/Schools/Logo"), _FileName);
                        _pathL2 = "~/Uploads/Schools/Logo/" + _FileName;
                        Model.LogoFile.SaveAs(_pathL);
                    }

                    //  ViewBag.Message = "File Uploaded Successfully!!";  

                    var schoolcount = Connection.SMGTSchoolCount().FirstOrDefault();
                    string SchoolId = "Scl" + Model.Division.ToString() + Model.District.ToString() + Model.Province.ToString() + Model.SchoolGroup.ToString() + schoolcount.ToString();


                    Connection.DCISsetSchool(SchoolId, Model.SchoolGroup, Model.SchoolName, Model.SchoolRank, "Y", Model.Division,
                        Model.District, Model.Description, UserId, Model.Address1, Model.Address2, Model.Address3, Model.Email, Model.Fax, _path1, Convert.ToInt16(Model.MinuteforPeriod), Model.Telephone, Model.SchoolCategory, Model.Province, Model.WebAddress, _pathL2
                        );
                    Connection.SaveChanges();


                    string result = "Success";
                    ModelState.Clear();
                    SchoolModel schl = new SchoolModel();
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
                ViewBag.ActivitydrpList = new SelectList(excatlist, "ActivityCode", "ActivityName");
                SchoolGradeDrpList();
                SchoolHouseDrpList();
              return View("SchoolCreate");


               
             

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


            List<tblSubjectCategory> sclSubcatlist = Connection.tblSubjectCategories.ToList();
            ViewBag.SubcatscldrpList = new SelectList(sclSubcatlist, "SubjectCategoryId", "SubjectCategoryName");
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

            TModel.SchoolCategory = TCtable.SchoolCategory;
            TModel.SchoolGroup = TCtable.SchoolGroup;
            TModel.SchoolRank =  TCtable.SchoolRank;
            TModel.District = TCtable.District;
            TModel.Division = TCtable.Division;
            TModel.Province = TCtable.Province;


            return View("Edit", TModel);




          
        }

        //
        // POST: /School/Edit/5

        [HttpPost]
        public ActionResult Edit(SchoolModel  Model)
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



                    

           SchoolModel TModel = new SchoolModel();

            tblSchool TCtable = Connection.tblSchools.SingleOrDefault(x => x.SchoolId == Model.SchoolId);
            //  TModel.IsActive = TCtable.IsActive;

            TModel.SchoolId = TCtable.SchoolId;
            TModel.SchoolName = TCtable.SchoolName;
            TModel.Address1 = TCtable.Address1;
            TModel.Description = TCtable.Description;
            TModel.Address1 = TCtable.Address1 +" "+ TCtable.Address2 + " "+TCtable.Address3;
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

            if (Model.SchoolName == null) {
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

            if (_path1 == "") {

                _path1 = TModel.ImagePath;
            
            }
            if (_pathL2 == "")
            {

                _pathL2 = TModel.LogoPath;

            }
               






                Connection.DCISModifySchool(Model.SchoolId, Model.SchoolGroup, Model.SchoolName, Model.SchoolRank, "Y", Model.Division,
                   Model.District, Model.Description, UserId, Model.Address1, Model.Address2, Model.Address3,Model.Email , Model.Fax, _path1, Convert.ToInt16(Model.MinuteforPeriod), Model.Telephone, Model.SchoolCategory, Model.Province, Model.WebAddress, _pathL2
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


    }
}
