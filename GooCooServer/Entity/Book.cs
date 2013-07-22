using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GooCooServer.Entity
{
    //实体集，表示某一本书
    public class Book
    {
        private int id;//primary key
        private long timestamp;

        public long Timestamp
        {
            get { return timestamp; }
            set { timestamp = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public static bool operator ==(Book lhs, Book rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Book lhs, Book rhs)
        {
            return !lhs.Equals(rhs);
        }

        public override bool Equals(object obj)
        {
            if (obj is Book)
            {
                return Id == (obj as Book).Id;
            }
            else return false;
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }
    }
}