using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GooCooServer.Entity;

namespace GooCooWeb.Models.BookInfoModels
{
    public class BookInfoRecordModel
    {
        public BookInfo Bookinfo { get; set; }
        public int AvailableCount { get; set; }     //剩余数量(出去借走、预定的书籍之后)
        public int OrderCount { get; set; }         //预定数量
        public List<BookRecordModel> Books { get; set; }

        //Comment
        public List<CommentRecordModel> TopComments { get; set; }       //最新评论,其余评论使用ajax获取
        public int TotalCommentCount { get; set; }        
    }
}