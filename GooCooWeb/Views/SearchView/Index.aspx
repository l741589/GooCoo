<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/FourAreasLayout.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="row-fluid offset3">
    <form action="#">
        <div class="input-prepend">
            <div class="btn-group">
                <button class="btn dropdown-toggle" data-toggle="dropdown">                    
                        <span id="searchTypeText">标题</span>                   
                    <span class="caret"></span>
                </button>
                <ul class="dropdown-menu">
                    <li onclick="changeSearchType('标题')"><a href="#">标题</a></li>
                    <li onclick="changeSearchType('作者')"><a href="#">作者</a></li>
                    <li onclick="changeSearchType('模糊')"><a href="#">模糊</a></li>                                                            
                </ul>
            </div>
            <input class="input-xlarge" placeholder="关键字" name="keyword" id="prependedDropdownButton" type="text">
            <button class="btn" type="button">Search</button>
        </div>

        <input type="text" id="searchType" name="type" value="标题" style="display:none"/>    
    </form>       
    </div>
     
    <div class="alert alert-info">
         搜索结果不存在
    </div>



    <div class="row-fluid offset3">
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

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="OtherCssStyle" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="LeftNavbar" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="OtherJavascript" runat="server">
    <script type="text/javascript">
        function changeSearchType(type)
        {
            var text = document.getElementById("searchTypeText");
            text.innerHTML = type;

            var typeChoose = document.getElementById("searchType");
            typeChoose.value = type;
        }
    </script>

</asp:Content>