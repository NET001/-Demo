using SouthCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using SouthCore.Core;

namespace SouthCore.Default.Synchronizer
{
    public static class DefaultAppEquipSynchronizerBuilderExtend
    {
        #region 服务名称
        internal const string SynchronizerCollections = "SynchronizerCollections";
        #endregion

        #region Builder扩展
        public static IDefaultAppEquipBuilder SetSynchronizers(this IDefaultAppEquipBuilder builder, Action<IDefaultAppEquipSynchronizerBuilder> configure)
        {
            IDefaultAppEquipSynchronizerBuilder appbuilder = new DefaultAppEquipSynchronizerBuilder();
            configure(appbuilder);
            foreach (var key in appbuilder.Services.Keys)
            {
                builder.Services[key] = appbuilder.Services[key];
            }
            return builder;
        }
        /// <summary>
        /// 设置同步器
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="actions"></param>
        /// <returns></returns>
        public static IDefaultAppEquipSynchronizerBuilder SetSynchronizer(this IDefaultAppEquipSynchronizerBuilder builder, SynchronizerAction action)
        {
            builder.Services.AddServices(SynchronizerCollections, action);
            return builder;
        }
        #endregion

        #region Host扩展
        internal static List<SynchronizerAction> GetSynchronizers(this ISouthHost southHost)
        {
            return southHost.Services.GetServices<SynchronizerAction>(SynchronizerCollections);
        }
        #endregion
    }
    public class SynchronizerAction
    {
        public string Name { get; set; }
        public Action<IDefaultAppEquipSynchronizerExe> Action { get; set; }
        public int SyncSleep { get; set; }
        public int RestartSleep { get; set; }
    }
}