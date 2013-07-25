using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GooCooServer.Exception
{
    public class BMException : ApplicationException
    {
        public BMException(String message) : base(message) { }
    }
}