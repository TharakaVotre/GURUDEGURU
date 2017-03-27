using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Data;
using System.Drawing;
using DCISDBManager.objLib.Usr;
using DCISDBManager.trnLib;
using DCISDBManager.trnLib.UserManagement;
using System.Reflection;
using DCISDBManager.trnLib.CheckAuth;

namespace DSCMS
{
    public partial class EditUser : System.Web.UI.Page
    {
        UserSession session ;
        UserManager ugm = new UserManager();
        UserGroupManager ug = new UserGroupManager();
        string grpidAdmin = System.Configuration.ConfigurationManager.AppSettings["UserGroupID_Admin"];
        protected void Page_Load(object sender, EventArgs e)
        {
            session = new UserSession();
            if (session.User_Id == "")
            {
                Response.Redirect("~/Views/Home/Login.aspx");
            }

            string groupId = session.User_Group;
            CheckAuthManager Am = new CheckAuthManager();
            bool auth = Am.IsUserGroupAuthorised(groupId, "edu");
            if (auth == false)
            {
                Response.Redirect("~/Views/Home/Login.aspx");
            }

            if (!IsPostBack){
            BindGrid();
            DropDown();
            

            }

        }

        public void BindGrid()
        {
            try
            {
                string UserID;
                string grpID;

                if (txtUserIDs.Text != "")
                {

                    UserID = txtUserIDs.Text;

                }
                else
                {

                    UserID = "%";

                }

                if (ddUserGroup.SelectedValue != "")
                {

                    grpID = ddUserGroup.SelectedValue;

                }
                else
                {

                    grpID = "%";

                }

                GridView1.DataSource = ugm.getUserEdit(UserID, "Y", grpID);

                GridView1.DataBind();


                


                //GridView1.DataSource = ugm.getUser("%", "y");
                //GridView1.DataBind();


            }
            catch (Exception ex)
            {
                System.Console.Error.Write(ex.Message);

            }

        }

        protected void grdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {






            GridView1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        public void DropDown()
        {
            try
            {
                ddGroupID.DataSource = ug.getUserGroup("%", "y");
                ddGroupID.DataTextField = "GroupId1";
                ddGroupID.DataValueField = "GroupId1";
                ddGroupID.DataBind();



            }
            catch (Exception ex)
            {
                System.Console.Error.Write(ex.Message);

            }

        }


        public void CertificateID_Click(object sender, EventArgs e)
        {
            string UserID;
            string grpID;

            if (txtUserIDs.Text != "")
            {

                UserID = txtUserIDs.Text;

            }
            else
            {

                UserID = "%";

            }

            if (ddUserGroup.SelectedValue != "")
            {

                grpID = ddUserGroup.SelectedValue;

            }
            else
            {

                grpID = "%";

            }

            GridView1.DataSource = ugm.getUserEdit(UserID, "Y", grpID);

            GridView1.DataBind();




        }





        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.mp1.Show();
         //   GridView1.DataBind();   
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            using (GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent)
            {
                txtUserID.Text = row.Cells[0].Text;
                ddGroupID.SelectedValue = row.Cells[1].Text; ;
               // txtGroupID.Text = row.Cells[1].Text;
                txtPersonName.Text = row.Cells[2].Text;
                txtAEmail.Text = row.Cells[3].Text;
                mp1.Show();
            }

        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            btnTdelete.Visible = true;
            mp3.Show();
           
            

                using (GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent)
           {
               txtUserID.Text = row.Cells[0].Text;

               txtPersonName.Text = row.Cells[1].Text;
          


            }
                   
          
        
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            DCISDBManager.objLib.Usr.User deleteUser = new DCISDBManager.objLib.Usr.User();

            deleteUser.User_ID = txtUserID.Text;

            deleteUser.UserGroup_ID = txtPersonName.Text;
            deleteUser.Person_Name = txtUserID.Text;
            deleteUser.Password_ = txtPersonName.Text;
            deleteUser.Is_Active = "N";
            deleteUser.Email_ = null;
            // deleteUser.User_ID = "user1";
          //  ugm.ActivateDeactivetUser(deleteUser.User_ID, deleteUser.Is_Active);
            ugm.ModifyUser(deleteUser);

            Response.Redirect("EditUser.aspx");
        
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtPersonName.Text == "")
            {
                lblErrorPersonName.Text = "Please Fill out the Person Name to continue.";
                mp1.Show();
                return;
            }
            if (ddGroupID.SelectedValue == "")
            {
                lblErrorPersonName.Text = "Please Fill out the Group ID to continue.";
                mp1.Show();
                return;
            }

            if (txtAEmail.Text == "")
            {
                lblErrorPersonName.Text = "Please Fill out the Email to continue.";
                mp1.Show();
                return;
            }

            DCISDBManager.objLib.Usr.User editUser = new DCISDBManager.objLib.Usr.User();
            editUser.Person_Name = txtPersonName.Text;
            editUser.Password_ = NewPassword.Text;
            editUser.User_ID = txtUserID.Text;
            editUser.UserGroup_ID = ddGroupID.SelectedValue;
            editUser.Is_Active = "y";

            editUser.Email_ = txtAEmail.Text;
          //  editUser.Is_Vat = "y";
           
            ugm.ModifyUser(editUser);
            Response.Redirect("EditUser.aspx");   
           

        }


        protected override void OnInit(EventArgs e)
        {
            GridView1.RowDataBound += new GridViewRowEventHandler(GridView1_RowDataBound);
            base.OnInit(e);
        }
        void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType != DataControlRowType.DataRow) return;


            if (e.Row.Cells[1].Text == "ADMIN")
            {
                LinkButton Delete = (LinkButton)e.Row.FindControl("Delete");
               

                Delete.Visible = false;
              
            }




        }
    
    
    
    }
}