using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace GooCooServer.Entity.Ex
{
    public class BookEx : BookInfo
    {
        public class Book
        {
            public String Owner { get; set; }
            public int Id { get; set; }
        }

        public List<String> Orderers { get; set; }
        public List<Book> Books { get; set; }
        public String Mark { get; set; }
        public String Owner_id { get; set; }
        public String Orderer_id { get; set; }
        public int? Count { get; set; }
        
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
            s = Mark + " " + s;
            if (Orderers != null && Books != null && Count != null) s += "\n" + Orderers.Count + "/" + Books.Count + "/" + Count;
            if (Owner_id != null) s += "\n" + "Owner: " + Owner_id;
            if (Orderer_id != null) s += "\n" + "Orderer: " + Orderer_id;
            s += "\n----------------------------------";
            return s;
        }
    }
}