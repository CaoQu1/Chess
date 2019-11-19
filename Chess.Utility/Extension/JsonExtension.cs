using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Utility
{
    /// <summary>
    /// Json扩展类
    /// </summary>
    public static class JsonExtension
    {
        /// <summary>
        /// json字符串=>对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(this string value)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 对象=>json字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeObject<T>(this T obj) where T : class, new()
        {
            try
            {
                if (obj != null)
                {
                    return JsonConvert.SerializeObject(obj);
                }
                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
