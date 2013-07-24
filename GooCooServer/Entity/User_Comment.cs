using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GooCooServer.Entity
{
    //Comment-->User
    public class User_Comment
    {
        private String user_id;//┓
        private int comment_id;//┛primary key
        
        public String User_id
        {
            get { return user_id; }
            set { user_id = value; }
        }
        

        public int Comment_id
        {
            get { return comment_id; }
            set { comment_id = value; }
        }
    }
}