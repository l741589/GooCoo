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
    public class User_BookDAO : BaseDAO, IUser_BookDAO
    {
        public void Add(String user_id, int book, User_Book.ERelation relation = User_Book.ERelation.BORROW)
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
                SqlParameter myParam = new SqlParameter("@time", SqlDbType.DateTime);
                myParam.Value = DateTime.Now;
                SqlCommand myCommand = new SqlCommand("INSERT INTO USER_BOOK (user_id, book_id, relation, time) " + "Values ("+user_id+", "+book+", "+(int)relation+", @time)", connecter);
                myCommand.Parameters.Add(myParam);
                try
                {
                    myCommand.ExecuteNonQuery();
                }
                catch (System.Exception)
                {
                    throw new BMException("USER_BOOK ADD error");                    
                }
            }
        }

        public void Del(int book, User_Book.ERelation relation = User_Book.ERelation.BORROW)
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
                SqlCommand myCommand = new SqlCommand("DELETE FROM USER_BOOK WHERE book_id = " + book + " AND relation = " + (int)relation + "", connecter); 
                try
                {
                    myCommand.ExecuteNonQuery();
                }
                catch (System.Exception)
                {
                    throw new BMException("USER_BOOK ADD error");
                }
            }
        }

        public List<Book> GetBook(String user_id, User_Book.ERelation relation = User_Book.ERelation.BORROW)
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
                string sqlQuery = "SELECT * FROM USER_BOOK WHERE user_id = "+user_id+" AND relation = "+(int)relation+"";
                SqlCommand myCommand = new SqlCommand(sqlQuery, connecter);

                SqlDataReader sqlDataReader = null;
                try
                {
                    sqlDataReader = myCommand.ExecuteReader();
                }
                catch (System.Exception)
                {
                    throw new BMException("");
                }

                List<int> bookID = new List<int>();

                while (sqlDataReader.Read())
                {
                    bookID.Add((int)sqlDataReader[1]);
                }

                List<Book> result = new List<Book>();

                for (int i = 0; i < bookID.Count; i++)
                {
                    SqlParameter myParam = new SqlParameter("@id", SqlDbType.Int);
                    myParam.Value = bookID[i];
                    sqlQuery = "SELECT time FROM BOOK WHERE id = @id";
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
                        Book book = new Book();
                        book.Id = bookID[i];
                        book.Timestamp = (DateTime)sqlDataReader[0];
                        result.Add(book);
                    }
                }
                if (result.Count != 0)
                    return result;
                else
                    throw new BMException("USER_BOOK GETBOOK Error");
            }
        }

        public User GetUser(int book, User_Book.ERelation relation = User_Book.ERelation.BORROW)
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
                myParam.Value = book;
                string sqlQuery = "SELECT * FROM USER_BOOK WHERE book_id = @id AND relation = "+(int)relation+"";
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
                string userID = null;
                if (sqlDataReader.Read())
                {
                    user = new User();
                    userID = (string)sqlDataReader[0];                
                }

                if (user != null)
                {
                    myParam = new SqlParameter("@id", SqlDbType.VarChar);
                    myParam.Value = userID;
                    sqlQuery = "SELECT * FROM USERINFO WHERE id = @id";
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
                        user.Email = (string)sqlDataReader[5];
                        user.Phonenumber = (string)sqlDataReader[6];
                    }
                }
                if (user != null)
                    return user;
                else
                    throw new BMException("USER_BOOK GETUSER Error");
            }
        }

        public User_Book Get(String user_id, int book, User_Book.ERelation relation = User_Book.ERelation.BORROW)
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
                string sqlQuery = "SELECT * FROM USER_BOOK WHERE book_id = " + book + " AND user_id = " + user_id + " AND relation = " + (int)relation + "";
                SqlCommand myCommand = new SqlCommand(sqlQuery, connecter);
                SqlDataReader sqlDataReader = null;
                try
                {
                    sqlDataReader = myCommand.ExecuteReader();
                }
                catch (System.Exception e)
                {
                    throw new BMException("");
                }

                User_Book userbook = null;

                if (sqlDataReader.Read())
                {
                    userbook = new User_Book();
                    userbook.User = (string)sqlDataReader[0];
                    userbook.Book = (int)sqlDataReader[1];
                    userbook.Relation = (User_Book.ERelation)sqlDataReader[2];
                    userbook.Timestamp = (DateTime)sqlDataReader[3];
                }

                if (userbook != null)
                    return userbook;
                else
                    throw new BMException("User_Book get error");
            }
        }
    }
}