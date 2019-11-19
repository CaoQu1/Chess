using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Utility
{
    /// <summary>
    /// 字符串扩展
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// 返回十六进制代表的字符串
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static string HexToString(this string hex)
        {
            hex = hex.Replace(" ", "");
            if (hex.Length == 0) return "";

            byte[] vBytes = new byte[hex.Length / 2];
            for (int i = 0; i < hex.Length; i += 2)
            {
                if (!byte.TryParse(hex.Substring(i, 2), NumberStyles.HexNumber, null, out vBytes[i / 2]))
                {
                    vBytes[i / 2] = 0;
                }
            }
            return ASCIIEncoding.Default.GetString(vBytes);
        }

        /// <summary>
        /// 返回处理后的十六进制字符串
        /// </summary>
        /// <param name="input"></param>
        /// <returns>hex</returns>
        public static string ToHex(this string input)
        {
            return BitConverter.ToString(ASCIIEncoding.Default.GetBytes(input)).Replace("-", "");
        }
    }
}
