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
    public class User_CommentDAO : BaseDAO, IUser_CommentDAO
    {
        public List<Comment> GetComment(String ID)
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
                SqlParameter myParam = new SqlParameter("@id", SqlDbType.Char);
                myParam.Value = ID;
                string sqlQuery = "SELECT * FROM USER_COMMENT WHERE user_id = @id";
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

        public User GetUser(int comment_id)
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
                SqlParameter myParam = new SqlParameter("@id", SqlDbType.Int);
                myParam.Value = comment_id;
                string sqlQuery = "SELECT * FROM USER_COMMENT WHERE comment_id = @id";
                SqlCommand myCommand = new SqlCommand(sqlQuery, connecter);
                myCommand.Parameters.Add(myParam);
                SqlDataReader sqlDataReader = myCommand.ExecuteReader();

                User user = null;
                string userID = null;
                if (sqlDataReader.Read())
                {
                    user = new User();
                    userID = (string)sqlDataReader[0];                
                }

                if (user != null)
                {
                    myParam = new SqlParameter("@id", SqlDbType.Char);
                    myParam.Value = userID;
                    sqlQuery = "SELECT * FROM USER WHERE id = @id";
                    myCommand = new SqlCommand(sqlQuery, connecter);
                    myCommand.Parameters.Add(myParam);
                    sqlDataReader.Close();
                    sqlDataReader = myCommand.ExecuteReader();

                    if (sqlDataReader.Read())
                    {
                        user = new User();
                        user.Id = (string)sqlDataReader[0];
                        user.Name = (string)sqlDataReader[1];
                        user.Authority = (User.EAuthority)sqlDataReader[3];
                        user.Repvalue = (int)sqlDataReader[4];
                    }
                }
                if (user != null)
                    return user;
                else
                    throw new BMException("USER_COMMENTDAO GET Error");
            }
        }
    }
}