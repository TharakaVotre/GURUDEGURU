<%@  Title="" Language="C#" AutoEventWireup="true" CodeBehind="EditUser.aspx.cs" MasterPageFile="~/Master.Master" Inherits="DSCMS.EditUser" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">NCEDCOS | Edit User
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

       



    


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <div class="col-lg-12">
                  <div class="row">
                    <div class="col-lg-12">
                        <h1 class="page-header">
                          <small>User</small>
                        </h1>
                        <ol class="breadcrumb">
                            <li class="active">
                                <i class="fa fa-users"></i> Edit User
                            </li>
                        </ol>
                    </div>
                </div>
    
        <div class="col-md-10 col-md-offset-1 ">
             <div class="form-horizontal" role="form" style="font-family: Cambria;">
              <div class="row" id="div3">
                    <div class="group">
                    <label class="control-label col-md-2" for="email">User ID</label>
                         
                    <div class="col-md-3">
                        <asp:TextBox ID="txtUserIDs"   enabled="true"  CssClass="form-control" runat="server"></asp:TextBox>
    
                    </div>

                         
                   <label class="control-label col-md-2" runat="server" id="lblcusid" for="email" text="">User Group</label>
                             
                    <div class="col-md-3" runat="server" id="divhid">
                      
                                    <asp:DropDownList ID="ddUserGroup"  CssClass="form-control"   runat="server" AppendDataBoundItems="true"><asp:ListItem Text="All" Value="" />

                                        <asp:ListItem Text="Customer" Value="CUSTOMER" />
                                        <asp:ListItem Text="Customer Admin" Value="CADMIN" />
                                        <asp:ListItem Text="Admin" Value="ADMIN" />
                                        <asp:ListItem Text="Finance Admin" Value="FADMIN" />
                                        <asp:ListItem Text="Signature Admin" Value="SADMIN" />
                                    </asp:DropDownList>
                          
                               
                      </div>
                         <div class="col-md-1">
                             

                         <asp:Button ID="Button4" CssClass="btn btn-primary"  runat="server"  Text="Search" OnClick="CertificateID_Click"/>
                       
                    </div>
                   
               
                        </div>
</div>
</div>
            <br/>
            <br />




            <div class="panel panel-default panel-table boxshadow">
              <div class="panel-heading">
                <div class="row">
                  <div class="col col-xs-7">
                    <h3 class="panel-title"> User List</h3>
                
                  </div>
                    <div class="col col-xs-4 ">
                           

                        </div>


                    <div class="col col-xs-3 text-right">       
                      <form action="#" method="get">
                        <div class="input-group">
                            <div class="col-sm-5">
                           
                           <asp:Button ID="btnShow" CssClass="btn btn-primary" width="200" runat="server" style="display:none" Text="Add New" />
                                <asp:Button ID="btnTdelete" runat="server" Width="200" CssClass="btn btn-Secondary" Text="Edit" style="display:none" visible="false"/>

                                </div>
                        </div>
                      </form>
                
                  </div>
                  

                </div>
              </div>
              <div class="panel-body">
                          <asp:GridView ID="GridView1" BorderStyle="NotSet" runat="server" AllowPaging="True" PageSize="6"
                              CssClass="table  table-bordered  table-hover table-list-search" OnPageIndexChanging="grdData_PageIndexChanging"
                              AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
 >
                              <Columns>
                                 <asp:BoundField HeaderText="User ID" DataField="User_ID" SortExpression="User_ID" />
                                  <asp:BoundField HeaderText="User Group ID" DataField="UserGroup_ID" SortExpression="User_ID" />
                                   <asp:BoundField HeaderText="Person Name" DataField="Person_Name" SortExpression="User_ID" /> 
                                  <asp:BoundField HeaderText="Email" DataField="Email_" SortExpression="User_ID" />
                                <asp:BoundField HeaderText="Company Name" DataField="Customer_ID" SortExpression="User_ID" />
                                   <asp:TemplateField HeaderText = "Edit Option">
                                   <ItemTemplate>
                                       <asp:LinkButton ID="LinkButton1" runat="server"  CssClass="btn btn-primary" OnClick="LinkButton1_Click">Edit</asp:LinkButton>
                                       <asp:LinkButton ID="Delete" runat="server" CssClass="btn btn-danger" OnClick="Delete_Click">DeActivate</asp:LinkButton>
                                   </ItemTemplate>
                                </asp:TemplateField>
                              </Columns>
                              <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                              <PagerStyle CssClass="Gridpaging" HorizontalAlign="Right" />
                          </asp:GridView>

               
            </div>

