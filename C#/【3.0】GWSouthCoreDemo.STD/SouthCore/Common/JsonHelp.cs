using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SouthCore.Common
{
    public static class JsonHelp
    {
        /// <summary>
        /// JSON序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        /// <summary>
        /// JSON反序列化
        /// </summary>
        /// <typeparam name="T">模板</typeparam>
        /// <param name="json">Json串</param>
        /// <returns></returns>
        public static T ToObject<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
