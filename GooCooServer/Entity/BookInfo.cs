using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GooCooServer.Entity
{
    //实体集，表示某一类书的信息
    public class BookInfo
    {       
        private String isbn;//primary key
        private String name;
        private String summary;
        private String photourl;
        //加一个作者
        private DateTime timestamp;

        public String Photourl
        {
            get { return photourl; }
            set { photourl = value; }
        }

        public DateTime Timestamp
        {
            get { return timestamp; }
            set { timestamp = value; }
        }

        public String Isbn
        {
            get { return isbn; }
            set { isbn = value; }
        }

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        public String Summary
        {
            get { return summary; }
            set { summary = value; }
        }

        public static bool operator ==(BookInfo lhs, BookInfo rhs)
        {
            return Object.Equals(lhs, rhs);
        }

        public static bool operator !=(BookInfo lhs, BookInfo rhs)
        {
            return !Object.Equals(lhs, rhs);
        }

        public override bool Equals(object obj)
        {
            if (Object.Equals(obj, null)) return false;
            if (obj is BookInfo)
            {
                return Isbn == (obj as BookInfo).Isbn;
            }
            else return false;
        }

        public override int GetHashCode()
        {
            return isbn.GetHashCode();
        }
    }
}