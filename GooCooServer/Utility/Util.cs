using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using GooCooServer.Entity;
using System.Xml;
using System.Net;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;

namespace GooCooServer.Utility
{
    public class Util
    {

        //对所有属性均采用“=”复制，若想修改值请自己复制属性值。
        public static T CloneEntity<T>(T obj)
        {
            return CloneEntity<T>(obj as object);
        }
        
        //对所有属性均采用“=”复制，若想修改值请自己复制属性值。
        public static T CloneEntity<T>(object obj)
        {
            if (obj == null) return default(T);
            Type type = typeof(T);
            T ret = (T)type.GetConstructor(new Type[0]).Invoke(new Object[0]);
            PropertyInfo[] properties = obj.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.CanRead)
                {
                    try
                    {
                        var s = property.ToString();
                        s = s.Substring(s.LastIndexOf(' ') + 1);
                        PropertyInfo target = type.GetProperty(s);
                        if (target != null&&target.CanWrite)
                        {
                            object value = property.GetValue(obj);
                            target.SetValue(ret, value);
                        }
                    }
                    catch (AmbiguousMatchException) { }
                }
            }
            return ret;
        }

        public static T Merge<T>(T target, T obj, bool cover = false)
        {
            Type type = typeof(T);
            PropertyInfo[] properties = obj.GetType().GetProperties();
            foreach (var property in properties)
            {
                if (property.CanRead && property.CanWrite)
                {
                    var left = property.GetValue(target);
                    var right = property.GetValue(obj);
                    if (left == null) property.SetValue(target, right);
                    else
                        if (right != null && cover) property.SetValue(target, right);
                }
            }
            return target;
        }

        public static T Merge<T>(T target, Object obj, bool cover = false)
        {
            Type type = typeof(T);
            PropertyInfo[] properties = obj.GetType().GetProperties();
            foreach (var property in properties)
            {
                if (property.CanRead)
                {
                    try
                    {
                        var s = property.ToString();
                        s = s.Substring(s.LastIndexOf(' ') + 1);
                        PropertyInfo targetprop = type.GetProperty(s);
                        if (targetprop!=null&&targetprop.CanRead && targetprop.CanWrite)
                        {
                            var left = targetprop.GetValue(target);
                            var right = property.GetValue(obj);
                            if (left == null) targetprop.SetValue(target, right);
                            else
                                if (right != null && cover) targetprop.SetValue(target, right);
                        }
                    }
                    catch (AmbiguousMatchException) { }
                }
            }
            return target;
        }

        public static String EncodeJson(object obj)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(obj);
        }

        public static String EncodeJson(params Object[] objects)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            String[] ss = new String[objects.Length];
            for (int i = 0; i < objects.Length; ++i)
            {
                ss[i] = jss.Serialize(objects[i]);
            }
            return jss.Serialize(ss);
        }

        public static T DecodeJson<T>(String input)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<T>(input);
        }

        public static Object DecodeJson(String input, Type type)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize(input, type);
        }

        public static Object[] DecodeJson(String input, params Type[] types)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            String[] ss=jss.Deserialize<String[]>(input);
            Object[] ret = new Object[ss.Length];
            for (int i = 0; i < ss.Length; ++i)
            {
                ret[i] = jss.Deserialize(ss[i], types[i]);
            }
            return ret;
        }

        public static async Task<BookInfo> GetBookFromInternet(String isbn)
        {
            XmlDocument xml = new XmlDocument();
            String path = Properties.Resources.URL_BOOKINFOAPI + isbn;
            String s = await Get(path);
            if (s == null||s=="bad isbn") return null;
            BookInfo book = new BookInfo();
            String name = null;
            String photourl = null;
            String summary = null;
            String author = null;
            String publisher = null;


            name = RegexParse(Properties.Resources.REGEX_NAME, s, "value");
            photourl = RegexParse(Properties.Resources.REGEX_PHOHOURL, s, "value");
            summary = RegexParse(Properties.Resources.REGEX_SUMMARY, s, "value");
            author = RegexParse(Properties.Resources.REGEX_AUTHOR, s, "value");
            publisher = RegexParse(Properties.Resources.REGEX_PUBLISHER, s, "value");
            if (isbn != null && name != null && photourl != null && summary != null)
            {
                book.Isbn = isbn;
                book.Name = name;
                book.Photourl = photourl;
                book.Summary = summary;
                //book.Author = root.SelectSingleNode("/entry/db:attribute[@name='author']/").InnerText;
                //book.Publisher = root.SelectSingleNode("/entry/db:attribute[@name='publisher']/").InnerText;
                book.Timestamp = default(DateTime);
                return book;
            }
            else return null;
        }
        

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

        static public async Task<String> Post(String path, Dictionary<String, String> args = null)
        {
            try
            {
                WebRequest request = WebRequest.Create(path);
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
            catch (System.Exception e)
            {
                return null;
            }
        }

        static public async Task<String> Get(String path, Dictionary<String, String> args = null)
        {
            try
            {
                string postData = BuildArgs(args);
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                WebRequest request = WebRequest.Create(path+"?"+byteArray);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";

                WebResponse response = await request.GetResponseAsync();


                response = request.GetResponse() as HttpWebResponse;
                Stream stream = response.GetResponseStream();
                StreamReader sr = null;
                sr = new StreamReader(stream,Encoding.UTF8);
                int count = 0;
                char[] leftBuffer = new char[2048];
                StringBuilder leftSb = new StringBuilder();
                while ((count = sr.Read(leftBuffer, 0, leftBuffer.Length)) > 0)
                {
                    String str = new String(leftBuffer, 0, count);
                    leftSb.Append(str);
                }
                sr.Close();

                return leftSb.ToString();
            }
            catch (System.Exception e)
            {
                return null;
            }
        }

        public static String RegexParse(String regex, String s, String group = null)
        {
            try
            {
                Regex r = new Regex(regex);
                Match m = r.Match(s);
                if (group == null) return m.ToString();
                var g = m.Groups[group];
                return g.ToString();
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }
    }
}