using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GooCooServer.IDAO;
using GooCooServer.Exception;

namespace GooCooServer.DAO
{
    public class DAOFactory
    {
        public static IBaseDAO createDAO(String name) 
        {
            IBaseDAO requestDAO = null;
            switch (name)
            {
                case "Book_BookInfoDAO":
                    requestDAO = new Book_BookInfoDAO();
                    break;
                case "BookDAO":
                    requestDAO = new BookDAO();
                    break;
                case "BookInfoDAO":
                    requestDAO = new BookInfoDAO();
                    break;
                case "UserDAO":
                    requestDAO = new UserDAO();
                    break;
                default: 
                    requestDAO = null;
                    break;
            }
            if (requestDAO != null)
                return requestDAO;
            else
                throw new BMException("DAOFactory request error");
        }
    }
}
