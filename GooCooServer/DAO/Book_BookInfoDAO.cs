using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GooCooServer.IDAO;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using GooCooServer.Exception;
using GooCooServer.Entity;

namespace GooCooServer.DAO
{
    class Book_BookInfoDAO : BaseDAO,IBook_BookInfoDAO
    {
        public Book_BookInfoDAO()
        {
        }    
           
        public BookInfo GetBookInfo(int book_id)
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
                myParam.Value = book_id;
                string sqlQuery = "SELECT * FROM BOOK_BOOKINFO WHERE id = @id";
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

                string isbn = null;

                if (sqlDataReader.Read())
                {
                    isbn = (string)sqlDataReader[0];
                }

                BookInfo result = null;

                if (isbn != null)
                {
                    myParam = new SqlParameter("@isbn", SqlDbType.Char);
                    myParam.Value = isbn;
                    sqlQuery = "SELECT * FROM BOOKINFO WHERE isbn = @isbn";
                    myCommand = new SqlCommand(sqlQuery, connecter);
                    myCommand.Parameters.Add(myParam);
                    sqlDataReader.Close(); 
                    try
                    {
                        sqlDataReader = myCommand.ExecuteReader();
                    }
                    catch (System.Exception)
                    {
                    }

                    if (sqlDataReader.Read())
                    {
                        result = new BookInfo();
                        result.Isbn = (string)sqlDataReader[0];
                        result.Name = (string)sqlDataReader[1];
                        result.Timestamp = (DateTime)sqlDataReader[2];
                        result.Summary = (string)sqlDataReader[4];                        
                    }
                }
                if (result != null)
                    return result;
                else
                    throw new BMException("BOOK_BOOKINFO GETBOOKINFO Error");
            }
        }

        public List<Book> GetBook(String isbn)
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
                string sqlQuery = "SELECT * FROM BOOK_BOOKINFO WHERE isbn = @isbn";
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

                List<int> bookID = new List<int>();

                while (sqlDataReader.Read())
                {
                    bookID.Add((int)sqlDataReader[1]);
                }

                List<Book> result = new List<Book>();

                for (int i = 0; i < bookID.Count; i++)
                {
                    myParam = new SqlParameter("@id", SqlDbType.Int);
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
                    }

                    if (sqlDataReader.Read())
                    {
                        Book book = new Book();
                        book.Id = bookID[i];
                        book.Timestamp = (DateTime)sqlDataReader[0];
                        result.Add(book);
                    }
                }
                if (result != null)
                    return result;
                else
                    throw new BMException("BOOK_BOOKINFO GETBOOK Error");
            }
        }

        public int Count(String isbn)
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
                string sqlQuery = "SELECT COUNT(isbn) FROM BOOK_BOOKINFO WHERE isbn = @isbn";
                SqlCommand myCommand = new SqlCommand(sqlQuery, connecter);
                myCommand.Parameters.Add(myParam);
                int count = (int)myCommand.ExecuteScalar();
                return count;
            }
        }

        public int GetAvaliableBookNumber(String isbn)
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
                string sqlQuery = "SELECT COUNT(id) FROM BOOK_BOOKINFO WHERE isbn = @isbn AND (id NOT IN (SELECT book_id FROM USER_BOOK WHERE relation = "+(int)User_Book.ERelation.BORROW+" ))";
                SqlCommand myCommand = new SqlCommand(sqlQuery, connecter);
                myCommand.Parameters.Add(myParam);
                int count = (int)myCommand.ExecuteScalar();
                return count;
            }
        }
    }
}
