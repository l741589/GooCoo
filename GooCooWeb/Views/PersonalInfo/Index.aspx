<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/FourAreasLayout.Master" Inherits="System.Web.Mvc.ViewPage<GooCooWeb.Models.PersonalInfoModel>" %>

<asp:Content ID="TitleContent" ContentPlaceHolderID="TitleContent" runat="server">
    个人信息
</asp:Content>

<asp:Content ID="LeftNavbar" ContentPlaceHolderID="LeftNavbar" runat="server">
    <% Html.RenderPartial("LeftNavbar"); %>
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row-fluid">
        <div class="hero-unit">
            <form>
                <fieldset>
                    <legend>个人信息</legend>
                    <div class="alert">
                        借阅册数 【<%: Model.BorrowBookNumer %>】&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;捐书册数 【<%: Model.DonateBookNumer %>】
                    </div>
                    <table class="table">
                        <tr>
                            <td><%: Html.LabelFor(model => model.Id) %></td>
                            <td><%: Model.Id %></td>
                        </tr>
                        <tr>
                            <td><%: Html.LabelFor(model => model.Name) %></td>
                            <td><%: Model.Name %></td>
                        </tr>
                        <tr>
                            <td><%: Html.LabelFor(model => model.PhoneNumber) %></td>
                            <td><%: Model.PhoneNumber %></td>
                        </tr>
                        <tr>
                            <td><%: Html.LabelFor(model => model.Email) %></td>
                            <td><%: Model.Email %></td>
                        </tr>
                        <tr>
                            <td><%: Html.LabelFor(model => model.Authority) %></td>
                            <td><%: ViewBag.UserLevel %></td>
                        </tr>
                    </table>
                    <div class="alert alert-error">
                        违规天次 【<%: Model.Repvalue %>】
                    </div>
                    <button type="submit" class="btn btn-primary">修改信息</button>
                </fieldset>
            </form>
        </div>
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
