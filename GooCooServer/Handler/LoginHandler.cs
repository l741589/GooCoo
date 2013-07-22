using System;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using GooCooServer.DAO;
using GooCooServer.Entity;
using GooCooServer.Exception;
using GooCooServer.IDAO;

namespace GooCooServer.Handler
{
    public class LoginHandler : IHttpHandler
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
            String id = context.Request["id"];
            String pw = context.Request["pw"];
            IUserDAO userDao = (IUserDAO)DAOFactory.createDAO("UserDAO");
            try
            {
                User user;
                if (userDao != null)
                {
                    String session = userDao.Login(id, pw);
                    user = userDao.Get(session);
                    
                }
                else
                {
                    if (id == "1152788" && pw == "1234")
                    {
                        user = new User();
                        user.Name = "测试";
                        user.Id = "1152788";
                        user.Authority = User.EAuthority.ADMIN;
                    }
                    else
                    {
                        throw new BMException("登录失败");
                    }
                }
                StringBuilder ret = new StringBuilder();
                new JavaScriptSerializer().Serialize(user, ret);
                context.Response.Output.Write(ret.ToString());
            }
            catch(BMException e)
            {
            }
            catch(NullReferenceException e)
            {
            }
        }

        #endregion
    }
}
