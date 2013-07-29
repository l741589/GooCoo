using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GooCooServer.Entity;

namespace GooCooServer.IDAO
{
    public interface IUser_Comment
    {
        //User_Comment Add(String user_id,int comment_id);
        //bool Del(String user_id, int comment_id);
        List<Comment> GetComment(String ID);
        User GetUser(int comment_id);
    }
}
