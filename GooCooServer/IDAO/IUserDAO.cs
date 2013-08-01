using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GooCooServer.Entity;
using System.Collections.ObjectModel;

namespace GooCooServer.IDAO
{
    public interface IUserDAO : IBaseDAO
    {
        //返回session
        String Login(String ID, String password);

        int GetCountByID(String ID);
        List<User> GetByID(String ID, int from = 0, int count = 0);
        int GetCountByName(String name);
        List<User> GetByName(String name, int from = 0, int count = 0);
        int GetCountByKeyWord(String keyWord);
        List<User> GetByKeyword(String keyWord, int from = 0, int count = 0);

<<<<<<< HEAD
=======
        bool isExist(String ID);

>>>>>>> origin/LYZ
        //对所有用户password置空
        User Get(String session);
        String Add(User user);
        void Set(String session, User user);
        //只有管理员有权限调用
        void Del(String ID);
    }
}