</div>

	<div class="row">
        <div class="col-md-3">

        </div>
		<div class="col-md-9">
    	   
		</div>
	</div>

</div>
</div>

            <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>


                  <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel3" TargetControlID="btnTdelete"
    CancelControlID="btnCloseD" BackgroundCssClass="modalBackground"></cc1:ModalPopupExtender>
            <asp:Panel ID="Panel3" runat="server" CssClass="modalPopup modal-dialog modal-lg" align="center" width="600" style = "display:none">
    <!-- Modal content-->
    <div class="modal-content" >
      <div class="modal-header"  >
           <div class="col-xs-3 text-left" >
        Delete User</div>
      </div>
      <div class="modal-body" >
                      <div class="form-horizontal" role="form" style="font-family:Cambria;">
                      
                         
                           

                            <div class="form-group">
                            <label class="control-label col-sm-2" for="email"></label>
                            <div class="col-sm-8">
                            <asp:Label ID="Label1" Font-Size="Medium"  runat="server" Text="Are  You Sure,You Want to Deactivate this Account?"></asp:Label>
                         </div>
                    </div>       

                      </div> <%--End of form-horizontal--%>
      </div>
      <div class="modal-footer">
          <asp:Button ID="Button1" runat="server" Text="Yes" CssClass="btn btn-primary" Width="150px" OnClick="btnDelete_Click" />
          <asp:Button ID="btnCloseD" runat="server" Text="No"  Width="150px"  CssClass="btn btn-Secondary" />
      </div>
    </div>
</asp:Panel>


<!-- ModalPopupExtender -->
<cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="btnShow"
    CancelControlID="btnClose" BackgroundCssClass="modalBackground">
