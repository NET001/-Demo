using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using Microsoft.Net.Http.Headers;

namespace Cache
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
        /// <summary>
        /// 内存缓存
        /// </summary>
        static void Demo1()
        {
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                    .ConfigureServices(svcs => svcs.AddMemoryCache())
                    .Configure(app => app.Run(ProocessAsync)))
                .Build()
                .Run();
            static async Task ProocessAsync(HttpContext httpContext)
            {
                var cache = httpContext.RequestServices.GetRequiredService<IMemoryCache>();
                if (!cache.TryGetValue<DateTime>("CurrentTime", out var currentTime))
                {
                    cache.Set("CurrentTime", currentTime = DateTime.Now);
                }
                await httpContext.Response.WriteAsync($"{currentTime}({DateTime.Now})");
            }
        }
        /// <summary>
        /// Redis缓存
        /// </summary>
        static void Demo2()
        {
            Host.CreateDefaultBuilder()
              .ConfigureWebHostDefaults(builder => builder
                  .ConfigureServices(svcs => svcs.AddDistributedRedisCache(options =>
                  {
                      options.Configuration = "localhost";
                      options.InstanceName = "Demo";
                  }))
                  .Configure(app => app.Run(ProocessAsync)))
              .Build()
              .Run();
            static async Task ProocessAsync(HttpContext httpContext)
            {
                var cache = httpContext.RequestServices.GetRequiredService<IDistributedCache>();
                var currentTime = await cache.GetStringAsync("CurrentTime");
                if (null == currentTime)
                {
                    currentTime = DateTime.Now.ToString();
                    await cache.SetAsync("CurrentTime", Encoding.UTF8.GetBytes(currentTime));
                }
                await httpContext.Response.WriteAsync($"{currentTime}({DateTime.Now})");
            }
        }
        /// <summary>
        /// Sql Server缓存
        /// </summary>
        static void Demo3()
        {
            Host.CreateDefaultBuilder()
              .ConfigureWebHostDefaults(builder => builder
                  .ConfigureServices(svcs => svcs.AddDistributedSqlServerCache(options =>
                  {
                      options.ConnectionString = "server=.;database=demodb;uid=sa;pwd=password";
                      options.SchemaName = "dbo";
                      options.TableName = "AspnetCache";
                  }))
                  .Configure(app => app.Run(ProocessAsync)))
              .Build()
              .Run();

            static async Task ProocessAsync(HttpContext httpContext)
            {
                var cache = httpContext.RequestServices.GetRequiredService<IDistributedCache>();
                var currentTime = await cache.GetStringAsync("CurrentTime");
                if (null == currentTime)
                {
                    currentTime = DateTime.Now.ToString();
                    await cache.SetAsync("CurrentTime", Encoding.UTF8.GetBytes(currentTime));
                }
                await httpContext.Response.WriteAsync($"{currentTime}({DateTime.Now})");
            }
        }
        /// <summary>
        /// 缓存整个http响应
        /// </summary>
        static void Demo4()
        {
            Host.CreateDefaultBuilder()
                 .ConfigureWebHostDefaults(builder => builder
                     .ConfigureServices(svcs => svcs.AddResponseCaching())
                     .Configure(app => app
                         .UseResponseCaching()
                         .Run(ProcessAsync)))
                 .Build()
                 .Run();
            static async Task ProcessAsync(HttpContext httpContext)
            {
                var response = httpContext.Response;
                response.GetTypedHeaders().CacheControl = new CacheControlHeaderValue
                {
                    Public = true,
                    MaxAge = TimeSpan.FromSeconds(3600)
                };
                var isUtc = httpContext.Request.Query.ContainsKey("utc");
                await response.WriteAsync(isUtc ? DateTime.UtcNow.ToString() : DateTime.Now.ToString());
            }
        }
    }
}
