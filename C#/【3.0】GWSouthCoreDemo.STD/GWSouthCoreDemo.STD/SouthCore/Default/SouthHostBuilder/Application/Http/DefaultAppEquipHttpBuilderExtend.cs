using SouthCore.Common;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using SouthCore.Core;
using SouthCore.Default.Synchronizer;
using SouthCore.Default.Cache;

namespace SouthCore.Default.Http
{
    public static class DefaultAppEquipHttpBuilderExtend
    {
        #region 服务名称
        private const string HttpCollections = "HttpCollections";
        private const string HttpCacheMaps = "HttpCacheMaps";
        #endregion

        #region Builder扩展
        /// <summary>
        /// 设置Http
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IDefaultAppEquipBuilder SetHttps(this IDefaultAppEquipBuilder builder, Action<IDefaultAppEquipHttpBuilder> configure)
        {
            IDefaultAppEquipHttpBuilder appbuilder = new DefaultAppEquipHttpBuilder();
            configure(appbuilder);
            foreach (var key in appbuilder.Services.Keys)
            {
                builder.Services[key] = appbuilder.Services[key];
            }
            return builder;
        }
        /// <summary>
        /// 设置Http
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="httpSend"></param>
        /// <returns></returns>
        public static IDefaultAppEquipHttpBuilder SetHttp(this IDefaultAppEquipHttpBuilder builder, string name, Func<string, string> func)
        {
            builder.Services.AddServices(HttpCollections, new Tuple<string, Func<string, string>>(name, func));
            return builder;
        }
        #endregion

        #region Host扩展
        /// <summary>
        /// 获取HTTP请求实例
        /// </summary>
        /// <param name="southHost"></param>
        /// <returns></returns>
        internal static List<Tuple<string, Func<string, string>>> GetHttps(this ISouthHost southHost)
        {
            List<Tuple<string, Func<string, string>>> result = southHost.Services.GetServices<Tuple<string, Func<string, string>>>(HttpCollections);
            return result;
        }
        /// <summary>
        /// 获取Http请求结果
        /// </summary>
        /// <param name="southHost"></param>
        /// <param name="name"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        internal static string GetHttpSend(this ISouthHost southHost, string name, string parameter = null)
        {
            string response = southHost.Services.GetServices<Tuple<string, Func<string, string>>>(HttpCollections)
                 ?.Where(t => t.Item1 == name)
                 .FirstOrDefault()
                 ?.Item2(parameter);
            return response;
        }
        #endregion

    }
}