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

namespace GooCooServer.DAO
{
    class Book_BookInfoDAO : BaseDAO,IBook_BookInfoDAO
    {
        public Book_BookInfoDAO()
        {
            createConnection();
        }       
        public void testinsert()
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
                string content = "动态链接";
                SqlParameter myParam = new SqlParameter("@Param1", SqlDbType.DateTime);
                myParam.Value = DateTime.Now;
                SqlParameter myParam2 = new SqlParameter("@Param2", SqlDbType.Text);
                myParam2.Value = content;
                SqlCommand myCommand = new SqlCommand("INSERT INTO LOG (content, time) " + "Values (@Param2, @Param1)", connecter);
                myCommand.Parameters.Add(myParam2);
                myCommand.Parameters.Add(myParam);
                myCommand.ExecuteNonQuery();
            }
        }
    }
}
