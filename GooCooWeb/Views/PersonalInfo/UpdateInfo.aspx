<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/FourAreasLayout.Master" Inherits="System.Web.Mvc.ViewPage<GooCooWeb.Models.PersonalInfoModel>" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    修改个人信息
</asp:Content>

<asp:Content ID="LeftNavbar" ContentPlaceHolderID="LeftNavbar" runat="server">
    <% Html.RenderPartial("LeftNavbar"); %>
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row-fluid">
        <div class="hero-unit">
            <% using (Html.BeginForm("UpdateInfo", "PersonalInfo", null, FormMethod.Post, new { @class = "form-horizontal" }))
               {
            %>
                <fieldset>
                    <legend>修改个人信息</legend>
                    <div class="control-group">
                        <label class="control-label" for="Id"><%= Model.Id %></label>
                        <div class="controls">
                            <%: Html.TextBoxFor(model => model.Id, new { disabled = "disabled"})%>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label" for="Name"><%= Model.Name %></label>
                        <div class="controls">
                            <%: Html.TextBoxFor(model => model.Id, new { disabled = "disabled"})%>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label" for="PhoneNumber"><%= Model.PhoneNumber %></label>
                        <div class="controls">
                            <%: Html.TextBoxFor(model => model.PhoneNumber, new { @placeholder = "Phone Numer"})%>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label" for="Email"><%= Model.Email %></label>
                        <div class="controls">
                            <%: Html.TextBoxFor(model => model.Email, new { @placeholder = "E-mail"})%>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label" for="Password"><%= Model.Password %></label>
                        <div class="controls">
                            <%: Html.PasswordFor(model => model.Password, new { @placeholder = "Password"})%>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label" for="ConfirmPassword"><%= Model.ConfirmPassword %></label>
                        <div class="controls">
                            <%: Html.PasswordFor(model => model.ConfirmPassword, new { @placeholder = "Confirm Password"})%>
                        </div>
                    </div>
                    <button type="submit" class="btn btn-primary">确定修改</button>
                </fieldset>
            <%
                } 
            %>
        </div>
    </div>
</asp:Content>


<asp:Content ID="Footer" ContentPlaceHolderID="Footer" runat="server">
    <% Html.RenderPartial("~/Views/Shared/Footer.ascx"); %>
</asp:Content>
