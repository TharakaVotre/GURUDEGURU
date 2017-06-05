using GDWEBSolution.Models;
using GDWEBSolution.Models.Event;
using GDWEBSolution.Models.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
/* ===============================
 * AUTHOR     : G.M. Tharaka Madusanka
 * CREATE DATE     : April 26 2017
*/
namespace GDWEBSolution.Controllers.Event
{
    public class EventController : Controller
    {
        SchoolMGTEntitiesConnectionString Connection = new SchoolMGTEntitiesConnectionString();

        UserSession _session = new UserSession();

        //
        // GET: /Event/

        public ActionResult Index()
        {
            List<tblEventcategory> EventList = Connection.tblEventcategories.ToList();
            ViewBag.EventList = new SelectList(EventList, "EventCategoryId", "EventCategoryDesc");
            return View();
        }

        public ActionResult getEvents()
        {
            var STQlist = Connection.tblEventCalendars.Where(r => r.SchoolId == _session.School_Id).ToList();

            List<Events> List = STQlist.Select(x => new Events
            {
                id = x.EventNo.ToString(),
                name = x.EventTitle,
                desc = x.EventDescription,
                category = x.EventCategory.ToString(),
                organizer = x.EventOrganizer,
                startDate = x.FromDate.ToString("yyyy/MM/dd"),
                endDate = x.ToDate.ToString("yyyy/MM/dd"),
                syear = x.FromDate.ToString("yyyy"),
                smonth = x.FromDate.ToString("MM"),
                sday = x.FromDate.ToString("dd"),
                eyear = x.ToDate.ToString("yyyy"),
                emonth = x.ToDate.ToString("MM"),
                eday = x.ToDate.ToString("dd"),

                fromtime = DateTime.Parse(x.FromTime.ToString()).ToString("hh:mm tt"),
                totime = DateTime.Parse(x.ToTime.ToString()).ToString("hh:mm tt")

            }).ToList();
            return Json(List, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Event/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Event/Create

        public ActionResult Create()
        {

            return Json("", JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Event/Create

        [HttpPost]
        public ActionResult Create(EventModel Model)
        {
            try
            {
                

                if (Model.EventNo == 0)
                {
                    tblEventCalendar Events = new tblEventCalendar();
                    Events.CreatedBy = _session.User_Id;
                    Events.CreatedDate = DateTime.Now;
                    Events.SchoolId = _session.School_Id;
                    Events.EventTitle = Model.EventName;
                    Events.EventCategory = Model.EventCategoryId;
                    Events.EventDescription = Model.EventDescription;
                    Events.EventOrganizer = Model.EventOrganizer;
                    Events.FromDate = Convert.ToDateTime(Model.SFromDate);
                    Events.ToDate = Convert.ToDateTime(Model.SToDate);

                    DateTime F = DateTime.Parse(Model.SFromTime);
                    DateTime T = DateTime.Parse(Model.SToTime);
                    Events.FromTime = TimeSpan.Parse(F.ToString("HH:mm"));
                    Events.ToTime = TimeSpan.Parse(T.ToString("HH:mm"));
                    Events.IsActive = "Y";

                    Connection.tblEventCalendars.Add(Events);
                    Connection.SaveChanges();
                }
                else
                {
                    tblEventCalendar Events = Connection.tblEventCalendars.SingleOrDefault(x => x.EventNo == Model.EventNo);

                    Events.CreatedBy = _session.User_Id;
                    Events.CreatedDate = DateTime.Now;
                    Events.SchoolId = _session.School_Id;
                    Events.EventTitle = Model.EventName;
                    Events.EventCategory = Model.EventCategoryId;
                    Events.EventDescription = Model.EventDescription;
                    Events.EventOrganizer = Model.EventOrganizer;
                    Events.FromDate = Convert.ToDateTime(Model.SFromDate);
                    Events.ToDate = Convert.ToDateTime(Model.SToDate);

                    DateTime F = DateTime.Parse(Model.SFromTime);
                    DateTime T = DateTime.Parse(Model.SToTime);
                    Events.FromTime = TimeSpan.Parse(F.ToString("HH:mm"));
                    Events.ToTime = TimeSpan.Parse(T.ToString("HH:mm"));
                    Events.IsActive = "Y";

                    Connection.SaveChanges();
                }
                //return View();
                return Json("Succsess", JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                var result = new { r = "E" };
                Errorlog.ErrorManager.LogError("@EventController/Create", Ex);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        //
        // GET: /Event/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Event/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Event/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Event/Delete/5

        [HttpPost]
        public ActionResult Delete(EventModel Model)
        {
            try
            {
                tblEventCalendar DEvents = Connection.tblEventCalendars.Find(Model.EventNo);
                Connection.tblEventCalendars.Remove(DEvents);
                Connection.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("Index");
            }
            catch(Exception Ex)
            {
                Errorlog.ErrorManager.LogError("@EventController/Delete", Ex);
                return View();
            }
        }
    }
}
