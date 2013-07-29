using System;
using System.Collections.Generic;
using System.Web;
using GooCooServer.DAO;
using GooCooServer.Entity;
using GooCooServer.Exception;
using GooCooServer.IDAO;
using GooCooServer.Utility;

namespace GooCooServer.Handler
{
    public class GetLogHandler : IHttpHandler
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
                ILogDAO dao = DAOFactory.createDAO("LogDAO") as ILogDAO;
                String s=context.Request["time"];
                DateTime[] dates=Util.DecodeJson<DateTime[]>(s);
                List<Log> ret=dao.GetBetween(dates[0], dates[1]);
                context.Response.Write(Util.EncodeJson(ret));
            }
            catch(BMException)
            {
            }
            catch (NullReferenceException)
            {
                List<Log> ret = new List<Log>();
                ret.Add(new Log() { Id = 1, Content = "23243你反而贵额hi复旦hi哈覅额", Timestamp = DateTime.UtcNow });
                ret.Add(new Log() { Id = 2, Content = "哦护额哈广佛合法佛了佛号", Timestamp = DateTime.UtcNow });
                ret.Add(new Log() { Id = 3, Content = "弄org和欧日韩过户给热火给io", Timestamp = DateTime.UtcNow });
                ret.Add(new Log() { Id = 4, Content = "似乎疯狗恶劣红楼维护费光和热更哈皮兔肉，肉irejfr", Timestamp = DateTime.UtcNow });
                ret.Add(new Log() { Id = 5, Content = "恶化荣光和u两三点hi很疯狂荒废了网上客服", Timestamp = DateTime.UtcNow });
                context.Response.Write(Util.EncodeJson(ret));
            }
        }

        #endregion
    }
}
