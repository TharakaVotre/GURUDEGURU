using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace GDWEBSolution
{
    /// <summary>
    /// Summary description for Handler
    /// </summary>
    public class Handler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.Files.Count > 0)
            {
                HttpFileCollection selectedFiles = context.Request.Files;
                for (int i = 0; i < selectedFiles.Count; i++)
                {
                    System.Threading.Thread.Sleep(1000);
                    HttpPostedFile PostedFile = selectedFiles[i];
                    string FileName = context.Server.MapPath("~/UploadedFiles/" + Path.GetFileName(PostedFile.FileName));
                    PostedFile.SaveAs(FileName);
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}