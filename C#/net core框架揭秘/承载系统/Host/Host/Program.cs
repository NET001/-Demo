using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Autofac.Extensions.DependencyInjection;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        Demo1();
    }
    //配置一个承载服务
    static void Demo1()
    {
        new HostBuilder()
              .ConfigureServices(svcs => svcs
                  .AddHostedService<PerformanceMetricsCollector>())
              .Build()
              .Run();
    }
    /// <summary>
    /// 配置选项
    /// </summary>
    static void Demo2(string[] args)
    {
        var collector = new FakeMetricsCollector();
        new HostBuilder()
            .ConfigureHostConfiguration(builder => builder.AddCommandLine(args))
            .ConfigureAppConfiguration((context, builder) => builder
                .AddJsonFile(path: "appsettings.json", optional: false)
                .AddJsonFile(
                    path: $"appsettings.{context.HostingEnvironment.EnvironmentName}.json",
                    optional: true))
            .ConfigureServices((context, svcs) => svcs
                .AddSingleton<IProcessorMetricsCollector>(collector)
                .AddSingleton<IMemoryMetricsCollector>(collector)
                .AddSingleton<INetworkMetricsCollector>(collector)
                .AddSingleton<IMetricsDeliverer, FakeMetricsDeliverer>()
                .AddSingleton<IHostedService, PerformanceMetricsCollector>()

                .AddOptions()
                .Configure<MetricsCollectionOptions>(
                     context.Configuration.GetSection("MetricsCollection")))
             .ConfigureLogging(builder => builder.AddConsole())
            .Build()
            .Run();
    }
    /// <summary>
    /// 利用HostApplicationLifetime关闭当前应用
    /// </summary>
    static void Demo3()
    {
        new HostBuilder()
            .ConfigureServices(svcs => svcs.AddHostedService<FakeHostedService>())
            .Build()
            .Run();
    }
    /// <summary>
    /// 使用第三方框架替换原有容器的能力
    /// </summary>
    static void Demo4()
    {
        Host
        .CreateDefaultBuilder()
        //添加Autofac的能力
        .UseServiceProviderFactory(new AutofacServiceProviderFactory())
        .Build();
    }
}

#region 承载服务
public sealed class PerformanceMetricsCollector : IHostedService
{
    private IDisposable _scheduler;
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _scheduler = new Timer(Callback, null, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5));
        return Task.CompletedTask;

        static void Callback(object state)
        {
            Console.WriteLine($"[{DateTimeOffset.Now}]{PerformanceMetrics.Create()}");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _scheduler?.Dispose();
        return Task.CompletedTask;
    }
}
public class PerformanceMetrics
{
    private static readonly Random _random = new Random();

    public int Processor { get; set; }
    public long Memory { get; set; }
    public long Network { get; set; }

    public override string ToString() => $"CPU: {Processor * 100}%; Memory: {Memory / (1024 * 1024)}M; Network: {Network / (1024 * 1024)}M/s";

    public static PerformanceMetrics Create() => new PerformanceMetrics
    {
        Processor = _random.Next(1, 8),
        Memory = _random.Next(10, 100) * 1024 * 1024,
        Network = _random.Next(10, 100) * 1024 * 1024
    };
}

public sealed class FakeHostedService : IHostedService
{
    private readonly IHostApplicationLifetime _lifetime;
    private IDisposable _tokenSource;

    public FakeHostedService(IHostApplicationLifetime lifetime)
    {
        _lifetime = lifetime;
        _lifetime.ApplicationStarted.Register(() => Console.WriteLine("[{0}]Application started", DateTimeOffset.Now));
        _lifetime.ApplicationStopping.Register(() => Console.WriteLine("[{0}]Application is stopping.", DateTimeOffset.Now));
        _lifetime.ApplicationStopped.Register(() => Console.WriteLine("[{0}]Application stopped.", DateTimeOffset.Now));
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _tokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(5)).Token.Register(_lifetime.StopApplication);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _tokenSource?.Dispose();
        return Task.CompletedTask;
    }
}
#endregion

#region 配置选项

public class FakeMetricsCollector : IProcessorMetricsCollector, IMemoryMetricsCollector, INetworkMetricsCollector
{
    long INetworkMetricsCollector.GetThroughput() => PerformanceMetrics.Create().Network;
    int IProcessorMetricsCollector.GetUsage() => PerformanceMetrics.Create().Processor;
    long IMemoryMetricsCollector.GetUsage() => PerformanceMetrics.Create().Memory;
}

public class FakeMetricsDeliverer : IMetricsDeliverer
{
    private readonly TransportType _transport;
    private readonly Endpoint _deliverTo;
    private readonly ILogger _logger;
    private readonly Action<ILogger, DateTimeOffset, PerformanceMetrics, Endpoint, TransportType, Exception> _logForDelivery;

    public FakeMetricsDeliverer(IOptions<MetricsCollectionOptions> optionsAccessor, ILogger<FakeMetricsDeliverer> logger)
    {
        var options = optionsAccessor.Value;
        _transport = options.Transport;
        _deliverTo = options.DeliverTo;
        _logger = logger;
        _logForDelivery = LoggerMessage.Define<DateTimeOffset, PerformanceMetrics, Endpoint, TransportType>(LogLevel.Information, 0, "[{0}]Deliver performance counter {1} to {2} via {3}");
    }

    public Task DeliverAsync(PerformanceMetrics counter)
    {
        _logForDelivery(_logger, DateTimeOffset.Now, counter, _deliverTo, _transport, null);
        return Task.CompletedTask;
    }
}

public interface IMemoryMetricsCollector
{
    long GetUsage();
}
public interface IMetricsDeliverer
{
    Task DeliverAsync(PerformanceMetrics counter);
}
public interface INetworkMetricsCollector
{
    long GetThroughput();
}
public interface IProcessorMetricsCollector
{
    int GetUsage();
}
public class MetricsCollectionOptions
{
    public TimeSpan CaptureInterval { get; set; }
    public TransportType Transport { get; set; }
    public Endpoint DeliverTo { get; set; }
}

public enum TransportType
{
    Tcp,
    Http,
    Udp
}

public class Endpoint
{
    public string Host { get; set; }
    public int Port { get; set; }
    public override string ToString() => $"{Host}:{Port}";
}

#endregion