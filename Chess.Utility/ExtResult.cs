using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Utility
{
    /// <summary>
    /// 结果描述类
    /// </summary>
    public class ExtResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public Object Data { get; set; }

        /// <summary>
        /// ctor
        /// </summary>
        public ExtResult() { }

        /// <summary>
        /// 自动ctor
        /// </summary>
        /// <param name="isSuccess"></param>
        public ExtResult(bool isSuccess)
        {
            this.IsSuccess = isSuccess;
        }

        /// <summary>
        /// 自动ctor
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <param name="message"></param>
        public ExtResult(bool isSuccess, string message) : this(isSuccess)
        {
            this.Message = message;
        }

        /// <summary>
        /// 自动ctor
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        public ExtResult(bool isSuccess, string message, object data) : this(isSuccess, message)
        {
            this.Data = data;
        }
    }
}
