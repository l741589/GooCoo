<<<<<<< HEAD
﻿using System;
=======
using System;
>>>>>>> origin/LYZ
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
<<<<<<< HEAD
        private long timestamp;
=======
        private DateTime timestamp;
>>>>>>> origin/LYZ

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

<<<<<<< HEAD
        public long Timestamp
=======
        public DateTime Timestamp
>>>>>>> origin/LYZ
        {
            get { return timestamp; }
            set { timestamp = value; }
        }

        public static bool operator ==(Comment lhs, Comment rhs)
        {
<<<<<<< HEAD
            return lhs.Equals(rhs);
=======
            return Object.Equals(lhs, rhs);
>>>>>>> origin/LYZ
        }

        public static bool operator !=(Comment lhs, Comment rhs)
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