using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GooCooServer.Entity
{
    //Comment-->Book
    public class BookInfo_Comment
    {
        private String book_isbn;//┓
        private int comment_id;  //┛primary key

        public String Book_isbn
        {
            get { return book_isbn; }
            set { book_isbn = value; }
        }
        

        public int Comment_id
        {
            get { return comment_id; }
            set { comment_id = value; }
        }
    }
}