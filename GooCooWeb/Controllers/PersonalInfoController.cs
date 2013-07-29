using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GooCooServer.DAO;
using System.Web.Routing;
using GooCooServer.IDAO;
using GooCooServer.Exception;
using GooCooWeb.Models;

namespace GooCooWeb.Controllers
{

    public class PersonalInfoController : Controller
    {
        //
        // GET: /PersonalInfo/

        //  [LoggedOnFilter]
        public ActionResult Index()
        {
            PersonalInfoModel model = new PersonalInfoModel();
            model.Id = "1152789";
            model.PhoneNumer = "18817369213";
            model.Name = "林凡";
            return View(model);
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