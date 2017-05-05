using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GDWEBSolution.Controllers.Message
{
    public class SPMessageController : Controller
    {
        //
        // GET: /SPMessage/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /SPMessage/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /SPMessage/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /SPMessage/Create

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
        // GET: /SPMessage/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /SPMessage/Edit/5

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
        // GET: /SPMessage/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /SPMessage/Delete/5

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
