using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace GooCooWeb.Models
{
    public class DonateBookInfo
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public DateTime DonateTime { get; set; }
    }

    public class DonateInfoModel
    {
        private List<DonateBookInfo> books = new List<DonateBookInfo>();

        public void Add(DonateBookInfo bookInfo)
        {
            books.Add(bookInfo);
        }

        public List<DonateBookInfo> GetBooks()
        {
            return books;
        }
    }
}