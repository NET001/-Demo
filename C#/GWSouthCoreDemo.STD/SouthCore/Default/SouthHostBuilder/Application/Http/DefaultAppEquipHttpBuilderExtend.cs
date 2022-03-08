using SouthCore.Common;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;

using SouthCore.Default.Synchronizer;
using SouthCore.Default.Cache;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SouthCore.Default.Http
{
    public static class DefaultAppEquipHttpBuilderExtend
    {
        #region Builder扩展
        /// <summary>
        /// 设置Http
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IDefaultAppEquipBuilder SetHttps(this IDefaultAppEquipBuilder builder, Action<IDefaultAppEquipHttpBuilder> configure)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }
            configure(new DefaultAppEquipHttpBuilder(builder));
            return builder;
        }
        /// <summary>
        /// 设置Http
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="httpSend"></param>
        /// <returns></returns>
        public static IDefaultAppEquipHttpBuilder SetHttp(this IDefaultAppEquipHttpBuilder builder, string name, HttpClient httpClient, Func<HttpClient, object[], object[]> send)
        {
            builder.Builder.ConfigureServices((IServiceCollection services) =>
            {
                services.AddSingleton(new HttpDescriptor
                {
                    Name = name,
                    HttpClient = httpClient,
                    Send = send
                });
            });
            return builder;
        }
        #endregion
        #region IServiceProvider扩展
        /// <summary>
        /// 获取HTTP请求实例
        /// </summary>
        /// <param name="southHost"></param>
        /// <returns></returns>
        internal static IEnumerable<HttpDescriptor> GetHttps(this IServiceProvider serviceProvider)
        {
            return serviceProvider.GetServices<HttpDescriptor>();
        }
        /// <summary>
        /// 获取Http请求结果
        /// </summary>
        /// <param name="southHost"></param>
        /// <param name="name"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        internal static object[] GetHttpSend(this IServiceProvider serviceProvider, string name, object[] parameter = null)
        {
            HttpDescriptor httpDescriptor = serviceProvider.GetHttps()
                 ?.Where(t => t.Name == name).FirstOrDefault();
            object[] response = null;
            if (httpDescriptor != null)
            {
                response = httpDescriptor.Send(httpDescriptor.HttpClient, parameter);
            }
            return response;
        }
        #endregion
    }
    public class HttpDescriptor
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Http客户端
        /// </summary>
        public HttpClient HttpClient { get; set; }
        /// <summary>
        /// 发送请求(请求客户端,入参,返回值)
        /// </summary>
        public Func<HttpClient, object[], object[]> Send { get; set; }
    }
}