using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using GDWEBSolution.Models;

namespace GDWEBSolution.Util
{
    public class MailManager
    {
        SchoolMGTEntitiesConnectionString Connection = new SchoolMGTEntitiesConnectionString();
        public int SendEmail(string To, string Subject, string Body)
        {
            try
            {
                
                string DECKey = System.Configuration.ConfigurationManager.AppSettings["DecKey"];
                var Eresult = Connection.SMGT_GETPasswordChangeEMailConfig().ToList();

                List<tblParameter> Eb = Eresult.Select(x => new tblParameter
                {
                    ParameterId = x.ParameterId,
                    ParameterValue = x.ParameterValue

                }).ToList();
                     
                string _EMAIL = Eb[0].ParameterValue;

                string DecryptID = DECKey.Substring(10);
                //string _PASS = Encrypt_Decrypt.Decrypt(Eb[1].ParameterValue, DecryptID);
                string _PASS = Eb[1].ParameterValue;

                string _SMTP = Eb[2].ParameterValue;
                int _SMTP_PORT = Convert.ToInt32(Eb[3].ParameterValue);

                if (_EMAIL.Equals("") || _PASS.Equals("") || _SMTP.Equals("") || _SMTP_PORT.Equals(0))
                {
                    return 2;
                }
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(_SMTP);//"smtp.live.com"
                mail.From = new System.Net.Mail.MailAddress(_EMAIL);
                mail.To.Add(To);
                mail.Subject = Subject;
                mail.IsBodyHtml = true;
                mail.Body = Body;

                //System.Net.Mail.Attachment attachment;
                //attachment = new System.Net.Mail.Attachment(Attachment_Path);
                //mail.Attachments.Add(attachment);

                SmtpServer.Port = _SMTP_PORT;
                SmtpServer.Credentials = new System.Net.NetworkCredential(_EMAIL, _PASS);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                return 1;
            }
            catch (Exception Ex)
            {

                Errorlog.ErrorManager.LogError("Mail Send Faild (MailManager->SendEmail())", Ex);
                return 0;
            }

        }

        public static void LogMail(string To,string Code, string Msg)
        {
            string Message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            Message += Environment.NewLine;
            Message += "-----------------------------------------------------------";
            Message += Environment.NewLine;
            Message += string.Format("TO : {0}", To);
            Message += Environment.NewLine;
            Message += string.Format("Code : {0}", Code);
            Message += Environment.NewLine;
            Message += string.Format("Message : {0}", Msg);
            Message += "-----------------------------------------------------------";
            Message += Environment.NewLine;
            string settedPath = System.Configuration.ConfigurationManager.AppSettings["Mail_Log"] +"/Password_Change_Log/"+ DateTime.Now +"/";
            if (!Directory.Exists(settedPath))
            {
                Directory.CreateDirectory(settedPath);
            }
            string DirectoryPath = settedPath + DateTime.Now.ToString("yyyy_MM_dd_HH") + ".txt";
            using (StreamWriter writer = new StreamWriter(DirectoryPath, true))
            {
                writer.WriteLine(Message);
                writer.Close();
            }
        }
    }
}