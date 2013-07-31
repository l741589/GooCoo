<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SearchViewLayout.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">            
    Index
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
                    <img class="media-object" src="<%:bookInfo.Photourl %>">
                    <div id="two-button">
                        <button class="btn btn-small" type="button"
                            <%if (isLoggedOn){ %>
                            onclick="orderBook(<%:bookInfo.Isbn %>)"
                            <%} else { %>
                             onclick="changeToLoginPage()"
                            <%} %>
                            >预定</button>

                        <button class="btn btn-primary btn-small" type="button" 
                            <%if (isLoggedOn){ %>
                            onclick="favorBook(<%:bookInfo.Isbn %>)"
                            <%} else { %>
                            onclick="changeToLoginPage()"
                            <%} %>
                            >收藏</button>
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

            <textarea class="span8" id="comment-content" rows="5"></textarea>
            <button class="btn btn-primary" id="add-comment-button" type="button">Large button</button>



            <%
                foreach ( GooCooWeb.Models.BookInfoModels.CommentRecordModel commentRecord in bookInfoRecord.TopComments){
             %>
            <p>
                <%:commentRecord.CommentMaker.Name%>:<%:commentRecord.Content.Content %>
            </p>
            <%
                }
             %>
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
        function addComment()
        {

        }
        function orderBook(isbn)
        {
            alert("order" + isbn);
        }
        function favorBook(isbn)
        {
            alert("favor" + isbn);
        }
    </script>
</asp:Content>
