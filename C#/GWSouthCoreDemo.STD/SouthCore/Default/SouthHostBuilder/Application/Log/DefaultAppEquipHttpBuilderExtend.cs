using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;
using SouthCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;


namespace SouthCore.Default.Log
{
    public static class DefaultAppEquipLogBuilderExtend
    {
        #region Builder扩展
        /// <summary>
        /// 配置log
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IDefaultAppEquipBuilder SetLogs(this IDefaultAppEquipBuilder builder, string logName, Action<IDefaultAppEquipLogBuilder> configure)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }
            configure(new DefaultAppEquipLogBuilder(builder));
            //选项配置
            builder.ConfigureServices(services => services
                .AddOptions()
                .Configure<LogOption>(o =>
                {
                    o.LogName = logName;
                }));
            return builder;
        }
        /// <summary>
        /// 设置Log
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="LogSend"></param>
        /// <returns></returns>
        public static IDefaultAppEquipLogBuilder SetLog(this IDefaultAppEquipLogBuilder builder, Action<string> action)
        {
            builder.Builder.ConfigureServices((IServiceCollection services) =>
            {
                services.AddSingleton(new LogDescriptor() { Write = action });
            });
            return builder;
        }
        #endregion
        #region IServiceProvider扩展
        /// <summary>
        /// 执行log写入默认实现
        /// </summary>
        /// <param name="southHost"></param>
        /// <param name="text"></param>
        /// <param name="ex"></param>
        public static void WriteDefaultLog(this IServiceProvider serviceProvider, string text, Exception ex = null)
        {
            IEnumerable<LogDescriptor> equipLogs = serviceProvider.GetServices<LogDescriptor>();
            string logName = serviceProvider.GetService<IOptions<LogOption>>().Value.LogName;
            string logContext = "【" + logName + "】" + text + "(" + ex?.ToString() + ")";
            foreach (var item in equipLogs)
            {
                item.Write(logContext);
            }
        }
        #endregion
    }
    public class LogDescriptor
    {
        public Action<string> Write { get; set; }
    }
    public class LogOption
    {
        public string LogName { get; set; }
    }

}