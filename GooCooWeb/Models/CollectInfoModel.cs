using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using GooCooServer.Entity;

namespace GooCooWeb.Models
{
    public class CollectInfoModel
    {
        private List<BookInfo> books = new List<BookInfo>();

        public CollectInfoModel(List<BookInfo> books)
        {
            this.books = books;
        }

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