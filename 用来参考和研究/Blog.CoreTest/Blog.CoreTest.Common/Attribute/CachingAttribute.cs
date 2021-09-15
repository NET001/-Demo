using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.CoreTest.Common.Attribute
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class CachingAttribute : System.Attribute
    {
        /// <summary>
        /// 缓存绝对过期时间（分钟）
        /// </summary>
        public int AbsoluteExpiration { get; set; } = 30;
    }
}