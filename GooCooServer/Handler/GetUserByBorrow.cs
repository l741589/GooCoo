using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using GooCooServer.DAO;
using GooCooServer.Entity;
using GooCooServer.Exception;
using GooCooServer.IDAO;

namespace GooCooServer.Handler
{
    public class GetUserByBorrow : IHttpHandler
    {
        /// <summary>
        /// 您将需要在网站的 Web.config 文件中配置此处理程序 
        /// 并向 IIS 注册它，然后才能使用它。有关详细信息，
        /// 请参见下面的链接: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpHandler Members

        public bool IsReusable
        {
            // 如果无法为其他请求重用托管处理程序，则返回 false。
            // 如果按请求保留某些状态信息，则通常这将为 false。
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            IUser_BookDAO ub = DAOFactory.createDAO("User_BookDAO") as IUser_BookDAO;
            IBook_BookInfoDAO bb = DAOFactory.createDAO("Book_BookInfoDAO") as IBook_BookInfoDAO;
            List<User> users = new List<User>();
            if (bb != null && ub != null)
            {
                List<Book> lbs = bb.GetBook(context.Request["isbn"]);
                foreach (var e in lbs)
                {
                    users.Add(ub.GetUser(e.Id));
                }
            }
            else
            {
                if (context.Request["isbn"] == null) throw new BMException("参数错误");
                User user = new User();
                user.Id = "1123435";
                user.Name = "ewrwrew";
                user.Authority = User.EAuthority.USER;
                users.Add(user);

                user = new User();
                user.Id = "2342132";
                user.Name = "书而已";
                user.Authority = User.EAuthority.USER;
                users.Add(user);

                user = new User();
                user.Id = "1123345";
                user.Name = "孙建华";
                user.Authority = User.EAuthority.ADMIN;
                users.Add(user);
            }
            StringBuilder ret = new StringBuilder();
            new JavaScriptSerializer().Serialize(users, ret);
            context.Response.Output.Write(ret.ToString());
        }
        #endregion
    }
}