</cc1:ModalPopupExtender>
<asp:Panel ID="Panel1" runat="server" CssClass="modalPopup modal-dialog modal-lg" align="center" style = "display:none">
    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header">
        <h4 class="modal-title">User Edit</h4>
      </div>
      <div class="modal-body">
                      <div class="form-horizontal" role="form" style="font-family:Cambria;">
                      
                         <div class="form-group">
                            <label class="control-label col-sm-2" for="email">User ID</label>
                            <div class="col-sm-8">
                            <asp:TextBox ID="txtUserID" Enabled="false" name="address3" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                                   <div class="form-group">
                            <label class="control-label col-sm-2" for="email">Group ID</label>
                            

                                       <div class="col-sm-1">
                                           <asp:DropDownList ID="ddGroupID" Width="200" height="30" runat="server" AppendDataBoundItems="true"><asp:ListItem Text="--Select User Group--" Value="" /></asp:DropDownList>
                            
                            </div>
                        </div>
                           <div class="form-group">
                            <label class="control-label col-sm-2" for="email">Email</label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtAEmail" enabled="true" name="address3" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtAEmail" ForeColor="Red" ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator>
                            </div>
                        </div>




                         <div class="form-group">
                            <label class="control-label col-sm-2" for="email">Person Name</label>
                            <div class="col-sm-8">
                            <asp:TextBox ID="txtPersonName" name="address3" CssClass="form-control" runat="server"></asp:TextBox>

                            </div>
                        </div>
                            <div class="form-group">
                            <label class="control-label col-sm-2" for="Password">New Password</label>
                            <div class="col-sm-5">
                            <asp:TextBox ID="NewPassword" placeholder="Minimum 8 Characters, Numeric and special Characters are musts " Font-Size="X-Small" inputtype="password" TextMode="Password"  Height="30" CssClass="form-control" width="360" runat="server"></asp:TextBox>
                            </div>
                             <div class="col-sm-3">
                            <%-- <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ControlToValidate="NewPassword" ErrorMessage="New Password is a Required."  ForeColor="Red"></asp:RequiredFieldValidator><br/>--%>
                                  <asp:RegularExpressionValidator ForeColor="Red" ID="RegExp1" runat="server" style="width:40%"    
                                    ErrorMessage="Weak Password or Password is too Long"
                                    ControlToValidate="NewPassword"    
                                    Tooltip="Minimum 8 characters atleast 1 Alphabet, 1 Number and 1 Special Character."
                                    ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,15}$"/>
                             
                        </div>
                        </div>

                            <div class="form-group">

                            <label class="control-label col-sm-2" for="ConfirmPassword">Confirm Password</label>
                            <div class="col-sm-5">
                            <asp:TextBox ID="ConfirmPassword" inputtype="password" TextMode="Password" Height="30" Font-Size="X-Small"  CssClass="form-control" width="360" runat="server"></asp:TextBox>
                            </div>
                                <div class="col-sm-3">
                                                       <%-- <asp:RequiredFieldValidator id="RequiredFieldValidator4" runat="server" ControlToValidate="ConfirmPassword"  ErrorMessage="Confirm Password is a Required."  ForeColor="Red"></asp:RequiredFieldValidator><br/>--%>
                            
                             <asp:CompareValidator ID="CompareValidator1" runat="server" 
  ControlToValidate="ConfirmPassword"
    CssClass="ValidationError"
    ControlToCompare="NewPassword"
    ErrorMessage="Password Confirmation Failed" 
      ForeColor="Red"
    ToolTip="Password must be the same" />
 
                              
                        </div> 
                        </div>


                           <div class="form-group">
                            <label class="control-label col-sm-2" for="email"></label>
                            <div class="col-sm-8">
                            <asp:Label ID="lblErrorPersonName" ForeColor="Red" runat="server" Text=""></asp:Label>
                         </div>
                    </div>

                        

                         

                      </div> <%--End of form-horizontal--%>
      </div>
      <div class="modal-footer">
          <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" Width="200px" OnClick="btnSubmit_Click" />
          <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-default" />
      </div>
    </div>
</asp:Panel>
<!-- ModalPopupExtender -->

        <cc1:ModalPopupExtender ID="mp3" runat="server" PopupControlID="Panel3" TargetControlID="btnTdelete"
    CancelControlID="btnCloseD" BackgroundCssClass="modalBackground"></cc1:ModalPopupExtender>
            <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup modal-dialog modal-lg" align="center" style = "display:none">
    <!-- Modal content-->
    <div class="modal-content" >
      <div class="modal-header">
        
      </div>
      <div class="modal-body" >
                      <div class="form-horizontal" role="form" style="font-family:Cambria;">
                      
                         
                           

                            <div class="form-group">
                            <label class="control-label col-sm-2" for="email"></label>
                            <div class="col-sm-8">
                            <asp:Label ID="Label2" Font-Size="X-Large" ForeColor="Red" runat="server" Text="Are  You Sure ,You Want to Delete this Record"></asp:Label>
                         </div>
                    </div>
                      


                        

                        

                         

                      </div> <%--End of form-horizontal--%>
      </div>
        
      <div class="modal-footer">
          <asp:Button ID="Button2" runat="server" Text="Yes" CssClass="btn btn-primary" Width="200px" OnClick="btnDelete_Click" />
          <asp:Button ID="Button3" runat="server" Text="No"  CssClass="btn btn-Secondary"/>
      </div>
    </div>
</asp:Panel>



<!-- Modal -->
            </ContentTemplate>
</asp:UpdatePanel>




</asp:Content>
