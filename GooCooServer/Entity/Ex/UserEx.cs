using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace GooCooServer.Entity.Ex
{
    public class UserEx : User
    {
        private List<String> holds;
        private List<String> orders;

        public List<String> Holds
        {
            get { return holds; }
            set { holds = value; }
        }        

        public List<String> Orders
        {
            get { return orders; }
            set { orders = value; }
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
            return Id + " " + Name + " " + Authority;
        }
    }
}