<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

    <div class="span3" style="border:thin solid white; padding-left:15px">
        <h4>新书速递</h4>

        
        <%foreach(GooCooServer.Entity.BookInfo bookInfo in ViewBag.RecommentBooks){ %>
        <div class="media" style="padding-bottom:30px; border-bottom:thin dashed #d5d5d5">
            <a class="pull-left" href="#">
                <img class="media-object" src="<%:bookInfo.Photourl %>">
            </a>
            <div class="media-body">
                <h5 class="media-heading"><%:bookInfo.Name %></h5>
            </div>
        </div>
        <%} %>            
    </div>