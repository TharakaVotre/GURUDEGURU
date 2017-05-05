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
            try
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
            catch (Exception Ex)
            {
                Errorlog.ErrorManager.LogError("Dropdowns() @PSMessageController", Ex);
            }
        }

        public ActionResult ShowInbox()
        {
            return PartialView("InboxView");
        }

        public ActionResult ShowNewMessage()
        {
            try
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
            }
            catch (Exception Ex)
            {
                Errorlog.ErrorManager.LogError("ShowNewMessage() @PSMessageController", Ex);
            }
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
                MsgHead.ParentId = 2;//session parent id
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

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Errorlog.ErrorManager.LogError("SendParenttoSchoolMsg(PtoSMessageHeaderModel Model) @PSMessageController", dbEx);
                return Json("Validation", JsonRequestBehavior.AllowGet); 
            }
            catch (Exception Ex)
            {
                Errorlog.ErrorManager.LogError("SendParenttoSchoolMsg(PtoSMessageHeaderModel Model) @PSMessageController", Ex);
                return Json("Exception", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult AttachmentUpload(PtoSMessageHeaderModel Model)
        {
            try
            {
                var file = Model.Attachment_File;
                long AttachmentId = 0;
                tblParentToSchollMessageAttachment Attachmentfile = new tblParentToSchollMessageAttachment();
                if (file != null)
                {
                    var Atid = Connection.tblParameters.Where(x => x.ParameterId == "PSMAS").Select(x => x.ParameterValue).SingleOrDefault();
                    AttachmentId = Convert.ToInt64(Atid);
                    long Next = AttachmentId + 1;
                    var fileName = Path.GetFileName(file.FileName);
                    var extention = Path.GetExtension(file.FileName);
                    Attachmentfile.AttachementName = file.FileName;
                    Attachmentfile.AttachementPath = "/UploadedFiles/" + file.FileName;
                    Attachmentfile.MessageId = Model.MessageId;
                    Attachmentfile.SeqNo = AttachmentId;

                    Connection.tblParentToSchollMessageAttachments.Add(Attachmentfile);
                    Connection.SaveChanges();

                    tblParameter TCtable = Connection.tblParameters.SingleOrDefault(x => x.ParameterId == "PSMAS");
                    TCtable.ParameterValue = Next.ToString();
                    Connection.SaveChanges();
                    file.SaveAs(Server.MapPath("/UploadedFiles/" + file.FileName));
                }
                var result = new { FileName = file.FileName, SeqNo = AttachmentId };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Errorlog.ErrorManager.LogError("AttachmentUpload(PtoSMessageHeaderModel Model) @PSMessageController", Ex);
                var result = new { FileName = "Error", SeqNo = "Error" };
                return Json(result, JsonRequestBehavior.AllowGet);
            }   
        }

        public ActionResult ShowSentMessages()
        {
            var STQlist = Connection.SMGTgetParentToSchoolSentMail(2).ToList(); //ParentId session
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

        [HttpPost]
        public ActionResult DeleteAttachment(PtoSMessageHeaderModel Model)
        {
            try
            {
                tblParentToSchollMessageAttachment Tble = Connection.tblParentToSchollMessageAttachments.Find(Model.SeqNo);
                string path = Tble.AttachementPath;
                Connection.tblParentToSchollMessageAttachments.Remove(Tble);
                Connection.SaveChanges();
                System.IO.File.Delete(Server.MapPath(path));
                return Json(Model.SeqNo, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ViewPSMessage(long MessageId)
        {
            PtoSMessageHeaderModel M = new PtoSMessageHeaderModel();
            try
            {
                var H = Connection.SMGTgetPtoSMessageView(MessageId).SingleOrDefault();
                M.MessageId = H.MessageId;
                M.Message = H.Message.Replace("<br />", "\r\n"); 
                M.MessageType = Convert.ToInt64(H.MessageType);
                M.MessageTypeDes = H.MessageTypeDescription;
                M.ParentId = H.ParentId;
                M.ParentName = H.ParentName;
                M.RecepientUser = H.RecepientUser;
                M.SchoolId = H.SchoolId;
                M.SeqNo = H.SeqNo;
                M.Status = H.Status;
                M.Subject = H.Subject;
                M.TeacherName = H.PersonName;
         
                List<tblParentToSchollMessageAttachment> AList = Connection.tblParentToSchollMessageAttachments.Where(x => x.MessageId == MessageId).ToList();
                M.AttachmentList = AList;
            }
            catch (Exception Ex)
            {
                Errorlog.ErrorManager.LogError("ActionResult ViewPSMessage(long MessageId) @ PSMessageController", Ex);
            }
            return PartialView("ViewMessage", M);
        }
        public ActionResult DownloadAttachment(long SeqNo)
        {
            var file = Connection.tblParentToSchollMessageAttachments.FirstOrDefault(x => x.SeqNo == SeqNo);
            byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath(file.AttachementPath));
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, file.AttachementName);
        }
    }
}
