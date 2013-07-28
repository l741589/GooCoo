using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GooCooServer.IDAO;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using GooCooServer.Exception;
using GooCooServer.Entity;

namespace GooCooServer.DAO
{
    public class Book_CommentDAO : BaseDAO, IBook_CommentDAO
    {
        public int GetCommentCount(String isbn)
        {
            SqlParameter myParam = new SqlParameter("@isbn", SqlDbType.Char);
            myParam.Value = isbn;
            string sqlQuery = "SELECT COUNT(comment_id) FROM BOOKINFO_COMMENT WHERE isbn = "+isbn+"";
            return dbManagerCount(myParam, sqlQuery);
        }

        public List<Comment> GetComment(String isbn, int from = 0, int count = 0)
        {
            using (connecter = new SqlConnection(connectStr))
            {
                try
                {
                    connecter.Open();
                }
                catch (System.Exception)
                {
                    throw new BMException("Create Connnect Error");
                }
                SqlParameter myParam = new SqlParameter("@isbn", SqlDbType.Char);
                myParam.Value = isbn;
                int to = from - 1;
                if (to < 0) to = 0;
                string sqlQuery = "SELECT TOP " + count + " * FROM BOOKINFO_COMMENT WHERE (isbn NOT IN (SELECT TOP " + to + " isbn FROM BOOKINFO_COMMENT)) AND isbn = "+isbn+"";
                SqlCommand myCommand = new SqlCommand(sqlQuery, connecter);
                myCommand.Parameters.Add(myParam);
                SqlDataReader sqlDataReader = myCommand.ExecuteReader();

                List<int> commentID = new List<int>();

                while (sqlDataReader.Read())
                {
                    commentID.Add((int)sqlDataReader[1]);
                }

                List<Comment> result = new List<Comment>();

                for (int i = 0; i < commentID.Count; i++)
                {
                    myParam = new SqlParameter("@id", SqlDbType.Int);
                    myParam.Value = commentID[i];
                    sqlQuery = "SELECT conent time FROM COMMENT WHERE id = @id";
                    myCommand = new SqlCommand(sqlQuery, connecter);
                    myCommand.Parameters.Add(myParam);
                    sqlDataReader.Close();
                    sqlDataReader = myCommand.ExecuteReader();

                    if (sqlDataReader.Read())
                    {
                        Comment comment = new Comment();
                        comment.Id = commentID[i];
                        comment.Content = (string)sqlDataReader[0];
                        comment.Timestamp = (DateTime)sqlDataReader[1];
                        result.Add(comment);
                    }
                }
                if (result != null)
                    return result;
                else
                    throw new BMException("BOOKINFO_COMMENT GETCOMMENT Error");
            }
        }
    }
}