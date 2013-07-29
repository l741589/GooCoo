using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GooCooServer.Entity;

namespace GooCooWeb.Models.BookInfoModels
{
    public class CommentRecordModel
    {
        public Comment Content { get; set; }        //评论内容
        public User CommentMaker { get; set; }      //评论者
    }
}