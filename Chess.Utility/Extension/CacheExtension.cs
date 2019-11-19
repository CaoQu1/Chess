using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Utility
{
    /// <summary>
    /// 缓存扩展类
    /// </summary>
    public static class CacheExtension
    {
        /// <summary>
        /// 获取缓存（不存在添加）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheManager"></param>
        /// <param name="func"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static T GetOrAdd<T>(this IMemoryCacheManager cacheManager, string key, Func<T> func, int timeout = 60)
        {
            LogHelper.Info($"cache.key=>{key}");
            if (cacheManager.IsExists(key))
            {
                LogHelper.Info($"cache.IsExists=>{true}");
                var cache_t_result = (T)cacheManager.Get(key);
                return cache_t_result;
            }
            var result = func();
            if (result != null)
            {
                LogHelper.Info($"cache.value=>{result}");
                cacheManager.Add(key, result, timeout);
            }
            return result;
        }
    }
}
