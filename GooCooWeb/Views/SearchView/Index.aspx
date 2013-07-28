<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SearchViewLayout.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    

    

    <div class="span8"> 
        <% Html.RenderPartial("searchBar"); %>



        <div class="media">
            <a class="pull-left" href="#">
                <img class="media-object" src="http://img3.douban.com/mpic/s2157315.jpg">
            </a>
            <div class="media-body">
                <a href="<%:Url.Action("Index","BookInfo",new {isbn="111"}) %>">
                    <h4 class="media-heading">Aspekte zur Weiterentwicklung der sozialen Marktwirtschaft</h4>
                </a>
                <p class="book-author">作者：Dräger, Heinrich.</p>
                <p class="book-description">《武林外史》作于1965年，原名《风雪会中州》，是古龙中期转型作品，从中可看到古龙对武侠不断求新探索，其中不少人物是古龙后来作品的原型。 全书围绕一段武林恩怨展开，主角是四个性格各异的少年——沈浪、朱七七、王怜花、熊猫儿，更有许多江湖奇人异士，纠缠其中，场景跨越中原、太行、大漠、楼兰、可称宏图巨著，情节跌宕。</p>
            </div>
        </div>





        <!--页码-->
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