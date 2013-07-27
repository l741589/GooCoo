using System;
using System.Web;
using GooCooServer.DAO;
using GooCooServer.Entity;
using GooCooServer.Exception;
using GooCooServer.IDAO;
using GooCooServer.Utility;

namespace GooCooServer.Handler
{
    public class UpdateBook : IHttpHandler
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
                IBookInfoDAO dao = DAOFactory.createDAO("BookInfoDAO") as IBookInfoDAO;
                String sbook = context.Request["book"];
                if (sbook == null) throw new NullReferenceException("user域为空");
                BookInfo book = Util.DecodeJson<BookInfo>(sbook);
                if (book == null) throw new NullReferenceException("user解析失败");
                dao.Set(book);
                context.Response.Write("修改成功");
            }
            catch (NullReferenceException e)
            {
                //context.Response.Write(e.Message);
                context.Response.Write("成功");
            }
            catch (BMException e)
            {
                context.Response.Write(e.Message);
            }
        }

        #endregion
    }
}
