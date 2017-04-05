using GDWEBSolution.Models;
using GDWEBSolution.Models.SchoolCalender;
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
        string SchooId = "CKC";
        string UserId = "ADMIN";
        string AcademicYear = null;
        public ActionResult Index(string AcademicYear)
        {
            try
            {
                if (AcademicYear == null & Session["AcademicYear"] == null)
                {
                    ViewBag.AcademicYear = DateTime.Now.Year.ToString();
                    AcademicYear = DateTime.Now.Year.ToString();
                }
                else if (Session["AcademicYear"] != null & AcademicYear == null)
                {
                    ViewBag.AcademicYear = Session["AcademicYear"].ToString();
                    AcademicYear = Session["AcademicYear"].ToString();
                }
                else
                {
                    ViewBag.AcademicYear = AcademicYear;
                }
                Session["AcademicYear"] = AcademicYear;
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
                //Errorlog.ErrorManager.LogError(ex);

                return View();
            }   
           
        }

        public ActionResult ShowAddView(int id)
        {
            return PartialView("AddView");
        }


        [HttpPost]
        public ActionResult Create(SchoolCalenderModel Model)
        {
            try
            {
                
                string holyday="N";
                if (Model.IsHoliday == "on")
                {
                     holyday="Y";
                }
                AcademicYear = Session["AcademicYear"].ToString();
                Connection.GDsetSchoolCalenderActivity(SchooId, AcademicYear, Model.DateComment, holyday, Model.SpecialComment, Model.FromDate, Model.ToDate, UserId, "Y");
                Connection.SaveChanges();

                //return View();

                return RedirectToAction("Index");
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

        public ActionResult Edit(long SeqNo)
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


        [HttpPost]
        public ActionResult Edit(SchoolCalenderModel Model, string IsHoliday1)
        {
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


        [HttpPost]
        public ActionResult Delete(SchoolCalenderModel Model)
        {
            try
            {
                Connection.GDDeleteSchoolCalenderActivity(Model.CalenderSeqNo, UserId,"N");
                Connection.SaveChanges();


                return Json(true, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                Errorlog.ErrorManager.LogError(ex);
                return View();

            }
        }



        public ActionResult ParentView(string AcademicYear)
        {
            try
            {
                if (AcademicYear == null & Session["AcademicYear"] == null)
                {
                    ViewBag.AcademicYear = DateTime.Now.Year.ToString();
                    AcademicYear = DateTime.Now.Year.ToString();
                }
                else if (Session["AcademicYear"] != null & AcademicYear == null)
                {
                    ViewBag.AcademicYear = Session["AcademicYear"].ToString();
                    AcademicYear = Session["AcademicYear"].ToString();
                }
                else
                {
                    ViewBag.AcademicYear = AcademicYear;
                }
                Session["AcademicYear"] = AcademicYear;
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
                //Errorlog.ErrorManager.LogError(ex);

                return View();
            }

        }
    }
}
