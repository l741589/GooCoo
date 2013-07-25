using System;
using System.Collections.Generic;
using System.Web;
using GooCooServer.DAO;
using GooCooServer.Entity;
using GooCooServer.Entity.Ex;
using GooCooServer.Exception;
using GooCooServer.IDAO;
using GooCooServer.Utility;

namespace GooCooServer.Handler
{
    public class GetUserByFavor : IHttpHandler
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
            IUser_BookInfoDAO ub = DAOFactory.createDAO("User_BookInfoDAO") as IUser_BookInfoDAO;
            List<UserEx> users = new List<UserEx>();
            String book_isbn = context.Request["isbn"];
            if (ub != null)
            {
                List<User> lus = ub.GetUser(book_isbn);
                User avaliableuser = ub.GetAvaliableUser(book_isbn);
                foreach (var e in lus)
                {
                    UserEx u = Util.CloneEntity<UserEx>(e);
                    u.Orders.Add(book_isbn);
                    users.Add(u);
                }
            }
            else
            {
                if (context.Request["isbn"] == null) throw new BMException("参数错误");
                UserEx user = new UserEx();
                user.Id = "9993435";
                user.Name = "康火平";
                user.Authority = UserEx.EAuthority.USER;
                users.Add(user);

                user = new UserEx();
                user.Id = "2799965";
                user.Name = "康海东";
                user.Authority = UserEx.EAuthority.USER;
                users.Add(user);

                user = new UserEx();
                user.Id = "1128885";
                user.Name = "潘庆伟";
                user.Authority = UserEx.EAuthority.ADMIN;
                users.Add(user);
            }
            context.Response.Output.Write(Util.EncodeJson(users));
        }

        #endregion
    }
}
