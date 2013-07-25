using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GooCooServer.Entity
{
    //User---BookInfo
    public class User_BookInfo
    {
        public enum ERelation { ORDER, FAVOR }
        private String user;        //┓
        private String isbn;        //┣primary key
        private ERelation relation; //┛
        private long timestamp;        

        public ERelation Relation
        {
            get { return relation; }
            set { relation = value; }
        }

        public String User
        {
            get { return user; }
            set { user = value; }
        }

        public String Isbn
        {
            get { return isbn; }
            set { isbn = value; }
        }
        

        public long Timestamp
        {
            get { return timestamp; }
            set { timestamp = value; }
        }
    }
}