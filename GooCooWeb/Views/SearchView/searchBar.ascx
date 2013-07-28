<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>



    <form id="searchForm" action="<%:Url.Action("Index","SearchView")%>" method="get">
        <div class="input-prepend">
            <div class="btn-group">
                <button class="btn dropdown-toggle" data-toggle="dropdown">                    
                        <span id="searchTypeText">标题</span>                   
                    <span class="caret"></span>
                </button>
                <ul class="dropdown-menu">
                    <li onclick="changeSearchType('标题')"><a href="#">标题</a></li>
                    <li onclick="changeSearchType('ISBN')"><a href="#">ISBN</a></li>                    
                    <li onclick="changeSearchType('模糊')"><a href="#">模糊</a></li>                                                            
                </ul>
            </div>
            <input class="input-xlarge" placeholder="关键字" name="keyword" id="prependedDropdownButton" type="text">
            <button class="btn" type="submit" >Search</button>
        </div>

        <input type="text" id="searchType" name="type" value="标题" style="display:none"/>
        <input type="text" name="page" value="1" style="display:none" />
    </form>       
    


 <script type="text/javascript">
     function changeSearchType(type) {
         var text = document.getElementById("searchTypeText");
         text.innerHTML = type;

         var typeChoose = document.getElementById("searchType");
         typeChoose.value = type;
     }

     function submitSearchForm()
     {
         var searchForm = document.getElementById("searchForm");
         searchForm.submit();
     }
    </script>