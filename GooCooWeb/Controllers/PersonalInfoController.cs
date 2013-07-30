using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GooCooServer.DAO;
using System.Web.Routing;
using GooCooServer.Entity;
using GooCooServer.IDAO;
using GooCooServer.Exception;
using GooCooWeb.Models;

namespace GooCooWeb.Controllers
{
    
    public class PersonalInfoController : Controller
    {
        //
        // GET: /PersonalInfo/

        [LoggedOnFilter]
        public ActionResult Index()
        {
            //获取用户基本信息
            IUserDAO userDAO = DAOFactory.createDAO("UserDAO") as IUserDAO;
            User user = userDAO.Get((string)Session["UserSessionID"]);
            PersonalInfoModel model = new PersonalInfoModel(user);

            IUser_BookDAO user_bookDAO = DAOFactory.createDAO("User_BookDAO") as IUser_BookDAO;
            //获取用户的借阅册数
            try
            {
                List<Book> books = user_bookDAO.GetBook(user.Id, User_Book.ERelation.BORROW);
                model.BorrowBookNumer = books.Count;
            }
            catch (BMException)
            {
                model.BorrowBookNumer = 0;
            }

            //获取用户的捐赠册数
            try
            {
                List<Book> books = user_bookDAO.GetBook(user.Id, User_Book.ERelation.DONATE);
                model.DonateBookNumer = books.Count;
            }
            catch (BMException)
            {
                model.DonateBookNumer = 0;
            }

            model.Authority = PersonalInfoModel.EAuthority.USER;
            switch (model.Authority)
            {
                case PersonalInfoModel.EAuthority.ADMIN:
                    ViewBag.UserLevel = "管理员";
                    break;
                case PersonalInfoModel.EAuthority.SUPERADMIN:
                    ViewBag.UserLevel = "超级管理员";
                    break;
                default:
                    ViewBag.UserLevel = "普通用户";
                    break;
            }         
            return View(model);
        }

        [LoggedOnFilter]
        public ActionResult BorrowInfo()
        {
            BorrowInfoModel model = new BorrowInfoModel();
            IUser_BookDAO user_bookDAO = DAOFactory.createDAO("User_BookDAO") as IUser_BookDAO;
            try
            {
                List<Book> books = user_bookDAO.GetBook((string)Session["UserSessionID"], User_Book.ERelation.BORROW);
                ViewBag.BorrowBookNumber = books.Count;
                IBook_BookInfoDAO book_bookinfoDAO = DAOFactory.createDAO("Book_BookInfoDAO") as IBook_BookInfoDAO;
                foreach (Book book in books)
                {
                    BorrowBookInfo borrowBookInfo = new BorrowBookInfo();
                    borrowBookInfo.Id = book.Id;
                    BookInfo bookInfo = book_bookinfoDAO.GetBookInfo(book.Id);
                    borrowBookInfo.Name = bookInfo.Name;
                    borrowBookInfo.BorrowTime = bookInfo.Timestamp;
                    /* TO-DO 添加返回时间*/
                    //borrowBookInfo.ExpectedReturnTime = 
                    model.Add(borrowBookInfo);
                }
            }
            catch (BMException)
            {
                ViewBag.BorrowBookNumber = 0;
            }
            return View(model);
        }

        [LoggedOnFilter]
        public ActionResult DonateInfo()
        {
            DonateInfoModel model = new DonateInfoModel();
            IUser_BookDAO user_bookDAO = DAOFactory.createDAO("User_BookDAO") as IUser_BookDAO;
            try
            {
                List<Book> books = user_bookDAO.GetBook((string)Session["UserSessionID"], User_Book.ERelation.DONATE);
                ViewBag.BorrowBookNumber = books.Count;
                IBook_BookInfoDAO book_bookinfoDAO = DAOFactory.createDAO("Book_BookInfoDAO") as IBook_BookInfoDAO;
                foreach (Book book in books)
                {
                    DonateBookInfo donateBookInfo = new DonateBookInfo();
                    donateBookInfo.Id = book.Id;
                    BookInfo bookInfo = book_bookinfoDAO.GetBookInfo(book.Id);
                    donateBookInfo.Name = bookInfo.Name;
                    donateBookInfo.DonateTime = bookInfo.Timestamp;
                    model.Add(donateBookInfo);
                }
            }
            catch (BMException)
            {
                ViewBag.DonateBookNumber = 0;
            }
            return View(model);
        }

        [LoggedOnFilter]
        public ActionResult CollectInfo()
        {
            return View();
        }

        [LoggedOnFilter]
        public ActionResult PreorderInfo()
        {
            return View();
        }
    }

    //判断用户是否登录 否则跳转到登录页面
    public class LoggedOnFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            string userSessionID = (string)filterContext.HttpContext.Session["UserSessionID"];
            if (userSessionID == null)
            {
                HttpCookie cookie = filterContext.HttpContext.Request.Cookies["UserSessionID"];
                if (cookie != null)
                {
                    userSessionID = cookie.Value;
                    filterContext.HttpContext.Session["UserSessionID"] = userSessionID;
                }
            }
            //不存在连接
            if (userSessionID == null)
            {
                LogOn(filterContext);
            }
            else
            {
                IUserDAO userDAO = DAOFactory.createDAO("UserDAO") as IUserDAO;
                try
                {
                    userDAO.Get(userSessionID);
                }
                catch (BMException ex)
                {
                    //如果session已经过期
                    LogOn(filterContext);
                }
            }
        }

        //跳转到登录页面
        private void LogOn(ActionExecutingContext filterContext)
        {
            RouteValueDictionary dictionary = new RouteValueDictionary
            (
                new 
                { 
                    controller = "Account",
                    action = "LogOn",
                    returnUrl = filterContext.HttpContext.Request.RawUrl
                }
            );
            filterContext.Result = new RedirectToRouteResult(dictionary);
        }
    }
}