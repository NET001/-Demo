using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core_CJL.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var flag1 = Host
                  //默认初始化配置
                  .CreateDefaultBuilder(args)
                   //注入autofac工厂实例
                   .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                  //之后的开发配置
                  .ConfigureWebHostDefaults(webBuilder =>
                  {
                      webBuilder
                      .UseStartup<Startup>()
                      .UseUrls("http://*:8081")
                      //对日志进行重写配置
                      .ConfigureLogging((hostingContext, builder) =>
                      {
                          // 1.过滤掉系统默认的一些日志
                          builder.AddFilter("System", LogLevel.Error);
                          builder.AddFilter("Microsoft", LogLevel.Error);
                          // 2.也可以在appsettings.json中配置，LogLevel节点
                          // 3.统一设置
                          builder.SetMinimumLevel(LogLevel.Error);
                          // 默认log4net.confg
                          //将log4net来实现Ilogger接口让log4net来进行接管
                          //引入log4net的包
                          builder.AddLog4Net(Path.Combine(Directory.GetCurrentDirectory(), "Log4net.config"));
                      });
                  });
            //构建
            var flag2 = flag1.Build();
            //运行启动
            flag2.Run();
        }
    }
}
