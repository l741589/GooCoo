<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SearchViewLayout.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%:ViewBag.BookInfoRecord.Bookinfo.Name %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% 
        GooCooWeb.Models.BookInfoModels.BookInfoRecordModel bookInfoRecord = ViewBag.BookInfoRecord;
        GooCooServer.Entity.BookInfo bookInfo = bookInfoRecord.Bookinfo;        
        List<GooCooWeb.Models.BookInfoModels.BookRecordModel> bookRecordList = bookInfoRecord.Books;


        bool isLoggedOn = false;
        string userSessionID = (string)Session["UserSessionID"];
        if (userSessionID != null)
        {
            isLoggedOn = true;
        }        
     %>
    
    <div class="row-fluid">
        <div class="span9">
            <h2><%:bookInfo.Name %></h2>
                <div class="media">
                <div class="pull-left">
                    <a target="_blank" href="<%:GooCooServer.Entity.BookInfo.getLargePhotoUrl(bookInfo) %>">
                        <img class="media-object" src="<%:GooCooServer.Entity.BookInfo.getMidPhotoUrl(bookInfo) %>">
                    </a>
                    <div id="two-button">
                        <%if (bookInfoRecord.AvailableCount == 0){ %>
                        <button class="btn btn-small" type="button"  data-loading-text="Loading..." id="order-button"
                            onclick=
                                <%if (isLoggedOn){ %>
                                    <%if (ViewBag.HasOrder){ %>
                                        "cancelOrderBook(<%:bookInfo.Isbn %>)"
                                    <%} else { %>
                                        "orderBook(<%:bookInfo.Isbn %>)"
                                    <% } %>
                                <%} else { %>
                                    "changeToLoginPage()"
                                <%} %>
                            >
                            <%if (isLoggedOn){ %>
                                    <%if (ViewBag.HasOrder){ %>
                                        取消预定
                                    <%} else { %>
                                        预定
                                    <% } %>
                                <%} else { %>
                                    预定
                                <%} %>

                        </button>
                        <%} %>

                        <button class="btn btn-primary btn-small" type="button" data-loading-text="Loading..." id="favor-button"
                            onclick=
                                <%if (isLoggedOn){ %>
                                    <%if (ViewBag.HasFavor){ %>
                                        "cancelFavorBook(<%:bookInfo.Isbn %>)"
                                    <%} else { %>
                                        "favorBook(<%:bookInfo.Isbn %>)"
                                    <%} %>
                                <%} else { %>
                                    "changeToLoginPage()"
                                <%} %>
                            >
                             <%if (isLoggedOn){ %>
                                    <%if (ViewBag.HasFavor){ %>
                                        取消收藏
                                    <%} else { %>
                                        收藏
                                    <%} %>
                                    <%} else { %>
                                        收藏
                                    <%} %>
                        </button>
                    </div>
                </div>
                <div class="media-body">
                    
                    <dl class="booklist">
                        <dt>ISBN：</dt>            
                        <dd><%:bookInfo.Isbn %></dd>
                    </dl>
                    <dl class="booklist">
                        <dt>作者：</dt>
                        <dd><%:bookInfo.Author %></dd>
                    </dl>
                    <dl class="booklist">
                        <dt>出版社：</dt>
                        <dd><%:bookInfo.Publisher %></dd>
                    </dl>
                    <dl class="booklist">
                        <dt>书籍信息：</dt>
                        <dd><%:bookInfo.Summary %></dd>                   
                    </dl> 
                </div>
            </div>
        </div>
    </div>
    <div class="row-fluid">
        <div class="span9">
            <h4>书籍状态：</h4>
            <table class="table">
                <thead>
                    <tr>
                        <td>编号</td>
                        <td>书籍状态</td>
                    </tr>
                </thead>

                <tbody>
                    <% foreach (GooCooWeb.Models.BookInfoModels.BookRecordModel bookRecord in bookRecordList ){ %>
                    <tr>
                        <td><%:bookRecordList.IndexOf(bookRecord) + 1 %></td>
                        <td>
                            <%if (bookRecord.CurrentCondition == GooCooWeb.Models.BookInfoModels.BookCondition.AVAILABLE){ %>
                                <p style="color:green">可借</p>
                            <%} else { %>
                                <p>借出-应还日期：<%:GooCooServer.Entity.Book.getReturnTime(bookRecord.AvailableTime) %></p>
                            <%} %>
                            
                        </td>
                    </tr>
                    <%} %>
                </tbody>

            </table>
        </div>
    </div>

    <div class="row-fluid">
        <div class="span9">
            <h4>评论：</h4>
            <div class="row-fluid">
            <textarea class="span10" id="comment-content" rows="5" style="margin-left:20px"></textarea>
            <button class="btn btn-primary" id="add-comment-button" type="button" data-loading-text="Loading..."
                 onclick=
                    <%if (isLoggedOn){ %>
                        "addComment(<%:bookInfo.Isbn %>)"
                    <%} else { %>
                        "changeToLoginPage()"
                    <%} %>
                >确定</button>
            </div>
                <table class="table table-striped span9" >
                    <tbody id="comment-table-body">
                <%
                    foreach ( GooCooWeb.Models.BookInfoModels.CommentRecordModel commentRecord in bookInfoRecord.TopComments){
                 %>
                <tr>
                    <td>
                        <p><span class="comment-author"><%:commentRecord.CommentMaker.Name%></span>&nbsp;&nbsp;&nbsp;&nbsp;<span class="comment-time"><%:commentRecord.Content.Timestamp %></span></p>
                        <p><%:commentRecord.Content.Content %></p>                        
                    </td>
                </tr>
                    <%
                        }
                    %>
                        </tbody>
                </table>     
        </div>       
        
    </div>
   

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="OtherCssStyle" runat="server">
    <style type="text/css">
        
        .booklist 
        {
            width: 99%;
            float: left;
            clear: both;
            padding: 0px;
            margin: 0px auto;
        }
        .booklist dt {
            float: left;
            text-align: right;
            width: 15%;
            height: 24px;
            color: rgb(51, 51, 51);
            font-weight: bold;
        }
        .booklist dd {
            text-align: left;
            float: right;
            width: 85%;
            padding: 0px;
        }
        .comment-author {
            color:#8a8a8a;
            font:15px;
        }
        .comment-time {
            color:#aaaaaa;
        }
        dt, dd {
            padding: 0px;
            margin: 0px auto;
            list-style: none outside none;
        }
        #two-button {
            margin-top: 20px;
        }
    </style>
