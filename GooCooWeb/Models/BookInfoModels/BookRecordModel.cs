using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GooCooServer.Entity;

namespace GooCooWeb.Models.BookInfoModels
{
    public enum BookCondition { BORROW, ORDER, AVAILABLE}
    public class BookRecordModel
    {
        public Book BookRecord { get; set; }
        public BookCondition CurrentCondition { get; set; }
        public DateTime AvailableTime { get; set; }         //可得时间

        public BookRecordModel()
        {
            this.CurrentCondition = BookCondition.AVAILABLE;
        }
    }
}