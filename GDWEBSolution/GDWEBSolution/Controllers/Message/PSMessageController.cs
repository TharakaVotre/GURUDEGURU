using GDWEBSolution.Models;
using GDWEBSolution.Models.Message;
using GDWEBSolution.Models.Teacher;
using System;
using System.Collections.Generic;
using System.IO;
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
            Dropdowns();

            return View();
        }

        public ActionResult Sent()
        {
            Dropdowns();

            return View();
        }

        private void Dropdowns()
        {
            List<tblTeacherCategory> TCategorylist = Connection.tblTeacherCategories.ToList();
            ViewBag.TeacherCategoryDrpDown = new SelectList(TCategorylist, "TeacherCategoryId", "TeacherCategoryName");

            var TeacherList = Connection.SMGTgetAllTeachers("CKC", "%", "Y").ToList(); // Order by teacher category
            List<TeacherModel> tcmlist = TeacherList.Select(x => new TeacherModel
            {
                TeacherCategoryId = x.TeacherCategoryId,
                UserId = x.UserId,
                Name = x.Name,
                TeacherId = x.TeacherId

            }).ToList();
            ViewBag.TeacherDropdown = new SelectList(tcmlist, "UserId", "Name");
        }

        public ActionResult ShowInbox()
        {
            return PartialView("InboxView");
        }

        public ActionResult ShowNewMessage()
        {
            var TeacherList = Connection.SMGTgetAllTeachers("CKC", "%", "Y").ToList(); // Order by teacher category
            List<TeacherModel> tcmlist = TeacherList.Select(x => new TeacherModel
            {
                TeacherCategoryId = x.TeacherCategoryId,
                UserId = x.UserId,
                Name = x.Name,
                TeacherId = x.TeacherId

            }).ToList();
            ViewBag.TeacherDropdown = new SelectList(tcmlist, "UserId", "Name");

            List<tblMessageType> MsgTypeList = Connection.tblMessageTypes.ToList();
            ViewBag.MessageTypesDropdown = new SelectList(MsgTypeList, "MessageTypeId", "MessageTypeDescription");

            var MsgId = Connection.tblParameters.Where(x => x.ParameterId == "PSMHS").Select(x => x.ParameterValue).SingleOrDefault();
            long a = Convert.ToInt64(MsgId) + 1;

            tblParameter TCtable = Connection.tblParameters.SingleOrDefault(x => x.ParameterId == "PSMHS");
            TCtable.ParameterValue = a.ToString();
            Connection.SaveChanges();

            ViewBag.Message_Id = a.ToString();

            return PartialView("NewMessage");
        }

        [HttpPost]
        public JsonResult SendParenttoSchoolMsg(PtoSMessageHeaderModel Model)
        {
            string result = "Success";
            try
            {
                tblParentToSchoolMessageHeader MsgHead = new tblParentToSchoolMessageHeader();

                MsgHead.SchoolId = "CKC";
                MsgHead.MessageId = Model.MessageId;
                MsgHead.ParentId = 1;
                MsgHead.Message = Model.Message.Replace("\r\n", "<br />");
                MsgHead.CreatedBy = "ADMIN";
                MsgHead.CreatedDate = DateTime.Now;
                MsgHead.MessageType = Model.MessageType;
                MsgHead.IsActive = "Y";
                MsgHead.Status = "N";
                MsgHead.Subject = Model.Subject;
                MsgHead.Attachments = 0;

                tblParentToSchoolMessageDetail MsgDetail = new tblParentToSchoolMessageDetail();
                MsgDetail.SchoolId = "CKC";
                MsgDetail.MessageId = Convert.ToInt64(Model.MessageId); ;
                MsgDetail.RecepientUser = Model.RecepientUser;
                MsgDetail.IsActive = "Y";
                MsgDetail.Status = "N";
                MsgDetail.AuthorizationDate = DateTime.Now;
                MsgDetail.AuthorizedBy = "ADMIN";
                MsgDetail.AuthStatus = "A";
                MsgDetail.CreatedBy = "ADMIN";
                MsgDetail.CreatedDate = DateTime.Now;



                Connection.tblParentToSchoolMessageHeaders.Add(MsgHead);
                Connection.SaveChanges();
                Connection.tblParentToSchoolMessageDetails.Add(MsgDetail);
                Connection.SaveChanges();

                //return View();

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
            //catch (Exception Ex)
            //{
            //    Errorlog.ErrorManager.LogError("public JsonResult SendParenttoSchoolMsg(PtoSMessageHeaderModel Model) @ PSMessageController", Ex);
            //    return Json("Error", JsonRequestBehavior.AllowGet);
            //}
        }

        [HttpPost]
        public JsonResult AttachmentUpload(PtoSMessageHeaderModel Model)
        {
            var file = Model.Attachment_File;
            long AttachmentId = 0;
            tblParentToSchollMessageAttachment Attachmentfile = new tblParentToSchollMessageAttachment();

            if (file != null)
            {
                var Atid = Connection.tblParameters.Where(x => x.ParameterId == "PSMAS").Select(x => x.ParameterValue).SingleOrDefault();
                AttachmentId = Convert.ToInt64(Atid) + 1;

                var fileName = Path.GetFileName(file.FileName);
                var extention = Path.GetExtension(file.FileName);


                Attachmentfile.AttachementName = file.FileName;
                Attachmentfile.AttachementPath = "/UploadedFiles" + file.FileName;
                Attachmentfile.MessageId = Model.MessageId;
                Attachmentfile.SeqNo = AttachmentId;

                Connection.tblParentToSchollMessageAttachments.Add(Attachmentfile);
                Connection.SaveChanges();

                file.SaveAs(Server.MapPath("/UploadedFiles/" + file.FileName));

            }
            var result = new { FileName = file.FileName, SeqNo = AttachmentId };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowSentMessages()
        {
            var STQlist = Connection.SMGTgetParentToSchoolSentMail(1).ToList(); //ParentId

            List<PtoSMessageHeaderModel> List = STQlist.Select(x => new PtoSMessageHeaderModel
            {
                SchoolId = x.SchoolId,
                MessageId = x.MessageId,
                MessageTypeDes = x.MessageTypeDescription,
                //ParentId = x.ParentId, 
                Message= x.Message.Replace("<br />", " "),  
                Status = x.Status,   
                IsActive = x.IsActive,
                SeqNo = x.SeqNo, 
                RecepientUser = x.RecepientUser,
                TeacherName = x.Name,
                Subject = x.Subject,
                CreatedDate = x.CreatedDate,

            }).ToList();
            return PartialView("SentView",List);
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
