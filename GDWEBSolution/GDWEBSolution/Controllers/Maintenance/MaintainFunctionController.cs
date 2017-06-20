using GDWEBSolution.Models;
using GDWEBSolution.Models.Maintenance;
using GDWEBSolution.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GDWEBSolution.Controllers
{
    public class MaintainFunctionController : Controller
    {
        //
        // GET: /MaintainFunction/

        private SchoolMGTEntitiesConnectionString Connection = new SchoolMGTEntitiesConnectionString();
        string UserId = null;
        UserSession USession = new UserSession();
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
            Authentication("MaFun");
            try{
            var Function = Connection.GDgetAllFunction("Y");
            List<GDgetAllFunction_Result> Functionlist = Function.ToList();

            FunctionModel tcm = new FunctionModel();

            List<FunctionModel> tcmlist = Functionlist.Select(x => new FunctionModel
            {
                FunctionId = x.FunctionId,
                FunctionName = x.FunctionName,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate,
                IsActive = x.IsActive,
                ModifiedBy = x.ModifiedBy,
                ModifiedDate = x.ModifiedDate

            }).ToList();



            return View(tcmlist);
            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return View();
                
            }
        }

        //
        // GET: /TeacherCategory/Details/5

        public ActionResult Details(string code)
        {
            return View();
        }

        //

        public ActionResult ShowAddView(string code)
        {
            return PartialView("AddView");
        }

        // GET: /TeacherCategory/Create

        public ActionResult Create()
        {
            Authentication("MaFun");
            return View();
        }

        //
        // POST: /Application Status/Create

        [HttpPost]
        public ActionResult Create(tblFunction Model)
        {
            Authentication("MaFun");
            UserId = USession.User_Id;
            try
            {

                Connection.GDsetFunction(Model.FunctionId, Model.FunctionName, UserId, "Y");
                Connection.SaveChanges();

                //return View();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return View();
            }
        }


        public ActionResult ShowEditEventCategory(string Code)
        {

            return PartialView("AddView");
        }
        //
        // GET: /TeacherCategory/Edit/5

        public ActionResult Edit(string Code)
        {
            Authentication("MaFun");
            try{
                string url = Request.Url.AbsoluteUri;
            FunctionModel TModel = new FunctionModel();

            tblFunction TCtable = Connection.tblFunctions.SingleOrDefault(x => x.FunctionId == Code);
            TModel.IsActive = TCtable.IsActive;

            TModel.FunctionId = TCtable.FunctionId;
            TModel.FunctionName = TCtable.FunctionName;

            return PartialView("EditView", TModel);
            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return View();
                
            }
        }

        //
        // POST: /TeacherCategory/Edit/5

        [HttpPost]
        public ActionResult Edit(FunctionModel Model)
        {
            Authentication("MaFun");
            UserId = USession.User_Id;
            try
            {

                tblFunction TCtable = Connection.tblFunctions.SingleOrDefault(x => x.FunctionId == Model.FunctionId);

                Connection.GDModifyFunction(Model.FunctionName, Model.FunctionId, UserId);
                Connection.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return View();
                
            }
        }


        public ActionResult ShowDeleteModel(string Code)
        {

            return PartialView("AddView");
        }
        //
        // GET: /TeacherCategory/Delete/5

        public ActionResult Delete(string Code)
        {
            Authentication("MaFun");
            try
            {
                FunctionModel TModel = new FunctionModel();
                TModel.FunctionId = Code;
                return PartialView("DeleteView", TModel);
            }
            catch (Exception ex)
            {
                Errorlog.ErrorManager.LogError(ex);
                return PartialView();
                
            }
        }

        //
        // POST: /TeacherCategory/Delete/5

        [HttpPost]
        public ActionResult Delete(FunctionModel Model)
        {
            Authentication("MaFun");
            UserId = USession.User_Id;
            try
            {
                Connection.GDdeleteFunction("N", Model.FunctionId, UserId);
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
    }
}
