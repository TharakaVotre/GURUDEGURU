using GDWEBSolution.Models;
using GDWEBSolution.Models.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GDWEBSolution.Controllers.Application
{
    public class ApplicationController : Controller
    {
        //
        // GET: /Application/
        private SchoolMGTEntitiesConnectionString Connection = new SchoolMGTEntitiesConnectionString();
        string UserId = "kamalasiri";
        string SchoolId = "CKC";
        public ActionResult Index(string FromDate, string ToDate, string StatusCode)
        {
            try
            {
                if (Session["StatusCode"] != null && ToDate == null)
                {
                    FromDate = Session["FromDate"].ToString();
                    ToDate = Session["ToDate"].ToString();
                    StatusCode = Session["StatusCode"].ToString();
                    ViewBag.FromDate = FromDate;
                    ViewBag.ToDate = ToDate;
                    ViewBag.StatusCode = StatusCode;
                }
                DropDownList();
                if (FromDate != null || ToDate != null || StatusCode != null)
                {
                if (StatusCode == "" || StatusCode==null)
                {
                    StatusCode = "0";
                }
                    long StatusCode1 = Convert.ToInt64(StatusCode);
                    if (ToDate == "" || ToDate == null)
                    {
                        ToDate = DateTime.Now.ToShortDateString();
                    }
                    if (FromDate == "" || FromDate == null)
                    {
                        FromDate = DateTime.Now.ToShortDateString();
                    }
                    Session["FromDate"] = FromDate;
                    Session["ToDate"] = ToDate;
                    Session["StatusCode"]=StatusCode;
                    DateTime StartDate = Convert.ToDateTime(FromDate);
                    DateTime EndDate = Convert.ToDateTime(ToDate);
                    string stDate = StartDate.ToString("yyyyMMdd");
                    string edDate = EndDate.ToString("yyyyMMdd");
                    var Grade = Connection.GDgetApplication(StatusCode1, SchoolId, stDate, edDate, "Y");
                    List<GDgetApplication_Result> Gradelist = Grade.ToList();

                    ApplicationModel tcm = new ApplicationModel();

                    List<ApplicationModel> tcmlist = Gradelist.Select(x => new ApplicationModel
                    {
                        AppStatusSeqNo = x.AppStatusSeqNo,
                        RefNo = x.RefNo,
                        StatusCode = x.StatusCode,
                        Remarks = x.Remarks,
                        CreatedDate = x.CreatedDate,
                        StatusDescription = x.StatusDescription


                    }).ToList();
                    return View(tcmlist);
                }else{
                    return View();
                }
            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return View();
            }
        }

        private void DropDownList()
        {
            var Class = Connection.GDgetAllApplicationStatus("Y", 0);
            List<GDgetAllApplicationStatus_Result> Classlist = Class.ToList();

            ViewBag.StatusCode = new SelectList(Classlist, "StatusCode", "StatusDescription");
        }


        public ActionResult Edit(long Code)
        {
            DropDownList();

            ApplicationModel TModel = new ApplicationModel();

            tblEntranceApplication TCtable = Connection.tblEntranceApplications.SingleOrDefault(x => x.AppStatusSeqNo == Code);
            TModel.AppStatusSeqNo = TCtable.AppStatusSeqNo;
            TModel.StatusCode = TCtable.StatusCode;
            TModel.Remarks = TCtable.Remarks;
            TModel.RefNo = TCtable.RefNo;
           
            return PartialView("EditView", TModel);
        }

        //
        // POST: /TeacherCategory/Edit/5

        [HttpPost]
        public ActionResult Edit(ApplicationModel Model)
        {
            try
            {
                

                            Connection.GDModifyApplicationStatus(Model.AppStatusSeqNo, Model.Remarks, Model.StatusCode, Model.RefNo,UserId);
                            Connection.SaveChanges();
                        
                   
                
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return View();
            }
        }

        public ActionResult Delete(long Id)
        {
            try
            {
                ApplicationModel TModel = new ApplicationModel();
                TModel.AppStatusSeqNo = Id;
                return PartialView("DeleteView", TModel);
            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return View();
            }
        }

        //
        // POST: /TeacherCategory/Delete/5

        [HttpPost]
        public ActionResult Delete(ApplicationModel Model)
        {
            try
            {
                Connection.GDdeleteApplication(Model.AppStatusSeqNo, "N");
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

        public ActionResult ParentView(string RefNo)
        {
            try
            {

                if (RefNo != null)
                {
                    var Grade = Connection.GDgetParentApplicationStatus(RefNo);
                    List<GDgetParentApplicationStatus_Result> Gradelist = Grade.ToList();

                    ApplicationModel tcm = new ApplicationModel();

                    List<ApplicationModel> tcmlist = Gradelist.Select(x => new ApplicationModel
                    {
                        AppStatusSeqNo = x.AppStatusSeqNo,
                        RefNo = x.RefNo,
                        StatusCode = x.StatusCode,
                        Remarks = x.Remarks,
                        CreatedDate = x.CreatedDate,
                        StatusDescription = x.StatusDescription


                    }).ToList();
                    return View(tcmlist);
                }
                return View();
            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return View();
            }
        }

    }
}
