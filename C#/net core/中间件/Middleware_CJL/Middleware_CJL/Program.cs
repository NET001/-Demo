using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware_CJL
{
    class Program
    {

        static void Main(string[] args)
        {
            Demo5();
        }
        //注册中间件
        static void Demo1()
        {
            Func<RequestDelegate, RequestDelegate> func1 = new Func<RequestDelegate, RequestDelegate>((RequestDelegate next) =>
            {
                RequestDelegate requestDelegate1 = new RequestDelegate(async (HttpContext context) =>
                {
                    await context.Response.WriteAsync("Hello");
                    await next(context);
                });
                return requestDelegate1;
            });
            Func<RequestDelegate, RequestDelegate> func2 = new Func<RequestDelegate, RequestDelegate>((RequestDelegate next) =>
            {
                RequestDelegate requestDelegate1 = new RequestDelegate(async (HttpContext context) =>
                {
                    await context.Response.WriteAsync("World");
                    await next(context);
                });
                return requestDelegate1;
            });
            RequestDelegate Middleware1(RequestDelegate next) => async context =>
            {
                await context.Response.WriteAsync("Hello");
                await next(context);
            };
            RequestDelegate Middleware2(RequestDelegate next) => async context =>
            {
                await context.Response.WriteAsync("World");
                await next(context);
            };
            Host.CreateDefaultBuilder().ConfigureWebHostDefaults(builder => builder
                .Configure(app => app
                    .Use(func1)
                    .Use(func2)
                    //.Use(Middleware1)
                    //.Use(Middleware2)
                    ))
            .Build()
            .Run();
        }
        //基于接口的中间件注册
        static void Demo2()
        {
            Host.CreateDefaultBuilder().ConfigureWebHostDefaults(builder => builder
             .Configure(app => app
                   .UseMiddleware<My1Middlewre>()
                   .UseMiddleware<My2Middlewre>()
                 ))
         .Build()
         .Run();
        }
        //基于约定的中间件注册
        static void Demo3()
        {
            Host.CreateDefaultBuilder().ConfigureWebHostDefaults(builder => builder
             .Configure(app => app
                   .UseMiddleware<My3Middlewre>("Hello World", true)
                 ))
         .Build()
         .Run();
        }
        //服务注册
        static void Demo4()
        {
            Host
                .CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                    .UseStartup<Startup>())
                .Build()
                .Run();
        }
        /// <summary>
        /// 访问控制器
        /// </summary>
        static void Demo5()
        {
            Host
                .CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                    //注册mvc服务
                    .ConfigureServices(svcs => svcs
                        .AddControllersWithViews())
                    //配置mvc处理管道
                    .Configure(app => app.
                        UseRouting()
                        .UseEndpoints(endpoints => endpoints.MapControllers())))
                .Build()
                .Run();
        }
        //添加配置选项系统
        static void Demo6()
        {
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                    .ConfigureAppConfiguration(config => config
                        .AddInMemoryCollection(new Dictionary<string, string>
                        {
                            ["Foobar:Foo"] = "Foo",
                            ["Foobar:Bar"] = "Bar",
                            ["Baz"] = "Baz"
                        }))
                    .UseStartup<Startup>())
                .Build()
                .Run();

        }
    }
    //基于接口定义的中间件
    public class My1Middlewre : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("Hello World");
            await next(context);
        }
    }
    public class My2Middlewre : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("Hello World");
            await next(context);
        }
    }
    //基于约定的
    public class My3Middlewre
    {
        private readonly RequestDelegate _next;
        private readonly string _contents;
        private readonly bool _forewardToNext;
        public My3Middlewre(RequestDelegate next, string contents, bool forewardToNext)
        {
            _next = next;
            _contents = contents;
            _forewardToNext = forewardToNext;
        }
        public async Task Invoke(HttpContext context)
        {
            await context.Response.WriteAsync(_contents);
            if (_forewardToNext)
            {
                await _next(context);
            }
        }


    }

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            foreach (var item in services)
            {

            }
        }
        public void Configure(IApplicationBuilder app)
        {

        }
    }

    public class HomeController : Controller
    {
        [HttpGet("/")]
        public string Index()
        {
            return "helow word";
        }
    }
}
