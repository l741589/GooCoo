using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GooCooServer.DAO;
using System.Web.Routing;

namespace GooCooWeb.Controllers
{
    
    public class PersonalInfoController : Controller
    {
        //
        // GET: /PersonalInfo/

        [LoggedOnFilter]
        public ActionResult Index()
        {
            return View("PersonalInfo");
        }

        [LoggedOnFilter]
        public ActionResult BorrowInfo()
        {
            return View();
        }

        [LoggedOnFilter]
        public ActionResult DonateInfo()
        {
            return View();
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
            var userSessionID = filterContext.HttpContext.Session["UserSessionID"];
            //还要判断session是否过期
            if (userSessionID == null)
            {
                LogOn(filterContext);
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