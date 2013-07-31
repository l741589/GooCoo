using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GooCooServer.IDAO;
using GooCooServer.DAO;
using GooCooServer.Entity;

namespace GooCooWeb.Controllers.ajax
{
    public class AjaxCommentController : Controller
    {
        //
        // GET: /AjaxComment/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddComment(string content, int bookInfoId)
        {
            try
            {
                ICommentDAO commentDAO = DAOFactory.createDAO("CommentDAO") as ICommentDAO;
                IUser_CommentDAO user_commentDAO = DAOFactory.createDAO("User_CommentDAO") as IUser_CommentDAO;

                Comment comment = new Comment();
                comment.Content = content;
                return Json(new { result = true });
            }
            catch (Exception)
            {
                return Json(new { result = false });
            }

        }
    }
}
