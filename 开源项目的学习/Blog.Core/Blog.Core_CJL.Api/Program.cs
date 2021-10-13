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
                  //Ĭ�ϳ�ʼ������
                  .CreateDefaultBuilder(args)
                   //ע��autofac����ʵ��
                   .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                  //֮��Ŀ�������
                  .ConfigureWebHostDefaults(webBuilder =>
                  {
                      webBuilder
                      .UseStartup<Startup>()
                      .UseUrls("http://*:8081")
                      //����־������д����
                      .ConfigureLogging((hostingContext, builder) =>
                      {
                          // 1.���˵�ϵͳĬ�ϵ�һЩ��־
                          builder.AddFilter("System", LogLevel.Error);
                          builder.AddFilter("Microsoft", LogLevel.Error);
                          // 2.Ҳ������appsettings.json�����ã�LogLevel�ڵ�
                          // 3.ͳһ����
                          builder.SetMinimumLevel(LogLevel.Error);
                          // Ĭ��log4net.confg
                          //��log4net��ʵ��Ilogger�ӿ���log4net�����нӹ�
                          //����log4net�İ�
                          builder.AddLog4Net(Path.Combine(Directory.GetCurrentDirectory(), "Log4net.config"));
                      });
                  });
            //����
            var flag2 = flag1.Build();
            //��������
            flag2.Run();
        }
    }
}
