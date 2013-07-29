using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using GooCooServer.DAO;
using GooCooServer.IDAO;
using GooCooServer.Entity;

namespace GooCooServer.DAOtest
{
    class test
    {
        public static void Main()
        {
            IUserDAO tester = (IUserDAO)DAOFactory.createDAO("UserDAO");
            User user = new User();
            user.Authority = User.EAuthority.USER;
            user.Id = "1152797";
            user.Name = "linfan";
            user.Password = "oyfoyf";
            user.Repvalue = 0;
            tester.Add(user);
            Console.ReadLine();
        }
    }
}