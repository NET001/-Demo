using Blog.CoreTest.Common.MemoryCache;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.CoreTest.Extensions.ServiceExtensions
{
    /// <summary>
    /// Memory缓存 启动服务
    /// </summary>
    public static class MemoryCacheSetup
    {
        //比较固定的写法
        public static void AddMemoryCacheSetup(this IServiceCollection services)
        {
            //为空异常
            if (services == null) throw new ArgumentNullException(nameof(services));
            //注入缓存操作
            services.AddScoped<ICaching, MemoryCaching>();
            //注入缓存接口实例都是系统类
            services.AddSingleton<IMemoryCache>(factory =>
            {
                var cache = new MemoryCache(new MemoryCacheOptions());
                return cache;
            });
        }
    }
}
