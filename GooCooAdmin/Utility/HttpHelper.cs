﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GooCooAdmin.Utility
{
    class HttpHelper
    {
        static String BuildArgs(Dictionary<String, String> args)
        {
            if (args == null) return "";
            String s = "";
            Boolean start = true;
            Dictionary<String, String>.KeyCollection keys = args.Keys;
            foreach (String key in keys)
            {
                if (start) start = false;
                else s += "&";
                s += key + "=" + args[key];
            }
            return s;
        }

        static public async Task<String> Post(String path, Dictionary<String,String> args = null)
        {
            WebRequest request = WebRequest.Create("http://localhost:"+Properties.Settings.Default.PORT+"/"+path);
            request.Method = "POST";
            string postData = BuildArgs(args);
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse response = await request.GetResponseAsync();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            Console.Write(responseFromServer);
            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromServer;
        }
    }
}