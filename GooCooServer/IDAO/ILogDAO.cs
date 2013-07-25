using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GooCooServer.Entity;

namespace GooCooServer.IDAO
{
    interface ILogDAO
    {
        Log Get(int id);
        int GetCount();
        List<Log> GetBetween(long start_time, long end_time);
        List<Log> GetLogs(int from = 0, int count = 0);
    }
}
