using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GooCooServer.Entity;
using GooCooServer.IDAO;
using GooCooServer.DAO;


namespace GooCooWeb.Models.BookInfoModels
{
    public class BookInfoRecordModel
    {
        public const int CommentHomePage = 10;

        public BookInfo Bookinfo { get; set; }
        public int AvailableCount { get; set; }     //剩余数量(出去借走、预定的书籍之后)
        public int OrderCount { get; set; }         //预定数量
        public List<BookRecordModel> Books { get; set; }

        //Comment
        public List<CommentRecordModel> TopComments { get; set; }       //最新评论,其余评论使用ajax获取
        public int TotalCommentCount { get; set; }        

        //Constructor
        public BookInfoRecordModel()
        {
        }

        public BookInfoRecordModel(string isbn)
        {
            //获取BookInfo
            IBookInfoDAO bookInfoDAO = DAOFactory.createDAO("BookInfoDAO") as IBookInfoDAO;
            BookInfo bookinfo = null;
            try
            {
                bookinfo = bookInfoDAO.Get(isbn);
            }
            catch (Exception) { }
            
            if (bookinfo != null)
            {

                BookInfoRecordModel bookInfoRecord = new BookInfoRecordModel();
                bookInfoRecord.Bookinfo = bookinfo;


                //获取每本书信息
                IBook_BookInfoDAO book_bookInfoDAO = DAOFactory.createDAO("Book_BookInfoDAO") as IBook_BookInfoDAO;

                List<BookRecordModel> bookRecordList = null;
                List<Book> bookList = null;
                try
                {
                    bookList = book_bookInfoDAO.GetBook(bookinfo.Isbn);
                    bookRecordList = BookRecordModel.toRecord(bookList);
                    bookInfoRecord.Books = bookRecordList;
                }
                catch (Exception) { }

                //评论信息            
                IBook_CommentDAO book_commentDAO = DAOFactory.createDAO("Book_CommentDAO") as IBook_CommentDAO;
                //评论总数量
                int commentCount = 0;
                try
                {
                    commentCount = book_commentDAO.GetCommentCount(bookinfo.Isbn);
                }
                catch (Exception) { }
                bookInfoRecord.TotalCommentCount = commentCount;
                //最新评论内容
                if (commentCount > 0)
                {
                    List<Comment> commentList = null;
                    try
                    {
                        commentList = book_commentDAO.GetComment(bookinfo.Isbn, 1, BookInfoRecordModel.CommentHomePage);
                        //转换为CommentRecord
                        List<CommentRecordModel> commentRecordList = CommentRecordModel.toRecord(commentList);
                        bookInfoRecord.TopComments = commentRecordList;
                    }
                    catch (Exception) { }
                }

                //////////////////
                //暂时测试用
                //剩余数量
                bookInfoRecord.AvailableCount = 2;
                bookInfoRecord.OrderCount = 2;
            }
        }
    }
}