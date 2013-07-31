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

            BookInfoRecordModel bookInfoRecord = new BookInfoRecordModel(isbn);
            ViewBag.BookInfoRecord = bookInfoRecord;
            if (bookInfoRecord.Bookinfo == null)
            {
                return View("Error");
            }
            

            /*
            /////////////////
            //测试数据
            BookInfoRecordModel bookInfoRecord = new BookInfoRecordModel();
            /////////////////
            bookInfoRecord.AvailableCount = 2;
            bookInfoRecord.OrderCount = 2;
            bookInfoRecord.TotalCommentCount = 30;

            BookInfo bookInfo = new BookInfo();
            bookInfo.Isbn = "abdafsadsa";
            bookInfo.Summary = "《武林外史》作于1965年，原名《风雪会中州》，是古龙中期转型作品，从中可看到古龙对武侠不断求新探索，其中不少人物是古龙后来作品的原型。 全书围绕一段武林恩怨展开，主角是四个性格各异的少年——沈浪、朱七七、王怜花、熊猫儿，更有许多江湖奇人异士，纠缠其中，场景跨越中原、太行、大漠、楼兰、可称宏图巨著，情节跌宕。";
            bookInfo.Name = "武林外史";
            //bookInfo.Author = "古龙"
            bookInfo.Photourl = "http://img3.douban.com/mpic/s2157315.jpg";
            bookInfoRecord.Bookinfo = bookInfo;

            List<BookRecordModel> bookRecordList = new List<BookRecordModel>();
            for (int i = 0; i < 6; i++)
            {
                Book book = new Book();
                book.Id = i;
                book.Timestamp = new DateTime();
                BookRecordModel bookRecord = new BookRecordModel();
                bookRecord.BookRecord = book;
                bookRecord.CurrentCondition = i < 2 ? BookCondition.BORROW : BookCondition.AVAILABLE;
                bookRecordList.Add(bookRecord);
            }
            bookInfoRecord.Books = bookRecordList;

            List<CommentRecordModel> comments = new List<CommentRecordModel>();
            for (int i = 0; i < BookInfoRecordModel.CommentHomePageCount; i++)
            {
                Comment comment = new Comment();
                comment.Content = "CommentContent" + i;
                User user = new User();
                user.Name = "User" + i;

                CommentRecordModel record = new CommentRecordModel();
                record.CommentMaker = user;
                record.Content = comment;
                
                comments.Add(record);
            }
            bookInfoRecord.TopComments = comments;

            ViewBag.BookInfoRecord = bookInfoRecord;
            ////////////////////////////////
            */

            return View();
        }

    }
}
