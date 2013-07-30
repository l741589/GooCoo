using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace GooCooServer.Utility
{
    public class SqlHelper
    {
        private SqlConnection conn;
        private StringBuilder sb;
        private List<SqlParameter> args;
        private bool start;
        private char cmdtype;
        private SqlHelper(SqlConnection conn, String tablename)
        {
            this.conn = conn;
            sb=new StringBuilder();
            start=true;
            args = new List<SqlParameter>();
        }

        public static SqlHelper Update(SqlConnection conn, String tablename)
        {
            var helper = new SqlHelper(conn, tablename);
            helper.sb.Append("UPDATE " + tablename + " SET ");
            helper.cmdtype = 'U';
            return helper;
        }

        public SqlHelper Add(String name, SqlDbType type, Object value)
        {
            if (cmdtype == 'U')
            {
                if (value==null||value.Equals(Util.DefaultForType(value.GetType()))) return this;
                if (start) start = false; else sb.Append(",");
                sb.Append(name);
                sb.Append(" = @");
                sb.Append(name);
                sb.Append(" ");
                SqlParameter myParam = new SqlParameter("@" + name, type);
                myParam.Value = value;
                args.Add(myParam);
                return this;
            }
            return null;
        }

        public SqlHelper Where(String s)
        {
            sb.Append(" WHERE ");
            sb.Append(s);
            return this;
        }

        public int Execute(){
            String sql=this.ToString();
            Console.WriteLine(sql);
            SqlCommand cmd=new SqlCommand(sql,conn);
            foreach (var e in args) cmd.Parameters.Add(e);
            return cmd.ExecuteNonQuery();
        }

        public override string ToString()
        {
            return sb.ToString();
        }
    }
}