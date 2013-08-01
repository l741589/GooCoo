using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace GooCooWeb.Models
{
    public class BorrowBookInfo
    {
        public String Isbn { get; set; }
        public int Id { get; set; }
        public String Name { get; set; }
        public DateTime BorrowTime { get; set; }
        public DateTime ExpectedReturnTime { get; set; }
    }

    public class BorrowInfoModel
    {
        private List<BorrowBookInfo> books = new List<BorrowBookInfo>();

        public void Add(BorrowBookInfo bookInfo)
        {
            books.Add(bookInfo);
        }

        public List<BorrowBookInfo> GetBooks()
        {
            return books;
        }
    }
}