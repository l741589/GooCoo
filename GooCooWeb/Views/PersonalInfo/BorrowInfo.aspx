<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/FourAreasLayout.Master" Inherits="System.Web.Mvc.ViewPage<GooCooWeb.Models.BorrowInfoModel>" %>

<asp:Content ID="TitleContent" ContentPlaceHolderID="TitleContent" runat="server">
    借阅列表
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
                    <legend>借阅列表</legend>
                    <div class="alert">
                        借阅册数 【<%: ViewBag.BorrowBookNumber %>】
                    </div>
                    <table class="table table-hover">
                        <thead>
                            <tr>
                                <th>图书编号</th>
                                <th>书名</th>
                                <th>借阅日期</th>
                                <th>应还日期</th>
                            </tr>
                        </thead>
                        <tbody>
                            <%
                                List<GooCooWeb.Models.BorrowBookInfo> books = Model.GetBooks();
                                for (int idx = 0; idx < books.Count; idx++)
                                {
                                    GooCooWeb.Models.BorrowBookInfo book = books[idx];
                                    if (book.ExpectedReturnTime < DateTime.Now)
                                    {
                           %>
                                        <tr class="error">
                           <%
                                    }
                                    else
                                    if (idx % 2 == 0)
                                    {
                            %>
                                        <tr class="info">
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
                                            <td><%: book.BorrowTime %></td>
                                            <td><%: book.ExpectedReturnTime %></td>
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
        document.getElementById("nav_borrowInfo").className = "active";
    </script>
</asp:Content>
