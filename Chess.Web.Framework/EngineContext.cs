using Chess.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Web.Framework
{
    /// <summary>
    /// 引擎上下文
    /// </summary>
    public class EngineContext
    {
        /// <summary>
        /// 初始化引擎
        /// </summary>
        /// <param name="isInstance"></param>
        /// <returns></returns>
        public static Engine Init(bool isInstance = false)
        {
            if (Singleton<Engine>.Instance == null || isInstance)
            {
                var engine = new Engine();
                engine.Init();
                Singleton<Engine>.Instance = engine;
            }
            return Singleton<Engine>.Instance;
        }

        /// <summary>
        /// 获取引擎实例
        /// </summary>
        public static Engine Current
        {
            get
            {
                if (Singleton<Engine>.Instance == null)
                {
                    Init(true);
                }
                return Singleton<Engine>.Instance;
            }
        }
    }
}
