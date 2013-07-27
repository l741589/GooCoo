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
        public static int ExpiredTime = 1;

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

        public static String createSession()
        {
            String sessionID = "";
            String sessionTable = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890abcdefghijklmnopqrstuvwxyz";
            Random random = new Random();
            for (int i = 0; i < 32; i++)
                sessionID += sessionTable[random.Next(0, 61)];

            random = new Random();
            for (int i = 0; i < 32; i++)
                sessionID += sessionTable[random.Next(0, 61)];

            return sessionID;
        }
    }
}