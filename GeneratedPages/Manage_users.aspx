<%@ Page Title="" Language="VB" MasterPageFile="~/App.master" AutoEventWireup="false" CodeFile="Manage_users.aspx.vb" Inherits="Admin_Manage_users" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class='rtl text-right'>
        <br><h4>مدیرت کاربران</h4>
    <hr />
    <div>
        <asp:Panel ID="Panel_list" runat="server" Visible="true">
             <div class="col-md-12 ">
                 <div class="form-inline form-group-sm rtl">
                     <asp:Button ID="Button_AddNew" runat="server" CssClass="btn btn-sm btn-primary btn-110" Text="افزودن" UseSubmitBehavior="false" />
                     &nbsp;&nbsp;&nbsp;&nbsp;
                     جستجو در این صفحه : &nbsp;<asp:TextBox ID="TextBox_SearchValue" CssClass="form-control form-control-sm" runat="server"></asp:TextBox> &nbsp; 
                     <asp:Button ID="Button_Search" runat="server" CssClass="btn btn-sm btn-info btn-110" Text="جستجو"  />
                         &nbsp;
                     <asp:Label CssClass="form-control form-control-sm" ID="Label_ActionMessage" runat="server" Text="" ForeColor="Green" EnableViewState="False"></asp:Label>
                 </div>             </div> <br>
    <div class="col-md-12 ">
            <asp:SqlDataSource ID="SqlDataSource_List" runat="server" ConnectionString="<%$ ConnectionStrings:CnnStr %>" SelectCommand="SELECT * FROM (SELECT * , (SELECT * FROM dbo.TBL_AZ_Users WHERE UserID=SRCTBL.UserID FOR JSON PATH ) AS SearchField FROM dbo.TBL_AZ_Users SRCTBL ) TBL Where SearchField like '%' + @SV + '%' " >
                <SelectParameters> 
                    <asp:ControlParameter ControlID="TextBox_SearchValue" DefaultValue="%" Name="SV" />
                </SelectParameters>
            </asp:SqlDataSource>
    <asp:GridView CssClass="table table-sm table-hover table-bordered table-striped az-table rtl table-responsive" ID="GridView_List" runat="server" AutoGenerateColumns="False" DataKeyNames="UserID" DataSourceID="SqlDataSource_List" AllowPaging="True" AllowSorting="True" GridLines="None">
        <Columns>
            <asp:TemplateField HeaderText="ردیف" ItemStyle-CssClass="text-center" HeaderStyle-Width="1%"><ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate></asp:TemplateField>
            <asp:BoundField DataField="UserID" HeaderText="UserID" SortExpression="UserID" />
            <asp:BoundField DataField="Str_RealName" HeaderText="نام و نام خانوادگی" SortExpression="Str_RealName" />
            <asp:BoundField DataField="Str_UserName" HeaderText="نام کاربری" SortExpression="Str_UserName" />
            <asp:BoundField DataField="Str_PassWord" HeaderText="کلمه عبور" SortExpression="Str_PassWord" />
            <asp:BoundField DataField="Str_Tel" HeaderText="تلفن تماس" SortExpression="Str_Tel" />
            <asp:BoundField DataField="Str_Email" HeaderText="آدرس پست الکترونیک" SortExpression="Str_Email" />
            <asp:BoundField DataField="Str_ACL" HeaderText="سطوح دسترسی" SortExpression="Str_ACL" />
            <asp:BoundField DataField="Bol_UseDomain" HeaderText="کاربر دامین" SortExpression="Bol_UseDomain" />
            <asp:BoundField DataField="Str_DomainName" HeaderText="نام دامین" SortExpression="Str_DomainName" />
            <asp:BoundField DataField="Bol_IsActive" HeaderText="فعال" SortExpression="Bol_IsActive" />
         <asp:TemplateField>
             <ItemTemplate>
                 <asp:Button CssClass="btn btn-sm btn-primary" ID="Button_Edit" runat="server" CommandArgument='<%# Eval("UserID") %>' Text="ویرایش" OnClick="Button_Edit_Click" CommandName="Select" />
             </ItemTemplate>
             <HeaderStyle Width="1%" Wrap="False" />
         </asp:TemplateField>
     </Columns>
        <PagerStyle CssClass="app-pager" />
        <SelectedRowStyle CssClass="table-selected" />
     </asp:GridView>
 </div>
</asp:Panel>
        <asp:Panel ID="Panel_Edit" runat="server" Visible="false">
            <div class="row justify-content-center">
                <div class="col-6">
                    <div class="card border-primary">
                        <div class="card-header bg-info text-center text-center text-white">
                            <asp:Label ID="Label_EditTitle" runat="server" Text="Label"></asp:Label>
                        </div>
                        <div class="card-body">
                            UserID : <asp:Label ID="Label_UserID" runat="server" Text="0" Visible="true"></asp:Label>
                            <div class="form-group mb-3">
                            </div>
                            <div class="form-group mb-3">
                                <label>نام و نام خانوادگی : </label>
                                <asp:TextBox CssClass="form-control" ID="TextBox_Str_RealName" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group mb-3">
                                <label>نام کاربری : </label>
                                <asp:TextBox CssClass="form-control" ID="TextBox_Str_UserName" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group mb-3">
                                <label>کلمه عبور : </label>
                                <asp:TextBox CssClass="form-control" ID="TextBox_Str_PassWord" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group mb-3">
                                <label>تلفن تماس : </label>
                                <asp:TextBox CssClass="form-control" ID="TextBox_Str_Tel" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group mb-3">
                                <label>آدرس پست الکترونیک : </label>
                                <asp:TextBox CssClass="form-control" ID="TextBox_Str_Email" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group mb-3">
                                <label>سطوح دسترسی : </label>
                                <asp:TextBox CssClass="form-control" ID="TextBox_Str_ACL" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group mb-3">
                                <label>کاربر دامین : </label>
                                <asp:CheckBox CssClass="custom-checkbox" ID="CheckBox_Bol_UseDomain" runat="server" />                            </div>
                            <div class="form-group mb-3">
                                <label>نام دامین : </label>
                                <asp:TextBox CssClass="form-control" ID="TextBox_Str_DomainName" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group mb-3">
                                <label>فعال : </label>
                                <asp:CheckBox CssClass="custom-checkbox" ID="CheckBox_Bol_IsActive" runat="server" />                            </div>
                            <div class="form-group">
                                <asp:Label ID="Label_Message" runat="server" Text="" EnableViewState="False" ForeColor="Red"></asp:Label>
                            </div>
                            <div class="rtl ">
                                <asp:Button ID="Button_SaveNew" runat="server" Text="ثبت" CssClass="btn btn-sm btn-success btn-110" />
                                <asp:Button ID="Button_SaveChanges" runat="server" Text="ثبت تغییرات" CssClass="btn btn-sm btn-success btn-110" />
                                <asp:Button ID="Button_Cancel" runat="server" Text="انصراف" CssClass="btn btn-sm btn-warning btn-110" />
                                <asp:Button ID="Button_Delete" runat="server" Text="حذف" CssClass="btn btn-sm btn-danger btn-110 pull-left" OnClientClick="return confirm('اطلاعات انتخاب شده حذف شود ؟');" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
    </div>
</asp:Content>
