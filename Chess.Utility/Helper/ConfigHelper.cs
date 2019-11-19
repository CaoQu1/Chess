using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Chess.Utility
{
    /// <summary>
    /// 配置文件帮助类
    /// </summary>
    public class ConfigHelper
    {
        /// <summary>
        /// 主站域名
        /// </summary>
        public static string WebServerDamain
        {
            get
            {
                return GetAppSetting(ConfigConst.WEBSERVER);
            }
        }

        /// <summary>
        /// 数据库连接
        /// </summary>
        public static string DataContext
        {
            get
            {
                return GetConfigSetting(ConfigConst.DATACONTEXT);
            }
        }

        /// <summary>
        /// 获取token
        /// </summary>
        public static string Token
        {
            get
            {
                return GetAppSetting(ConfigConst.TOKEN);
            }
        }

        /// <summary>
        /// 短网址
        /// </summary>
        public static string ShortUrl
        {
            get
            {
                return GetAppSetting(ConfigConst.SHORTURL);
            }
        }

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConfigSetting(string key)
        {
            return ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }

        /// <summary>
        /// 获取配置文件路径
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetAppSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        /// <summary>
        /// 获取物理路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetPhysicsPath(string path)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(path);
            }
            return Path.Combine(HttpRuntime.AppDomainAppPath, path);
        }
    }
}
