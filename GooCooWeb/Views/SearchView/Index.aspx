<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/FourAreasLayout.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
        <div class="input-prepend">
            <div class="btn-group">
                <button class="btn dropdown-toggle" data-toggle="dropdown">                    
                        标题                   
                    <span class="caret"></span>
                </button>
                <ul class="dropdown-menu">
                    <li>标题</li>
                    <li>作者</li>
                    <li>模糊</li>
                </ul>
            </div>
            <input class="span2" name="keyword" id="prependedDropdownButton" type="text">
            <button class="btn" type="button">Search</button>
        </div>


    <div class="input-prepend">
  <div class="btn-group">
    <button class="btn dropdown-toggle" data-toggle="dropdown">
      Action
      <span class="caret"></span>
    </button>
    <ul class="dropdown-menu">
      <li><a href="#">aaa</a></li>
      <li><a href="#">bbb</a></li>
    </ul>
  </div>
  <input class="span2" id="Text1" type="text">
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