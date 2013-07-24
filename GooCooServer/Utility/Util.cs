using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace GooCooServer.Utility
{
    public class Util
    {
        //对所有属性均采用“=”复制，若想修改值请自己复制属性值。
        public static T CloneEntity<T>(Object obj)
        {
            if (obj == null) return default(T);
            Type type = typeof(T);
            T ret = (T)type.GetConstructor(new Type[0]).Invoke(new Object[0]);
            PropertyInfo[] properties = obj.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                try
                {
                    PropertyInfo target = type.GetProperty(property.ToString());
                    if (target != null)
                    {
                        object value = property.GetValue(obj);
                        target.SetValue(ret, value);
                    }
                }
                catch (AmbiguousMatchException) { }
            }
            return ret;
        }

        public static T Merge<T>(T target, T obj, bool cover = false)
        {
            Type type = typeof(T);
            PropertyInfo[] properties = obj.GetType().GetProperties();
            foreach (var property in properties)
            {
                var left = property.GetValue(target);
                var right = property.GetValue(obj);
                if (left == null) property.SetValue(target, right);
                else
                    if (right != null && cover) property.SetValue(target, right);
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
    }
}