using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Utility
{

    /// <summary>
    /// 用户自定义异常
    /// </summary>
    public class CustomException : Exception
    {
        public CustomException() : base()
        {
        }

        public CustomException(string message) : base(message) { }

        public CustomException(string message, Exception exce) : base(message, exce)
        {
        }
    }
}
