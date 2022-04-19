<%@ Page Title="" Language="VB" MasterPageFile="~/App.master" AutoEventWireup="false" CodeFile="Manage_Category.aspx.vb" Inherits="Admin_Manage_Category" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class='rtl text-right'>
        <br><h4>مدیریت دسته بندی ها</h4>
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
            <asp:SqlDataSource ID="SqlDataSource_List" runat="server" ConnectionString="<%$ ConnectionStrings:CnnStr %>" SelectCommand="SELECT * FROM (SELECT * , (SELECT * FROM dbo.TBL_Cats WHERE CatID=SRCTBL.CatID FOR JSON PATH ) AS SearchField FROM dbo.TBL_Cats SRCTBL ) TBL Where SearchField like '%' + @SV + '%' " >
                <SelectParameters> 
                    <asp:ControlParameter ControlID="TextBox_SearchValue" DefaultValue="%" Name="SV" />
                </SelectParameters>
            </asp:SqlDataSource>
    <asp:GridView CssClass="table table-sm table-hover table-bordered table-striped az-table rtl" ID="GridView_List" runat="server" AutoGenerateColumns="False" DataKeyNames="CatID" DataSourceID="SqlDataSource_List" AllowPaging="True" AllowSorting="True" GridLines="None">
        <Columns>
            <asp:TemplateField HeaderText="ردیف" ItemStyle-CssClass="text-center" HeaderStyle-Width="1%"><ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate></asp:TemplateField>
            <asp:BoundField DataField="CatID" HeaderText="CatID" SortExpression="CatID" />
            <asp:BoundField DataField="SectID" HeaderText="بخش" SortExpression="SectID" />
            <asp:BoundField DataField="ParentCatID" HeaderText="دسته بندی والد" SortExpression="ParentCatID" />
            <asp:BoundField DataField="Str_CatName" HeaderText="نام دسته بندی" SortExpression="Str_CatName" />
            <asp:BoundField DataField="int_ShowOrder" HeaderText="ترتیب نمایش" SortExpression="int_ShowOrder" />
         <asp:TemplateField>
             <ItemTemplate>
                 <asp:Button CssClass="btn btn-sm btn-primary" ID="Button_Edit" runat="server" CommandArgument='<%# Eval("CatID") %>' Text="ویرایش" OnClick="Button_Edit_Click" CommandName="Select" />
             </ItemTemplate>
             <HeaderStyle Width="1%" Wrap="False" />
         </asp:TemplateField>
     </Columns>
        <PagerStyle CssClass="app-pager" />
        <SelectedRowStyle CssClass="table-selected" />
 </asp:GridView>
</asp:Panel>
        <asp:Panel ID="Panel_Edit" runat="server" Visible="false">
            <div class="row justify-content-center">
                <div class="col-6">
                    <div class="card border-primary">
                        <div class="card-header bg-info text-center text-center text-white">
                            <asp:Label ID="Label_EditTitle" runat="server" Text="Label"></asp:Label>
                        </div>
                        <div class="card-body">
                            CatID : <asp:Label ID="Label_CatID" runat="server" Text="0" Visible="true"></asp:Label>
                            <div class="form-group mb-3">
                            </div>
                            <div class="form-group mb-3">
                                <label>بخش : </label>
                                <asp:TextBox CssClass="form-control" ID="TextBox_SectID" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group mb-3">
                                <label>دسته بندی والد : </label>
                                <asp:TextBox CssClass="form-control" ID="TextBox_ParentCatID" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group mb-3">
                                <label>نام دسته بندی : </label>
                                <asp:TextBox CssClass="form-control" ID="TextBox_Str_CatName" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group mb-3">
                                <label>ترتیب نمایش : </label>
                                <asp:TextBox CssClass="form-control" ID="TextBox_int_ShowOrder" runat="server"></asp:TextBox>
                            </div>
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
