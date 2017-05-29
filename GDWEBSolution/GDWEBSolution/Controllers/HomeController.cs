using GDWEBSolution.Models.Menu;
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
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        [ChildActionOnly]
        public ActionResult Menu()
        {
            var _menu = new Menu();

            var xmlData = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/navigation.xml");
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
