using GDWEBSolution.Models;
using GDWEBSolution.Models.Event;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GDWEBSolution.Controllers.Event
{
    public class EventController : Controller
    {
        SchoolMGTEntitiesConnectionString Connection = new SchoolMGTEntitiesConnectionString();
        //
        // GET: /Event/

        public ActionResult Index()
        {
            var STQlist = Connection.tblEventCalendars.Where(r => r.SchoolId == "CKC").ToList();

            List<Events> List = STQlist.Select(x => new Events
            {
                id = x.EventNo.ToString(),
                name = x.EventTitle,
                //des = x.EventDescription,
                category = x.EventCategory.ToString(),
                organizer = x.EventOrganizer,
                startDate = Newtonsoft.Json.JsonConvert.SerializeObject(DateTime.Now).ToString(),
                endDate = Newtonsoft.Json.JsonConvert.SerializeObject(DateTime.Now).ToString(),
                fromtime = x.ToTime.ToString(),
                totime = x.FromTime.ToString()

            }).ToList();

            //ViewData["SchoolEvents"] = List;
            return View(List);
        }

        public ActionResult getEvents()
        {
            var STQlist = Connection.tblEventCalendars.Where(r => r.SchoolId == "CKC").ToList();

            List<Events> List = STQlist.Select(x => new Events
            {
                id = x.EventNo.ToString(),
                name = x.EventTitle,
                //des = x.EventDescription,
                category = x.EventCategory.ToString(),
                organizer = x.EventOrganizer,

                syear = x.FromDate.ToString("yyyy"),
                smonth = x.FromDate.ToString("MM"),
                sday = x.FromDate.ToString("dd"),

                eyear = x.ToDate.ToString("yyyy"),
                emonth = x.ToDate.ToString("MM"),
                eday = x.ToDate.ToString("dd"),

                fromtime = DateTime.Parse(x.FromTime.ToString()).ToString("hh:mm tt"),
                totime = DateTime.Parse(x.ToTime.ToString()).ToString("hh:mm tt")

            }).ToList();

            //ViewData["SchoolEvents"] = List;
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
            return View();
        }

        //
        // POST: /Event/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
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
