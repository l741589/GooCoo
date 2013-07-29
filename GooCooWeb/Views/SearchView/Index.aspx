<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SearchViewLayout.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    

    

    <div class="span8"> 
        <% Html.RenderPartial("searchBar"); %>

        <% if (ViewBag.SearchResult.HasSearch){ %>
        
            <p id="search-header">
                <%
                    int recordFrom = (ViewBag.SearchResult.CurrentPage - 1) * GooCooWeb.Models.SearchResultModel.recordPerPage + 1;
                    int recordTo = recordFrom + ViewBag.SearchResult.Results.Count - 1;
                 %>
                搜索结果<%:recordFrom %>-<%:recordTo %> 共<%:ViewBag.SearchResult.ResultCount %>
            </p>
        
        <%foreach (GooCooServer.Entity.BookInfo bookInfo in ViewBag.SearchResult.Results)
          { %>

        <div class="search-record">
        <div class="media">
            <a class="pull-left" href="<%:Url.Action("Index","BookInfo",new {isbn= bookInfo.Isbn}) %>">
                <img class="media-object" src="<%:bookInfo.Photourl %>">                
            </a>
            <div class="media-body">
                <a class="book-title" href="<%:Url.Action("Index","BookInfo",new {isbn= bookInfo.Isbn}) %>">
                    <p class="media-heading"><%:bookInfo.Name %></p>
                </a>
                <p class="book-author">作者：</p>
                <p class="book-description"><%:bookInfo.Summary %></p>
            </div>
        </div>
        </div>

        <%} %>      

        <!--页码-->
        <%
            string searchKeyword = ViewBag.SearchResult.Keyword;
           string searchType = ViewBag.SearchResult.SearchType;
        %>
            <div class="pagination">
                <ul>
                    <!--上一页-->
                    
                        <%if (ViewBag.SearchResult.PreviewPage != 0){ %>                            
                            <li><a href="<%:Url.Action("Index","SearchView",new {keyword = searchKeyword, type = searchType, page = ViewBag.SearchResult.PreviewPage}) %>"><</a></li>
                        <%} %>
                    
                    <!--第一页-->
                    <% if (ViewBag.SearchResult.PageFrom > 1){ %>
                        <li><a href="<%:Url.Action("Index", "SearchView", new { keyword = searchKeyword, type = searchType, page = 1 })%>"> <%:ViewBag.SearchResult.PageFrom == 2? "1" : "1..."%> </a></li>
                    <%} %>

                    <!--中间页码-->
                    <%for (int i = ViewBag.SearchResult.PageFrom; i <= ViewBag.SearchResult.PageTo; i++ ){ %>
                        <% if (i == ViewBag.SearchResult.CurrentPage){ %>
                            <!--当前页-->
                            <li class="active"><a href="#"> <%:i %> </a></li>
                        <%} else { %>
                            <li><a href="<%:Url.Action("Index", "SearchView", new { keyword = searchKeyword, type = searchType, page = i })%>"> <%:i %> </a></li>
                        <%} %>
                    <%} %>
                    
                    <!--最后一页-->
                     <% if (ViewBag.SearchResult.PageTo < ViewBag.SearchResult.TotalPage){ %>
                        <li><a href="<%:Url.Action("Index", "SearchView", new { keyword = searchKeyword, type = searchType, page = ViewBag.SearchResult.TotalPage })%>"> <%:ViewBag.SearchResult.PageTo == (ViewBag.SearchResult.TotalPage - 1)? ViewBag.SearchResult.TotalPage : ("..." + Convert.ToString(ViewBag.SearchResult.TotalPage))%> </a></li>
                    <%} %>

                    <!--下一页-->
                    <%if (ViewBag.SearchResult.NextPage != 0){ %>                        
                        <li><a href="<%:Url.Action("Index", "SearchView", new { keyword = searchKeyword, type = searchType, page = ViewBag.SearchResult.NextPage })%>">></a></li>
                    <%} %>                                        

                </ul>
            </div>
        
        <%} %>
    </div>


      <% Html.RenderPartial("newBooksRecommendPanel"); %>

    
         
    

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="OtherCssStyle" runat="server">
    <style type="text/css">
        .book-title {
            font-size:large;
            background-color:red;
        }
        .book-author {
            color: #909090;        
        }
        .book-description {
            color: #808080; 
        }
        .search-record {                        
            padding-bottom:15px;
            margin-bottom: 15px;
            border-bottom:thin dashed #d5d5d5;
        }
        #search-header {
            text-align:right;
            padding-bottom:5px;
            margin-bottom: 15px;
            border-bottom:thin dashed #d5d5d5;
        }

    </style>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="OtherJavascript" runat="server">
   

</asp:Content>