<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div class="well sidebar-nav">
    <ul class="nav nav-list">
        <li id="nav_personalInfo">
            <%: Html.ActionLink("个人信息", "Index") %>
        </li>
        <li id="nav_borrowInfo">
            <%: Html.ActionLink("借阅列表", "BorrowInfo") %>
        </li>
        <li id="nav_donateInfo">
            <%: Html.ActionLink("捐书信息", "DonateInfo") %>
        </li>
        <li id="nav_collectInfo">
            <%: Html.ActionLink("我的书架", "CollectInfo") %>
        </li>
        <li id="nav_preorderInfo">
            <%: Html.ActionLink("预定信息", "PreorderInfo") %>
        </li>
    </ul>
</div>
