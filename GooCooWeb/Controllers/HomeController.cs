using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GooCooServer.DAO;
using GooCooServer.IDAO;
using GooCooServer.Entity;

namespace GooCooWeb.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            IBookInfoDAO bookInfoDao = DAOFactory.createDAO("BookInfoDAO") as IBookInfoDAO;
            List<BookInfo> recommentBooks = bookInfoDao.GetBookInfos(3);
            ViewBag.RecommentBooks = recommentBooks;
            return View();
        }

    }
}
