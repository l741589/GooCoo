using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using GooCooServer.Utility;

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
        public String Orderer_id { get; set; }
        public int? Count { get; set; }

        public Book this[String owner]
        {
            get
            {
                Book ret = null;
                foreach (Book b in Books)
                {
                    if (b.Owner == owner)
                    {
                        ret = b;
                        break;
                    }
                }
                return ret;
            }
        }

        public Book this[int id]
        {
            get
            {
                Book ret = null;
                foreach (Book b in Books)
                {
                    if (b.Id == id)
                    {
                        ret = b;
                        break;
                    }
                }
                return ret;
            }
        }

        public int BorrowedBook
        {
            get
            {
                if (Books == null) return 0;
                int ret = 0;
                foreach (var e in Books)
                {
                    if (e.Owner != null) ++ret;
                }
                return ret;
            }
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
            s = Mark + " " + s;
            if (Orderers != null && Books != null && Count != null) s += "\n" + Orderers.Count + "/" + BorrowedBook + "/" + Count;
            if (Orderer_id != null) s += "\n" + "Orderer: " + Orderer_id;
            if (Books != null)
            {
                s += "\nOwners: ";
                foreach (var e in Books)
                {
                    if (e.Owner != null)
                        s += "[" + e.Id + ":" + e.Owner + "] ";
                    else s += "[" + e.Id + "]";
                }
            }
            s += "\n----------------------------------";
            return s;
        }

        public bool Filled
        {
            get
            {
                if (Books == null) return false;
                if (Orderers == null) return false;
                if (Orderers.Count > 0 && Orderer_id == null) return false;
                return true;
            }
        }
    }
}