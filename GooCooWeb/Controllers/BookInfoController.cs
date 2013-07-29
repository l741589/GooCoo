using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GooCooServer.Entity;
using GooCooServer.DAO;
using GooCooServer.IDAO;
using GooCooWeb.Models.BookInfoModels;


namespace GooCooWeb.Controllers
{
    public class BookInfoController : Controller
    {
        //
        // GET: /BookInfo/
        
        public ActionResult Index(string isbn)
        {

            //获取BookInfo
            IBookInfoDAO bookInfoDAO = DAOFactory.createDAO("BookInfoDAO") as IBookInfoDAO;
            BookInfo bookinfo = null;
            try
            {
                bookinfo = bookInfoDAO.Get(isbn);
            }
            catch (Exception) { }
            if (bookinfo == null)
            {
                return View("Error");
            }
                    
            //////////////////////////
            //测试用
            bookinfo = new BookInfo();
            bookinfo.Isbn = isbn;
            bookinfo.Name = "程序设计与项目实训教程";
            //////////////////////////

            BookInfoRecordModel bookInfoRecord = new BookInfoRecordModel();
            bookInfoRecord.Bookinfo = bookinfo;


            //获取Book            
            IBook_BookInfoDAO book_bookInfoDAO = DAOFactory.createDAO("Book_BookInfoDAO") as IBook_BookInfoDAO;
            IUser_BookDAO user_bookDAO = DAOFactory.createDAO("User_BookDAO") as IUser_BookDAO;

            List<BookRecordModel> bookRecordList = new List<BookRecordModel>();
            List<Book> bookList = null;
            try 
            {
                bookList = book_bookInfoDAO.GetBook(bookinfo.Isbn);
            }
            catch (Exception ){ }
            
            if (bookList != null)
            {
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
                    catch (Exception ) { }
                    if (tempUser != null)
                    {
                        bookRecordModel.CurrentCondition = BookCondition.BORROW;
                    }
                    bookRecordList.Add(bookRecordModel);
                }
                bookInfoRecord.Books = bookRecordList;
            }

            //评论信息
            ICommentDAO commentDAO = DAOFactory.createDAO("CommentDAO") as ICommentDAO;
            IBook_CommentDAO book_commentDAO = DAOFactory.createDAO("Book_CommentDAO") as IBook_CommentDAO;



            //ViewBag.Bookinfo = bookinfo;
            ViewBag.BookInfoRecord = bookInfoRecord;
            return View();
        }

    }
}
