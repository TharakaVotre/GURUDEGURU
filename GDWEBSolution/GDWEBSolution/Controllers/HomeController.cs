using GDWEBSolution.Models;
using GDWEBSolution.Models.Home;
using GDWEBSolution.Models.Menu;
using GDWEBSolution.Models.User;
using GDWEBSolution.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;

namespace GDWEBSolution.Controllers
{
    public class HomeController : Controller
    {
        SchoolMGTEntitiesConnectionString Connection = new SchoolMGTEntitiesConnectionString();

        UserSession USession = new UserSession();

        static string DECKey = System.Configuration.ConfigurationManager.AppSettings["DecKey"];
        string Password = DECKey.Substring(10);

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(LoginModel login)
        {
            string result = "Error";
            string pass = Encrypt_Decrypt.Encrypt(login.Password, Password);
            var loggedU = Connection.SMGT_UserLogin(login.UserName, pass).FirstOrDefault();

            if (loggedU != null)
            {
                USession.Email_ = loggedU.LoginEmail;
                USession.Is_Active = loggedU.IsActive;
                USession.Job_ = loggedU.JobDescription;
                USession.Mobile_ = loggedU.Mobile;
                USession.Person_Name = loggedU.PersonName;
                USession.School_Id = loggedU.SchoolId;
                USession.User_Category = loggedU.UserCategory;
                USession.User_Id = loggedU.UserId;

                result = "Succes";
            }
            else
            {
                result = "Failed";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [ChildActionOnly]
        public ActionResult Menu()
        {
            var _menu = new Menu();

            string naviga = "";

            if (USession.User_Category == "Admin")
            {
                naviga = "~/App_Data/adminnavigation.xml";
            }
            else
            {
                naviga = "~/App_Data/navigation.xml";
            }
           
            var xmlData = System.Web.HttpContext.Current.Server.MapPath(naviga);
            if (xmlData == null)
            {
                throw new ArgumentNullException("xmlData");
            }

            var xmldoc = new XmlDataDocument();

            var fs = new FileStream(xmlData, FileMode.Open, FileAccess.Read);
            xmldoc.Load(fs);

            var xmlnode = xmldoc.GetElementsByTagName("Navigation");

            for (var i = 0; i <= xmlnode.Count - 1; i++)
            {
                var xmlAttributeCollection = xmlnode[i].Attributes;

                if (xmlAttributeCollection != null)
                {
                    //var nodeMenu = new MenuItem() { MenuItemName = xmlAttributeCollection["title"].Value; MenuItemPath = xmlAttributeCollection["title"].Value; };
                    var nodeMenu = new MenuItem()
                    {
                        Name = xmlAttributeCollection["title"].Value,
                        Action = xmlAttributeCollection["action"].Value,
                        Controller = xmlAttributeCollection["controller"].Value,
                        Link = xmlAttributeCollection["url"].Value,
                    };

                    if (xmlnode[i].ChildNodes.Count != 0)
                    {
                        for (var j = 0; j < xmlnode[i].ChildNodes.Count; j++)
                        {

                           var xmlNode = xmlnode[i].ChildNodes.Item(j);
                            if (xmlNode != null)
                            {
                                if (xmlNode.Attributes != null)
                                {
                                    nodeMenu.ChildMenuItems.Add(new MenuItem()
                                    {
                                        Name = xmlNode.Attributes["title"].Value,
                                        Action = xmlNode.Attributes["action"].Value,
                                        Controller = xmlNode.Attributes["controller"].Value,
                                        Link = xmlNode.Attributes["url"].Value,
                                    });
                                   
                                }
                            }
                        }
                    }

                    _menu.Items.Add(nodeMenu);
                }
            }

            //var _google = new MenuItem()
            //{
            //    MenuItemName = "Google",
            //    MenuItemPath = "http://google.com/",
            //};

            //_google.ChildMenuItems.Add(new MenuItem()
            //{
            //    MenuItemName = "Google Images",
            //    MenuItemPath = "http://google.com/images/"
            //});

            //var _bing = new MenuItem()
            //{
            //    MenuItemName = "Bing",
            //    MenuItemPath = "http://bing.com/"
            //};

            //_bing.ChildMenuItems.Add(new MenuItem()
            //{
            //    MenuItemName = "Bing Images",
            //    MenuItemPath = "http://bing.com/images/"
            //});

            //_menu.Items.Add(_google);
            //_menu.Items.Add(_bing);

            return PartialView("_Menu", _menu);
        }


    }
}
