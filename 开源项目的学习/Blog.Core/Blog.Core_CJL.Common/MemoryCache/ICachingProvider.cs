using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core_CJL.Common.MemoryCache
{
    /// <summary>
    /// 简单的缓存接口，只有查询和添加，以后可以进行扩展
    /// </summary>
    public interface ICaching
    {
        object Get(string cacheKey);

        void Set(string cacheKey, object cacheValue, int timeSpan);
    }
}
