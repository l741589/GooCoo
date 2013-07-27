<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/FourAreasLayout.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="TitleContent" ContentPlaceHolderID="TitleContent" runat="server">
    个人信息
</asp:Content>

<asp:Content ID="OtherCss" ContentPlaceHolderID="OtherCssStyle" runat="server">
</asp:Content>

<asp:Content ID="LeftNavbar" ContentPlaceHolderID="LeftNavbar" runat="server">
    <% Html.RenderPartial("LeftNavbar"); %>
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row-fluid">
    </div>
</asp:Content>


<asp:Content ID="Footer" ContentPlaceHolderID="Footer" runat="server">
    <% Html.RenderPartial("~/Views/Shared/Footer.ascx"); %>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="OtherJavascript" runat="server">
    <script type="text/javascript">
        document.getElementById("nav_personalInfo").className = "active";
    </script>
</asp:Content>