<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:CnnStr %>" SelectCommand="select 1 as foo"></asp:SqlDataSource>

    <asp:CheckBox CssClass="custom-checkbox" runat="server" ID="CheckBox_1" Text="Text Of CheckBox" Checked="true"  />
</asp:Content>

