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
            if (USession.User_Id == "")
            {
                return View("Login");
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Error()
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

                Session["User_Name"] = loggedU.UserId;

                result = "Succes";
            }
            else
            {
                result = "Failed";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            return View();
        }

        [HttpPost]
        public JsonResult Forgot(LoginModel login)
        {
            string result = "Success";
            var loggedU = Connection.tblUsers.Where(u => u.LoginEmail == login.LoginEmail).FirstOrDefault();
            if (loggedU != null)
            {
                tblUserCode Codex = new tblUserCode();

                Codex.UserId = loggedU.UserId;
                Codex.IsActive = "Y";
                Codex.Code = Convert.ToInt64(GeneratenewRandom());
                Connection.tblUserCodes.Add(Codex);
                Connection.SaveChanges();

                MailManager SendM = new MailManager();
                SendM.SendEmail(loggedU.LoginEmail, "Password Recovery", "Your Password Recovery Code : " + Codex.Code + " Use It in the Link : http://124.43.22.85/Home/Verify");
            }
            else
            {
                result = "Error";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private string GeneratenewRandom()
        {
            Random generator = new Random();
            String r = generator.Next(0, 1000000).ToString("D6");
            if (r.Distinct().Count() == 1)
            {
               r = GeneratenewRandom();
            }
            return r;
        }

        public ActionResult Verify()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Verify(LoginModel login)
        {
            string result = "Error";
            long Code = Convert.ToInt64(login.Code);
            tblUserCode Codex = Connection.tblUserCodes.Where(u => u.Code == Code &&
                                                              u.IsActive == "Y" ).FirstOrDefault();
            if (Codex != null)
            {
                Codex.IsActive = "U";
                Connection.SaveChanges();
                Session["VerifiedUserID"] = Codex.UserId;
                result = "Success";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ChangePassword()
        {
            ViewBag.VerifiedUserId = Session["VerifiedUserID"].ToString();
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(LoginModel login)
        {
            string result = "Error";
            string Uid = Session["VerifiedUserID"].ToString();
            tblUser Usre = Connection.tblUsers.Where(u => u.UserId == Uid ).FirstOrDefault();
            string pass = Encrypt_Decrypt.Encrypt(login.Password, Password);
            if (Usre != null)
            {
                Usre.Password = pass;
                Connection.SaveChanges();
                result = "Success";
                Session.Remove("VerifiedUserID");
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PasswordChange()
        {
            ViewBag.userId = USession.User_Id;
            return View();
        }
        [HttpPost]
        public ActionResult PasswordChange(LoginModel login)
        {
            string result = "Error";
            tblUser Usre = Connection.tblUsers.Where(u => u.UserId == USession.User_Id).FirstOrDefault();
            string oldpass = Encrypt_Decrypt.Encrypt(login.Password, Password);
            string newpass = Encrypt_Decrypt.Encrypt(login.NewPassword,Password);
            if (Usre != null)
            {
                if (Usre.Password == oldpass)
                {
                    Usre.Password = newpass;
                    Connection.SaveChanges();
                    result = "Success";
                }
                else
                {
                    result = "Wrong";
                }
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
            else if (USession.User_Category == "Teach")
            {
                naviga = "~/App_Data/teachernavigation.xml";
            }
            else if (USession.User_Category == "PARNT")
            {
                naviga = "~/App_Data/parentnavigation.xml";
            }
            else if (USession.User_Category == "SADMI")
            {
                naviga = "~/App_Data/teachernavigation.xml";
            }
            else { naviga = "~/App_Data/adminnavigation.xml"; }
          
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
            return PartialView("_Menu", _menu);
        }
    }
}
