using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GooCooServer.Entity;

namespace GooCooServer.IDAO
{
    public interface IBook_BookInfoDAO : IBaseDAO
    {
        //Book_BookInfo Add(String isbn, int id);
        //bool Del(String isbn, int id);
        BookInfo GetBookInfo(int book_id);
        List<Book> GetBook(String isbn);
        int Count(String isbn);

        int GetAvaliableBookNumber(String isbn);
    }
}
