<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/FourAreasLayout.Master" Inherits="System.Web.Mvc.ViewPage<GooCooWeb.Models.PreorderInfoModel>" %>

<asp:Content ID="TitleContent" ContentPlaceHolderID="TitleContent" runat="server">
    预定信息
</asp:Content>

<asp:Content ID="OtherCss" ContentPlaceHolderID="OtherCssStyle" runat="server">
    <style type="text/css">
        .text-green {
            color: green;
        }
        .text-red {
            color: red;
        }
    </style>
</asp:Content>

<asp:Content ID="LeftNavbar" ContentPlaceHolderID="LeftNavbar" runat="server">
    <% Html.RenderPartial("LeftNavbar"); %>
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row-fluid">
        <div class="hero-unit">
            <form>
                <fieldset>
                    <legend>预定列表</legend>
                    <div class="alert">
                        预定册数 【<%: ViewBag.PreorderBookNumber %>】
                    </div>
                    <table class="table table-hover">
                        <thead>
                            <tr>
                                <th>书名</th>
                                <th>作者</th>
                                <th>预定日期</th>
                                <th>借出数量</th>
                                <th>预定人数</th>
                            </tr>
                        </thead>
                        <tbody>
                            <%
                                List<GooCooWeb.Models.PreorderBookInfo> books = Model.GetBooks();
                                for (int idx = 0; idx < books.Count; idx++)
                                {
                                    GooCooWeb.Models.PreorderBookInfo book = books[idx];
                            %>
                            <a href="/BookInfo?isbn=<%: book.Isbn %>">
                                    <tr>
                                        <td><%: book.Name %></td>
                                        <td><%: book.Author %></td>
                                        <td><%: book.PreorderDate %></td>
                                        <td><%: book.BorrowedNumber %></td>
                                        <td class="text-success"><%: book.PreorderNumber %></td>
                                    </tr>
                            </a>
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
        document.getElementById("nav_preorderInfo").className = "active";
    </script>
</asp:Content>

