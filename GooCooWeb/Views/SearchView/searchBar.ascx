<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>



    <form id="searchForm" action="<%:Url.Action("Index","SearchView")%>" method="get">
        <div class="input-prepend">
            <div class="btn-group">
                <button class="btn dropdown-toggle" data-toggle="dropdown">                    
                        <span id="searchTypeText"><%:ViewBag.SearchResult.SearchType %></span>                   
                    <span class="caret"></span>
                </button>
                <ul class="dropdown-menu">
                    <li onclick="changeSearchType('标题')"><a href="#">标题</a></li>
                    <li onclick="changeSearchType('ISBN')"><a href="#">ISBN</a></li>                    
                    <li onclick="changeSearchType('模糊')"><a href="#">模糊</a></li>                                                            
                </ul>
            </div>
            <input class="input-xlarge" placeholder="关键字" name="keyword" id="keyword" type="text" value="<%:ViewBag.SearchResult.Keyword %>" onkeypress="submitSearchForm(event)">
            <button class="btn" type="submit" >Search</button>
        </div>

        <input type="text" id="searchType" name="type" value="<%:ViewBag.SearchResult.SearchType %>" style="display:none"/>
        <input type="text" id="page" name="page" value="1" style="display:none" />
    </form>       
    


 <script type="text/javascript">
     function changeSearchType(type) {
         var text = document.getElementById("searchTypeText");
         text.innerHTML = type;

         var typeChoose = document.getElementById("searchType");
         typeChoose.value = type;
     }

     function submitSearchForm(oEvent)
     {
         if (oEvent.keyCode == 13) {
             var searchForm = document.getElementById("searchForm");
             var keyword = document.getElementById("keyword");
             var type = document.getElementById("searchType");
             var page = document.getElementById("page");

             //由于表单submit因button影响无法获取正确的参数，故更改location进行get请求
             window.location = "<%:Url.Action("Index","SearchView")%>" + "?keyword=" + keyword.value + "&type=" + type.value + "&page=" + page.value;
             //searchForm.submit();
         }
     }

     
    </script>