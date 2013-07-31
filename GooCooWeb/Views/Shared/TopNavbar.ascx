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
                    <form id="searchForm" class="navbar-search pull-right" action="<%:Url.Action("Index","SearchView") %>">
                        <input type="text" class="search-query" name="keyword" placeholder="search" />
                        <input type="submit" style="display: none" />
                    </form>
                </li>


                <%
                    bool isLoggedOn = false;
                    string userSessionID = (string)Session["UserSessionID"];
                    if (userSessionID != null)
                        isLoggedOn = true;
                    else
                    {
                        HttpCookie cookie = Request.Cookies["UserSessionID"];
                        if (cookie != null)
                        {
                            isLoggedOn = true;
                            Session["UserSessionID"] = cookie.Value;
                            userSessionID = cookie.Value;
                        }
                    }
                    if (!isLoggedOn)
                    {
                %>
                <li>
                    <% string returnUrl = Request["returnUrl"];
                       if (returnUrl == null)
                           returnUrl = Request.RawUrl;
                    %>
                    <%: Html.ActionLink("登录", "LogOn", "Account", new { returnUrl = returnUrl }, null)%>
                </li>
                <li>
                    <%: Html.ActionLink("注册", "Register", "Account", new { returnUrl = returnUrl }, null)%>
                </li>
                <%
                    }
                    else
                    {
                        GooCooServer.IDAO.IUserDAO userDAO = GooCooServer.DAO.DAOFactory.createDAO("UserDAO") as GooCooServer.IDAO.IUserDAO;
                        GooCooServer.Entity.User user = userDAO.Get(userSessionID);
                %>
                <li>
                    <%: Html.ActionLink(user.Name, "Index", "PersonalInfo") %>
                </li>
                <li>
                    <%: Html.ActionLink("注销", "LogOut", "Account", new { returnUrl = Request.RawUrl }, null)%>
                </li>
                <% 
                    }
                %>
            </ul>

        </div>
    </div>
</div>
