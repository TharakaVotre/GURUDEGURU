using GDWEBSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GDWEBSolution.Controllers.Message
{
    public class PSMessageController : Controller
    {
        SchoolMGTEntitiesConnectionString Connection = new SchoolMGTEntitiesConnectionString();
        //
        // GET: /PSMessage/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowInbox()
        {
            return PartialView("InboxView");
        }

        //
        // GET: /PSMessage/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /PSMessage/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /PSMessage/Create

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
        // GET: /PSMessage/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /PSMessage/Edit/5

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
        // GET: /PSMessage/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /PSMessage/Delete/5

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
