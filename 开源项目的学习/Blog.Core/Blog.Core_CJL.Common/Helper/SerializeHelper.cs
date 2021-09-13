using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core_CJL.Common.Helper
{
    /// <summary>
    /// 序列化帮助类
    /// </summary>
    public class SerializeHelper
    {
        /*
         将对象转换为一种传输格式的过程,叫序列化。用http通过internet在客户端和服务端直接传递对象，
        在另一端反序列化从流中重新获得对象
         */
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static byte[] Serialize(object item)
        {
            var jsonString = JsonConvert.SerializeObject(item);

            return Encoding.UTF8.GetBytes(jsonString);
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TEntity Deserialize<TEntity>(byte[] value)
        {
            if (value == null)
            {
                return default(TEntity);
            }
            var jsonString = Encoding.UTF8.GetString(value);
            return JsonConvert.DeserializeObject<TEntity>(jsonString);
        }
    }
}
