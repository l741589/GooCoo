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
    class BookDAO : BaseDAO, IBookDAO
    {
        public BookDAO()
        {
        }

        public Book Add()
        {
            return null;
        }

        public Book Add(BookInfo bookInfo)
        {
            Book book = new Book();
            book.Timestamp = DateTime.Now;

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
                myParam.Value = book.Timestamp;
                SqlCommand myCommand = new SqlCommand("INSERT INTO BOOK (time) " + "Values (@time); " + "select @@IDENTITY as 'Identity'", connecter);
                myCommand.Parameters.Add(myParam);
                int id = 0;
                try
                {
                    id = Convert.ToInt32(myCommand.ExecuteScalar());
                }
                catch (System.Exception)
                {
                    throw new BMException("");
                }
                if (id == 0)
                    throw new BMException("BOOK ADD error");
                else
                {
                    book.Id = id;
                    myParam = new SqlParameter("@id", SqlDbType.Int);
                    myParam.Value = book.Id;
                    SqlParameter myParam2 = new SqlParameter("@isbn", SqlDbType.Char);
                    myParam2.Value = bookInfo.Isbn;
                    myCommand = new SqlCommand("INSERT INTO BOOK_BOOKINFO (id, isbn) " + "Values (@id, @isbn)", connecter);
                    myCommand.Parameters.Add(myParam);
                    myCommand.Parameters.Add(myParam2);
                    try
                    {
                        myCommand.ExecuteNonQuery();
                    }
                    catch (System.Exception)
                    {
                        book = null;
                    }
                    return book;
                }
            }
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
                SqlParameter myParam = new SqlParameter("@id", SqlDbType.Int);
                myParam.Value = id;
                SqlCommand myCommand = new SqlCommand("DELETE FROM BOOK " + "WHERE id = @id", connecter);
                myCommand.Parameters.Add(myParam);
                try
                {
                    myCommand.ExecuteNonQuery();
                }
                catch (System.Exception)
                {
                    throw new BMException("DEl");
                }
            }
        }

        public Book Get(int id)
        {
            Book book = null;

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
                myParam.Value = id;
                string sqlQuery = "SELECT * FROM BOOK WHERE id = @id";
                SqlCommand myCommand = new SqlCommand(sqlQuery, connecter);
                myCommand.Parameters.Add(myParam);

                SqlDataReader sqlDataReader = null;
                try
                {
                    sqlDataReader = myCommand.ExecuteReader();
                }
                catch (System.Exception)
                {
                }
                if (sqlDataReader.Read())
                {
                    book = new Book();
                    book.Id = id;
                    book.Timestamp = (DateTime)sqlDataReader[1];
                }
            }

            if (book != null)
                return book;
            else
                throw new BMException("BOOK GET error");
        }
    }
}
