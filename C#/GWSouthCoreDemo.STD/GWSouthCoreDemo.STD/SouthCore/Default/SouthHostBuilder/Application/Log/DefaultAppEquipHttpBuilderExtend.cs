using SouthCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using SouthCore.Core;

namespace SouthCore.Default.Log
{
    public static class DefaultAppEquipLogBuilderExtend
    {

        #region 服务名称
        private const string LogName = "LogName";
        private const string LogCollections = "LogCollections";
        #endregion
        #region Builder扩展
        /// <summary>
        /// 配置log
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IDefaultAppEquipBuilder SetLogs(this IDefaultAppEquipBuilder builder, string logName, Action<IDefaultAppEquipLogBuilder> configure)
        {
            IDefaultAppEquipLogBuilder appbuilder = new DefaultAppEquipLogBuilder();
            configure(appbuilder);
            foreach (var key in appbuilder.Services.Keys)
            {
                builder.Services[key] = appbuilder.Services[key];
            }
            builder.Services.AddServices(LogName, logName);
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
            builder.Services.AddServices(LogCollections, action);
            return builder;
        }
        #endregion
        #region Host扩展
        /// <summary>
        /// 执行log写入默认实现
        /// </summary>
        /// <param name="southHost"></param>
        /// <param name="text"></param>
        /// <param name="ex"></param>
        public static void WriteDefaultLog(this ISouthHost southHost, string text, Exception ex = null)
        {
            List<Action<string>> equipLogs = southHost.Services.GetServices<Action<string>>(LogCollections);
            string logName = southHost.Services.GetServices<string>(LogName).FirstOrDefault();
            string logContext = "【" + logName + "】" + text + "(" + ex?.ToString() + ")";
            equipLogs.ForEach(t =>
            {
                t.Invoke(logContext);
            });
        }
        #endregion
    }
}