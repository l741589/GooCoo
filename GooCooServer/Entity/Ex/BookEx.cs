﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace GooCooServer.Entity.Ex
{
    public class BookEx : BookInfo
    {
        private String owner_id = null;
        private String orderer_id = null;
        private String mark = null;

        public String Mark
        {
            get { return mark; }
            set { mark = value; }
        }

        public String Owner_id
        {
            get { return owner_id; }
            set { owner_id = value; }
        }
        
        public String Orderer_id
        {
            get { return orderer_id; }
            set { orderer_id = value; }
        
        }
        public string ToString(String[] fields)
        {
            StringBuilder s = new StringBuilder();
            bool start = true;

            foreach (var e in fields)
            {
                if (start) start = false;
                else s.Append(" ");
                if (e == "Timestamp")
                {
                    s.Append(new DateTime((long)GetType().GetProperty(e).GetValue(this)).ToString("yyyy:MM:dd hh:mm:ss"));
                }
                else
                    s.Append(GetType().GetProperty(e).GetValue(this));
            }
            return s.ToString();
        }

        public override string ToString()
        {
            String s = Isbn + " " + Name + " " + new DateTime(Timestamp).ToString("yyyy:MM:dd hh:mm:ss");
            if (Mark != null) return Mark + " " + s;
            return s;
        }
    }
}