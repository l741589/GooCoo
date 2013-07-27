using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GooCooServer.IDAO;
using System.Data.SqlClient;
using System.Configuration;
using GooCooServer.Exception;

namespace GooCooServer.DAO
{
    class BaseDAO : IBaseDAO
    {
        protected SqlConnection connecter;
        protected String connectStr;
        
        public BaseDAO()
        {
        }

        public void createConnection()
        {
            connectStr = "server=localhost;Initial catalog=GooCooDB;user ID=sa;password=;Integrated Security = true";
        }

        public int dbManagerCount(SqlParameter myParam, string sqlQuery)
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
                SqlCommand myCommand = new SqlCommand(sqlQuery, connecter);
                myCommand.Parameters.Add(myParam);
                int count = (int)myCommand.ExecuteScalar();
                return count;
            }
        }

    }
}
