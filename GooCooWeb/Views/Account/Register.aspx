<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ThreeAreasLayout.Master" Inherits="System.Web.Mvc.ViewPage<GooCooWeb.Models.RegisterModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    注册
</asp:Content>

<asp:Content ID="OtherCss" ContentPlaceHolderID="OtherCssStyle" runat="server">
    <link href="<%= Url.Content("~/Content/RegisterPage.css") %>" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="TopNavBar" ContentPlaceHolderID="TopNavBar" runat="server">
    <% Html.RenderPartial("~/Views/Shared/TopNavbar.ascx"); %>
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <a href="/Home/Index">
        <img class="span6 offset3" alt="Google Camp Logo" src="../../Content/image/Logo.jpg" />
    </a>
    <% using (Html.BeginForm("Register", "Account", new { returnUrl = Request["returnUrl"] }, FormMethod.Post, new { @class = "form-horizontal" }))
       { %>
    <div class="span6 offset3 left">
        <fieldset>
            <legend>欢迎加入Google Camp - GooCoo 图书计划</legend>
            <div class="well text-center">
                <p>&gt;&nbsp;已经成为Gcer？&nbsp;<%: Html.ActionLink("直接登录", "LogOn", "Account", new { returnUrl = Request["returnUrl"] }, null)%></p>
            </div>
            <%: Html.ValidationSummary(true, "帐户创建不成功。请更正错误并重试!") %>
            <div class="control-group">
                <div class="control-label">
                    <%: Html.LabelFor(model => model.Id)%>
                </div>
                <div class="controls">
                    <%: Html.TextBoxFor(model => model.Id, new { @placeholder="Student Number" }) %>
                </div>
            </div>
            <div class="control-group">
                <div class="control-label">
                    <%: Html.LabelFor(model => model.Name)%>
                </div>
                <div class="controls">
                    <%: Html.TextBoxFor(model => model.Name, new { @placeholder="Your Name" }) %>
                </div>
            </div>
            <div class="control-group">
                <div class="control-label">
                    <%: Html.LabelFor(model => model.PhoneNumer)%>
                </div>
                <div class="controls">
                    <%: Html.TextBoxFor(model => model.PhoneNumer, new { @placeholder="Phone Number" }) %>
                    <%: Html.ValidationMessageFor(model => model.PhoneNumer) %>
                </div>
            </div>
            <div class="control-group">
                <div class="control-label">
                    <%: Html.LabelFor(model => model.Email)%>
                </div>
                <div class="controls">
                    <%: Html.TextBoxFor(model => model.Email, new { @placeholder="E-mail Address" }) %>
                    <%: Html.ValidationMessageFor(model => model.Email) %>
                </div>
            </div>
            <div class="control-group">
                <div class="control-label">
                    <%: Html.LabelFor(model => model.Password) %>
                </div>
                <div class="controls">
                    <%: Html.PasswordFor(model => model.Password, new { @placeholder="Password" }) %>
                </div>
            </div>
            <div class="control-group">
                <div class="control-label">
                    <%: Html.LabelFor(model => model.ConfirmPassword) %>
                </div>
                <div class="controls">
                    <%: Html.PasswordFor(model => model.ConfirmPassword, new { @placeholder="Confirm Password" }) %>
                    <%: Html.ValidationMessageFor(model => model.ConfirmPassword) %>
                </div>
            </div>
            <div class="control-group">
                <div class="controls">
                    <button type="submit" class="btn btn-info">&nbsp;注册&nbsp;</button>
                </div>
            </div>
        </fieldset>
    </div>
    <% } %>
</asp:Content>


<asp:Content ID="Content6" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>
