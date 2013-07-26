using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GooCooServer.Entity;


namespace GooCooWeb.Controllers
{
    public class BookInfoController : Controller
    {
        //
        // GET: /BookInfo/

        public ActionResult Index(string isbn)
        {
            BookInfo bookinfo = new BookInfo();
            bookinfo.Isbn = isbn;
            bookinfo.Name = "程序设计与项目实训教程";
            string[] tags = new string[3];
            tags[0] = "标签1";
            tags[1] = "标签2";
            tags[2] = "标签3";
            bookinfo.Tags = tags;

            ViewBag.Bookinfo = bookinfo;
            

            return View();
        }

    }
}
