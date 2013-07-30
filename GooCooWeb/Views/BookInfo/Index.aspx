<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SearchViewLayout.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">            
    Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% 
        GooCooWeb.Models.BookInfoModels.BookInfoRecordModel bookInfoRecord = ViewBag.BookInfoRecord;
        GooCooServer.Entity.BookInfo bookInfo = bookInfoRecord.Bookinfo;        
     %>

    <div class="row">
        <div class="span12">
            <h2><%:bookInfo.Name %></h2>
        </div>
    </div>

    <div class="span12">
        <div class="span7">            
            <dl class="booklist">
                <dt>
                    ISBN：
                </dt>            
                <dd>
                    <%:bookInfo.Isbn %>
                </dd>
            </dl>
            <dl class="booklist">
                <dt>作者：</dt>
                <dd></dd>
            </dl>
            <dl class="booklist">
                <dt>书籍信息：</dt>
                <dd><%:bookInfo.Summary %></dd>                   
            </dl>            
        </div>
        <div class="span5">
            <p>
                <img src="<%:bookInfo.Photourl %>" />
            </p>
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
            width: 18%;
            height: 24px;
            color: rgb(51, 51, 51);
            font-weight: bold;
        }
        .booklist dd {
            text-align: left;
            float: right;
            width: 81%;
            padding: 0px;
        }

        dt, dd {
            padding: 0px;
            margin: 0px auto;
            list-style: none outside none;
        }
    </style>
</asp:Content>


<asp:Content ID="Content5" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="OtherJavascript" runat="server">
</asp:Content>
