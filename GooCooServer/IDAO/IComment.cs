using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GooCooServer.Entity;
using System.Collections.ObjectModel;

namespace GooCooServer.IDAO
{
    public interface IComment : IBaseDAO
    {
        Comment Add(Comment comment);
        Comment Get(int id);
        void Del(int id);
        void Set(Comment comment);

        int GetCountByContent(String keyWord);
        List<Comment> GetByContent(String keyWord, int from = 0, int count = 0);
    }
}
