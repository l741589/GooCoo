using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GooCooAdmin.Util
{
    class ObjectItem
    {
        private String[] fields;       
        private Object obj;
        private char delims;

        public delegate string ToStringDelegate();

        public ToStringDelegate doToString = null;
       

        public ObjectItem(Object obj, params String[] fields) : this(obj,' ',fields)
        {
        }

        public ObjectItem(Object obj, ToStringDelegate doToString,char delims =' ')  : this(obj, delims)
        {
            this.doToString = doToString;
        }

        public ObjectItem(Object obj,char delims, params String[] fields)
        {
            this.fields = fields;
            this.obj = obj;
            this.delims = delims;
            this.doToString = null;
        }

        public Object Obj
        {
            get { return obj; }
        }

        public String[] Fields
        {
            get { return fields; }
            set { fields = value; }
        }

        public char Delims
        {
            get { return delims; }
            set { delims = value; }
        }

        override public String ToString()
        {
            if (doToString != null)
            {
                return doToString();
            }
            else
            {
                return DefaultToString();
            }
           
        }

        public String DefaultToString()
        {
            StringBuilder s = new StringBuilder();
            bool start = true;

            foreach (var e in fields)
            {
                if (start) start = false;
                else s.Append(" ");
                if (e == "Timestamp")
                {
                    s.Append(new DateTime((long)obj.GetType().GetProperty(e).GetValue(obj)).ToString("yyyy:MM:dd hh:mm:ss"));
                }
                else
                    s.Append(obj.GetType().GetProperty(e).GetValue(obj));
            }
            return s.ToString();
        }

    }
}
