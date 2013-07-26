using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using GooCooServer.DAO;
using GooCooServer.IDAO;

namespace GooCooServer.DAOtest
{
    class test
    {
        public static void Main()
        {
            IBook_BookInfoDAO tester = (IBook_BookInfoDAO)DAOFactory.createDAO("Book_BookInfoDAO");
            tester.testinsert();
            Console.ReadLine();
        }
    }
}