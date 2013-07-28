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
            

            ViewBag.Bookinfo = bookinfo;
            //bookinfo.Tags.Count();
            
            

            return View();
        }

    }
}
