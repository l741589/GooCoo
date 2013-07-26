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
    }
}
