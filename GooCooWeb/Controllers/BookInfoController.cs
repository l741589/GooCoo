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
           
     

            //ViewBag.Bookinfo = bookinfo;
            
            return View();
        }

    }
}
