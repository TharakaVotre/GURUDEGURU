using GDWEBSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GDWEBSolution.Models.Message;
using System.IO;
using System.Transactions;
using GDWEBSolution.Models.User;
using GDWEBSolution.Filters;
/* ===============================
 * AUTHOR     : G.M. Tharaka Madusanka
 * CREATE DATE     : May 5 2017
*/
namespace GDWEBSolution.Controllers.Message
{
    public class SPMessageController : Controller
    {
        //
        // GET: /SPMessage/
        SchoolMGTEntitiesConnectionString Connection = new SchoolMGTEntitiesConnectionString();
        UserSession _session = new UserSession();

        [UserFilter(Function_Id = "SPMSi")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        [UserFilter(Function_Id = "SPNMG")]
        public ActionResult NewMessage()
        {
            var SchoolGrade = Connection.SMGTgetSchoolGrade(_session.School_Id).ToList();//Need to Pass a Session Schoolid
            List<tblGrade> SchoolGradeList = SchoolGrade.Select(x => new tblGrade
            {
                GradeId = x.GradeId,
                GradeName = x.GradeName,
                IsActive = x.IsActive

            }).ToList();
            ViewBag.SchoolGrades = new SelectList(SchoolGradeList, "GradeId", "GradeName");
            List<SMGT_getSchoolExactivity_Result> ex = Connection.SMGT_getSchoolExactivity(_session.School_Id).ToList();//Need to Pass a Session Schoolid

            ViewBag.SchoolExactivity = new SelectList(ex, "ActivityCode", "ActivityName");

            List<tblMessageType> MsgTypeList = Connection.tblMessageTypes.ToList();
            ViewBag.MessageTypesDropdown = new SelectList(MsgTypeList, "MessageTypeId", "MessageTypeDescription");

            var MsgId = Connection.tblParameters.Where(x => x.ParameterId == "PSMHS").Select(x => x.ParameterValue).SingleOrDefault();
            long a = Convert.ToInt64(MsgId) + 1;

            tblParameter TCtable = Connection.tblParameters.SingleOrDefault(x => x.ParameterId == "PSMHS");
            TCtable.ParameterValue = a.ToString();
            Connection.SaveChanges();

            ViewBag.Message_Id = a.ToString();

            return View();
        }

        [HttpPost]
        public ActionResult NewMessage(StoPMessageHeaderModel Model)
        {
            string result = "Success";
            using (SchoolMGTEntitiesConnectionString Connection = new SchoolMGTEntitiesConnectionString())
            {
                using (var scope = new TransactionScope())
                {
                    try
                    {
                        tblSchoolToParentMessageHeader MsgHead = new tblSchoolToParentMessageHeader();
                        MsgHead.SchoolId = _session.School_Id;
                        MsgHead.MessageId = Model.MessageId;
                        MsgHead.Message = Model.Message.Replace("\r\n", "<br />");
                        MsgHead.CreatedBy = _session.User_Id;
                        MsgHead.CreatedDate = DateTime.Now;
                        MsgHead.MessageType = Model.MessageType;
                        MsgHead.IsActive = "Y";
                        MsgHead.Sender = Model.Sender;
                        MsgHead.Subject = Model.Subject;
                        Connection.SaveChanges();
                        //MsgHead.Attachments = 0;
                        Connection.tblSchoolToParentMessageHeaders.Add(MsgHead);
                        if (Model.ParentId != -1)
                        {
                            for (int i = 0; i < Model.ParentIdArray.Length; i++)
                            {
                                tblSchoolToParentMessageDetail MsgDetail = new tblSchoolToParentMessageDetail();
                                MsgDetail.SchoolId = _session.School_Id;
                                MsgDetail.MessageId = Convert.ToInt64(Model.MessageId); ;
                                MsgDetail.ParentId = Model.ParentIdArray[i];
                                MsgDetail.IsActive = "Y";
                                MsgDetail.Status = "N";
                                MsgDetail.CreatedBy = _session.User_Id;
                                MsgDetail.CreatedDate = DateTime.Now;
                                Connection.tblSchoolToParentMessageDetails.Add(MsgDetail);
                                Connection.SaveChanges();
                            }
                        }
                        else
                        {
                            tblSchoolToParentMessageDetail MsgDetail = new tblSchoolToParentMessageDetail();
                            MsgDetail.SchoolId = _session.School_Id;
                            MsgDetail.MessageId = Convert.ToInt64(Model.MessageId); ;
                            MsgDetail.ParentId = -1;
                            MsgDetail.IsActive = "Y";
                            MsgDetail.Status = "N";
                            MsgDetail.CreatedBy = _session.User_Id;
                            MsgDetail.CreatedDate = DateTime.Now;
                            Connection.tblSchoolToParentMessageDetails.Add(MsgDetail);
                            Connection.SaveChanges();
                        }
                        result = "Success";
                        scope.Complete();
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                    {
                        Errorlog.ErrorManager.LogError("SendParenttoSchoolMsg(PtoSMessageHeaderModel Model) @PSMessageController", dbEx);
                        result = "Validation";
                    }
                    catch (Exception Ex)
                    {
                        Errorlog.ErrorManager.LogError("SendParenttoSchoolMsg(PtoSMessageHeaderModel Model) @PSMessageController", Ex);
                        result = "Exception";
                    }
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowGradeClass(string GradeId)
        {
            List<tblClass> ClassesList = Connection.tblClasses.Where(r => r.SchoolId == _session.School_Id && r.GradeId == GradeId).ToList();
            ViewBag.GradeClassse = new SelectList(ClassesList, "ClassId", "ClassName");

            return PartialView("loadClass");
        }

        public ActionResult ShowParentByClass(string GradeId,string ClassId)
        {
            List<SMGT_getSchoolGreadClassParent_Result> gcp = Connection.SMGT_getSchoolGreadClassParent(_session.School_Id, GradeId, ClassId).ToList();
            ViewBag.GradeClassseParent = new MultiSelectList(gcp, "ParentId", "ParentName");

            return PartialView("loadParent");
        }

        public ActionResult ShowParentByExActivity(string ExActivityId)
        {
            List<SMGT_getSchoolExactivityParent_Result> exl = Connection.SMGT_getSchoolExactivityParent(_session.School_Id, ExActivityId).ToList();
            ViewBag.ExactParents = new MultiSelectList(exl, "ParentId", "ParentName");

            return PartialView("loadExParent");
        }

        public ActionResult ShowInboxMessages()
        {
            var STQlist = Connection.SMGT_getTeacherInbox(_session.User_Id).ToList();
            List<PtoSMessageHeaderModel> List = STQlist.Select(x => new PtoSMessageHeaderModel
            {
                SchoolId = x.SchoolId,
                MessageId = x.MessageId,
                MessageTypeDes = x.MessageTypeDescription,
                ParentId = x.ParentId, 
                ParentName = x.ParentName,
                Message = x.Message.Replace("<br />", " "),
                Status = x.Status,
                IsActive = x.IsActive,
                SeqNo = x.SeqNo,
                RecepientUser = x.RecepientUser,
                TeacherName = x.Name,
                Subject = x.Subject,
                CreatedDate = x.CreatedDate,

            }).ToList();
            return PartialView("InboxView", List);
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
            return PartialView("ViewInboxMessage", M);
        }

        [UserFilter(Function_Id = "SPMSs")]
        public ActionResult Sent()
        {
            return View();
        }

        public ActionResult ShowSentMessages()
        {
            var STQlist = Connection.SMGT_getSchooltoParentSentMail(_session.User_Id).ToList(); //ParentId session
            List<StoPMessageHeaderModel> List = STQlist.Select(x => new StoPMessageHeaderModel
            {
                SchoolId = x.SchoolId,
                MessageId = x.MessageId,
                MessageTypeDes = x.MessageTypeDescription,
                ParentId = x.ParentId,
                ParentName = x.ParentName,
                Message = x.Message.Replace("<br />", " "),
                IsActive = x.IsActive,
                SeqNo = x.SeqNo,
                Subject = x.Subject,
                CreatedDate = x.CreatedDate,

            }).ToList();
            return PartialView("SentView", List);
        }

        public ActionResult ViewSPMessage(long MessageId,long ParentId)
        {
            StoPMessageHeaderModel M = new StoPMessageHeaderModel();
            try
            {
                var H = Connection.SMGTgetStoPMessageView(MessageId,ParentId).SingleOrDefault();
                M.MessageId = H.MessageId;
                M.Message = H.Message.Replace("<br />", "\r\n");
                M.MessageType = Convert.ToInt64(H.MessageType);
                M.MessageTypeDes = H.MessageTypeDescription;
                M.ParentId = H.ParentId;
                M.ParentName = H.ParentName;
                M.Sender = H.Sender;
                M.SchoolId = H.SchoolId;
                M.SeqNo = H.SeqNo;
                M.Subject = H.Subject;

                List<tblSchoolToParentMessageAttachment> AList = Connection.tblSchoolToParentMessageAttachments.Where(x => x.MessageId == MessageId).ToList();
                M.AttachmentList = AList;
            }
            catch (Exception Ex)
            {
                Errorlog.ErrorManager.LogError("ActionResult ViewPSMessage(long MessageId) @ PSMessageController", Ex);
            }
            return PartialView("ViewMessage", M);
        }

        [HttpPost]
        public JsonResult AttachmentUpload(PtoSMessageHeaderModel Model)
        {
            try
            {
                var file = Model.Attachment_File;
                long AttachmentId = 0;
                tblSchoolToParentMessageAttachment Attachmentfile = new tblSchoolToParentMessageAttachment();
                if (file != null)
                {
                    var Atid = Connection.tblParameters.Where(x => x.ParameterId == "SPMAS").Select(x => x.ParameterValue).SingleOrDefault();
                    AttachmentId = Convert.ToInt64(Atid);
                    long Next = AttachmentId + 1;
                    var fileName = Path.GetFileName(file.FileName);
                    var extention = Path.GetExtension(file.FileName);
                    Attachmentfile.AttachmentName = file.FileName;
                    Attachmentfile.AttachmentPath = "/Attachments/" + file.FileName;
                    Attachmentfile.MessageId = Model.MessageId;
                    Attachmentfile.SeqNo = AttachmentId;

                    Connection.tblSchoolToParentMessageAttachments.Add(Attachmentfile);
                    Connection.SaveChanges();

                    tblParameter TCtable = Connection.tblParameters.SingleOrDefault(x => x.ParameterId == "SPMAS");
                    TCtable.ParameterValue = Next.ToString();
                    Connection.SaveChanges();
                    file.SaveAs(Server.MapPath("/Attachments/" + file.FileName));
                }
                var result = new { FileName = file.FileName, SeqNo = AttachmentId };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Errorlog.ErrorManager.LogError("AttachmentUpload(StoPMessageHeaderModel Model) @SPMessageController", Ex);
                var result = new { FileName = "Error", SeqNo = "Error" };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult DeleteAttachment(PtoSMessageHeaderModel Model)
        {
            try
            {
                tblSchoolToParentMessageAttachment Tble = Connection.tblSchoolToParentMessageAttachments.Find(Model.SeqNo);
                string path = Tble.AttachmentPath;
                Connection.tblSchoolToParentMessageAttachments.Remove(Tble);
                Connection.SaveChanges();
                System.IO.File.Delete(Server.MapPath(path));
                return Json(Model.SeqNo, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DownloadAttachment(long SeqNo)
        {
            var file = Connection.tblSchoolToParentMessageAttachments.FirstOrDefault(x => x.SeqNo == SeqNo);
            byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath(file.AttachmentPath));
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, file.AttachmentName);
        }

        public ActionResult DownloadAttachmentInbox(long SeqNo)
        {
            var file = Connection.tblParentToSchollMessageAttachments.FirstOrDefault(x => x.SeqNo == SeqNo);
            byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath(file.AttachementPath));
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, file.AttachementName);
        }

    }
}
