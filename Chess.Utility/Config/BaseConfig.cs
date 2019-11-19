using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Utility
{
    /// <summary>
    /// 抽象配置基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseConfig<T> where T : BaseConfig<T>, new()
    {
        /// <summary>
        /// 锁
        /// </summary>
        private static readonly Object lockobj = new object();

        /// <summary>
        /// 缓存
        /// </summary>
        private static ConcurrentDictionary<int, T> _cacheConfig = new ConcurrentDictionary<int, T>();

        /// <summary>
        /// 路径
        /// </summary>
        private string _path;

        /// <summary>
        /// key
        /// </summary>
        private int _filekey
        {
            get
            {
                return IsExists ? this._path.GetHashCode() : 0;
            }
        }

        /// <summary>
        /// 文件是否存在
        /// </summary>
        public bool IsExists
        {
            get
            {
                return !string.IsNullOrEmpty(this._path) && File.Exists(this._path);
            }
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="path"></param>
        public BaseConfig(string path)
        {
            this._path = ConfigHelper.GetPhysicsPath(ConfigHelper.GetAppSetting(path));
            this.loadConfig();
        }

        /// <summary>
        /// 单例
        /// </summary>
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (lockobj)
                    {
                        if (_instance == null)
                        {
                            _instance = new T().loadConfig();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// 初始化配置文件
        /// </summary>
        /// <returns></returns>
        public abstract T initConfig();

        /// <summary>
        /// 加载配置文件
        /// </summary>
        /// <returns></returns>
        public virtual T loadConfig()
        {
            var model = _cacheConfig[this._filekey];
            model = model ?? this.initConfig();
            _cacheConfig[this._filekey] = model;
            return model;
        }

        /// <summary>
        /// 保存配置文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        public virtual void saveConfig(T model)
        {
            SerializeHelper.Serialze<T>(this._path, model);
        }
    }
}
