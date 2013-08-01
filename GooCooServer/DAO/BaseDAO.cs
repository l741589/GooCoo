using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GooCooServer.IDAO;
using GooCooServer.Exception;
using System.Data.SqlClient;

namespace GooCooServer.DAO
{
    public class BaseDAO : IBaseDAO
    {
        protected SqlConnection connecter;
        protected String connectStr;

        public BaseDAO()
        {
            createConnection();
        }

        public void createConnection()
        {
            System.Configuration.Configuration rootWebConfig =
                System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/Web.config");
            connectStr = rootWebConfig.ConnectionStrings.ConnectionStrings["GooCooConnectString"].ToString(); 
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
