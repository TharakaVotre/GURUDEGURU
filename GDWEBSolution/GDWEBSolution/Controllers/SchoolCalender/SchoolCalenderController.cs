using GDWEBSolution.Filters;
using GDWEBSolution.Models;
using GDWEBSolution.Models.SchoolCalender;
using GDWEBSolution.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GDWEBSolution.Controllers.SchoolCalender
{
    public class SchoolCalenderController : Controller
    {
        //
        // GET: /SchoolCalender/
        SchoolMGTEntitiesConnectionString Connection = new SchoolMGTEntitiesConnectionString();
        UserSession USession = new UserSession();
        // GET: /TeacherCategory/
        string SchooId = null;
        string UserId = null;
        string AcademicYear = null;

       

         //[UserFilter(Function_Id = "ScCal")]
        public ActionResult Index(string AcademicYear)
        {

            SchooId = USession.School_Id;
            try
            {
               
                 if (AcademicYear == null)
                {
                    tblAccadamicYear AccYear = Connection.tblAccadamicYears.SingleOrDefault(a => a.SchoolId == SchooId);
                    ViewBag.AcademicYear=AccYear.AccadamicYear;
                }
                else
                {

                    ViewBag.AcademicYear = AcademicYear;
                }
                
                var Group = Connection.GDgetSchoolCalenderEvent(SchooId, AcademicYear, "Y");
                List<GDgetSchoolCalenderEvent_Result> Grouplist = Group.ToList();

                SchoolCalenderModel tcm = new SchoolCalenderModel();

                List<SchoolCalenderModel> tcmlist = Grouplist.Select(x => new SchoolCalenderModel
                {
                    CalenderSeqNo=x.CalenderSeqNo,
                    SchoolId=x.SchoolId,
                    AcadamicYear=x.AcadamicYear,
                    DateComment = x.DateComment,
                    IsHoliday=x.IsHoliday,
                    SpecialComment=x.SpecialComment,
                    FromDate = x.FromDate,
                    ToDate = x.ToDate,
                    StartMonth=x.FromDate.Month.ToString(),
                    EndMonth = x.ToDate.Month.ToString(),
                    CreatedBy=x.CreatedBy,
                    CreatedDate = x.CreatedDate.ToShortDateString(),
                    IsActive=x.IsActive
                }).ToList();

                return View(tcmlist);
            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);

                return View();
            }   
           
        }


        public ActionResult getSchoolCalendar()
        {

            var Group = Connection.GDgetSchoolCalenderEvent("CKC", "2017", "Y");
            List<GDgetSchoolCalenderEvent_Result> Grouplist = Group.ToList();

            List<SchoolCalenderModel> List = Grouplist.Select(x => new SchoolCalenderModel
            {
                CalenderSeqNo=x.CalenderSeqNo,
                SchoolId=x.SchoolId,
                AcadamicYear=x.AcadamicYear,
                DateComment = x.DateComment,
                IsHoliday=x.IsHoliday,
                SpecialComment=x.SpecialComment,
                FromDate = x.FromDate,
                ToDate = x.ToDate,
                IsActive=x.IsActive

            }).ToList();
            return Json(List, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Calendar()
        {
            return View();
        }

        public ActionResult ShowAddView(string id)
        {
            SchoolCalenderModel TModel = new SchoolCalenderModel();
            TModel.AcadamicYear = id;
            ViewBag.AccYear = id;
            return PartialView("AddView", TModel);
        }

       //  [UserFilter(Function_Id = "ScCal")]
        [HttpPost]
        public ActionResult Create(SchoolCalenderModel Model)
        {
            string _resutl = "Error";
            try
            {
                if (Model.CalenderSeqNo == 0)
                {
                    Connection.GDsetSchoolCalenderActivity("CKC", 
                                                            Model.AcadamicYear, 
                                                            Model.DateComment, 
                                                            "N",
                                                            Model.SpecialComment, 
                                                            Model.FromDate,
                                                            Model.ToDate,
                                                            "Azi@",
                                                            "Y");
                }
                else
                {
                    Connection.GDModifySchoolCalenderActivity(Model.CalenderSeqNo,
                                                                Model.ToDate,
                                                                Model.FromDate, 
                                                                Model.SpecialComment,
                                                                Model.DateComment,
                                                                "N",
                                                                "Azi@");   
                }
                Connection.SaveChanges();
                _resutl = "Success";   
            }
            catch (Exception Ex)
            {
                Errorlog.ErrorManager.LogError("@SchoolCalender Create(SchoolCalenderModel Model)", Ex);
                _resutl = "Exception";
            }
            return Json(_resutl, JsonRequestBehavior.AllowGet);
        }

        private void drplist()
        {
            List<SelectListItem> Selectlist = new List<SelectListItem>();
            Selectlist.Add(new SelectListItem
            {
                Text = "Yes",
                Value = "Y"
            });
            Selectlist.Add(new SelectListItem
            {
                Text = "No",
                Value = "N",
            });

            ViewBag.IsHoliday = new SelectList(Selectlist, "Value", "Text");
        }
         [UserFilter(Function_Id = "ScCal")]
        public ActionResult Edit(long SeqNo)
        {
            
            
            try
            {
                drplist();
                SchoolCalenderModel TModel = new SchoolCalenderModel();

                tblSchoolCalendar TCtable = Connection.tblSchoolCalendars.SingleOrDefault(x => x.CalenderSeqNo == SeqNo);
                TModel.CalenderSeqNo = SeqNo;
                TModel.AcadamicYear = TCtable.AcadamicYear;
                TModel.FromDate = TCtable.FromDate;
                TModel.ToDate = TCtable.ToDate;
                TModel.SpecialComment = TCtable.SpecialComment;
                TModel.IsHoliday = TCtable.IsHoliday;
                TModel.DateComment = TCtable.DateComment;



                return PartialView("EditView", TModel);
            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return View();
            }
        }

         [UserFilter(Function_Id = "ScCal")]
        [HttpPost]
        public ActionResult Edit(SchoolCalenderModel Model, string IsHoliday1)
        {
            
            UserId = USession.User_Id;
            try
            {
                string holiday = IsHoliday1;
                if (IsHoliday1 == null)
                {
                    holiday ="N";
                }
                Connection.GDModifySchoolCalenderActivity(Model.CalenderSeqNo, Model.ToDate, Model.FromDate, Model.SpecialComment, Model.DateComment, holiday, UserId);
                Connection.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }




        // [UserFilter(Function_Id = "ScCal")]
        public ActionResult Delete(long SeqNo)
        {
           
            try
            {
                SchoolCalenderModel TModel = new SchoolCalenderModel();
                TModel.CalenderSeqNo = SeqNo;
                return PartialView("DeleteView", TModel);
            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return View();
            }
        }

        // [UserFilter(Function_Id = "ScCal")]
        [HttpPost]
        public ActionResult Delete(SchoolCalenderModel Model)
        {

            UserId = USession.User_Id;
            try
            {
                Connection.GDDeleteSchoolCalenderActivity(Model.CalenderSeqNo, "Azi@","N");
                Connection.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                Errorlog.ErrorManager.LogError(ex);
                return Json(false, JsonRequestBehavior.AllowGet);

            }
            
        }


        [UserFilter(Function_Id = "ScClP")]
        public ActionResult ParentView(string AcademicYear)
        {
            
            SchooId = USession.School_Id;
            try
            {
                if (AcademicYear == null)
                {
                    tblAccadamicYear AccYear = Connection.tblAccadamicYears.SingleOrDefault(a => a.SchoolId == SchooId);
                    ViewBag.AcademicYear=AccYear.AccadamicYear;
                }
                else
                {

                    ViewBag.AcademicYear = AcademicYear;
                }
               
                var Group = Connection.GDgetSchoolCalenderEvent(SchooId, AcademicYear, "Y");
                List<GDgetSchoolCalenderEvent_Result> Grouplist = Group.ToList();

                SchoolCalenderModel tcm = new SchoolCalenderModel();

                List<SchoolCalenderModel> tcmlist = Grouplist.Select(x => new SchoolCalenderModel
                {
                    CalenderSeqNo = x.CalenderSeqNo,
                    SchoolId = x.SchoolId,
                    AcadamicYear = x.AcadamicYear,
                    DateComment = x.DateComment,
                    IsHoliday = x.IsHoliday,
                    SpecialComment = x.SpecialComment,
                    FromDate = x.FromDate,
                    ToDate = x.ToDate,
                    StartMonth = x.FromDate.Month.ToString(),
                    EndMonth = x.ToDate.Month.ToString(),
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate.ToShortDateString(),
                    IsActive = x.IsActive
                }).ToList();

                return View(tcmlist);
            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);

                return View();
            }

        }
    }
}
