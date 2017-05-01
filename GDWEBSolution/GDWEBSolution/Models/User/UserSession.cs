using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//ref //www.c-sharpcorner.com/uploadfile/9505ae/session-variables-as-objects/
namespace GDWEBSolution.Models.User
{
    [Serializable]
    public class UserSession
    {
        private const string LoggedUser = "LoggedUserInfo";

        string UserId;

        public string User_Id
        {
            get { return UserId; }
            set { UserId = value; Save(); }
        }
        string UserCategory;

        public string User_Category
        {
            get { return UserCategory; }
            set { UserCategory = value; Save(); }
        }
        string IsActive;

        public string Is_Active
        {
            get { return IsActive; }
            set { IsActive = value; Save(); }
        }
        string Email;

        public string Email_
        {
            get { return Email; }
            set { Email = value; Save(); }
        }
        string SchoolId;

        public string School_Id
        {
            get { return SchoolId; }
            set { SchoolId = value; Save(); }
        }

        string PersonName;

        public string Person_Name
        {
            get { return PersonName; }
            set { PersonName = value; Save(); }
        }
        string Job;

        public string Job_
        {
            get { return Job; }
            set { Job = value; Save(); }
        }
        string Mobile;

        public string Mobile_
        {
            get { return Mobile; }
            set { Mobile = value; Save(); }
        }

        private void CheckExisting()
        {
            if (HttpContext.Current.Session[LoggedUser] == null)
            {
                HttpContext.Current.Session[LoggedUser] = this;
                User_Id = "";
                User_Category = "";
                Email_ = "";
                School_Id = "";
                Is_Active = "";
                Person_Name = "";
                Job_ = "";
                Mobile = "";
            }
            else
            {
                UserSession oInfo = (UserSession)HttpContext.Current.Session[LoggedUser];
                this.User_Id = oInfo.User_Id;
                this.User_Category = oInfo.User_Category;
                this.Email_ = oInfo.Email_;
                this.School_Id = oInfo.School_Id;
                this.Is_Active = oInfo.Is_Active;
                this.Person_Name = oInfo.Person_Name;
                this.Job_ = oInfo.Job;
                this.Mobile_ = oInfo.Mobile_;

                oInfo = null;
            }
        }

        private void Save()
        {
            HttpContext.Current.Session[LoggedUser] = this;
        }

        public UserSession()
        {
            this.CheckExisting();
        }
    }
}