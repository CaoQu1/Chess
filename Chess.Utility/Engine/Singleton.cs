using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Utility
{
    /// <summary>
    /// 单例容器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SingletonList<T>
    {
        private static ConcurrentDictionary<string, T> singletons;

        static SingletonList()
        {
            singletons = new ConcurrentDictionary<string, T>();
        }

        public static ConcurrentDictionary<string, T> AllSingletons
        {
            get
            {
                return singletons;
            }
        }
    }

    /// <summary>
    /// 单例管理
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Singleton<T> : SingletonList<T> where T : class, new()
    {
        /// <summary>
        /// 单例
        /// </summary>
        public static T Instance
        {
            get
            {
                return AllSingletons.ContainsKey(nameof(T)) ? AllSingletons[nameof(T)] : null;
            }
            set
            {
                AllSingletons[nameof(T)] = value;
            }
        }
    }
}
