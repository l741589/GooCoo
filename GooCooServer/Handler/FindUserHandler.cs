using System;
using System.Collections.ObjectModel;
using System.Web;
using GooCooServer.DAO;
using GooCooServer.Entity;
using GooCooServer.IDAO;
using GooCooServer.Exception;
using System.Collections.Generic;
using System.Text;
using System.Web.Script.Serialization;
using GooCooServer.Utility;

namespace GooCooServer.Handler
{
    public class FindUserHandler : IHttpHandler
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

            IUserDAO userDAO = (IUserDAO)DAOFactory.createDAO("UserDAO");
            
            List<User> users;
            StringBuilder ret=new StringBuilder();
            if (userDAO != null)
            {
                try
                {
                    String keyword = context.Request["keyword"];
                    if (keyword == null) keyword = "";
                    users = userDAO.GetByKeyword(keyword);
                }
                catch(BMException e)
                {
                    users = new List<User>();
                }
            }
            else
            {
                users = new List<User>();
                User user=new User();
                user.Id = "1123345";
                user.Name = "ewrwrew";
                user.Authority = User.EAuthority.USER;
                users.Add(user);

                user = new User();
                user.Id = "2342123";
                user.Name = "上官郝";
                user.Authority = User.EAuthority.USER;
                users.Add(user);

                user = new User();
                user.Id = "1123346";
                user.Name = "佘文涛";
                user.Authority = User.EAuthority.USER;
                users.Add(user);

            }
            context.Response.Output.Write(Util.EncodeJson(users));
        }

        #endregion
    }
}
