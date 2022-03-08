using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SouthCore.Default.Log;
using SouthCore.Default.Synchronizer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SouthCore.Default
{
    /// <summary>
    /// 南向框架运行服务
    /// </summary>
    internal class GenericSouthHostService : IHostedService
    {
        private readonly IServiceProvider serviceProvider;
        //在执行IHostBuilder的Builder方法时会在容器中加入IHost的实例
        public GenericSouthHostService(IHost host)
        {
            this.serviceProvider = host.Services;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            //一个线程跑一个同步器
            foreach (var item in serviceProvider.GetSynchronizers())
            {
                Task.Run(() =>
                {
                    serviceProvider.WriteDefaultLog("同步器" + item.Name + "运行了");
                    while (true)
                    {
                        try
                        {
                            item.Action(new DefaultAppEquipSynchronizerExe(serviceProvider));
                            Thread.Sleep(item.SyncSleep);
                        }
                        catch (Exception ex)
                        {
                            serviceProvider.WriteDefaultLog(item.Name + "同步器异常过[" + item.RestartSleep + "]毫秒后重启", ex);
                            //异常重启
                            Thread.Sleep(item.RestartSleep);
                        }
                    }
                });
            }
            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            //还未考虑到线程的回收
            return Task.CompletedTask;
        }
    }
}
