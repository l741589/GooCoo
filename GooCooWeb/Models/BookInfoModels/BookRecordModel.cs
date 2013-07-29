using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GooCooServer.Entity;
using GooCooServer.IDAO;
using GooCooServer.DAO;

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


        public static List<BookRecordModel> toRecord(List<Book> bookList)
        {
            
            List<BookRecordModel> resultList = new List<BookRecordModel>();
            IUser_BookDAO user_bookDAO = DAOFactory.createDAO("User_BookDAO") as IUser_BookDAO;


            foreach (Book book in bookList)
            {
                BookRecordModel bookRecordModel = new BookRecordModel();
                bookRecordModel.BookRecord = book;
                User tempUser = null;

                //检查书籍是否已被借走
                try
                {
                    tempUser = user_bookDAO.GetUser(book.Id);
                }
                catch (Exception) { }
                if (tempUser != null)
                {
                    bookRecordModel.CurrentCondition = BookCondition.BORROW;
                }
                resultList.Add(bookRecordModel);

                //还差AvailableTime
            }

            return resultList;
        }
    }
}