﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GooCooServer.IDAO;

namespace GooCooServer.DAO
{
    public class DAOFactory
    {
        public static IBaseDAO createDAO(String name) 
        {
            IBaseDAO requestDAO;
            switch (name)
            {
                case "Book_BookInfoDAO":
                    requestDAO = new Book_BookInfoDAO();
                    break;
                default: 
                    requestDAO = null;
                    break;
            }
            return requestDAO;
        }
    }
}
