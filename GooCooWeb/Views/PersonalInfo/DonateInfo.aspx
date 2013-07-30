<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/FourAreasLayout.Master" Inherits="System.Web.Mvc.ViewPage<GooCooWeb.Models.DonateInfoModel>" %>

<asp:Content ID="TitleContent" ContentPlaceHolderID="TitleContent" runat="server">
    捐书信息
</asp:Content>

<asp:Content ID="OtherCss" ContentPlaceHolderID="OtherCssStyle" runat="server">
</asp:Content>

<asp:Content ID="LeftNavbar" ContentPlaceHolderID="LeftNavbar" runat="server">
    <% Html.RenderPartial("LeftNavbar"); %>
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row-fluid">
        <div class="hero-unit">
            <form>
                <fieldset>
                    <legend>捐赠列表</legend>
                    <div class="alert">
                        捐赠册数 【<%: ViewBag.DonateBookNumber %>】
                    </div>
                    <table class="table table-hover table-bordered">
                        <thead>
                            <tr>
                                <th>图书编号</th>
                                <th>书名</th>
                                <th>借阅日期</th>
                            </tr>
                        </thead>
                        <tbody>
                            <%
                                List<GooCooWeb.Models.DonateBookInfo> books = Model.GetBooks();
                                for (int idx = 0; idx < books.Count; idx++)
                                {
                                    GooCooWeb.Models.DonateBookInfo book = books[idx];
                                    if (idx % 2 == 0)
                                    {
                            %>
                            <tr class="success">
                                <%
                                    }
                                    else
                                    {
                                %>
                            <tr>
                                <%
                                    }   
                                %>
                                <td><%: book.Id %></td>
                                <td><%: book.Name %></td>
                                <td><%: book.DonateTime %></td>
                            </tr>
                            <%
                                }
                            %>
                        </tbody>
                    </table>
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
        document.getElementById("nav_donateInfo").className = "active";
    </script>
</asp:Content>
