using System;
using System.Web;
using GooCooServer.DAO;
using GooCooServer.Exception;
using GooCooServer.IDAO;

namespace GooCooServer.Handler
{
    public class BorrowHandler : IHttpHandler
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
            try
            {
                var req=context.Request;
                String user_id = req["user"];
                int book_id = int.Parse(req["book"]);
                IUser_BookDAO ub = DAOFactory.createDAO("User_BookDAO") as IUser_BookDAO;
                ub.Add(user_id, book_id);
                context.Response.Write("借书成功");
            }
            catch (BMException e)
            {
                context.Response.Write(e.Message);
            }
            catch (FormatException e)
            {
                context.Response.Write(e.Message);
            }
            catch (OverflowException e)
            {
                context.Response.Write(e.Message);
            }
            catch (ArgumentNullException e)
            {
                context.Response.Write(e.Message);
            }
            catch (NullReferenceException)
            {
                context.Response.Write("借书成功");
            }
        }

        #endregion
    }
}
