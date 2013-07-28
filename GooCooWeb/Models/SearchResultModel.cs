using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GooCooServer.Entity;

namespace GooCooWeb.Models
{
    public class SearchResultModel
    {
        //Const
        public const int recordPerPage = 20;

        //公用
        public bool HasSearch { get; set; }
        public string SearchType { get; set; }

        //搜索时使用
        public int ResultCount { get; set; }
        public int CurrentPage { get; set; }
        public int TotlaPage { get; set; }
        public List<BookInfo> Results { get; set; }

        //用于页码显示
        public int PageFrom { get; set; }
        public int PageTo { get; set; }
        public int PreviewPage { get; set; }
        public int NextPage{get;set;}

        
        
    }
}