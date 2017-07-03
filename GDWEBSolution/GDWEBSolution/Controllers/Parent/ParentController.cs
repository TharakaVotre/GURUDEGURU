using GDWEBSolution.Models;
using GDWEBSolution.Models.Parent;
using GDWEBSolution.Models.Schools;
using GDWEBSolution.Models.Student;
using GDWEBSolution.Models.User;
using GDWEBSolution.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace GDWEBSolution.Controllers.Parent
{
    public class ParentController : Controller
    {
        string Sessparent="2";
        string SessionSchool = "Scl13137";
        static string DECKey = System.Configuration.ConfigurationManager.AppSettings["DecKey"];
        string Password = DECKey.Substring(10);
       

        UserSession USession = new UserSession();
      

        SchoolMGTEntitiesConnectionString Connection = new SchoolMGTEntitiesConnectionString();

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

        //
        // GET: /Parent/
        public ActionResult Index()

        {
            Authentication("PCF");
            if (USession.User_Category == "SADMI") {

                var parent = Connection.SMGTGetParent("%", USession.School_Id);

                List<SMGTGetParent_Result> Parentlist = parent.Where(X => X.ParentId != -1).ToList();
                StudentModel schl = new StudentModel();

                List<ParentModel> Parenlist = Parentlist.Select(x => new ParentModel
                {
                    ParentId = x.ParentId.ToString(),






                    // ParentStudent=

                    ParentName = x.ParentName,
                    PersonalEmail = x.PersonalEmail,
                    UserId = x.UserId,
                    RelationshipId = x.RelationshipId,
                    RelationshipName = x.RelashionshipName,
                    HomeTelephone = x.HomeTelephone,

                    IsActive = x.IsActive,
                    ModifiedBy = x.ModifiedBy,
                    ModifiedDate = x.ModifiedDate

                }).ToList();
                return View(Parenlist);
            
            
            
            
            }


            else
            {

                var parent = Connection.SMGTGetParent("%", "%");

                List<SMGTGetParent_Result> Parentlist = parent.Where(X => X.ParentId != -1).ToList();
                StudentModel schl = new StudentModel();

                List<ParentModel> Parenlist = Parentlist.Select(x => new ParentModel
                {
                    ParentId = x.ParentId.ToString(),






                    // ParentStudent=

                    ParentName = x.ParentName,
                    PersonalEmail = x.PersonalEmail,
                    UserId = x.UserId,
                    RelationshipId = x.RelationshipId,
                    RelationshipName = x.RelashionshipName,
                    HomeTelephone = x.HomeTelephone,

                    IsActive = x.IsActive,
                    ModifiedBy = x.ModifiedBy,
                    ModifiedDate = x.ModifiedDate

                }).ToList();
                return View(Parenlist);
            }
        }
        //
        // GET: /Parent/Details/5
        public ActionResult Details(long ParentId)
        {

            Authentication("PCF");


            ParentModel TModel = new ParentModel();

            tblParent TCtable = Connection.tblParents.SingleOrDefault(x => x.ParentId == ParentId);

            tblRelashionship TCtable2 = Connection.tblRelashionships.SingleOrDefault(x => x.RelashionshipId == TCtable.RelationshipId);

           // var varstudents  = Connection.tblParentStudents.SingleOrDefault(x => x.ParentId == TCtable.ParentId);

            TModel.RelationshipId = TCtable.RelationshipId;
            TModel.RelationshipName = TCtable2.RelashionshipName;
            //  TModel.IsActive = TCtable.IsActive;
            TModel.ParentId = TCtable.ParentId.ToString();
            TModel.Address1 = TCtable.Address1 ;
            TModel.Address2 =  TCtable.Address2 ;
            TModel.Address3 =  TCtable.Address3;
            TModel.DateofBirth = TCtable.DateofBirth;
            TModel.OfficeAddress1 = TCtable.OfficeAddress1;
            TModel.OfficeAddress2 = TCtable.OfficeAddress2;
            TModel.OfficeAddress3 = TCtable.OfficeAddress3;
            TModel.HomeTelephone = TCtable.HomeTelephone;
            TModel.PersonalEmail = TCtable.PersonalEmail;
            TModel.PersonalMobile = TCtable.PersonalMobile;
            TModel.Occupation = TCtable.Occupation;
            TModel.OfficePhone = TCtable.OfficePhone;
            TModel.officeEmail = TCtable.officeEmail;
            TModel.NIC = TCtable.NIC;

            if (TCtable.ImgUrl == null)
            {
                TModel.ImagePath = "~/Uploads/Parent/Photo/no_image.jpg";

            }
            else {
                TModel.ImagePath = TCtable.ImgUrl;
            
            }
            
            if (TCtable.DateofBirth == null)
            {
               

            }
            else
            {

                string a = TCtable.DateofBirth.ToString();
                DateTime b = DateTime.Parse(a);
                TModel.datetimestring = b.ToShortDateString();
            }

            //TModel.DateofBirth = TCtable.DateofBirth;
           // TModel.DateofBirth = DateTime.ParseExact(TCtable.DateofBirth.GetValueOrDefault().ToShortDateString(), "yyyy/MM/DD");
         //   DateTime A= DateTime.ParseExact(TCtable.DateofBirth.GetValueOrDefault().ToShortDateString()
          
            string parent = TCtable.ParentId.ToString();




            var STQlist = Connection.SMGTgetParentStudentadd(parent,"%").ToList();

            List<ParentModel> List2 = STQlist.Select(x => new ParentModel
            {
                StudentId = x.StudentId,
                ParentName = x.ParentName,
                ParentId = x.ParentId.ToString(),
                StudentName = x.studentName




            }).ToList();
            string s="";


           for(int i=1;i<List2.Count;i++){
               s = s + "," + List2[i].StudentName;
               

           }
           if (List2.Count > 1)
           {

               TModel.Children = "" + List2[0].StudentName + s;

           }
          
           else
           {

               if (List2.Count == 1)
               {

                   TModel.Children = "" + List2[0].StudentName;
               }
               else
               {
                   TModel.Children = "";
               }

           }
              
            TModel.ParentName = TCtable.ParentName;


            return View(TModel);
        }
        //
        // GET: /Parent/Create
        public ActionResult Create()
        {

            Authentication("PCF");

          //  ViewBag.SchoolIDc = SessionSchool;

            ViewBag.SchoolIDc = USession.School_Id;
            List<tblParent> prntRlist = Connection.tblParents.OrderBy(X=>X.ParentName).Where(X => X.IsActive == "Y" && X.SchoolId==USession.School_Id).ToList();
            ViewBag.ParentlList = new SelectList(prntRlist, "ParentId", "ParentName");

            List<tblRelashionship> SRlist = Connection.tblRelashionships.Where(X=>X.IsActive=="Y").ToList();
            ViewBag.RelationshipList = new SelectList(SRlist, "RelashionshipId", "RelashionshipName");
            List<tblSchool> Schoollist = Connection.tblSchools.OrderBy(X => X.SchoolName).Where(X => X.IsActive == "Y").ToList();
            ViewBag.SchoolDrpDown = new SelectList(Schoollist, "SchoolId", "SchoolName");
            List<tblGrade> gradelist = Connection.tblGrades.Where(X => X.IsActive == "Y").ToList();
            ViewBag.SchoolGradeList = new SelectList(gradelist, "GradeId", "GradeName");
            List<tblClass> Classlist = Connection.tblClasses.Where(X => X.IsActive == "Y").ToList();
            ViewBag.classDrpDown = new SelectList(Classlist, "ClassId", "ClassName");
            List<tblStudent> Studentlist = Connection.tblStudents.Where(X => X.IsActive == "Y" && X.SchoolId == USession.School_Id).ToList();
            ViewBag.StudentIdList = new SelectList(Studentlist, "StudentId", "StudentName");
            String parentid = "2";
            var STQlist = Connection.SMGTgetParentStudentadd(parentid,"%").ToList();
            List<ParentModel> List = STQlist.Select(x => new ParentModel
            {
                StudentId = x.StudentId,
                ParentName = x.ParentName,
                ParentId = x.ParentId.ToString(),
                StudentName = x.studentName
            }).ToList();
            return View();
        }
        //
        // POST: /Parent/Create
        [HttpPost]
        public ActionResult Create(ParentModel Model)
        {

            Authentication("PCF");

            using (var scope = new TransactionScope())
            {

                try
                {

                    String result = "error";


                    string _path = "";
                    string _pathL = "";
                    Model.IsActive = "Y";
                    Model.CreatedBy = "User1";
                 //   Model.UserId = "User1";
                    if (ModelState.IsValid)
                    {

                        if (Model.File == null)
                        {

                        }
                        else
                        {
                            if (Model.File.ContentLength > 0)
                            {






                                string fnm = DateTime.Now.ToString("");
                                string nwString22 = fnm.Replace("-", ".");
                                string nwString = nwString22.Replace("/", ".");
                                string nwString2 = nwString.Replace(" ", ".");
                                string time = nwString2.Replace(":", ".");

                                string _FileName = time + "_" + Path.GetFileName(Model.File.FileName);
                                _pathL = Path.Combine(Server.MapPath("~/Uploads/Parent/Photo"), _FileName);
                                _path = "~/Uploads/Parent/Photo/" + _FileName;
                                Model.File.SaveAs(_pathL);



                            }
                        }




                        Connection.SMGTsetParent(Model.ParentName, Model.RelationshipId, Model.Address1, Model.Address2, Model.Address3, Model.HomeTelephone, Model.PersonalEmail, Model.PersonalMobile, Model.Occupation, Model.OfficeAddress1, Model.OfficeAddress2, Model.OfficeAddress3, Model.OfficePhone, Model.officeEmail, Model.NIC, Model.UserId, _path, Model.DateofBirth, Model.CreatedBy, Model.IsActive, USession.School_Id);
                        Connection.SaveChanges();
                        var Count = Connection.tblUsers.Count(X => X.UserId == Model.UserId);
                        if (Count == 0)
                        {
                            tblUser usr = new tblUser();
                            usr.CreatedBy = USession.User_Id;
                            usr.CreatedDate = DateTime.Now;
                            usr.LoginEmail = Model.PersonalEmail;
                            usr.Mobile = Model.PersonalMobile;
                            usr.UserCategory = "PARNT";
                            usr.JobDescription = Model.Occupation;
                            usr.SchoolId = USession.School_Id;

                            string pass = Encrypt_Decrypt.Encrypt(Model.Password, Password);

                            usr.Password = pass;
                            usr.UserId = Model.UserId;
                            usr.PersonName = Model.ParentName;
                            usr.IsActive = "Y";

                            Connection.tblUsers.Add(usr);
                            Connection.SaveChanges();


                            List<tblParent> prntRlist = Connection.tblParents.Where(X => X.IsActive == "Y").ToList();
                            ViewBag.ParentlList = new SelectList(prntRlist, "ParentId", "ParentName");

                            List<tblRelashionship> SRlist = Connection.tblRelashionships.Where(X => X.IsActive == "Y").ToList();
                            ViewBag.RelationshipList = new SelectList(SRlist, "RelashionshipId", "RelashionshipName");
                            List<tblSchool> Schoollist = Connection.tblSchools.Where(X => X.IsActive == "Y").ToList();
                            ViewBag.SchoolDrpDown = new SelectList(Schoollist, "SchoolId", "SchoolName");
                            List<tblGrade> gradelist = Connection.tblGrades.Where(X => X.IsActive == "Y").ToList();
                            ViewBag.SchoolGradeList = new SelectList(gradelist, "GradeId", "GradeName");
                            List<tblClass> Classlist = Connection.tblClasses.Where(X => X.IsActive == "Y").ToList();
                            ViewBag.classDrpDown = new SelectList(Classlist, "ClassId", "ClassName");
                            List<tblStudent> Studentlist = Connection.tblStudents.Where(X => X.IsActive == "Y" &&X.SchoolId==USession.School_Id).ToList();
                            ViewBag.StudentIdList = new SelectList(Studentlist, "StudentId", "StudentName");
                            String parentid = "2";
                            var STQlist = Connection.SMGTgetParentStudentadd(parentid, "%").ToList();
                            List<ParentModel> List = STQlist.Select(x => new ParentModel
                            {
                                StudentId = x.StudentId,
                                ParentName = x.ParentName,
                                ParentId = x.ParentId.ToString(),
                                StudentName = x.studentName
                            }).ToList();

                            Model.UserId = "Parent1";




                            //  scope.Complete();


                            scope.Complete();
                            result = "Success";
                            ModelState.Clear();
                            return Json(result, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            result = "Exits";

                            List<tblParent> prntRlist = Connection.tblParents.Where(X => X.IsActive == "Y").ToList();
                            ViewBag.ParentlList = new SelectList(prntRlist, "ParentId", "ParentName");

                            List<tblRelashionship> SRlist = Connection.tblRelashionships.Where(X => X.IsActive == "Y").ToList();
                            ViewBag.RelationshipList = new SelectList(SRlist, "RelashionshipId", "RelashionshipName");
                            List<tblSchool> Schoollist = Connection.tblSchools.Where(X => X.IsActive == "Y").ToList();
                            ViewBag.SchoolDrpDown = new SelectList(Schoollist, "SchoolId", "SchoolName");
                            List<tblGrade> gradelist = Connection.tblGrades.Where(X => X.IsActive == "Y").ToList();
                            ViewBag.SchoolGradeList = new SelectList(gradelist, "GradeId", "GradeName");
                            List<tblClass> Classlist = Connection.tblClasses.Where(X => X.IsActive == "Y").ToList();
                            ViewBag.classDrpDown = new SelectList(Classlist, "ClassId", "ClassName");
                            List<tblStudent> Studentlist = Connection.tblStudents.Where(X => X.IsActive == "Y" && X.SchoolId == USession.School_Id).ToList();
                            ViewBag.StudentIdList = new SelectList(Studentlist, "StudentId", "StudentName");
                            String parentid = "2";
                            var STQlist = Connection.SMGTgetParentStudentadd(parentid, "%").ToList();
                            List<ParentModel> List = STQlist.Select(x => new ParentModel
                            {
                                StudentId = x.StudentId,
                                ParentName = x.ParentName,
                                ParentId = x.ParentId.ToString(),
                                StudentName = x.studentName
                            }).ToList();

                            Model.UserId = "Parent1";




                            scope.Dispose();

                            return Json(result, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {


                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                }
                catch
                {
                  String  result = "Exception";
                    List<tblParent> prntRlist = Connection.tblParents.Where(X => X.IsActive == "Y").ToList();
                    ViewBag.ParentlList = new SelectList(prntRlist, "ParentId", "ParentName");

                    List<tblRelashionship> SRlist = Connection.tblRelashionships.Where(X => X.IsActive == "Y").ToList();
                    ViewBag.RelationshipList = new SelectList(SRlist, "RelashionshipId", "RelashionshipName");
                    List<tblSchool> Schoollist = Connection.tblSchools.Where(X => X.IsActive == "Y").ToList();
                    ViewBag.SchoolDrpDown = new SelectList(Schoollist, "SchoolId", "SchoolName");
                    List<tblGrade> gradelist = Connection.tblGrades.Where(X => X.IsActive == "Y").ToList();
                    ViewBag.SchoolGradeList = new SelectList(gradelist, "GradeId", "GradeName");
                    List<tblClass> Classlist = Connection.tblClasses.Where(X => X.IsActive == "Y").ToList();
                    ViewBag.classDrpDown = new SelectList(Classlist, "ClassId", "ClassName");
                    List<tblStudent> Studentlist = Connection.tblStudents.Where(X => X.IsActive == "Y" && X.SchoolId == USession.School_Id).ToList();
                    ViewBag.StudentIdList = new SelectList(Studentlist, "StudentId", "StudentName");
                    String parentid = "2";
                    var STQlist = Connection.SMGTgetParentStudentadd(parentid,"%").ToList();
                    List<ParentModel> List = STQlist.Select(x => new ParentModel
                    {
                        StudentId = x.StudentId,
                        ParentName = x.ParentName,
                        ParentId = x.ParentId.ToString(),
                        StudentName = x.studentName
                    }).ToList();

                    Model.UserId = "Parent1";


                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetSchoolGrade(string SchoolId)
        {
           
            //int id = 0;
            //bool isValid = Int32.TryParse(SchoolId, out id);
            //if (SchoolId == "") {

            //    SchoolId = "asdas123err";
            //}
            if (SchoolId == null)
            {


            }



            var SchoolGrade = Connection.SMGTgetSchoolGrade(USession.School_Id).OrderBy(X => X.GradeName).ToList();//Need to Pass a Session Schoolid





            //  var StudentSextra = Connection.SMGTloadScholExtraCadd(SchoolId, "%").ToList();

            var result2 = (from s in SchoolGrade
                           select new
                           {
                               GradeId = s.GradeId,
                               GradeName = s.GradeName


                           }).ToList();



            //   ViewBag.excdropdown = new SelectList(excList2, "ActivityCode", "ActivityName");

            //var states = _repository.GetAllStatesByCountryId(id);
            //var result = (from s in states
            //              select new
            //              {
            //                  id = s.Id,
            //                  name = s.Name
            //              }).ToList();
            return Json(result2, JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetSchoolParent()
        {

            //int id = 0;
            //bool isValid = Int32.TryParse(SchoolId, out id);
            //if (SchoolId == "") {

            //    SchoolId = "asdas123err";
            //}
           



          //  var SchoolGrade = Connection.SMGTgetSchoolGrade(USession.School_Id).OrderBy(X => X.GradeName).ToList();//Need to Pass a Session Schoolid

            var Parent = Connection.tblParents.OrderBy(X => X.ParentName).Where(X=>X.IsActive=="Y" && X.SchoolId==USession.School_Id).ToList();



            //  var StudentSextra = Connection.SMGTloadScholExtraCadd(SchoolId, "%").ToList();

            var result = (from s in Parent
                           select new
                           {
                               ParentId = s.ParentId,
                               ParentName = s.ParentName


                           }).ToList();



            //   ViewBag.excdropdown = new SelectList(excList2, "ActivityCode", "ActivityName");

            //var states = _repository.GetAllStatesByCountryId(id);
            //var result = (from s in states
            //              select new
            //              {
            //                  id = s.Id,
            //                  name = s.Name
            //              }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }




        public ActionResult AddChildAc(ParentModel Model)
        {


            List<tblRelashionship> SRlist = Connection.tblRelashionships.Where(X => X.IsActive == "Y").ToList();
            ViewBag.RelationshipList = new SelectList(SRlist, "RelashionshipId", "RelashionshipName");
            List<tblSchool> Schoollist = Connection.tblSchools.Where(X => X.IsActive == "Y").ToList();
            ViewBag.SchoolDrpDown = new SelectList(Schoollist, "SchoolId", "SchoolName");
            List<tblGrade> gradelist = Connection.tblGrades.Where(X => X.IsActive == "Y").ToList();
            ViewBag.SchoolGradeList = new SelectList(gradelist, "GradeId", "GradeName");
            List<tblClass> Classlist = Connection.tblClasses.Where(X => X.IsActive == "Y").ToList();
            ViewBag.classDrpDown = new SelectList(Classlist, "ClassId", "ClassName");
            List<tblStudent> Studentlist = Connection.tblStudents.Where(X => X.IsActive == "Y" && X.SchoolId == USession.School_Id).ToList();
            ViewBag.StudentIdList = new SelectList(Studentlist, "StudentId", "StudentName");


            var STQlist = Connection.SMGTgetParentStudentadd(Model.ParentId,"%").ToList();

            List<ParentModel> List = STQlist.Select(x => new ParentModel
            {
                StudentId = x.StudentId,
                ParentName = x.ParentName,
                ParentId = x.ParentId.ToString(),
                StudentName = x.studentName




            }).ToList();



            return View();

           
        
        
        
        }
        // GET: /Parent/Edit/5
        [HttpPost]
        public ActionResult AddChild(ParentModel Model)
        {
            string result = "Error";

            Model.IsActive = "Y";
            Model.CreatedBy = "User1";
            Model.UserId = "Parent1";
          

         //   var  item = Connection.tblParents.FirstOrDefault(i => i.UserId ==Model.UserId);
            if (Model.ParentId != null && Model.StudentId1 != null)
            {



                //  long  parentId = item.ParentId;
                long parentId = 2;
                //     Model.ParentId = parentId.ToString();
                long longP = long.Parse(Model.ParentId);
                var count = Connection.tblParentStudents.Count(u => u.ParentId == longP && u.StudentId == Model.StudentId1);

                if (count == 0)
                {
                    tblParentStudent tps = new tblParentStudent();
                    tps.CreatedBy = "User1";
                    tps.CreatedDate = DateTime.Today;
                    tps.IsActive = "Y";
                    //tps.ParentId = parentId;
                    tps.ParentId = long.Parse(Model.ParentId);
                    tps.SchoolId = Model.SchoolId;
                    tps.StudentId = Model.StudentId1;
                    Connection.tblParentStudents.Add(tps);
                    Connection.SaveChanges();


                    result = Model.ParentId + "!" + Model.SchoolId;
                }
                else
                {
                    result = "Exits";
                }
            }
            else {

                result = "fill";
            
            }
            //ShowTeacherQualificatoin();

            return Json(result, JsonRequestBehavior.AllowGet);


           
        
        }
        public ActionResult ShowchildAdd(string ParentId)
        {
            if (ParentId == null) { 
            
            
            }
           // ParentId = "2";
            
           // var STQlist = Connection.SMGTgetParentStudentadd("2").ToList();
            var STQlist = Connection.SMGTgetParentStudentadd(ParentId,USession.School_Id).ToList();

            List<ParentModel> List = STQlist.Select(x => new ParentModel
            {
                StudentId=x.StudentId,
                ParentName=x.ParentName,
                ParentId=x.ParentId.ToString(),
                StudentName=x.studentName

               


            }).ToList();
            return PartialView("ChildrenList", List);
        }


        public ActionResult ShowchildAddn(string ParentId,string SchoolId)
        {
            if (ParentId == null)
            {


            }
            // ParentId = "2";

            // var STQlist = Connection.SMGTgetParentStudentadd("2").ToList();
            var STQlist = Connection.SMGTgetParentStudentadd(ParentId, SchoolId).ToList();

            List<ParentModel> List = STQlist.Select(x => new ParentModel
            {
                StudentId = x.StudentId,
                ParentName = x.ParentName,
                ParentId = x.ParentId.ToString(),
                StudentName = x.studentName




            }).ToList();
            return PartialView("ChildrenList", List);
        }



        public ActionResult ShowchildAddt(string ParentId)
        {
            ParentId = "2";

            // var STQlist = Connection.SMGTgetParentStudentadd("2").ToList();
            var STQlist = Connection.SMGTgetParentStudentadd(ParentId,"%").ToList();

            List<ParentModel> List = STQlist.Select(x => new ParentModel
            {
                StudentId = x.StudentId,
                ParentName = x.ParentName,
                ParentId = x.ParentId.ToString(),
                StudentName = x.studentName




            }).ToList();
            return PartialView("ChildtestList", List);
        }
        public ActionResult EditParent(string ParentId)
        {
            ViewBag.EditParentID = ParentId;



            return View();
        }





        public ActionResult ShowEditParent(string ParentId)
        {

            long pid = long.Parse(ParentId);
              List<tblRelashionship> SRlist = Connection.tblRelashionships.Where(X=>X.IsActive=="Y").ToList();
            ViewBag.RelationshipList = new SelectList(SRlist, "RelashionshipId", "RelashionshipName");

           
         

            ParentModel TModel = new ParentModel();

            tblParent TCtable = Connection.tblParents.SingleOrDefault(x => x.ParentId == pid);
            //  TModel.IsActive = TCtable.IsActive;
            TModel.ParentId = TCtable.ParentId.ToString();
            TModel.Address1 = TCtable.Address1;
            TModel.Address2 = TCtable.Address2;
            TModel.Address3 = TCtable.Address3;
            TModel.DateofBirth = TCtable.DateofBirth;
            TModel.OfficeAddress1 = TCtable.OfficeAddress1;
            TModel.OfficeAddress2 = TCtable.OfficeAddress2;
            TModel.OfficeAddress3 = TCtable.OfficeAddress3;
            TModel.HomeTelephone = TCtable.HomeTelephone;
            TModel.PersonalEmail = TCtable.PersonalEmail;
            TModel.PersonalMobile = TCtable.PersonalMobile;
            TModel.Occupation = TCtable.Occupation;
            TModel.OfficePhone = TCtable.OfficePhone;
            TModel.officeEmail = TCtable.officeEmail;
            TModel.NIC = TCtable.NIC;
            TModel.DateofBirth = TCtable.DateofBirth;
            TModel.RelationshipId = TCtable.RelationshipId;

            TModel.ParentName = TCtable.ParentName;



            return PartialView("EditParentDetailsView", TModel);
        }


        [HttpPost]
        public JsonResult EditParentDetails(ParentModel TCtable)
        {
            try
            {
                string result = "Error";
                long a = long.Parse(TCtable.ParentId);
                tblParent TModel = Connection.tblParents.SingleOrDefault(x => x.ParentId == a);

                TModel.ParentId = a;
                TModel.Address1 = TCtable.Address1;
                TModel.Address2 = TCtable.Address2;
                TModel.Address3 = TCtable.Address3;
                TModel.DateofBirth = TCtable.DateofBirth;
                TModel.OfficeAddress1 = TCtable.OfficeAddress1;
                TModel.OfficeAddress2 = TCtable.OfficeAddress2;
                TModel.OfficeAddress3 = TCtable.OfficeAddress3;
                TModel.HomeTelephone = TCtable.HomeTelephone;
                TModel.PersonalEmail = TCtable.PersonalEmail;
                TModel.PersonalMobile = TCtable.PersonalMobile;
                TModel.Occupation = TCtable.Occupation;
                TModel.OfficePhone = TCtable.OfficePhone;
                TModel.officeEmail = TCtable.officeEmail;
                TModel.NIC = TCtable.NIC;
                TModel.DateofBirth = TCtable.DateofBirth;
                TModel.RelationshipId = TCtable.RelationshipId;

                TModel.ParentName = TCtable.ParentName;

               

                Connection.SaveChanges();

                result = TCtable.ParentId.ToString();
                //tblTeacherSchool Tschool = new tblTeacherSchool();

                return Json(result, JsonRequestBehavior.AllowGet);
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


        //
        // POST: /Parent/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {

            Authentication("PCF");







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
        // GET: /Parent/Delete/5
        public ActionResult Delete(string ParentId)
        {
            Authentication("PCF");

            ParentModel TModel = new ParentModel();
            TModel.ParentId = ParentId;
            TModel.IsActive = "N";




            return PartialView("DeleteParent" , TModel);
        }
        //
        // POST: /Parent/Delete/5
        [HttpPost]
        public ActionResult Delete(ParentModel Model)
        {
            try
            {
                Connection.SMGTDeleteParent("N", Model.ParentId, "Admin");
                

                // TODO: Add delete logic here

                return View("Index");
            }
            catch
            {
                return View();
            }
        }



        public ActionResult DeleteChild(string ParentId, string StudentId)
        {
            ParentModel Model = new ParentModel();
            Model.ParentId = ParentId;
            Model.StudentId = StudentId;

            return PartialView("DeleteChild", Model);
        }

        [HttpPost]
        public ActionResult DeleteChild(ParentModel Model)
        {
            try
            {

                long ParentId = long.Parse(Model.ParentId);
                var item = Connection.tblParentStudents.FirstOrDefault(i => i.ParentId == ParentId && i.StudentId==Model.StudentId);

                tblParentStudent Tble = Connection.tblParentStudents.Find( item.SeqNo);

                Connection.tblParentStudents.Remove(Tble);
                Connection.SaveChanges();


                return Json(Model.ParentId, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("Index");
            }
            catch
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }

    }
}
