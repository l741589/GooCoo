using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GooCooServer.Exception;

namespace GooCooAdmin.Util
{
    public class ListHolder<T>
    {
        private Dictionary<String, List<T>> lists;

        private Dictionary<String, int> priorities;
            
        public ListHolder()
        {
            lists = new Dictionary<string, List<T>>();
            priorities = new Dictionary<string, int>();
        }

        public List<T> this[String key]
        {
            get { if (!lists.ContainsKey(key)) AddList(key); return lists[key]; }
            set { if (!lists.ContainsKey(key)) AddList(key, value); else lists[key] = value; }
        }

        public Dictionary<String, int> Priorities
        {
            get { return priorities; }
            set { priorities = value; }
        }        

        public List<T> AddList(String name, int priority = 0)
        {
            if (!lists.ContainsKey(name))
            {
                lists.Add(name, new List<T>());
                priorities.Add(name, priority);
                return null;
            }
            else
            {
                List<T> ret = lists[name];
                lists[name]=new List<T>();
                priorities[name] = priority;
                return ret;
            }
        }

        public List<T> AddList(String name,List<T> value , int priority = 0)
        {
            if (value == null) throw new BMException("");
            if (!lists.ContainsKey(name))
            {
                lists.Add(name, value);
                priorities.Add(name, priority);
                return null;
            }
            else
            {
                List<T> ret = lists[name];
                lists[name] = value;
                priorities[name] = priority;
                return ret;
            }
        }

        public void RemoveList(String name)
        {
            if (lists.ContainsKey(name))
            {
                lists.Remove(name);
                priorities.Remove(name);
            }
        }

        public void reset()
        {
            var keys = priorities.Keys;
            for (int i = 0; i < keys.Count();++i )
            {
                String key=keys.ElementAt(i);
                priorities[key] = 0;
            }
        }        

        public List<T> ToList()
        {
            List<T> ret = new List<T>();
            var plist = priorities.ToList<KeyValuePair<String, int>>();
            plist.Sort((lhs, rhs) => { return rhs.Value - lhs.Value; });
            foreach (var e in plist)
            {
                var list = lists[e.Key];
                for (int i = 0; i < list.Count; ++i)
                {
                    T f = list[i];
                    if (!ret.Contains(f))
                        ret.Add(f);
                }
            }
            return ret;
        }

    }
}
