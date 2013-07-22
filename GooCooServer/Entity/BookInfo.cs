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
        private String[] tags;
        private long timestamp;

        public long Timestamp
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

        public String[] Tags
        {
            get { return tags; }
            set { tags = value; }
        }

        public static bool operator ==(BookInfo lhs, BookInfo rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(BookInfo lhs, BookInfo rhs)
        {
            return !lhs.Equals(rhs);
        }

        public override bool Equals(object obj)
        {
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