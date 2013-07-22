using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using GooCooServer.Entity;

namespace GooCooServer.IDAO 
{
    public interface IBookInfoDAO : IBaseDAO
    {
        int GetCountByIsbn(String isbn);
        List<BookInfo> GetByIsbn(String isbn, int from = 0, int count = 0);
        int GetCountByName(String name);
        List<BookInfo> GetByName(String name, int from = 0, int count = 0);
        int GetCountByTag(String tag);
        List<BookInfo> GetByTag(String tag, int from = 0, int count = 0);
        int GetCountByKeyWord(String keyWord);
        List<BookInfo> GetByKeyWord(String keyWord, int from = 0, int count = 0);

        BookInfo Get(String isbn);
        BookInfo Add(BookInfo book);
        void Del(String isbn);
        void Set(BookInfo book);   
    }
}