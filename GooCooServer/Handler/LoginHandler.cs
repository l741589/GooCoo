using System;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using GooCooServer.DAO;
using GooCooServer.Entity;
using GooCooServer.Exception;
using GooCooServer.IDAO;
using GooCooServer.Utility;
using GooCooServer.Entity.Ex;

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
                UserEx user;
                String session;
                if (userDao != null)
                {
                    session = userDao.Login(id, pw);
                    user = Util.CloneEntity<UserEx>(userDao.Get(session));
                    user.Session = session;
                }
                else
                {
                    if (id == "1152788" && pw == "1234")
                    {
                        user = new UserEx();
                        user.Name = "测试";
                        user.Id = "1152788";
                        user.Authority = User.EAuthority.SUPERADMIN;
                    }else if (id == "2789665" && pw == "1111")
                    {
                        user = new UserEx();
                        user.Name = "小明";
                        user.Id = "2789665";
                        user.Authority = User.EAuthority.USER;
                    }
                    else
                    {
                        throw new BMException("登录失败");
                    }
                    session = "1213234456767455465";
                }
                user.Session = session;
                context.Response.Output.Write(Util.EncodeJson(user));
            }
            catch (BMException)
            {
            }
            catch (NullReferenceException)
            {
            }
        }

        #endregion
    }
}
