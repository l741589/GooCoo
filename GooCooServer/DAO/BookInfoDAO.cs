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
using GooCooServer.Utility;

namespace GooCooServer.DAO
{
    class BookInfoDAO : BaseDAO, IBookInfoDAO
    {
        public BookInfoDAO()
        {
        }

        public List<BookInfo> dbManagerList(SqlParameter myParam, string sqlQuery)
        {
            List<BookInfo> bookInfos;

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

                SqlDataReader sqlDataReader = null;
                try
                {
                    sqlDataReader = myCommand.ExecuteReader();
                }
                catch (System.Exception)
                {
                    throw new BMException("");
                }
                
                bookInfos = new List<BookInfo>();

                while (sqlDataReader.Read())
                {
                    BookInfo book = new BookInfo();
                    book.Isbn = (string)sqlDataReader[0];
                    book.Name = (string)sqlDataReader[1];
                    book.Timestamp = (DateTime)sqlDataReader[2];
                    book.Summary = (string)sqlDataReader[4];
                    bookInfos.Add(book);
                }
            }
            return bookInfos;
        }

        public int GetCountByIsbn(String isbn)
        {   
            SqlParameter myParam = new SqlParameter("@isbn", SqlDbType.Char);
            myParam.Value = isbn;
            string sqlQuery = "SELECT COUNT(isbn) FROM BOOKINFO WHERE isbn LIKE  '%' + @isbn + '%'";
            return dbManagerCount(myParam, sqlQuery);
        }

        public List<BookInfo> GetByIsbn(String isbn, int from = 0, int count = 0)
        {
            SqlParameter myParam = new SqlParameter("@isbn", SqlDbType.Char);
            myParam.Value = isbn;
            string sqlQuery;
            if (from == 0 && count == 0)
                sqlQuery = "SELECT * FROM BOOKINFO WHERE isbn LIKE  '%' + @isbn + '%'";
            else
            {
                int to = from - 1;
                if (to < 0) to = 0;
                sqlQuery = "SELECT TOP "+count+" * FROM BOOKINFO WHERE (isbn NOT IN (SELECT TOP "+to+" isbn FROM BOOKINFO)) AND isbn LIKE '%' + @isbn + '%'";
            }

            List<BookInfo> bookInfos = dbManagerList(myParam, sqlQuery);
            
            if (bookInfos.Count != 0)
                return bookInfos;
            else
                throw new BMException("BOOKINFO GETBYISBN error");
        }

        public int GetCountByName(String name)
        {
            SqlParameter myParam = new SqlParameter("@name", SqlDbType.VarChar);
            myParam.Value = name;
            string sqlQuery = "SELECT COUNT(name) FROM BOOKINFO WHERE name LIKE  '%' + @name + '%'";
            return dbManagerCount(myParam, sqlQuery);
        }

        public List<BookInfo> GetByName(String name, int from = 0, int count = 0)
        {
            SqlParameter myParam = new SqlParameter("@name", SqlDbType.VarChar);
            myParam.Value = name;
            string sqlQuery;
            if (from == 0 && count == 0)
                sqlQuery = "SELECT * FROM BOOKINFO WHERE name LIKE  '%' + @name + '%'";
            else
            {
                int to = from - 1;
                if (to < 0) to = 0;
                sqlQuery = "SELECT TOP "+count+" * FROM BOOKINFO WHERE (isbn NOT IN (SELECT TOP "+to+" isbn FROM BOOKINFO)) AND name LIKE '%' + @name + '%'";
            }

            List<BookInfo> bookInfos = dbManagerList(myParam, sqlQuery);

            if (bookInfos.Count != 0)
                return bookInfos;
            else
                throw new BMException("BOOKINFO GETBYNAME error");
        }

        public int GetCountByKeyWord(String keyWord)
        {
            SqlParameter myParam = new SqlParameter("@keyWord", SqlDbType.Char);
            myParam.Value = keyWord;
            string sqlQuery = "SELECT COUNT(name) FROM BOOKINFO WHERE name LIKE  '%' + @keyWord + '%' OR isbn LIKE  '%' + @keyWord + '%'";
            return dbManagerCount(myParam, sqlQuery);
        }

        public List<BookInfo> GetByKeyWord(String keyWord, int from = 0, int count = 0)
        {
            SqlParameter myParam = new SqlParameter("@keyWord", SqlDbType.Char);
            myParam.Value = keyWord;
            string sqlQuery;
            if (from == 0 && count == 0)
                sqlQuery = "SELECT * FROM BOOKINFO WHERE name LIKE  '%' + @keyWord + '%' OR isbn LIKE  '%' + @keyWord + '%'";
            else
            {
                int to = from - 1;
                if (to < 0) to = 0;
                sqlQuery = "SELECT TOP " + count + " * FROM BOOKINFO WHERE (isbn NOT IN (SELECT TOP " + to + " isbn FROM BOOKINFO)) AND name LIKE '%' + @keyWord + '%' OR isbn LIKE  '%' + @keyWord + '%'";
            }

            List<BookInfo> bookInfos = dbManagerList(myParam, sqlQuery);

            if (bookInfos.Count != 0)
                return bookInfos;
            else
                throw new BMException("BOOKINFO GETBYKEYWORD error");
        }

