﻿using System;
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
        public ActionResult UpdateInfo()
        {
            //获取用户基本信息
            IUserDAO userDAO = DAOFactory.createDAO("UserDAO") as IUserDAO;
            User user = userDAO.Get((string)Session["UserSessionID"]);
            PersonalInfoModel model = new PersonalInfoModel(user);
            return View(model);
        }

        [LoggedOnFilter]
        [HttpPost]
        public ActionResult UpdateInfo(PersonalInfoModel model)
        {
            if (ModelState.IsValid)
            {
                IUserDAO userDAO = DAOFactory.createDAO("UserDAO") as IUserDAO;

                User user = new User();
                user.Id = model.Id;
                user.Name = model.Name;
                user.Password = model.Password;
                user.Phonenumber = model.PhoneNumber;
                user.Email = model.Email;
                user.Repvalue = 0;
                user.Authority = (GooCooServer.Entity.User.EAuthority)model.Authority;

                string userSessionID = (string)Session["UserSessionID"];
                userDAO.Set(userSessionID, user);
                return Redirect("/PersonalInfo/Index");
            }

            return View(model);
        }

        [LoggedOnFilter]
        public ActionResult BorrowInfo()
        {
            BorrowInfoModel model = new BorrowInfoModel();
            IUserDAO userDAO = DAOFactory.createDAO("UserDAO") as IUserDAO;
            User localUser = userDAO.Get((string)Session["UserSessionID"]);
            IUser_BookDAO user_bookDAO = DAOFactory.createDAO("User_BookDAO") as IUser_BookDAO;
            try
            {
                List<Book> books = user_bookDAO.GetBook(localUser.Id, User_Book.ERelation.BORROW);
                ViewBag.BorrowBookNumber = books.Count;

                IBook_BookInfoDAO book_bookinfoDAO = DAOFactory.createDAO("Book_BookInfoDAO") as IBook_BookInfoDAO;
                foreach (Book book in books)
                {
                    BookInfo bookInfo = book_bookinfoDAO.GetBookInfo(book.Id);

                    BorrowBookInfo borrowBookInfo = new BorrowBookInfo();
                    borrowBookInfo.Id = book.Id;
                    borrowBookInfo.Name = bookInfo.Name;
                    borrowBookInfo.Isbn = bookInfo.Isbn;
                    borrowBookInfo.BorrowTime = user_bookDAO.Get(localUser.Id, book.Id, User_Book.ERelation.BORROW).Timestamp;
                    borrowBookInfo.ExpectedReturnTime = Book.getReturnTime(borrowBookInfo.BorrowTime);
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
            IUserDAO userDAO = DAOFactory.createDAO("UserDAO") as IUserDAO;
            User localUser = userDAO.Get((string)Session["UserSessionID"]);
            IUser_BookDAO user_bookDAO = DAOFactory.createDAO("User_BookDAO") as IUser_BookDAO;
            try
            {
                List<Book> books = user_bookDAO.GetBook(localUser.Id, User_Book.ERelation.DONATE);
                ViewBag.DonateBookNumber = books.Count;

                IBook_BookInfoDAO book_bookinfoDAO = DAOFactory.createDAO("Book_BookInfoDAO") as IBook_BookInfoDAO;
                foreach (Book book in books)
                {
                    BookInfo bookInfo = book_bookinfoDAO.GetBookInfo(book.Id);

                    DonateBookInfo donateBookInfo = new DonateBookInfo();
                    donateBookInfo.Id = book.Id;
                    donateBookInfo.Name = bookInfo.Name;
                    donateBookInfo.Isbn = bookInfo.Isbn;
                    donateBookInfo.DonateTime = book.Timestamp;
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
            IUserDAO userDAO = DAOFactory.createDAO("UserDAO") as IUserDAO;
            User localUser = userDAO.Get((string)Session["UserSessionID"]);
            IUser_BookInfoDAO user_bookinfoDAO = DAOFactory.createDAO("User_BookInfoDAO") as IUser_BookInfoDAO;
            List<BookInfo> books = null;
            try
            {
                books = user_bookinfoDAO.GetBookInfo(localUser.Id, User_BookInfo.ERelation.FAVOR);
            }
            catch (BMException)
            {
                books = new List<BookInfo>();
            }
            CollectInfoModel model = new CollectInfoModel(books);
            return View(model);
        }

        [LoggedOnFilter]
        public ActionResult PreorderInfo()
        {
            PreorderInfoModel model = new PreorderInfoModel();
            IUserDAO userDAO = DAOFactory.createDAO("UserDAO") as IUserDAO;
            User localUser = userDAO.Get((string)Session["UserSessionID"]);
            IUser_BookInfoDAO user_bookinfoDAO = DAOFactory.createDAO("User_BookInfoDAO") as IUser_BookInfoDAO;
            try
            {
                List<BookInfo> books = user_bookinfoDAO.GetBookInfo(localUser.Id, User_BookInfo.ERelation.ORDER);
                ViewBag.PreorderBookNumber = books.Count;
 
                IBook_BookInfoDAO book_bookinfoDAO = DAOFactory.createDAO("Book_BookInfoDAO") as IBook_BookInfoDAO;
                foreach (BookInfo bookInfo in books)
                {
                    PreorderBookInfo preorderBookInfo = new PreorderBookInfo();
                    preorderBookInfo.Isbn = bookInfo.Isbn;
                    preorderBookInfo.Name = bookInfo.Name;
                    preorderBookInfo.Author = bookInfo.Author;
                    preorderBookInfo.BorrowedNumber = book_bookinfoDAO.GetBook(bookInfo.Isbn).Count - book_bookinfoDAO.GetAvaliableBookNumber(bookInfo.Isbn);
                    try
                    {
                        preorderBookInfo.PreorderNumber = user_bookinfoDAO.GetUser(bookInfo.Isbn, User_BookInfo.ERelation.ORDER).Count;
                    }
                    catch (BMException)
                    {
                        preorderBookInfo.PreorderNumber = 0;
                    }

                    User_BookInfo user_bookInfo = user_bookinfoDAO.Get(bookInfo.Isbn, localUser.Id, User_BookInfo.ERelation.ORDER);
                    preorderBookInfo.PreorderDate = user_bookInfo.Timestamp;
                    model.Add(preorderBookInfo);
                }
            }
            catch (BMException)
            {
                ViewBag.PreorderBookNumber = 0;
            }
            return View(model);
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