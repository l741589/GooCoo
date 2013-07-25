using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GooCooServer.Entity
{
    public class Session
    {
        String user_id;
        String session_id;

        public String User_id
        {
            get { return user_id; }
            set { user_id = value; }
        }        

        public String Session_id
        {
            get { return session_id; }
            set { session_id = value; }
        }
    }
}