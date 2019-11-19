using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Utility
{
    /// <summary>
    /// 接口返回数据
    /// </summary>
    public class ResultJson<T>
    {
        /// <summary>
        /// 编码
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public T data { get; set; }
    }
}
