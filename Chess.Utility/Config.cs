using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Utility
{
    /// <summary>
    /// 常量配置
    /// </summary>
    public class ConfigConst
    {
        #region 节点名称（获取节点值）

        /// <summary>
        /// 邮件配置
        /// </summary>
        public const string EMAIL = "Email";

        /// <summary>
        /// api接口配置
        /// </summary>
        public const string APIURL = "ThredPart";

        /// <summary>
        /// 数据库连接
        /// </summary>
        public const string DATACONTEXT = "DataContext";

        /// <summary>
        /// 日志
        /// </summary>
        public const string LOG = "Log4net";

        /// <summary>
        /// 主站链接
        /// </summary>
        public const string WEBSERVER = "WebServerDomain";

        /// <summary>
        /// 文件扩展名
        /// </summary>
        public const string FILEEXT = "FileExt";

        /// <summary>
        /// 保存路径
        /// </summary>
        public const string SAVEPATH = "SavePath";

        /// <summary>
        /// 服务器授权
        /// </summary>
        public const string AUTH2 = "ThredPartAuth2";

        /// <summary>
        /// token
        /// </summary>
        public const string TOKEN = "Token";

        /// <summary>
        /// 短网址
        /// </summary>
        public const string SHORTURL = "ShortUrl";
        #endregion

        #region 缓存key

        /// <summary>
        /// cookie键
        /// </summary>
        public const string USERCOOKIE = "Chess.User";

        /// <summary>
        /// 内存键
        /// </summary>
        public const string CACHEUSER = "Chess.User.{0}";

        /// <summary>
        /// 站内来源地址
        /// </summary>
        public const string RECORDCOOKIE = "Return.Url";

        #endregion
    }
}
