using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GooCooServer.Entity;

namespace GooCooServer.IDAO
{
    public interface IBook_CommentDAO
    {
        //User_Comment Add(String book_id, int comment_id);
        //bool Del(String book_id, int comment_id);
        int GetCommentCount(String isbn);
        List<Comment> GetComment(String isbn,int from = 0, int count = 0);
        //Book GetBook(int comment_id);
    }
}
