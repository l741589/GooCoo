using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace GooCooServer.Utility
{
    public class Util
    {
        public static T CloneEntity<T>(Object obj){
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
                        target.SetValue(ret, property.GetValue(obj));
                    }
                }
                catch (AmbiguousMatchException) { }
            }
            return ret;
        }
    }
}