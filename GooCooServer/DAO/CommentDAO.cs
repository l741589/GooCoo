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
    public class CommentDAO : BaseDAO, ICommentDAO
    {
        public Comment Add(Comment comment)
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
                if (comment.Timestamp == null)
                    comment.Timestamp = DateTime.Now;
                SqlCommand myCommand = new SqlCommand("INSERT INTO COMMENT (content, time) " + "Values (" + comment.Content + ", " + comment.Timestamp + "); " + "select @@IDENTITY as 'Identity'", connecter);
                int id = Convert.ToInt32(myCommand.ExecuteScalar());
                if (id == 0)
                    throw new BMException("USER ADD error");
                else
                {
                    comment.Id = id;
                    return comment;
                }
            }
        }

        public Comment Get(int id)
        {
            Comment comment = null;

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
                string sqlQuery = "SELECT * FROM COMMENT WHERE id = "+id+"";
                SqlCommand myCommand = new SqlCommand(sqlQuery, connecter);

                SqlDataReader sqlDataReader = myCommand.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    comment = new Comment();
                    comment.Id = id;
                    comment.Content = (String)sqlDataReader[1];
                    comment.Timestamp = (DateTime)sqlDataReader[2];
                }
            }

            if (comment != null)
                return comment;
            else
                throw new BMException("COMMENT GET error");
        }
            
        public void Del(int id)
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
                SqlCommand myCommand = new SqlCommand("DELETE FROM COMMENT " + "WHERE id = "+id+"", connecter);
                myCommand.ExecuteNonQuery();
            }
        }

        public void Set(Comment comment)
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
                SqlCommand myCommand = new SqlCommand("UPDATE COMMENT SET content = "+comment.Content+", time = "+comment.Timestamp+" " + "WHERE id = "+comment.Id+"", connecter);
                myCommand.ExecuteNonQuery();
            }
        }

        public List<Comment> dbManagerList(SqlParameter myParam, string sqlQuery)
        {
            List<Comment> comments;

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
                SqlCommand myCommand = new SqlCommand(sqlQuery, connecter);
                myCommand.Parameters.Add(myParam);

                SqlDataReader sqlDataReader = myCommand.ExecuteReader();

                comments = new List<Comment>();

                while (sqlDataReader.Read())
                {
                    Comment comment = new Comment();
                    comment.Id = (int)sqlDataReader[0];
                    comment.Content = (string)sqlDataReader[1];
                    comment.Timestamp = (DateTime)sqlDataReader[2];
                    comments.Add(comment);
                }
            }
            return comments;
        }

        public int GetCountByContent(String keyWord)
        {
            SqlParameter myParam = new SqlParameter("@keyWord", SqlDbType.Char);
            myParam.Value = keyWord;
            string sqlQuery = "SELECT COUNT(id) FROM COMMENT WHERE content LIKE  '%' + @keyWord + '%'";
            return dbManagerCount(myParam, sqlQuery);
        }

        public List<Comment> GetByContent(String keyWord, int from = 0, int count = 0)
        {
            SqlParameter myParam = new SqlParameter("@keyWord", SqlDbType.VarChar);
            myParam.Value = keyWord;
            string sqlQuery;
            if (from == 0 && count == 0)
                sqlQuery = "SELECT * FROM COMMENT WHERE content LIKE  '%' + @keyWord + '%'";
            else
            {
                int to = from - 1;
                if (to < 0) to = 0;
                sqlQuery = "SELECT TOP " + count + " * FROM COMMENT WHERE (id NOT IN (SELECT TOP " + to + " id FROM COMMENT)) AND content LIKE '%' + @keyWord + '%'";
            }

            List<Comment> comments = dbManagerList(myParam, sqlQuery);

            if (comments.Count != 0)
                return comments;
            else
                throw new BMException("COMMENT GETBYKEYWORD error");
        }
    }
}