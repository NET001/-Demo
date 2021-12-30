using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Host_CJL
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo1();
        }
        static void Demo1()
        {
            new HostBuilder()
                .ConfigureServices(svcs => svcs
                    .AddHostedService<PerformanceMetricsCollector>())
                //在调用Build方法创建作为宿主的IHost对象之前，它包括承载服务在内的所有服务都是通过ConfigureServices方法进行注册
                .Build()
                .Run();
        }
        static void Demo2()
        {
            new HostBuilder()
                .ConfigureServices(svcs => svcs
                    .AddHostedService<PerformanceMetricsCollector>()
                    .AddSingleton<IHostedService, PerformanceMetricsCollector>())
                //在调用Build方法创建作为宿主的IHost对象之前，它包括承载服务在内的所有服务都是通过ConfigureServices方法进行注册
                .Build()
                .Run();
        }
    }
    public sealed class PerformanceMetricsCollector : IHostedService
    {
        private IDisposable _scheduler;
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _scheduler = new Timer(Callback, null, TimeSpan.FromSeconds(-1), TimeSpan.FromSeconds(5));
            return Task.CompletedTask;

            static void Callback(object state)
            {
                Console.WriteLine("Callback");
            }
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _scheduler?.Dispose();
            return Task.CompletedTask;
        }
    }

    interface IFood
    {

    }

    public class Food : IFood
    {

    }
}
