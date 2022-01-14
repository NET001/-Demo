using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace Log_CJL
{
    class Program
    {
        static void Main(string[] args)
        {
            Log4();
            Console.Read();
        }
        static void Log1()
        {
            //在输出的编辑器中可以查看到
            Debugger.Log(1, null, "这是一个日志1");
            Debug.WriteLine("这是一个日志2");
        }
        static void Log2()
        {
            var source = new TraceSource("Foobar", SourceLevels.Error);
            var eventTypes = (TraceEventType[])Enum.GetValues(typeof(TraceEventType));
            int i = 0;
            Array.ForEach(eventTypes, t => { source.TraceEvent(t, i++, "输出：" + t); });
        }
        static void Log3()
        {
            var logger = new ServiceCollection()
                  .AddLogging(builder => builder
                      .AddConsole()
                      .AddDebug())
                  .BuildServiceProvider()
                  .GetRequiredService<ILoggerFactory>()
                  .CreateLogger("Log_CJL.Program");

            logger.Log(LogLevel.Error, "message1", "这是日志的内容");
            logger.Log(LogLevel.Error, "message2", "这是日志的内容");

        }
        static void Log4()
        {
            var logger = new ServiceCollection()
                  .AddLogging(builder => builder
                      .AddConsole()
                      .AddDebug())
                  .BuildServiceProvider()
                  .GetRequiredService<ILogger<Program>>();
            logger.Log(LogLevel.Error, "message1", "这是日志的内容");
            logger.Log(LogLevel.Error, "message2", "这是日志的内容");
        }
    }
}