        public List<BookInfo> GetBookInfos(int n)
        {
            SqlParameter myParam = new SqlParameter("@number", SqlDbType.Int);
            myParam.Value = n;            
            string sqlQuery = "SELECT TOP @number * FROM BOOKINFO ORDER BY time DESC";            
            List<BookInfo> bookInfos = dbManagerList(myParam, sqlQuery);

            if (bookInfos.Count != 0)
                return bookInfos;
            else
                throw new BMException("BOOKINFO GETBookInfos error");
        }

        public BookInfo Get(String isbn)
        {
            SqlParameter myParam = new SqlParameter("@isbn", SqlDbType.Char);
            myParam.Value = isbn;
            string sqlQuery;
            sqlQuery = "SELECT TOP 1 * FROM BOOKINFO WHERE isbn = @isbn";

            List<BookInfo> bookInfos = dbManagerList(myParam, sqlQuery);

            if (bookInfos.Count != 0)
                return bookInfos[0];
            else
                throw new BMException("BOOKINFO GET error");
        }

        public BookInfo Add(BookInfo book)
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
                Task.Run(async () => { Util.Merge(book, await Util.GetBookFromInternet(book.Isbn), true); }).Wait();

                if (book.Name == null)
                    throw new BMException("");
                SqlParameter myParam = new SqlParameter("@isbn", SqlDbType.Char);
                myParam.Value = book.Isbn;
                SqlParameter myParam2 = new SqlParameter("@name", SqlDbType.VarChar);
                myParam2.Value = book.Name;
                SqlParameter myParam3 = new SqlParameter("@summary", SqlDbType.VarChar);
                myParam3.Value = book.Summary;
                SqlParameter myParam4 = new SqlParameter("@time", SqlDbType.DateTime);
                if (book.Timestamp != default(DateTime))
                    myParam4.Value = book.Timestamp;
                else
                    myParam4.Value = DateTime.Now;
                SqlCommand myCommand = new SqlCommand("INSERT INTO BOOKINFO (isbn, name, summary, time) " + "Values (@isbn, @name, @summary, @time)", connecter);
                myCommand.Parameters.Add(myParam4);
                myCommand.Parameters.Add(myParam3);
                myCommand.Parameters.Add(myParam2);
                myCommand.Parameters.Add(myParam);
                try
                {
                    myCommand.ExecuteNonQuery();
                }
                catch(System.Exception)
                {
                    throw new BMException("");
                }
            }
            return book;
        }

        public void Del(String isbn)
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
                SqlCommand myCommand = new SqlCommand("DELETE FROM BOOKINFO  " + "WHERE isbn = @isbn", connecter);
                myCommand.Parameters.Add(myParam);
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

        public void Set(BookInfo book)
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
                try{
                SqlHelper.Update(connecter, "BOOKINFO")
                    .Add("isbn", SqlDbType.Char, book.Isbn)
                    .Add("name", SqlDbType.VarChar, book.Name)
                    .Add("summary", SqlDbType.Text, book.Summary)
                    .Add("time", SqlDbType.DateTime, book.Timestamp)
                    .Where("isbn = @isbn").Execute();
                //SqlParameter myParam = new SqlParameter("@isbn", SqlDbType.Char);
                //myParam.Value = book.Isbn;
                //SqlParameter myParam2 = new SqlParameter("@name", SqlDbType.VarChar);
                //myParam2.Value = book.Name;
                //SqlParameter myParam3 = new SqlParameter("@summary", SqlDbType.VarChar);
                //myParam3.Value = book.Summary;
                //SqlParameter myParam4 = new SqlParameter("@time", SqlDbType.DateTime);
                //myParam4.Value = book.Timestamp;
                //
                //SqlCommand myCommand = new SqlCommand("UPDATE BOOKINFO SET name = @name, summary = @summary, time = @time " + "WHERE isbn = @isbn", connecter);               
                //myCommand.Parameters.Add(myParam4);
                //myCommand.Parameters.Add(myParam3);
                //myCommand.Parameters.Add(myParam2);
                //myCommand.Parameters.Add(myParam);
                //try
                //{
                //    myCommand.ExecuteNonQuery();
                }
                catch (System.Exception)
                {
                    throw new BMException("");
                }
            }
        }
    }
}
