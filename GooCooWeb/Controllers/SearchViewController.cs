using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GooCooWeb.Controllers
{
    public class SearchViewController : Controller
    {
        //
        // GET: /SearchView/

        public ActionResult Index(String keyword)
        {            
            return View();

            //return View("NoResult");
        }

    }
}
