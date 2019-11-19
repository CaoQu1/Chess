using Chess.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.DtoModel;
using Chess.Utility;
using Chess.Service;
using System.Web.Security;

namespace Chess.Web.Framework
{
    /// <summary>
    /// web上下文
    /// </summary>
    public class WebContext : IWebContext
    {
        /// <summary>
        /// 用户服务
        /// </summary>
        public ISystemUserService SystemUserService { get; set; }

        /// <summary>
        /// 内存缓存服务
        /// </summary>
        public IMemoryCacheManager CacheManager { get; set; }

        /// <summary>
        /// cookie服务
        /// </summary>
        public ICookieCacheManager CookieManager { get; set; }

        /// <summary>
        /// 当前登录用户
        /// </summary>
        private SystemUser _currentUser;
        public SystemUser CurrentUser
        {
            get
            {
                //if (this._currentUser == null)
                //{
                var value = CookieManager.Get(ConfigConst.USERCOOKIE);
                int userId = 0;
                LogHelper.Info($"WebContext=>SystemUser=>CurrentUser=>GET,>>>>>>>,cookie.value=>{value}");
                if (int.TryParse(value, out userId))
                {
                    //var systemUser = CacheManager.GetOrAdd<SystemUser>(string.Format(ConfigConst.CACHEUSER, userId), () =>
                    // {
                    var user = this.SystemUserService.GetAllListAsNoTrack().FirstOrDefault(x => x.Id == userId);
                    if (user != null)
                    {
                        LogHelper.Info($"WebContext=>SystemUser=>CurrentUser=>GET,>>>>>>>,datebase.user=>{user.Id},{user.AgentId}");
                    }
                    //});
                    this._currentUser = user;
                }
                //}
                return this._currentUser;

            }

            set
            {
                this._currentUser = null;
                CookieManager.Remove(ConfigConst.USERCOOKIE);
                if (value == null)
                {
                    FormsAuthentication.SignOut();
                }
                else
                {

                    LogHelper.Info($"WebContext=>SystemUser=>CurrentUser=>SET>>>>>>>,value=>{value.AgentId},{value.Id},{value.UserIdentity.ToString()}");

                    //this.CacheManager.Remove(string.Format(ConfigConst.CACHEUSER, value.Id));

                    //this.CacheManager.Add(string.Format(ConfigConst.CACHEUSER, value.Id), value);

                    this.CookieManager.Add(ConfigConst.USERCOOKIE, value.Id.ToString());

                    FormsAuthentication.SetAuthCookie(value.Id.ToString(), true);
                }
                LogHelper.Info($"WebContext=>SystemUser=>CurrentUser=>SET>>>>>>>SUCCESS!!!!!");
                this._currentUser = value;
            }
        }

        /// <summary>
        /// 实例编号
        /// </summary>
        public Guid InstanceId
        {
            get
            {
                return Guid.NewGuid();
            }
        }
    }
}
