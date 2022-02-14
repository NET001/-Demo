using SouthCore.Common;
using SouthCore.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SouthCore.Default.GetYc
{
    public static class DefaultAppEquipGetYcBuilderExtend
    {
        #region 服务名称
        private const string GetYcCollections = "GetYcCollections";
        #endregion

        #region Builder扩展
        /// <summary>
        /// 设置Yc获取
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IDefaultAppEquipBuilder SetGetYcs(this IDefaultAppEquipBuilder builder, Action<IDefaultAppEquipGetYcBuilder> configure)
        {
            IDefaultAppEquipGetYcBuilder appbuilder = new DefaultAppEquipGetYcBuilder();
            configure(appbuilder);
            foreach (var key in appbuilder.Services.Keys)
            {
                builder.Services[key] = appbuilder.Services[key];
            }
            return builder;
        }
        /// <summary>
        /// 设置获取遥测data
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="GetYc"></param>
        /// <returns></returns>
        public static IDefaultAppEquipGetYcBuilder SetGetYc(this IDefaultAppEquipGetYcBuilder builder, string name, Func<object[], string, string> func)
        {
            builder.Services.AddServices(GetYcCollections, new Tuple<string, Func<object[], string, string>>(name, func));
            return builder;
        }
        #endregion

        #region Host扩展
        /// <summary>
        /// 获取获取遥测data的方法
        /// </summary>
        /// <param name="southHost"></param>
        /// <returns></returns>
        internal static List<Tuple<string, Func<object[], string, string>>> GetGetYcs(this ISouthHost southHost)
        {
            List<Tuple<string, Func<object[], string, string>>> result = southHost.Services.GetServices<Tuple<string, Func<object[], string, string>>>(GetYcCollections);
            return result;
        }
        /// <summary>
        /// 获取遥测值
        /// </summary>
        /// <param name="southHost"></param>
        /// <param name="name"></param>
        /// <param name="parms"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetYc(this ISouthHost southHost, string name, object[] parms, string code)
        {
            return (southHost.Services.GetServices<Tuple<string, Func<object[], string, string>>>(GetYcCollections))
                  ?.Where(t => t.Item1 == name)
                  .FirstOrDefault()
                  ?.Item2(parms, code);
        }
        #endregion

        #region 其他扩展
        /// <summary>
        /// 获取业务对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objs"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static T ToBO<T>(this object[] objs, int index) where T : class
        {
            return objs == null ? null : objs.Length > index ? objs[index] as T : null;
        }
        #endregion
    }
}
