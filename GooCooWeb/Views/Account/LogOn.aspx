<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ThreeAreasLayout.Master" Inherits="System.Web.Mvc.ViewPage<GooCooWeb.Models.LogOnModel>" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    登录
</asp:Content>

<asp:Content ID="TopNavbar" ContentPlaceHolderID="TopNavBar" runat="server">
    <% Html.RenderPartial("~/Views/Shared/TopNavbar.ascx"); %>
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <section class="loginBox row-fluid">
        <img class="span5 offset3" alt="Google Camp Logo" src="../../Content/image/Logo.jpg" height="200" />
        <section class="span5 offset2 left">
            <% using (Html.BeginForm(new {returnUrl = Request["returnUrl"]}))
               { %>
            <%: Html.ValidationSummary(true, "登录不成功。请重试!")%>
            <fieldset>
                <legend>Gcer登录</legend>

                <label>
                    <%: Html.LabelFor(model => model.Id)%>
                </label>
                <%: Html.TextBoxFor(model => model.Id, new { @placeholder = "Username" })%>

                <label>
                    <%: Html.LabelFor(model => model.Password)%>
                </label>
                <%: Html.PasswordFor(model => model.Password, new { @placeholder = "Password" })%>

                <label class="checkbox">
                    <input type="checkbox">
                    下次自动登录
                </label>
                <button type="submit" class="btn btn-primary">&nbsp;登录&nbsp;</button>
            </fieldset>
            <% } %>
        </section>
        <section class="span4 right">
            <h3>没有帐户？</h3>
            <section>
                <p>欢迎加入同济Google Camp - GooCoo 图书计划！在这里，你可以借阅你想要的书籍，同时捐赠你不需要的书给学弟学妹，让我们把书籍循环利用起来~</p>
                <p>
                    <%: Html.ActionLink(" 注册 ", "Register", "Account", null, new { @class="btn btn-info" })%>
                </p>
            </section>
        </section>
    </section>
</asp:Content>

<asp:Content ID="Footer" ContentPlaceHolderID="Footer" runat="server">
    <footer class="span6 offset2">
        <% Html.RenderPartial("~/Views/Shared/Footer.ascx"); %>
    </footer>
</asp:Content>

