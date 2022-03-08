using SouthCore.Common;
using SouthCore.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SouthCore.Default.GetData
{
    public static class DefaultAppEquipGetDataBuilderExtend
    {
        #region 服务名称
        private const string GetDataCollections = "GetDataCollections";
        #endregion

        #region Builder扩展
        /// <summary>
        /// 设置data获取
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IDefaultAppEquipBuilder SetGetDatas(this IDefaultAppEquipBuilder builder, Action<IDefaultAppEquipGetDataBuilder> configure)
        {
            IDefaultAppEquipGetDataBuilder appbuilder = new DefaultAppEquipGetDataBuilder();
            configure(appbuilder);
            foreach (var key in appbuilder.Services.Keys)
            {
                builder.Services[key] = appbuilder.Services[key];
            }
            return builder;
        }
        /// <summary>
        /// 设置data获取
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="GetData"></param>
        /// <returns></returns>
        public static IDefaultAppEquipGetDataBuilder SetGetData(this IDefaultAppEquipGetDataBuilder builder, string name, Func<object[], IDefaultAppEquipGetDataExe, object[]> func)
        {
            builder.Services.AddServices(GetDataCollections, new Tuple<string, Func<object[], IDefaultAppEquipGetDataExe, object[]>>(name, func));
            return builder;
        }
        #endregion
        #region Host扩展
        /// <summary>
        /// 获取Data
        /// </summary>
        /// <param name="southHost"></param>
        /// <param name="name"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static object[] GetData(this ISouthHost southHost, string name, params object[] parms)
        {
            return (southHost.Services.GetServices<Tuple<string, Func<object[], IDefaultAppEquipGetDataExe, object[]>>>(GetDataCollections))
                ?.Where(t => t.Item1 == name)
                .FirstOrDefault()
                ?.Item2(parms, new DefaultAppEquipGetDataExe(southHost));
        }
        #endregion
    }
}
