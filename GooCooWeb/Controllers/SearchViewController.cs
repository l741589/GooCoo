using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GooCooServer.IDAO;
using GooCooServer.DAO;

namespace GooCooWeb.Controllers
{
    public class SearchViewController : Controller
    {
        //
        // GET: /SearchView/
        public ActionResult Index()
        {
            ViewBag.SearchType = "标题";      //默认搜索方法为标题搜索               
            return View();
        }

        public ActionResult Index(string keyword, string type, int page)
        {
            const int recordPerPage = 20;

            IBookInfoDAO bookInfoDao = DAOFactory.createDAO("IBookInfoDao") as IBookInfoDAO;

            ViewBag.SearchType = type;        //设置搜索方法

            int resultCount = 0;
            Array resultArray = null;

            if (type.Equals("标题"))
            {
                resultCount = bookInfoDao.GetCountByIsbn(keyword);

            }
            else if (type.Equals("ISBN"))
            { 
            }
            else if (type.Equals("模糊"))
            { 

            }            

            return View();

            //return View("NoResult");
        }

    }
}
