<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/FourAreasLayout.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="TitleContent" ContentPlaceHolderID="TitleContent" runat="server">
    我的书架
</asp:Content>

<asp:Content ID="OtherCss" ContentPlaceHolderID="OtherCssStyle" runat="server">
</asp:Content>

<asp:Content ID="LeftNavbar" ContentPlaceHolderID="LeftNavbar" runat="server">
    <% Html.RenderPartial("LeftNavbar"); %>
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row-fluid">
        <div class="hero-unit">
        <table>
            <% List<GooCooServer.Entity.BookInfo> bookInfos = Model.GetBooks();
               for (int idx = 0; idx < bookInfos.Count; idx ++)
               {
                   GooCooServer.Entity.BookInfo bookInfo = bookInfos[idx];
                   if (idx % 4 == 0)
                   {
            %>
                        <tr>
            <%      } 
            %>
                            <td><a href="/BookInfo?isbn=<%: bookInfo.Isbn %>"><img src="<%: bookInfo.Photourl %>" alt="<%: bookInfo.Name %>"/></a></td>
            <%      
                    if (idx % 4 == 3 || idx == bookInfos.Count)
                    {
            %>
                        </tr>
            <%
                    }
                }
            %>                    
        </table>
        </div>
    </div>
</asp:Content>


<asp:Content ID="Footer" ContentPlaceHolderID="Footer" runat="server">
    <% Html.RenderPartial("~/Views/Shared/Footer.ascx"); %>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="OtherJavascript" runat="server">
    <script type="text/javascript">
        document.getElementById("nav_collectInfo").className = "active";
    </script>
</asp:Content>
