using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SouthCore.Default.Synchronizer
{
    public static class DefaultAppEquipSynchronizerBuilderExtend
    {
        #region Builder扩展
        public static IDefaultAppEquipBuilder SetSynchronizers(this IDefaultAppEquipBuilder builder, Action<IDefaultAppEquipSynchronizerBuilder> configure)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }
            configure(new DefaultAppEquipSynchronizerBuilder(builder));
            return builder;
        }
        /// <summary>
        /// 设置同步器
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="actions"></param>
        /// <returns></returns>
        public static IDefaultAppEquipSynchronizerBuilder SetSynchronizer(this IDefaultAppEquipSynchronizerBuilder builder, string name, Action<IDefaultAppEquipSynchronizerExe> action, int syncSleep, int restartSleep = 1000 * 60)
        {
            builder.Builder.ConfigureServices((IServiceCollection services) =>
            {
                services.AddSingleton(new SynchronizerDescriptor
                {
                    Name = name,
                    Action = action,
                    SyncSleep = syncSleep,
                    RestartSleep = restartSleep
                });
            });
            return builder;
        }
        #endregion
        #region IServiceProvider扩展
        /// <summary>
        /// 获取同步器集合
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        internal static IEnumerable<SynchronizerDescriptor> GetSynchronizers(this IServiceProvider serviceProvider)
        {
            return serviceProvider.GetServices<SynchronizerDescriptor>();
        }
        #endregion
    }
    public class SynchronizerDescriptor
    {
        /// <summary>
        /// 同步器名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 同步器执行方法
        /// </summary>
        public Action<IDefaultAppEquipSynchronizerExe> Action { get; set; }
        /// <summary>
        /// 同步器间隔时间
        /// </summary>
        public int SyncSleep { get; set; }
        /// <summary>
        /// 同步器异常重启时间
        /// </summary>
        public int RestartSleep { get; set; }
    }
}