using Blog.Core_CJL.Common;
using Blog.Core_CJL.Common.Helper;
using Blog.Core_CJL.Common.Redis;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core_CJL.Extensions.ServiceExtensions
{
    /// <summary>
    /// Redis缓存 启动服务
    /// </summary>
    public static class RedisCacheSetup
    {
        public static void AddRedisCacheSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddTransient<IRedisBasketRepository, RedisBasketRepository>();

            // 配置启动Redis服务，虽然可能影响项目启动速度，但是不能在运行的时候报错，所以是合理的
            services.AddSingleton<ConnectionMultiplexer>(sp =>
            {
                //获取连接字符串
                string redisConfiguration = Appsettings.app(new string[] { "Redis", "ConnectionString" });
                //创建一个redis配置实例
                var configuration = ConfigurationOptions.Parse(redisConfiguration, true);
                //指定开启dns映射
                configuration.ResolveDns = true;
                //创建一个redis核心操作类
                return ConnectionMultiplexer.Connect(configuration);
            });

        }
    }
}
