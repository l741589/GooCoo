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
    //实体集，表示某一条日志，由trigger插入，只接受查询
    public class Log
    {
        private int id;//primay key
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

        public static bool operator ==(Log lhs, Log rhs)
        {
<<<<<<< HEAD
            return lhs.Equals(rhs);
=======
            return Object.Equals(lhs, rhs);
>>>>>>> origin/LYZ
        }

        public static bool operator !=(Log lhs, Log rhs)
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
            if (obj is Log)
            {
                return Id == (obj as Log).Id;
            }
            else return false;
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }
    }
}