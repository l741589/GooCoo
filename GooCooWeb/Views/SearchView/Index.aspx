<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SearchViewLayout.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    

    

    <div class="span8"> 
        <% Html.RenderPartial("searchBar"); %>

        <% if (ViewBag.SearchResult.HasSearch){ %>
        
        <%foreach (GooCooServer.Entity.BookInfo bookInfo in ViewBag.SearchResult.Results)
          { %>

        <div class="media">
            <a class="pull-left" href="<%:Url.Action("Index","BookInfo",new {isbn= bookInfo.Isbn}) %>">
                <img class="media-object" src="<%:bookInfo.Photourl %>">                
            </a>
            <div class="media-body">
                <a href="<%:Url.Action("Index","BookInfo",new {isbn= bookInfo.Isbn}) %>">
                    <h4 class="media-heading"><%:bookInfo.Name %></h4>
                </a>
                <p class="book-author">作者：</p>
                <p class="book-description"><%:bookInfo.Summary %></p>
            </div>
        </div>

        <%} %>      

        <!--页码-->
            <div class="pagination">
                <ul>
                    <li><a href="#">Prev</a></li>
                    <li><a href="#">1</a></li>
                    <li><a href="#">2</a></li>
                    <li><a href="#">3</a></li>
                    <li><a href="#">4</a></li>
                    <li><a href="#">5</a></li>
                    <li><a href="#">Next</a></li>
                </ul>
            </div>
        
        <%} %>
    </div>


      <% Html.RenderPartial("newBooksRecommendPanel"); %>

    
         
    

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="OtherCssStyle" runat="server">
    <style type="text/css">
        .book-title {

        }
        .book-author {

        }

    </style>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="OtherJavascript" runat="server">
   

</asp:Content>