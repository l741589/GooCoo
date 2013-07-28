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
    public class LogDAO : BaseDAO, ILogDAO
    {
        public Log Get(int id)
        {
            Log log = null;

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
                string sqlQuery = "SELECT * FROM LOG WHERE id = " + id + "";
                SqlCommand myCommand = new SqlCommand(sqlQuery, connecter);

                SqlDataReader sqlDataReader = myCommand.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    log = new Log();
                    log.Id = id;
                    log.Content = (String)sqlDataReader[1];
                    log.Timestamp = (DateTime)sqlDataReader[2];
                }
            }

            if (log != null)
                return log;
            else
                throw new BMException("LOG GET error");
        }

        public int GetCount()
        {
            SqlParameter myParam = new SqlParameter("@null", SqlDbType.Char);
            myParam.Value = null;
            string sqlQuery = "SELECT COUNT(id) FROM LOG";
            return dbManagerCount(myParam, sqlQuery);
        }

        public List<Log> dbManagerList(string sqlQuery)
        {
            List<Log> logs;

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

                SqlDataReader sqlDataReader = myCommand.ExecuteReader();

                logs = new List<Log>();

                while (sqlDataReader.Read())
                {
                    Log log = new Log();
                    log.Id = (int)sqlDataReader[0];
                    log.Content = (string)sqlDataReader[1];
                    log.Timestamp = (DateTime)sqlDataReader[2];
                    logs.Add(log);
                }
            }
            return logs;
        }

        public List<Log> GetBetween(DateTime start_time, DateTime end_time)
        {

            string sqlQuery = "SELECT * FROM LOG WHERE time >= "+start_time+" AND time <= "+end_time+"";
            List<Log> logs = dbManagerList(sqlQuery);

            if (logs.Count != 0)
                return logs;
            else
                throw new BMException("LOG GETBETWEEN error");
        }

        public List<Log> GetLogs(int from = 0, int count = 0)
        {
            string sqlQuery;
            if (from == 0 && count == 0)
                sqlQuery = "SELECT * FROM LOG";
            else
            {
                int to = from - 1;
                if (to < 0) to = 0;
                sqlQuery = "SELECT TOP " + count + " * FROM LOG WHERE (id NOT IN (SELECT TOP " + to + " id FROM LOG))";
            }

            List<Log> logs = dbManagerList(sqlQuery);

            if (logs.Count != 0)
                return logs;
            else
                throw new BMException("LOG Getlogs error");
        }
    }
}