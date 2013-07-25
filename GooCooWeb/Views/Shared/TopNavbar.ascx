<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div class="navbar navbar-fixed-top">
    <div class="navbar-inner">
        <div class="container-fluid">

            <a class="brand" href="/">GooCoo</a>

            <ul class="nav pull-left">
                <li><a href="#">首页</a></li>
                <li><a href="<%:Url.Action("Index","SearchView") %>">搜索</a></li>
            </ul>

            <ul class="nav pull-right">
                <li>
                    <form id="searchForm" class="navbar-search pull-right" action="#">
                        <input type="text" class="search-query" placeholder="search" />
                        <input type="submit" style="display: none" />
                    </form>
                </li>

                <li><a href="#">登陆</a></li>
                <li><a href="#">注册</a></li>

            </ul>

        </div>
    </div>
</div>