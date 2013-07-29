using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GooCooServer.Entity;

namespace GooCooServer.IDAO
{
    public interface IUser_BookDAO : IBaseDAO
    {
        void Add(String user_id, int book, User_Book.ERelation relation = User_Book.ERelation.BORROW);
        bool Del(int book, User_Book.ERelation relation = User_Book.ERelation.BORROW);
        List<Book> GetBook(String user_id, User_Book.ERelation relation = User_Book.ERelation.BORROW);
        User GetUser(int book, User_Book.ERelation relation = User_Book.ERelation.BORROW);
        User_Book Get(String user_id, int book, User_Book.ERelation relation = User_Book.ERelation.BORROW);
    }
}
