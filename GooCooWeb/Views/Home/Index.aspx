<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>
<html lang="en">
  <head>
    <meta charset="utf-8">
    <title>Google Camp - GooCoo</title>

    <!-- Le styles -->
    <link href="<%: Url.Content("~/Content/bootstrap.min.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%: Url.Content("~/Content/bootstrap-responsive.min.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%: Url.Content("~/Content/HomePage.css") %>" rel="stylesheet" type="text/css" />

    <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
      <script src="//cdnjs.bootcss.com/ajax/libs/html5shiv/3.6.2/html5shiv.js"></script>
    <![endif]-->
  </head>

  <body>



    <!-- NAVBAR
    ================================================== -->
    <div class="navbar-wrapper">
      <!-- Wrap the .navbar in .container to center it within the absolutely positioned parent. -->
      <div class="container">

        <div class="navbar navbar-inverse">
          <div class="navbar-inner">
            <!-- Responsive Navbar Part 1: Button for triggering responsive navbar (not covered in tutorial). Include responsive CSS to utilize. -->
            <button type="button" class="btn btn-navbar" data-toggle="collapse" data-target=".nav-collapse">
              <span class="icon-bar"></span>
              <span class="icon-bar"></span>
              <span class="icon-bar"></span>
            </button>
            <a class="brand" href="#">GooCoo</a>
            <!-- Responsive Navbar Part 2: Place all navbar contents you want collapsed withing .navbar-collapse.collapse. -->
            <div class="nav-collapse collapse">
              <ul class="nav">
                <li class="active"><a href="/Home/Index">首页</a></li>
                <li><a href="/SearchView/Index">搜索</a></li>

                
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
                <li class="dropdown">
                  <a href="#" class="dropdown-toggle" data-toggle="dropdown"><%: user.Name %> <b class="caret"></b></a>
                  <ul class="dropdown-menu">
                    <li><%: Html.ActionLink("个人信息", "Index", "PersonalInfo") %></li>
                    <li><%: Html.ActionLink("借阅列表", "BorrowInfo", "PersonalInfo") %></li>
                    <li><%: Html.ActionLink("捐书信息", "DonateInfo", "PersonalInfo") %></li>
                    <li><%: Html.ActionLink("我的书架", "CollectInfo", "PersonalInfo") %></li>
                    <li><%: Html.ActionLink("预定信息", "PreorderInfo", "PersonalInfo") %></li>
                  </ul>
                </li>
                <li>
                    <%: Html.ActionLink("注销", "LogOut", "Account", new { returnUrl = Request.RawUrl }, null)%>
                </li>
                <% 
                    }
                %>

              </ul>
            </div><!--/.nav-collapse -->
          </div><!-- /.navbar-inner -->
        </div><!-- /.navbar -->

      </div> <!-- /.container -->
    </div><!-- /.navbar-wrapper -->



    <!-- Carousel
    ================================================== -->
    <div id="myCarousel" class="carousel slide">
      <div class="carousel-inner">
        <div class="item">
          <img src="/Content/image/slide-02.jpg" alt="">
          <div class="container">
            <div class="carousel-caption">
              <h1>GooCoo</h1>
              <p class="lead">Google Camp - GooCoo 图书计划一角， 存放有各类教材和课外书籍， 供大家借阅。</p>
              <a class="btn btn-large btn-primary" href="/SearchView/Index">体验一下</a>
            </div>
          </div>
        </div>

        <div class="item active">
          <img src="/Content/image/slide-01.jpg" alt="">
          <div class="container">
            <div class="carousel-caption">
              <h1>Google Camp 会议区</h1>
              <p class="lead">济事楼518 Google Camp 会议区， 用于平时的各类会议和学习小组讨论。</p>
              <a class="btn btn-large btn-primary" href="#">加入我们</a>
            </div>
          </div>
        </div>
        
        <div class="item">
          <img src="/Content/image/slide-04.jpg" alt="">
          <div class="container">
            <div class="carousel-caption">
              <h1>Google Camp 图书柜</h1>
              <p class="lead">Google Camp - GooCoo 图书计划课外科技书籍部分， 存有大量与计算机相关课外暑假供大家借阅， 只对Google Camp内部开放。</p>
              <a class="btn btn-large btn-primary" href="#">Get Started!</a>
            </div>
          </div>
        </div>

        <div class="item">
          <img src="/Content/image/slide-03.jpg" alt="">
          <div class="container">
            <div class="carousel-caption">
              <h1>Google Camp 涂鸦墙</h1>
              <p class="lead">Google Camp 用于吐槽的涂鸦墙。</p>
              <a class="btn btn-large btn-primary" href="#">成为Gcer</a>
            </div>
          </div>
        </div>
      </div>
      <a class="left carousel-control" href="#myCarousel" data-slide="prev">&lsaquo;</a>
      <a class="right carousel-control" href="#myCarousel" data-slide="next">&rsaquo;</a>
    </div><!-- /.carousel -->



    <!-- Marketing messaging and featurettes
    ================================================== -->
    <!-- Wrap the rest of the page in another container to center all the content. -->

    <div class="container marketing">

      <!-- Three columns of text below the carousel -->
        
      <%foreach(GooCooServer.Entity.BookInfo bookInfo in ViewBag.RecommentBooks) { %>
      <div class="row">
        <div class="span4">
          <img class="img-polaroid" style="width:120px; height:145px;" src="<%: bookInfo.Photourl %>">
          <h2><%: bookInfo.Name %></h2>
          <%const int description_length = 100; %>
          <p><%:bookInfo.Summary.Length > description_length? bookInfo.Summary.Substring(0,description_length) + "..." : bookInfo.Summary %></p>
          <p><a class="btn" href="/BookInfo?isbn=<%: bookInfo.Isbn %>">View details &raquo;</a></p>
        </div><!-- /.span4 -->
       <% } %>

      <hr class="featurette-divider">

      <!-- FOOTER -->
      <footer>
        <p>&copy; Google Camp - GooCoo</p>
      </footer>

    </div><!-- /.container -->



    <!-- Le javascript
    ================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
    <script src="/Scripts/jquery-2.0.0.min.js"></script>
    <script src="/Scripts/bootstrap.min.js"></script>
    <script>
        !function ($) {
            $(function () {
                // carousel demo
                $('#myCarousel').carousel()
            })
        }(window.jQuery)
    </script>
    <script src="/Scripts/holder.js"></script>
  </body>
</html>
