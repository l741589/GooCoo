using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GooCooWeb.Models
{
    public class PreorderBookInfo
    {
        public string Isbn { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public DateTime PreorderDate { get; set; }
        public int BorrowedNumber { get; set; }
        public int PreorderNumber { get; set; }
    }

    public class PreorderInfoModel
    {
        private List<PreorderBookInfo> books = new List<PreorderBookInfo>();

        public void Add(PreorderBookInfo bookInfo)
        {
            books.Add(bookInfo);
        }

        public List<PreorderBookInfo> GetBooks()
        {
            return books;
        }
    }
}