using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Blog.CoreTest.Common;
using Blog.CoreTest.Common.LogHelper;
using Blog.CoreTest.Extensions.ServiceExtensions;
using Blog.CoreTest.Extensions;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Blog.CoreTest.Api.Filter;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Text;
using Autofac;
using Blog.CoreTest.Extensions.Middlewares;
using System.Reflection;
using Blog.CoreTest.Model.Seed;
using Blog.COreTest.Tasks;
using Blog.CoreTest.IServices;

namespace Blog.CoreTest
{
    public class Startup
    {
        private IServiceCollection _services;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }
        //提供对appsettings.json进行操作
        //此接口在Program的CreateDefaultBuilder方法中就就进行了注入,所以可以直接拿到实例
        public IConfiguration Configuration { get; }
        //提供有关应用程序正在其中运行的 web 宿主环境的信息
        //此接口在Program的CreateDefaultBuilder方法中就就进行了注入,所以可以直接拿到实例
        public IWebHostEnvironment Env { get; }
        //此方法中主要用于配置DI可以通过IServiceCollection扩展方法来进行配置
        //官方推荐将扩展方法都以Microsoft.Extensions.DependencyInjection命名,此处主要将实现都放在了Extensions层中
        public void ConfigureServices(IServiceCollection services)
        {
            /*
            DI注入有三种生命周期
            AddTransient：每次请求，都获取一个新的实例。即使同一个请求获取多次也会是不同的实例
            AddScoped：每次请求，都获取一个新的实例。同一个请求获取多次会得到相同的实例
            AddSingleton：每次都获取同一个实例
             */
            //注入封装appsettings.json操作的帮助类
            services.AddSingleton(new Appsettings(Configuration));
            //注入日志帮助类
            services.AddSingleton(new LogLock(Env.ContentRootPath));
            //此处配置主要解决JwtClaim申明中获取值的显示问题
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            //缓存注入
            services.AddMemoryCacheSetup();
            //Redis注入
            services.AddRedisCacheSetup();
            //注入ISqlSugarClient实例
            services.AddSqlsugarSetup();
            //数据种子
            services.AddDbSetup();
            //AutoMapper配置
            services.AddAutoMapperSetup();
            //开启和配置CORS跨域
            services.AddCorsSetup();
            //MiniProfiler一个请求性能检测和Swagger配合使用
            services.AddMiniProfilerSetup();
            //Swagger配置
            services.AddSwaggerSetup();
            //任务调度注入。使用Quartz.NET
            services.AddJobSetup();
            //注入获取授权相关信息的帮助类
            services.AddHttpContextSetup();
            //控制台打印
            services.AddAppConfigSetup(Env);
            //RedisInit
            services.AddRedisInitMqSetup();
            /*
             关于认证和授权的流程是先进行认证在进行授权,认证首先会检查你的JWT Token是否有效,
            若有效则会进入到授权判断的流程
            认证是判断你是否能够访问,而授权是判断你是否有权限进行访问
             */
            //JWT认证
            services.AddAuthentication_JWTSetup();
            //授权
            services.AddAuthorizationSetup();
            //IpRateLimit限流框架
            services.AddIpPolicyRateLimitSetup(Configuration);
            //IO配置能够访问到wwwroot下的文件
            services.Configure<KestrelServerOptions>(x => x.AllowSynchronousIO = true)
                    .Configure<IISServerOptions>(x => x.AllowSynchronousIO = true);
            //控制器配置
            services.AddControllers(o =>
            {
                o.Filters.Add(typeof(GlobalExceptionsFilter));
                o.Conventions.Insert(0, new GlobalRoutePrefixFilter(new RouteAttribute(RoutePrefix.Name)));
            })
            //Json序列化配置
             .AddNewtonsoftJson(options =>
             {
                 options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                 options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                 //时间格式以系统为准
                 options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
             });
            //替换
            services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());
            //编码方式为系统编码
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            _services = services;
        }
        //引入AutoFac做IOC配置时的一个固定写法
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModuleRegister());
            builder.RegisterModule<AutofacPropertityModuleReg>();
        }
        //中间件的配置,Configure中的一些变量在ConfigureServices中已经进行了注入可以直接使用
        /*
         中间件的配置也是通过扩展方法实现的
        自定义中间件主要在Extensions层的MiddlewareHelpers.cs中通过app.UseMiddleware方法进行添加
        - - -
        中间件的核心是对HttpContext进行操作,中间件的有执行顺序顺序是根据配置的顺序,中间件中分为
        next()方法前和方法后,方法前能拿到request,方法后能拿到response
         */
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MyContext myContext, ITasksQzServices tasksQzServices, ISchedulerCenter schedulerCenter, IHostApplicationLifetime lifetime)
        {
            // Ip限流,尽量放管道外层
            app.UseIpLimitMildd();
            /*
            此框架写了好几个不同类型的日志中间件,
            这些日志中间件可以通过配置进行开启或关闭,
            能根据需要来进行选择
             */
            //请求日志中间件
            app.UseReuestResponseLog();
            //用户日志中间件
            app.UseRecordAccessLogsMildd();
            //ip日志中间件
            app.UseIPLogMildd();
            //此处是配置添加了一个页面,用于来显示当前系统的依赖
            app.UseAllServicesMildd(_services);
            //判断是否为开发调试环境，在开发调试环境下在开发调试环境才注册露错误页面信息
            if (env.IsDevelopment())
            {
                // 在开发环境中，使用异常页面，这样可以暴露错误堆栈信息，所以不要放在生产环境。
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //重定向到error模板页
                app.UseExceptionHandler("/Error");
                // 在非开发环境中，使用HTTP严格安全传输(or HSTS) 对于保护web安全是非常重要的。
                // 强制实施 HTTPS 在 ASP.NET Core，配合 app.UseHttpsRedirection
                //app.UseHsts();
            }
            //swagger中间件这里指定了一个自定义的模板
            app.UseSwaggerMildd(() => GetType().GetTypeInfo().Assembly.GetManifestResourceStream("Blog.CoreTest.Api.index.html"));
            //CORS
            app.UseCors(Appsettings.app(new string[] { "Startup", "Cors", "PolicyName" }));
            //静态文件能够方法wwwroot
            app.UseStaticFiles();
            //Cookie
            app.UseCookiePolicy();
            //状态码
            app.UseStatusCodePages();
            //开启路由
            app.UseRouting();
            //认证
            app.UseAuthentication();
            //授权
            app.UseAuthorization();
            //miniprofiler
            app.UseMiniProfilerMildd();
            //断点中间件,配置路由
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            /*
             系统每次启动会运行一次下面代码中的方法
             */
            //生成初始数据
            app.UseSeedDataMildd(myContext, Env.WebRootPath);
            //初始化任务调度
            app.UseQuartzJobMildd(tasksQzServices, schedulerCenter);
            //服务注册配置
            app.UseConsulMildd(Configuration, lifetime);
        }
    }
}