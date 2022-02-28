using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo1();
        }
        /// <summary>
        /// 注册中间件
        /// </summary>
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
        /// <summary>
        /// 基于接口的中间件注册
        /// </summary>
        static void Demo2()
        {
            Host.CreateDefaultBuilder().ConfigureWebHostDefaults(builder => builder
             //容器注册后在注册中间件会在容器中获取
             .ConfigureServices(svcs => svcs.AddSingleton(new My2Middlewre("Hello World!")))
             .Configure(app => app
                   .UseMiddleware<My1Middlewre>()
                   .UseMiddleware<My2Middlewre>()
                 ))
         .Build()
         .Run();
        }
        /// <summary>
        /// 基于约定的中间件注册
        /// </summary>
        static void Demo3()
        {
            Host.CreateDefaultBuilder().ConfigureWebHostDefaults(builder => builder
             .Configure(app => app
                   .UseMiddleware<My3Middlewre>("Hello World", true)
                 ))
         .Build()
         .Run();
        }
        /// <summary>
        /// 中间件的注册也是基于容器化的管理
        /// </summary>
        static void Demo4()
        {
            Host.CreateDefaultBuilder().ConfigureWebHostDefaults(builder => builder
               .ConfigureServices(svcs => svcs
                   .AddSingleton<IFoo, Foo>()
                   .AddSingleton<IBar, Bar>())
               .Configure(app => app.UseMiddleware<FoobarMiddleware>()))
           .Build()
           .Run();
        }
        /// <summary>
        /// 服务注册
        /// </summary>
        static void Demo5()
        {
            //获取一个默认构建的承载系统构建对象
            Host.CreateDefaultBuilder()
                //asp.net core框架配置
                .ConfigureWebHostDefaults(builder => builder
                    .UseStartup<Startup>())
                .Build()
                .Run();
        }
        /// <summary>
        /// 进行配置
        /// </summary>
        static void Demo6()
        {
            Host.CreateDefaultBuilder().ConfigureWebHostDefaults(builder => builder
            .UseSetting("Foobar:Foo", "Foo")
            .UseSetting("Foobar:Bar", "Bar")
            .UseSetting("Baz", "Baz")
            .UseSetting("urls", "http://0.0.0.0:8888;http://0.0.0.0:9999")
            .UseStartup<Startup>())
        .Build()
        .Run();
        }
        /// <summary>
        /// 访问控制器
        /// </summary>
        static void Demo7()
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
                        //控制器路由
                        .UseEndpoints(endpoints => endpoints.MapControllers())))
                .Build()
                .Run();
        }
        /// <summary>
        /// 添加配置选项系统
        /// </summary>
        static void Demo8()
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
        /// <summary>
        /// 输出打印日志
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        static void Demo9()
        {
            Host.CreateDefaultBuilder()
                .ConfigureLogging(builder => builder.AddConsole(options => options.IncludeScopes = true))
                .ConfigureWebHostDefaults(builder => builder.Configure(app => app.Run(context =>
                {
                    var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
                    logger.LogInformation($"Log for event Foobar");
                    if (context.Request.Path == new PathString("/error"))
                    {
                        throw new InvalidOperationException(
                        "Manually throw exception.");
                    }
                    return Task.CompletedTask;
                })))
                .Build()
                .Run();
        }
        /// <summary>
        /// 开启访问静态文件的能力
        /// </summary>
        static void Demo10()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "doc");
            var options = new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(path),
                RequestPath = "/documents"
            };
            var fileProvider = new PhysicalFileProvider(path);
            var diretoryOptions = new DirectoryBrowserOptions
            {
                FileProvider = fileProvider,
                RequestPath = "/documents"
            };
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                    .Configure(app => app
                        //开启静态文件访问默认wwwroot
                        .UseStaticFiles()
                        //提供访问指定文件夹
                        .UseStaticFiles(options)
                        //开启文件夹显示能力
                        .UseDirectoryBrowser()
                        .UseDirectoryBrowser(diretoryOptions))
                    )
                .Build().Run();
        }
        /// <summary>
        /// 文件类型过滤
        /// </summary>
        static void Demo11()
        {
            var options = new StaticFileOptions
            {
                ServeUnknownFileTypes = true,
                DefaultContentType = "image/jpg"
            };
            var contentTypeProvider = new FileExtensionContentTypeProvider();
            contentTypeProvider.Mappings.Add(".img", "image/jpg");
            var options2 = new StaticFileOptions
            {
                ContentTypeProvider = contentTypeProvider
            };
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder.Configure(
                    app => app
                    .UseStaticFiles(options)
                    .UseStaticFiles(options2)
                    ))
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
        string str = "";
        public My2Middlewre(string str)
        {
            this.str = str;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync(str);
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
    public interface IFoo { }
    public interface IBar { }
    public class Foo : IFoo { }
    public class Bar : IBar { }
    public class FoobarMiddleware
    {
        private readonly RequestDelegate _next;
        public FoobarMiddleware(RequestDelegate next) => _next = next;
        public Task InvokeAsync(HttpContext context, IFoo foo, IBar bar)
        {
            return _next(context);
        }
    }
}
