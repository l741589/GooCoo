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
    public class AddBook : IHttpHandler
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
                IBookDAO bdao = DAOFactory.createDAO("BookDAO") as IBookDAO;
                String sbook = context.Request["book"];
                int count = int.Parse(context.Request["count"]);
                if (sbook == null) throw new NullReferenceException("user域为空");
                BookEx book = Util.DecodeJson<BookEx>(sbook);
                book.SetCount(count);
                if (book == null) throw new NullReferenceException("user解析失败");
                BookEx b = Util.CloneEntity<BookEx>(dao.Add(book));
                if (book.Count != 0)
                {
                    b.Books = new List<BookEx.Book>();
                    for (int i = 0; i < book.Count; ++i)
                    {
                        Book bk = bdao.Add(b);
                        BookEx.Book bex = new BookEx.Book();
                        bex.Id = bk.Id;
                        bex.Owner = null;
                        b.Books.Add(bex);
                    }
                }
                context.Response.Write(Util.EncodeJson(book, "成功"));
            }
            catch (NullReferenceException e)
            {
                context.Response.Write(e.Message);
            }
            catch (BMException e)
            {
                context.Response.Write(e.Message);
            }
            catch (ArgumentNullException e)
            {
                context.Response.Write(e.Message);
            }
            catch (FormatException e)
            {
                context.Response.Write(e.Message);
            }
        }

        #endregion
    }
}
