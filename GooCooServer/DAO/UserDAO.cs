﻿using System;
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
    class UserDAO : BaseDAO, IUserDAO
    {
        public UserDAO()
        {
            createConnection();
        }

        public String Login(String ID, String password)
        {
            String sessionID = null;
            String userID = null;

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
                SqlParameter myParam = new SqlParameter("@id", SqlDbType.Char);
                myParam.Value = ID;
                string sqlQuery = "SELECT * FROM USER WHERE id = @id AND password = @password";
                SqlCommand myCommand = new SqlCommand(sqlQuery, connecter);
                myCommand.Parameters.Add(myParam);

                SqlDataReader sqlDataReader = myCommand.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    userID = (String)sqlDataReader[0];
                    sessionID = Session.createSession();
                }
            }

            if (userID != null)
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
                    SqlParameter myParam = new SqlParameter("@sessionid", SqlDbType.Char);
                    myParam.Value = sessionID;
                    SqlParameter myParam2 = new SqlParameter("@userid", SqlDbType.Char);
                    myParam2.Value = userID;
                    SqlParameter myParam3 = new SqlParameter("@time", SqlDbType.DateTime);
                    myParam3.Value = DateTime.Now.AddDays(Session.ExpiredTime);
                    SqlCommand myCommand = new SqlCommand("INSERT INTO SESSION (session_id, user_id, time) " + "Values (@sessionid, @userid, @time)", connecter);
                    myCommand.Parameters.Add(myParam3);
                    myCommand.Parameters.Add(myParam2);
                    myCommand.Parameters.Add(myParam);
                    myCommand.ExecuteNonQuery();
                }
                return sessionID;
            }
            else
                throw new BMException("USER LOGIN error");
        }

        public List<User> dbManagerList(SqlParameter myParam, string sqlQuery)
        {
            List<User> users;

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

                SqlDataReader sqlDataReader = myCommand.ExecuteReader();

                users = new List<User>();

                while (sqlDataReader.Read())
                {
                    User user = new User();
                    user.Id = (string)sqlDataReader[0];
                    user.Name = (string)sqlDataReader[1];
                    user.Authority = (User.EAuthority)sqlDataReader[3];
                    user.Repvalue = (int)sqlDataReader[4];
                    users.Add(user);
                }
            }
            return users;
        }

        public int GetCountByID(String ID)
        {
            SqlParameter myParam = new SqlParameter("@id", SqlDbType.Char);
            myParam.Value = ID;
            string sqlQuery = "SELECT COUNT(id) FROM USER WHERE id LIKE  '%' + @id + '%'";
            return dbManagerCount(myParam, sqlQuery);
        }

        public List<User> GetByID(String ID, int from = 0, int count = 0)
        {
            SqlParameter myParam = new SqlParameter("@id", SqlDbType.Char);
            myParam.Value = ID;
            string sqlQuery;
            if (from == 0 && count == 0)
                sqlQuery = "SELECT * FROM USER WHERE id LIKE  '%' + @id + '%'";
            else
            {
                int to = from - 1;
                if (to < 0) to = 0;
                sqlQuery = "SELECT TOP " + count + " * FROM USER WHERE (id NOT IN (SELECT TOP " + to + " id FROM USER)) AND id LIKE '%' + @id + '%'";
            }

            List<User> users = dbManagerList(myParam, sqlQuery);

            if (users.Count != 0)
                return users;
            else
                throw new BMException("USER GETBYID error");
        }

        public int GetCountByName(String name)
        {
            SqlParameter myParam = new SqlParameter("@name", SqlDbType.VarChar);
            myParam.Value = name;
            string sqlQuery = "SELECT COUNT(name) FROM USER WHERE name LIKE  '%' + @name + '%'";
            return dbManagerCount(myParam, sqlQuery);
        }

        public List<User> GetByName(String name, int from = 0, int count = 0)
        {
            SqlParameter myParam = new SqlParameter("@name", SqlDbType.Char);
            myParam.Value = name;
            string sqlQuery;
            if (from == 0 && count == 0)
                sqlQuery = "SELECT * FROM USER WHERE name LIKE  '%' + @name + '%'";
            else
            {
                int to = from - 1;
                if (to < 0) to = 0;
                sqlQuery = "SELECT TOP " + count + " * FROM USER WHERE (id NOT IN (SELECT TOP " + to + " id FROM USER)) AND name LIKE '%' + @name + '%'";
            }

            List<User> users = dbManagerList(myParam, sqlQuery);

            if (users.Count != 0)
                return users;
            else
                throw new BMException("USER GETBYNAME error");
        }

        public int GetCountByKeyWord(String keyWord)
        {
            SqlParameter myParam = new SqlParameter("@keyWord", SqlDbType.VarChar);
            myParam.Value = keyWord;
            string sqlQuery = "SELECT COUNT(name) FROM USER WHERE name LIKE  '%' + @keyWord + '%' OR id LIKE  '%' + @keyWord + '%'";
            return dbManagerCount(myParam, sqlQuery);
        }

        public List<User> GetByKeyword(String keyWord, int from = 0, int count = 0)
        {
            SqlParameter myParam = new SqlParameter("@keyWord", SqlDbType.Char);
            myParam.Value = keyWord;
            string sqlQuery;
            if (from == 0 && count == 0)
                sqlQuery = "SELECT * FROM USER WHERE name LIKE  '%' + @keyWord + '%' || id LIKE  '%' + @keyWord + '%'";
            else
            {
                int to = from - 1;
                if (to < 0) to = 0;
                sqlQuery = "SELECT TOP " + count + " * FROM USER WHERE (id NOT IN (SELECT TOP " + to + " id FROM USER)) AND name LIKE '%' + @keyWord + '%' || id LIKE  '%' + @keyWord + '%'";
            }

            List<User> users = dbManagerList(myParam, sqlQuery);

            if (users.Count != 0)
                return users;
            else
                throw new BMException("USER GETBYKEYWORD error");
        }

        //对所有用户password置空
        public User Get(String session)
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
                SqlParameter myParam = new SqlParameter("@session", SqlDbType.Char);
                myParam.Value = session;
                string sqlQuery = "SELECT * FROM SESSION WHERE session_id = @session";
                SqlCommand myCommand = new SqlCommand(sqlQuery, connecter);
                myCommand.Parameters.Add(myParam);
                SqlDataReader sqlDataReader = myCommand.ExecuteReader();

                string userid = null;

                if (sqlDataReader.Read())
                {
                    userid = (string)sqlDataReader[0];
                }

                User user = null;

                if (userid != null)
                {
                    myParam = new SqlParameter("@id", SqlDbType.Char);
                    myParam.Value = userid;
                    sqlQuery = "SELECT * FROM USER WHERE id = @id";
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
                    }
                }
                if (user != null)
                    return user;
                else
                    throw new BMException("USER GET Error");
            }
        }

        public String Add(User user)
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
                SqlParameter myParam = new SqlParameter("@id", SqlDbType.Char);
                myParam.Value = user.Id;
                SqlParameter myParam1 = new SqlParameter("@name", SqlDbType.VarChar);
                myParam.Value = user.Name;
                SqlParameter myParam2 = new SqlParameter("@password", SqlDbType.Char);
                myParam.Value = user.Password;
                SqlParameter myParam3 = new SqlParameter("@authority", SqlDbType.Int);
                myParam.Value = user.Authority;
                SqlParameter myParam4 = new SqlParameter("@repvalue", SqlDbType.Int);
                myParam.Value = user.Repvalue;
                SqlCommand myCommand = new SqlCommand("INSERT INTO USER (id, name, password, authority, repvalue) " + "Values (@id, @name, @password, @authority, @repvalue); " + "select @@IDENTITY as 'Identity'", connecter);
                myCommand.Parameters.Add(myParam);
                myCommand.Parameters.Add(myParam1);
                myCommand.Parameters.Add(myParam2);
                myCommand.Parameters.Add(myParam3);
                myCommand.Parameters.Add(myParam4);
                int id = Convert.ToInt32(myCommand.ExecuteScalar());
                if (id == 0)
                    throw new BMException("USER ADD error");
                else
                {
                    String sessionID = Session.createSession();
                    String userID = user.Id;
                    try
                    {
                        connecter.Open();
                    }
                    catch (System.Exception)
                    {
                        throw new BMException("Create Connnect Error");
                    }
                    myParam = new SqlParameter("@sessionid", SqlDbType.Char);
                    myParam.Value = sessionID;
                    myParam2 = new SqlParameter("@userid", SqlDbType.Char);
                    myParam2.Value = userID;
                    myParam3 = new SqlParameter("@time", SqlDbType.DateTime);
                    myParam3.Value = DateTime.Now.AddDays(Session.ExpiredTime);
                    myCommand = new SqlCommand("INSERT INTO SESSION (session_id, user_id, time) " + "Values (@sessionid, @userid, @time)", connecter);
                    myCommand.Parameters.Add(myParam3);
                    myCommand.Parameters.Add(myParam2);
                    myCommand.Parameters.Add(myParam);
                    myCommand.ExecuteNonQuery();
                    return sessionID;
                }
            }
        }

        public void Set(String session, User user)
        {
        }

        //只有管理员有权限调用
        public void Del(String ID)
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
                SqlParameter myParam = new SqlParameter("@id", SqlDbType.Char);
                myParam.Value = ID;
                SqlCommand myCommand = new SqlCommand("DELETE FROM USER  " + "WHERE id = @id", connecter);
                myCommand.Parameters.Add(myParam);
                myCommand.ExecuteNonQuery();
            }
        }
    }
}