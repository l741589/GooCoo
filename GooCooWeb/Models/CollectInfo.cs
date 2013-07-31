using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using GooCooServer.Entity;

namespace GooCooWeb.Models
{
    public class CollectInfo
    {
        private List<BookInfo> books = new List<BookInfo>();

        public void Add(BookInfo bookInfo)
        {
            books.Add(bookInfo);
        }

        public List<BookInfo> GetBooks()
        {
            return books;
        }
    }
}