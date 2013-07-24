using System;
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

        public static bool operator ==(Log lhs, Log rhs)
        {
            return Object.Equals(lhs, rhs);
        }

        public static bool operator !=(Log lhs, Log rhs)
        {
            return !Object.Equals(lhs, rhs);
        }

        public override bool Equals(object obj)
        {
            if (Object.Equals(obj, null)) return false;
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