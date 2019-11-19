using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Utility
{
    /// <summary>
    /// 内存缓存接口
    /// </summary>
    public interface IMemoryCacheManager : IBaseCacheManager<object>
    {
        /// <summary>
        /// 添加缓存(文件依赖)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="path"></param>
        void Add(string key, object value, string path);
    }

    /// <summary>
    /// cookie缓存接口
    /// </summary>
    public interface ICookieCacheManager : IBaseCacheManager<String>
    {

    }

    /// <summary>
    /// 基础缓存接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseCacheManager<T>
    {
        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void Add(string key, T value);

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="dateTimeOffset">绝对过期时间</param>
        void Add(string key, T value, int dateTimeOffset);

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get(string key);

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool IsExists(string key);

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);
    }
}
