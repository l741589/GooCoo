using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GooCooServer.Entity;

namespace GooCooServer.IDAO
{
    public interface IBookDAO : IBaseDAO
    {
        //Book Add(Book book);
        Book Add();
        Book Add(BookInfo bookInfo);
        void Del(int id);
        //bool Set(Book book);
        Book Get(int id);
    }
}
