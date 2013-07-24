using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GooCooServer.Entity;

namespace GooCooServer.IDAO
{
    interface IUser_BookInfoDAO : IBaseDAO
    {
        void Add(String isbn, String user_id, User_BookInfo.ERelation relation = User_BookInfo.ERelation.ORDER);
        int Del(String isbn, String user_id, User_BookInfo.ERelation relation = User_BookInfo.ERelation.ORDER);
        List<User> GetUser(String isbn, User_BookInfo.ERelation relation = User_BookInfo.ERelation.ORDER);
        User GetAvaliableUser(String isbn);
        List<BookInfo> GetBookInfo(String user_id, User_BookInfo.ERelation relation = User_BookInfo.ERelation.ORDER);
        User_BookInfo Get(String isbn, String user_id, User_BookInfo.ERelation relation = User_BookInfo.ERelation.ORDER);
    }
}
