using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GooCooServer.Entity
{
    //实体集表示某一条评论
    public class Comment
    {
        private int id;//primary key
        private String content;
        private long timestamp;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public String Content
        {
            get { return content; }
            set { content = value; }
        }

        public long Timestamp
        {
            get { return timestamp; }
            set { timestamp = value; }
        }

        public static bool operator ==(Comment lhs, Comment rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Comment lhs, Comment rhs)
        {
            return !lhs.Equals(rhs);
        }

        public override bool Equals(object obj)
        {
            if (obj is Comment)
            {
                return Id == (obj as Comment).Id;
            }
            else return false;
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }
    }
}