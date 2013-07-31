<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SearchViewLayout.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    NoResult
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    


    <div class="span8">        
        <% Html.RenderPartial("searchBar"); %>        
        <div class="alert alert-info">
            搜索结果不存在
        </div>
    </div>

    <% Html.RenderPartial("newBooksRecommendPanel"); %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="OtherCssStyle" runat="server">
</asp:Content>


<asp:Content ID="Content5" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="OtherJavascript" runat="server">
</asp:Content>
