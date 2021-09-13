using System;

namespace Blog.Core_CJL.Common.Attribute
{

    /// <summary>
    /// 缓存特性,和AOP结合用于配置
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class CachingAttribute : System.Attribute
    {
        /// <summary>
        /// 缓存绝对过期时间（分钟）
        /// </summary>
        public int AbsoluteExpiration { get; set; } = 30;
    }
}
