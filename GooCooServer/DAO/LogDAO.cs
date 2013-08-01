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

                SqlDataReader sqlDataReader = null;
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

        public List<Log> dbManagerList(string sqlQuery, SqlParameter time1, SqlParameter time2)
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
                if (time1 != null)
                    myCommand.Parameters.Add(time1);
                if (time2 != null)
                    myCommand.Parameters.Add(time2);
                SqlDataReader sqlDataReader = null;
                try
                {
                    sqlDataReader = myCommand.ExecuteReader();
                }
                catch (System.Exception)
                {
                    throw new BMException("");
                }

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
            SqlParameter time1 = new SqlParameter("@time1", SqlDbType.DateTime);
            time1.Value = start_time;
            SqlParameter time2 = new SqlParameter("@time2", SqlDbType.DateTime);
            time2.Value = end_time;

            string sqlQuery = "SELECT * FROM LOG WHERE time >= @time1 AND time <= @time2 ORDER BY time DESC";
            List<Log> logs = dbManagerList(sqlQuery, time1, time2);

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
                sqlQuery = "SELECT TOP " + count + " * FROM LOG WHERE (id NOT IN (SELECT TOP " + to + " id FROM LOG))  ORDER BY time DESC";
            }

            List<Log> logs = dbManagerList(sqlQuery, null, null);

            if (logs.Count != 0)
                return logs;
            else
                throw new BMException("LOG Getlogs error");
        }
    }
}