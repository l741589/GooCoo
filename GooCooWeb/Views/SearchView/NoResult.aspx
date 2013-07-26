<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/FourAreasLayout.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    NoResult
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="span3">
        <h4>新书速递</h4>

        <div class="media">
            <a class="pull-left" href="#">
                <img class="media-object" src="http://img3.douban.com/view/ark_article_cover/cut/public/1171622.jpg">
            </a>
            <div class="media-body">
                <h5 class="media-heading">武林外史</h5>
            </div>
        </div>
        <div class="media">
            <a class="pull-left" href="#">
                <img class="media-object" src="http://img3.douban.com/view/ark_article_cover/cut/public/1171622.jpg">
            </a>
            <div class="media-body">
                <h5 class="media-heading">武林外史</h5>
            </div>
        </div>
        <div class="media">
            <a class="pull-left" href="#">
                <img class="media-object" src="http://img3.douban.com/view/ark_article_cover/cut/public/1171622.jpg">
            </a>
            <div class="media-body">
                <h5 class="media-heading">武林外史</h5>
            </div>
        </div>                  
    </div>


    <div class="span8">        
        <% Html.RenderPartial("searchBar"); %>        
        <div class="alert alert-info">
            搜索结果不存在
        </div>
    </div>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="OtherCssStyle" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="LeftNavbar" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="OtherJavascript" runat="server">
</asp:Content>
