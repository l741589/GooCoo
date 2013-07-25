using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GooCooServer.Entity
{
    //Book-->BookInfo
    public class Book_BookInfo
    {
        private int book_id;//┓
        private String isbn;//┛primary key
        private long timestamp;

        public long Timestamp
        {
            get { return timestamp; }
            set { timestamp = value; }
        }

        public int Book_id
        {
            get { return book_id; }
            set { book_id = value; }
        }        

        public String Isbn
        {
            get { return isbn; }
            set { isbn = value; }
        }
    }
}