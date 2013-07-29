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
            long tick = DateTime.Now.Ticks;
            Random random = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
            for (int i = 0; i < 64; i++)
                sessionID += sessionTable[random.Next(0, 61)];

            return sessionID;
        }
    }
}