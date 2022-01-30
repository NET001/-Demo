
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Diagnostics.Tracing;

public class Program
{
    public static void Main()
    {
        Demo4();
        Console.Read();
    }
    /// <summary>
    /// 和容器集成的日志框架获取ILoggerFactory
    /// </summary>
    static void Demo1()
    {
        var logger = new ServiceCollection()
            //进行日志配置
            .AddLogging(builder => builder
                //添加控制台日志
                .AddConsole()
                //添加Debug日志
                .AddDebug())
            .BuildServiceProvider()
            //获取日志工厂
            .GetRequiredService<ILoggerFactory>()
            //获取一个日志对象
            .CreateLogger("App.Program");

        var levels = (LogLevel[])Enum.GetValues(typeof(LogLevel));
        levels = levels.Where(it => it != LogLevel.None).ToArray();
        var eventId = 1;
        //写入日志
        Array.ForEach(levels, level => logger.Log(level, eventId++, "This is a/an {0} log message.", level));

    }
    /// <summary>
    /// 获取指定类的日志ILogger
    /// </summary>
    static void Demo2()
    {
        var logger = new ServiceCollection()
               .AddLogging(builder => builder
                   .AddConsole()
                   .AddDebug())
               .BuildServiceProvider()
               .GetRequiredService<ILogger<Program>>();

        var levels = (LogLevel[])Enum.GetValues(typeof(LogLevel));
        levels = levels.Where(it => it != LogLevel.None).ToArray();
        var eventId = 1;
        Array.ForEach(levels, level => logger.Log(level, eventId++, "This is a/an {level} log message.", level));

    }
    /// <summary>
    /// 设置最小日志等级
    /// </summary>
    static void Demo3()
    {
        var logger = new ServiceCollection()
              .AddLogging(builder => builder
                    //设置最小日志级别
                    .SetMinimumLevel(LogLevel.Information)
                  .AddConsole())
              .BuildServiceProvider()
              .GetRequiredService<ILoggerFactory>()
              .CreateLogger<Program>();

        var levels = (LogLevel[])Enum.GetValues(typeof(LogLevel));
        levels = levels.Where(it => it != LogLevel.None).ToArray();
        var eventId = 1;
        Array.ForEach(levels, level => logger.Log(level, eventId++, "This is a/an {level} log message.", level));
    }
    /// <summary>
    /// 基于等级和类别的日志过滤
    /// </summary>
    static void Demo4()
    {
        var loggerFactory = new ServiceCollection()
              .AddLogging(builder => builder
                  .AddFilter(Filter)
                  .AddConsole())
              .BuildServiceProvider()
              .GetRequiredService<ILoggerFactory>();

        var fooLogger = loggerFactory.CreateLogger("Foo");
        var barLogger = loggerFactory.CreateLogger("Bar");
        var bazLogger = loggerFactory.CreateLogger("Baz");

        var levels = (LogLevel[])Enum.GetValues(typeof(LogLevel));
        levels = levels.Where(it => it != LogLevel.None).ToArray();
        var eventId = 1;
        Array.ForEach(levels, level => fooLogger.Log(level, eventId++, "This is a/an {0} log message.", level));
        eventId = 1;
        Array.ForEach(levels, level => barLogger.Log(level, eventId++, "This is a/an {0} log message.", level));
        eventId = 1;
        Array.ForEach(levels, level => bazLogger.Log(level, eventId++, "This is a/an {0} log message.", level));

        static bool Filter(string category, LogLevel level)
        {
            return category switch
            {
                "Foo" => level >= LogLevel.Debug,
                "Bar" => level >= LogLevel.Warning,
                "Baz" => level >= LogLevel.None,
                _ => level >= LogLevel.Information,
            };
        }
    }
    /// <summary>
    /// 利用配置定义日志过滤规则
    /// </summary>
    static void Demo5()
    {
        var configuration = new ConfigurationBuilder()
                   .AddJsonFile("logging.json")
                   .Build();

        var loggerFactory = new ServiceCollection()
            .AddLogging(builder => builder
                .AddConfiguration(configuration)
                .AddConsole(options => options.IncludeScopes = true)
                .AddDebug())
            .BuildServiceProvider()
            .GetRequiredService<ILoggerFactory>();

        var fooLogger = loggerFactory.CreateLogger("Foo");
        var barLogger = loggerFactory.CreateLogger("Bar");
        var bazLogger = loggerFactory.CreateLogger("Baz");

        var levels = (LogLevel[])Enum.GetValues(typeof(LogLevel));
        levels = levels.Where(it => it != LogLevel.None).ToArray();

        var eventId = 1;
        Array.ForEach(levels, level => fooLogger.Log(level, eventId++, "This is a/an {0} log message.", level));

        eventId = 1;
        Array.ForEach(levels, level => barLogger.Log(level, eventId++, "This is a/an {0} log message.", level));

        eventId = 1;
        Array.ForEach(levels, level => bazLogger.Log(level, eventId++, "This is a/an {0} log message.", level));

    }
    /// <summary>
    /// 利用日志范围描述逻辑调用链
    /// </summary>
    static void Demo6()
    {
        var logger = new ServiceCollection()
         .AddLogging(builder => builder
         .AddConsole(options => options.IncludeScopes = true))
         .BuildServiceProvider()
         .GetRequiredService<ILogger<Program>>();

        using (logger.BeginScope("Foo"))
        {
            logger.Log(LogLevel.Information, "This is a log written in scope Foo.");
            using (logger.BeginScope("Bar"))
            {
                logger.Log(LogLevel.Information, "This is a log written in scope Bar.");
                using (logger.BeginScope("Baz"))
                {
                    logger.Log(LogLevel.Information, "This is a log written in scope Baz.");
                }
            }
        }
    }
    /// <summary>
    /// EventListener
    /// </summary>
    static void Demo7()
    {
        _ = new LoggingEventListener();
        var logger = new ServiceCollection()
            .AddLogging(builder => builder.AddEventSourceLogger())
            .BuildServiceProvider()
            .GetRequiredService<ILogger<Program>>();

        var state = new Dictionary<string, object>
        {
            ["ErrorCode"] = 100,
            ["Message"] = "Unhandled exception"
        };

        logger.Log(LogLevel.Error, 1, state, new InvalidOperationException("This is a manually thrown exception."), (_, ex) => ex.Message);

    }
}

public class LoggingEventListener : EventListener
{
    protected override void OnEventSourceCreated(EventSource eventSource)
    {
        if (eventSource.Name == "Microsoft-Extensions-Logging")
        {
            EnableEvents(eventSource, EventLevel.LogAlways);
        }
    }

    protected override void OnEventWritten(EventWrittenEventArgs eventData)
    {
        Console.WriteLine($"Event: {eventData.EventName}");
        for (int index = 0; index < eventData.Payload.Count; index++)
        {
            var element = eventData.Payload[index];
            if (element is object[] || element is IDictionary<string, object>)
            {
                Console.WriteLine($"{eventData.PayloadNames[index],-16}: { JsonConvert.SerializeObject(element)}");
                continue;
            }
            Console.WriteLine($"{eventData.PayloadNames[index],-16}: { eventData.Payload[index]}");
        }
        Console.WriteLine();
    }
}