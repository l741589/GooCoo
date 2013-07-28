<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SearchViewLayout.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    

    

    <div class="span8"> 
        <% Html.RenderPartial("searchBar"); %>


        <div class="row">
            <h5 class="book-title">Aspekte zur Weiterentwicklung der sozialen Marktwirtschaft /</h5>
            <p class="book-author">Dräger, Heinrich.</p>
        </div>

        <div class="row">
            <h5>Aspekte zur Weiterentwicklung der sozialen Marktwirtschaft /</h5>
            <p>Dräger, Heinrich.</p>
        </div>

        <div class="row">
            <h5>Aspekte zur Weiterentwicklung der sozialen Marktwirtschaft /</h5>
            <p>Dräger, Heinrich.</p>
        </div>


        <div class="row">
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
        </div>
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