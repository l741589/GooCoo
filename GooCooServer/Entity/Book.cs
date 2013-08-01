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
<<<<<<< HEAD
        private long timestamp;

        public long Timestamp
=======
        private DateTime timestamp;

        public DateTime Timestamp
>>>>>>> origin/LYZ
        {
            get { return timestamp; }
            set { timestamp = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

<<<<<<< HEAD
        public static bool operator ==(Book lhs, Book rhs)
        {
            return lhs.Equals(rhs);
=======
        public static DateTime getReturnTime(DateTime now)
        {
            return now.AddMonths(4);
        }

        public static bool operator ==(Book lhs, Book rhs)
        {
            return Object.Equals(lhs, rhs);
>>>>>>> origin/LYZ
        }

        public static bool operator !=(Book lhs, Book rhs)
        {
<<<<<<< HEAD
            return !lhs.Equals(rhs);
=======
            return !Object.Equals(lhs, rhs);
>>>>>>> origin/LYZ
        }

        public override bool Equals(object obj)
        {
<<<<<<< HEAD
=======
            if (Object.Equals(obj, null)) return false;
>>>>>>> origin/LYZ
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