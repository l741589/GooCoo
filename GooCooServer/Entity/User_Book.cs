using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GooCooServer.Entity
{
    //Book,relation-->User
    public class User_Book
    {
        public enum ERelation { BORROW, DONATE }

        private String user;       //┓
        private int book;          //┣primary key
        private ERelation relation;//┛
        private DateTime timestamp;

        public ERelation Relation
        {
            get { return relation; }
            set { relation = value; }
        }

        public DateTime Timestamp
        {
            get { return timestamp; }
            set { timestamp = value; }
        } 

        public String User
        {
            get { return user; }
            set { user = value; }
        }

        public int Book
        {
            get { return book; }
            set { book = value; }
        }
    }
}
