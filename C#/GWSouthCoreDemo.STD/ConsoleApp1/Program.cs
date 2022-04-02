using GWSouthCoreDemo.STD.Model;
using GWSouthCoreDemo.STD.Services;
using Microsoft.Extensions.Hosting;
using SouthCore;
using SouthCore.Common;

using SouthCore.Default;
using SouthCore.Default.AddEquips;
using SouthCore.Default.Cache;
using SouthCore.Default.GetData;
using SouthCore.Default.GetYc;
using SouthCore.Default.Http;
using SouthCore.Default.Log;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo2();
            Console.ReadKey();
        }
        static void Demo1()
        {
        }
        static void Demo2()
        {
            IHost host = Host.CreateDefaultBuilder()
                .ConfigureSouthHostDefaults(builder => builder
                    //设置日志
                    .SetStartup<LogStartup>()
                    //设置设备新增
                    .SetStartup<AddEquipStartup>()
                    //设置缓存
                    .SetStartup<CacheStartup>()
                    //设置Http请求
                    .SetStartup<HttpStartup>()
                    //设置GetData
                    .SetStartup<GetDataStartup>()
                    //设置GetYc
                    .SetStartup<GetYcStartup>()
                    //设置同步器
                    .SetStartup<SynchronizerStartup>()
                ).Build();
            //运行
            host.Run();
            //host.AddEquipsDefault();
        }
    }
}

