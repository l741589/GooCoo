<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/main_container.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    TestTitle
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HtmlBody" runat="server">
    
<h2>Test</h2>

    <p>body</p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="OtherCssStyle" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="OtherScript" runat="server">
</asp:Content>
