﻿using GDWEBSolution.Models;
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
            TModel.Address1 = TCtable.Address1;
            TModel.Address2 = TCtable.Address2;
            TModel.Address3 = TCtable.Address3;
            TModel.Email = TCtable.Email;
            TModel.WebAddress = TCtable.WebUrl;
            TModel.MinuteforPeriod = TCtable.MinuteforPeriod.ToString();
            TModel.Telephone = TCtable.Telephone;


            //List<tblSchoolCategory> SCategorylist = Connection.tblSchoolCategories.ToList();
            //ViewBag.SchoolCategoryDrpDown = new SelectList(SCategorylist, "SchoolCategoryId", "SchoolCategoryName");
            //ViewBag.SchoolCategoryDrpDown = TCtable.SchoolCategory;
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

            TModel.SchoolCategory = TCtable.SchoolCategory;
            TModel.SchoolGroup = TCtable.SchoolGroup;
            TModel.SchoolRank = TCtable.SchoolRank;
            TModel.District = TCtable.District;
            TModel.Division = TCtable.Division;
            TModel.Province = TCtable.Province;


            return View("Detail", TModel);



        }

        //
        // GET: /School/Create

        public ActionResult Create(SchoolHouseModel hsmodel)
        
       {

           ViewBag.Scliddef = Schoold;

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
            SchoolGradeDrpList();
            SchoolHouseDrpListe(Schoold);


          
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
                 Model.HouseId = count2.ToString();
                 var count = Connection.tblHouses.Count(u => u.SchoolId == Schoold && u.HouseName == Model.HouseName);
                 if (count == 0)
                 {
                     Model.SchoolId = Schoold;
                     tblHouse newscg = new tblHouse();

                     newscg.CreatedBy = "User1";
                     newscg.CreatedDate = DateTime.Now;
                     newscg.SchoolId = Schoold;
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

         public ActionResult ShowSchooHouse(string SchoolId)
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
             SchoolGradeDrpList();
             SchoolHouseDrpListe(Schoold);

             List<SchoolHouseModel> List = loadhouselist(SchoolId);
             
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


         public ActionResult DeleteGrade(string SchoolId, string GradeId)
         {
             SchoolGradeModel Model = new SchoolGradeModel();
             Model.GradeId = GradeId;
             Model.SchoolId = SchoolId;
             return PartialView("DeleteSchoolGrade", Model);
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
         public ActionResult DeleteSchoolHouses(SchoolHouseModel Model)
         {
             try
             {
                 Model.SchoolId="121127";

                 tblHouse Tble = Connection.tblHouses.Find( Model.SchoolId,Model.HouseId);
                 Connection.tblHouses.Remove(Tble);
                 Connection.SaveChanges();

               

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

          var schoolcount = Connection.SMGTSchoolCount().FirstOrDefault();
                    string SchoolId = "Scl" + Model.Division.ToString() + Model.District.ToString() + Model.Province.ToString() + Model.SchoolGroup.ToString() + schoolcount.ToString();

           // return View("SchoolCreate");


                    Model.SchoolId = SchoolId;
            try
            {
                if (ModelState.IsValid)
                {
                    if (Model.File.ContentLength > 0)
                    {


                        string _FileName = Path.GetFileName(Model.File.FileName);
                        _path = Path.Combine(Server.MapPath("~/Uploads"), _FileName);
                        Model.File.SaveAs(_path);
                    }

                    if (Model.LogoFile.ContentLength > 0)
                    {
                        string _FileName = Path.GetFileName(Model.LogoFile.FileName);
                        _pathL = Path.Combine(Server.MapPath("~/Uploads"), _FileName);
                        Model.LogoFile.SaveAs(_pathL);
                    }

                    //  ViewBag.Message = "File Uploaded Successfully!!";  

                   


                    Connection.DCISsetSchool(SchoolId, Model.SchoolGroup, Model.SchoolName, Model.SchoolRank, "Y", Model.Division,
                        Model.District, Model.Description, UserId, Model.Address1, Model.Address2, Model.Address3, Model.Email, Model.Fax, _path, Convert.ToInt16(Model.MinuteforPeriod), Model.Telephone, Model.SchoolCategory, Model.Province, Model.WebAddress, _pathL
                        );
                    Connection.SaveChanges();


                    string result = "Success";
                    ModelState.Clear();
                    SchoolModel schl = new SchoolModel();
                    //return View();
                

                }


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
            try
            {
                // TODO: Add update logic here
                if (Model.File.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(Model.File.FileName);
                    _path = Path.Combine(Server.MapPath("~/Uploads"), _FileName);
                    Model.File.SaveAs(_path);
                }

                if (Model.LogoFile.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(Model.LogoFile.FileName);
                    _pathL = Path.Combine(Server.MapPath("~/Uploads"), _FileName);
                    Model.LogoFile.SaveAs(_pathL);
                }
              

                Connection.DCISModifySchool(Model.SchoolId, Model.SchoolGroup, Model.SchoolName, Model.SchoolRank, "Y", Model.Division,
                   Model.District, Model.Description, UserId, Model.Address1, Model.Address2, Model.Address3, _path, Model.Fax, "", Convert.ToInt16(Model.MinuteforPeriod), Model.Telephone, Model.SchoolCategory, Model.Province, Model.WebAddress, _pathL
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
