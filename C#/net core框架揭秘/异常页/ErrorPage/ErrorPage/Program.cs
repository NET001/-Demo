using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace ErrorPage
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Demo1();
        }
        /// <summary>
        /// 自定义异常页面
        /// </summary>
        static void Demo1()
        {
            var options = new ExceptionHandlerOptions { ExceptionHandler = HandleAsync };
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder.Configure(app => app
                    .UseExceptionHandler(options)
                    .Run(context => Task.FromException(new InvalidOperationException("Manually thrown exception...")))))
                .Build()
                .Run();

            Task HandleAsync(HttpContext context)
                => context.Response.WriteAsync("Unhandled exception occurred!");
        }
        /// <summary>
        /// 自定义异常处理器IApplicationBuilder拥有构建中间件管道的能力
        /// </summary>
        static void Demo2()
        {
            Host.CreateDefaultBuilder()
               .ConfigureWebHostDefaults(builder => builder.Configure(app => app
                   .UseExceptionHandler(app2 => app2.Run(HandleAsync))
                   .Run(context => Task.FromException(new InvalidOperationException("Manually thrown exception...")))))
               .Build()
               .Run();
            static Task HandleAsync(HttpContext context)
                => context.Response.WriteAsync("Unhandled exception occurred!");
        }
        /// <summary>
        /// 指定异常页面路径
        /// </summary>
        static void Demo3()
        {
            Host.CreateDefaultBuilder()
                .ConfigureServices(svcs => svcs.AddRouting())
                .ConfigureWebHostDefaults(builder => builder.Configure(app => app
                    .UseExceptionHandler("/error")
                    .UseRouting()
                    .UseEndpoints(endpoints => endpoints.MapGet("error", HandleAsync))
                    .Run(context => Task.FromException(new InvalidOperationException("Manually thrown exception...")))))
                .Build()
                .Run();

            static Task HandleAsync(HttpContext context)
                => context.Response.WriteAsync("Unhandled exception occurred!");
        }
        /// <summary>
        /// 为状态码设置异常
        /// </summary>
        static void Demo4()
        {
            Random _random = new Random();
            Host.CreateDefaultBuilder()
                    .ConfigureWebHostDefaults(webBuilder => webBuilder.Configure(app => app
                        .UseStatusCodePages(HandleAsync)
                        .Run(context => Task.Run(() => context.Response.StatusCode = _random.Next(400, 599)))))
                    .Build()
                    .Run();
            static async Task HandleAsync(StatusCodeContext context)
            {
                var response = context.HttpContext.Response;
                if (response.StatusCode < 500)
                {
                    await response.WriteAsync($"Client error ({response.StatusCode})");
                }
                else
                {
                    await response.WriteAsync($"Server error ({response.StatusCode})");
                }
            }
        }
        /// <summary>
        /// 通过对IDeveloperPageExceptionFilter进行实现
        /// </summary>
        static void Demo5()
        {
            Host.CreateDefaultBuilder()
             .ConfigureWebHostDefaults(builder => builder
                 .ConfigureServices(svcs => svcs.AddSingleton<IDeveloperPageExceptionFilter, FakeExceptionFilter>())
                 .Configure(app => app
                     .UseDeveloperExceptionPage()
                     .Run(context => Task.FromException(new InvalidOperationException("Manually thrown exception...")))))
             .Build()
             .Run();
                 
    }


        #region Demo5
        class FakeExceptionFilter : IDeveloperPageExceptionFilter
        {
            public Task HandleExceptionAsync(ErrorContext errorContext, Func<ErrorContext, Task> next)
                => errorContext.HttpContext.Response.WriteAsync("Unhandled exception occurred!");
        }
        #endregion
    }
}