</asp:Content>


<asp:Content ID="Content5" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="OtherJavascript" runat="server">
    <script type="text/javascript">
        function changeToLoginPage()
        {            
            window.location = "<%:Url.Action("LogOn","Account", new {returnUrl = Request.RawUrl})%>" ;
        }
        function addComment(isbn)
        {
            var contentTextArea = document.getElementById("comment-content");
            var content = contentTextArea.value;
            $('#add-comment-button').button('loading');

            $.post('<%:Url.Action("AddComment","AjaxComment")%>', { 'content': content, 'isbn': isbn }, function (data) {
                $('#add-comment-button').button('reset');

                if (data.result) {
                    var tableBody = document.getElementById("comment-table-body");                    
                    var firstChild = tableBody.firstChild;

                    var tr = document.createElement("tr");
                    tr.innerHTML = "<td><p><p><span class='comment-author'>" + data.userName +"</span>&nbsp;&nbsp;&nbsp;&nbsp;<span class='comment-time'>"
                         + data.time + "</span></p>" + "<p>" + data.content + "</p>";                    
                    tableBody.insertBefore(tr, firstChild);

                    contentTextArea.value = "";

                }
                else {
                    alert("评论失败，请稍后再试");
                }
            });
        }
        function orderBook(isbn)
        {            
            //$.post('upload.php',{'sign':base64, 'score':currentScore}, function(data){ alert(data);});
            $.post('<%:Url.Action("AddOrder","AjaxBookInfoUser")%>', { 'isbn': isbn }, function (data)
            {
                if (data.result) {
                    alert("成功");
                }
                else {
                    alert("失败");
                }
            });
        }
        function cancelOrderBook(isbn)
        {
            $.post('<%:Url.Action("RemoveOrder","AjaxBookInfoUser")%>', { 'isbn': isbn }, function (data) {
                if (data.result) {
                    alert("成功");
                }
                else {
                    alert("失败");
                }
            });
        }
        function favorBook(isbn)
        {
            
            $.post('<%:Url.Action("AddFavor","AjaxBookInfoUser")%>', { 'isbn': isbn }, function (data) {
                if (data.result) {
                    alert("成功");
                }
                else {
                    alert("失败");
                }
            });
        }
        function cancelFavorBook(isbn)
        {
            $.post('<%:Url.Action("RemoveFavor","AjaxBookInfoUser")%>', { 'isbn': isbn }, function (data) {
                if (data.result) {
                    alert("成功");
                }
                else {
                    alert("失败");
                }
            });
        }
    </script>
</asp:Content>
