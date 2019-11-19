using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;

namespace Chess.Utility
{
    /// <summary>
    /// 缓存帮助类
    /// </summary>
    public class CacheManager : IMemoryCacheManager
    {
        /// <summary>
        /// 缓存对象
        /// </summary>
        private ObjectCache _memoryCache
        {
            get
            {
                return MemoryCache.Default;
            }
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, object value)
        {
            Add(key, value, null, null);
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <param name="path">依赖文件路径</param>
        public void Add(string key, object value, string path)
        {
            Add(key, value, null, null, path);
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <param name="dateTimeOffset">绝对过期时间</param>
        public void Add(string key, object value, int dateTimeOffset)
        {
            Add(key, value, dateTimeOffset, null);
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <param name="timeSpan">滑动过期时间</param>
        public void Add(string key, object value, TimeSpan timeSpan)
        {
            Add(key, value, null, timeSpan);
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, object value, int? dateTimeOffset = null, TimeSpan? timeSpan = null, string path = "")
        {
            CacheItem cacheItem = new CacheItem(key, value);
            CacheItemPolicy policy = new CacheItemPolicy();
            if (!string.IsNullOrEmpty(path))
            {
                CreateFilePolicy(policy, path);
            }
            if (dateTimeOffset.HasValue || timeSpan.HasValue)
            {
                CreatePolicy(policy, dateTimeOffset, timeSpan);
            }
            _memoryCache.Set(cacheItem, policy);
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public object Get(string key)
        {
            return IsExists(key) ? _memoryCache[key] : null;
        }

        /// <summary>
        /// 是否存在缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsExists(string key)
        {
            return _memoryCache.Contains(key);
        }

        /// <summary>
        /// 移除缓存项
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            if (IsExists(key))
            {
                _memoryCache.Remove(key);
            }
        }

        #region private
        /// <summary>
        /// 创建缓存过期信息
        /// </summary>
        /// <param name="absoluteExpiration"></param>
        /// <param name="SlidingExpiration"></param>
        /// <returns></returns>
        private void CreatePolicy(CacheItemPolicy policy, int? absoluteExpiration = null, TimeSpan? SlidingExpiration = null)
        {
            if (absoluteExpiration.HasValue)
            {
                policy.AbsoluteExpiration = DateTime.Now.AddMinutes(absoluteExpiration.Value);
            }
            if (SlidingExpiration.HasValue)
            {
                policy.SlidingExpiration = SlidingExpiration.Value;
            }
            policy.RemovedCallback += RemovedCallback;
            //policy.UpdateCallback += UpdateCallback;
        }

        /// <summary>
        /// 创建文件依赖项
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private void CreateFilePolicy(CacheItemPolicy policy, string path)
        {
            policy.ChangeMonitors.Add(new HostFileChangeMonitor(new List<string> { path }));
            policy.RemovedCallback += RemovedCallback;
            //policy.UpdateCallback += UpdateCallback;
        }

        /// <summary>
        /// 缓存移除某项后
        /// </summary>
        /// <param name="cacheEntryRemovedArguments"></param>
        private void RemovedCallback(CacheEntryRemovedArguments cacheEntryRemovedArguments)
        {
            StringBuilder builder = new StringBuilder("移除原因：");
            switch (cacheEntryRemovedArguments.RemovedReason)
            {
                case CacheEntryRemovedReason.Removed:
                    builder.Append("主动调用了set|remove方法");
                    break;
                case CacheEntryRemovedReason.Expired:
                    builder.Append("缓存已过期");
                    break;
                case CacheEntryRemovedReason.Evicted:
                    builder.Append("缓存内存溢出");
                    break;
                case CacheEntryRemovedReason.ChangeMonitorChanged:
                    builder.Append("文件被更改");
                    break;
                case CacheEntryRemovedReason.CacheSpecificEviction:
                    break;
                default:
                    break;
            }
            if (cacheEntryRemovedArguments.CacheItem != null)
            {
                builder.AppendLine($"移除对象:{cacheEntryRemovedArguments.CacheItem.Key}={cacheEntryRemovedArguments.CacheItem.Value}");
            }
            LogHelper.Info(builder.ToString());
        }

        /// <summary>
        /// 缓存移除某项前
        /// </summary>
        /// <param name="cacheEntryUpdateArguments"></param>
        private void UpdateCallback(CacheEntryUpdateArguments cacheEntryUpdateArguments)
        {
            StringBuilder builder = new StringBuilder();
            switch (cacheEntryUpdateArguments.RemovedReason)
            {
                case CacheEntryRemovedReason.Removed:
                    break;
                case CacheEntryRemovedReason.Expired:
                    break;
                case CacheEntryRemovedReason.Evicted:
                    break;
                case CacheEntryRemovedReason.ChangeMonitorChanged:
                    break;
                case CacheEntryRemovedReason.CacheSpecificEviction:
                    break;
                default:
                    break;
            }
            if (cacheEntryUpdateArguments.UpdatedCacheItem != null)
            {
                builder.AppendLine($"移除对象前:{cacheEntryUpdateArguments.UpdatedCacheItem.Key}={cacheEntryUpdateArguments.UpdatedCacheItem.Value}");
            }
            LogHelper.Info(builder.ToString());
        }
        #endregion
    }
}
