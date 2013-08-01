<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

    <div class="span3" style="border:thin solid white; padding-left:15px">
        <h4>新书速递</h4>

        
        <%foreach(GooCooServer.Entity.BookInfo bookInfo in ViewBag.RecommentBooks){ %>
        <div class="media" style="padding-bottom:30px; border-bottom:thin dashed #d5d5d5">
            <a class="pull-left" target="_blank" href="<%:Url.Action("Index","BookInfo",new {isbn= bookInfo.Isbn}) %>">
                <img class="media-object" src="<%:bookInfo.Photourl %>">
            </a>
            <div class="media-body">
                <a class="book-title" target="_blank" href="<%:Url.Action("Index","BookInfo",new {isbn= bookInfo.Isbn}) %>">
                    <h5 class="media-heading"><%:bookInfo.Name %></h5>
                </a>
                <p class="book-author">作者：<%:bookInfo.Author %>></p>
                <%const int description_length = 100; %>
                <p class="book-description"><%:bookInfo.Summary.Length > description_length? bookInfo.Summary.Substring(0,description_length) + "..." : bookInfo.Summary %></p>
            </div>
        </div>
        <%} %>            
    </div>