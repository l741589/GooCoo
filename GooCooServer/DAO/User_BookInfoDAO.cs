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
    public class User_BookInfoDAO : BaseDAO, IUser_BookInfoDAO
    {
        public void Add(String isbn, String user_id, User_BookInfo.ERelation relation = User_BookInfo.ERelation.ORDER)
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
                SqlCommand myCommand = new SqlCommand("INSERT INTO USER_BOOKINFO (user_id, isbn, realation, time) " + "Values (" + user_id + ", " + isbn + ", " + (int)relation + ", " + DateTime.Now + ")", connecter);
                try
                {
                    myCommand.ExecuteNonQuery();
                }
                catch (System.Exception)
                {
                    throw new BMException("");
                }
            }
        }

        public void Del(String isbn, String user_id, User_BookInfo.ERelation relation = User_BookInfo.ERelation.ORDER)
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
                SqlCommand myCommand = new SqlCommand("DELETE FROM USER_BOOKINFO WHERE user_id = "+user_id+" AND isbn = "+isbn+" AND relation = "+(int)relation+"", connecter);
                try
                {
                    myCommand.ExecuteNonQuery();
                }
                catch
                {
                    throw new BMException("");
                }
            }
        }

        public List<User> GetUser(String isbn, User_BookInfo.ERelation relation = User_BookInfo.ERelation.ORDER)
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
                string sqlQuery = "SELECT * FROM USER_BOOKINFO WHERE isbn = @isbn AND relation = "+(int)relation+"";
                SqlCommand myCommand = new SqlCommand(sqlQuery, connecter);
                myCommand.Parameters.Add(myParam);

                SqlDataReader sqlDataReader = null;
                try
                {
                    sqlDataReader = myCommand.ExecuteReader();
                }
                catch (System.Exception)
                {
                    throw new BMException("");
                }

                List<User> users = new List<User>();                                
                while (sqlDataReader.Read())
                {
                    User user = new User();
                    user.Id = (string)sqlDataReader[0];
                    users.Add(user);
                }

                for (int i = 0; i < users.Count; i++)
                {
                    myParam = new SqlParameter("@id", SqlDbType.VarChar);
                    myParam.Value = users[i].Id;
                    sqlQuery = "SELECT * FROM USERINFO WHERE id = @id";
                    myCommand = new SqlCommand(sqlQuery, connecter);
                    myCommand.Parameters.Add(myParam);
                    sqlDataReader.Close();
                    try
                    {
                        sqlDataReader = myCommand.ExecuteReader();
                    }
                    catch (System.Exception)
                    {
                        throw new BMException("");
                    }

                    if (sqlDataReader.Read())
                    {
                        users[i].Name = (string)sqlDataReader[1];
                        users[i].Authority = (User.EAuthority)sqlDataReader[3];
                        users[i].Repvalue = (int)sqlDataReader[4];
                    }
                }
                if (users.Count != 0)
                    return users;
                else
                    throw new BMException("USER_BOOKINFO GETUSER Error");
            }
        }

        public List<BookInfo> GetBookInfo(String user_id, User_BookInfo.ERelation relation = User_BookInfo.ERelation.ORDER)
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
                SqlParameter myParam = new SqlParameter("@id", SqlDbType.VarChar);
                myParam.Value = user_id;
                string sqlQuery = "SELECT * FROM USER_BOOKINFO WHERE user_id = @id AND relation = " + (int)relation + "";
                SqlCommand myCommand = new SqlCommand(sqlQuery, connecter);
                myCommand.Parameters.Add(myParam);
                SqlDataReader sqlDataReader = myCommand.ExecuteReader();

                List<BookInfo> bookInfos = new List<BookInfo>();
                while (sqlDataReader.Read())
                {
                    BookInfo book = new BookInfo();
                    book.Isbn = (string)sqlDataReader[1];
                    bookInfos.Add(book);
                }

                for (int i = 0; i < bookInfos.Count; i++)
                {
                    myParam = new SqlParameter("@isbn", SqlDbType.Char);
                    myParam.Value = bookInfos[i].Isbn;
                    sqlQuery = "SELECT * FROM BOOKINFO WHERE isbn = @isbn";
                    myCommand = new SqlCommand(sqlQuery, connecter);
                    myCommand.Parameters.Add(myParam);
                    sqlDataReader.Close();
                    sqlDataReader = myCommand.ExecuteReader();

                    if (sqlDataReader.Read())
                    {
                        bookInfos[i].Name = (string)sqlDataReader[1];
                        bookInfos[i].Timestamp = (DateTime)sqlDataReader[2];
                        bookInfos[i].Summary = (string)sqlDataReader[4];
                    }
                }
                if (bookInfos.Count != 0)
                    return bookInfos;
                else
                    throw new BMException("USER_BOOKINFO GETBOOKINFO Error");
            }
        }

        public User_BookInfo Get(String isbn, String user_id, User_BookInfo.ERelation relation = User_BookInfo.ERelation.ORDER)
        {
            string sqlQuery = "SELECT * FROM USER_BOOKINFO WHERE isbn = " + isbn + " AND user_id = " + user_id + " AND relation = " + relation + "";
            SqlCommand myCommand = new SqlCommand(sqlQuery, connecter);
            SqlDataReader sqlDataReader = myCommand.ExecuteReader();

            User_BookInfo userbook = null;

            if (sqlDataReader.Read())
            {
                userbook = new User_BookInfo();
                userbook.User = (string)sqlDataReader[0];
                userbook.Isbn = (string)sqlDataReader[1];
                userbook.Relation = (User_BookInfo.ERelation)sqlDataReader[2];
                userbook.Timestamp = (DateTime)sqlDataReader[3];
            }

            if (userbook != null)
                return userbook;
            else
                throw new BMException("User_BookInfo get error");
        }

        public User GetAvaliableUser(String book_isbn)
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
                myParam.Value = book_isbn;
                string sqlQuery = "SELECT * FROM USER_BOOKINFO WHERE isbn = @isbn AND relation = " + User_BookInfo.ERelation.ORDER + "  ORDER BY time";
                SqlCommand myCommand = new SqlCommand(sqlQuery, connecter);
                myCommand.Parameters.Add(myParam); 
                SqlDataReader sqlDataReader = null;
                try
                {
                    sqlDataReader = myCommand.ExecuteReader();
                }
                catch (System.Exception)
                {
                    throw new BMException("");
                }

                User user = null;
                if (sqlDataReader.Read())
                {
                    user = new User();
                    user.Id = (string)sqlDataReader[0];
                }

                if (user != null)
                {
                    myParam = new SqlParameter("@id", SqlDbType.VarChar);
                    myParam.Value = user.Id;
                    sqlQuery = "SELECT * FROM USERINFO WHERE id = @id";
                    myCommand = new SqlCommand(sqlQuery, connecter);
                    myCommand.Parameters.Add(myParam);
                    sqlDataReader.Close();
                    try
                    {
                        sqlDataReader = myCommand.ExecuteReader();
                    }
                    catch (System.Exception)
                    {
                        throw new BMException("");
                    }

                    if (sqlDataReader.Read())
                    {
                        user.Name = (string)sqlDataReader[1];
                        user.Authority = (User.EAuthority)sqlDataReader[3];
                        user.Repvalue = (int)sqlDataReader[4];
                    }
                    return user;
                }
                else
                    throw new BMException("USER_BOOKINFO GETAVAUSER Error");
            }
        }
    }
}