using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GooCooAdmin.Utility
{
    public class Util : GooCooServer.Utility.Util
    {
        private static List<SolidColorBrush> brushs = new List<SolidColorBrush>();

        public static SolidColorBrush GetBrush(Color color)
        {
            foreach (var e in brushs)
            {
                if (e.Color.Equals(color)) return e;
            }
            SolidColorBrush b = new SolidColorBrush(color);
            brushs.Add(b);
            return b;
        }

        public class KeyValuePair
        {
            private Dictionary<String, String> cv;
            public KeyValuePair()
            {
                cv = new Dictionary<string, string>();
            }

            public KeyValuePair(Dictionary<String, String> cv)
            {
                this.cv = cv;
            }

            public static implicit operator Dictionary<String, String>(KeyValuePair obj)
            {
                if (obj==null) return null;
                return obj.cv;
            }

            public KeyValuePair Add(String key, String value)
            {
                cv.Add(key, value);
                return this;
            }

            public async Task<String> Post(String url)
            {
                return await HttpHelper.Post(url, cv);
            }
        }

        public static KeyValuePair CreateContentValue()
        {
            return new KeyValuePair();
        }
    }
}
