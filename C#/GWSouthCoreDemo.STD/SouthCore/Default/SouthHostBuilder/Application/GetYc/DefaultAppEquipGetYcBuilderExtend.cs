using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SouthCore.Default.GetYc
{
    public static class DefaultAppEquipGetYcBuilderExtend
    {
        #region Builder扩展
        /// <summary>
        /// 设置Yc获取
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IDefaultAppEquipBuilder SetGetYcs(this IDefaultAppEquipBuilder builder, Action<IDefaultAppEquipGetYcBuilder> configure)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }
            configure(new DefaultAppEquipGetYcBuilder(builder));
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
            builder.Builder.ConfigureServices((IServiceCollection services) =>
            {
                services.AddSingleton(new GetYcDescriptor
                {
                    Name = name,
                    Func = func
                });
            });
            return builder;
        }
        #endregion
        #region Host扩展
        /// <summary>
        /// 获取获取遥测data的方法
        /// </summary>
        /// <param name="southHost"></param>
        /// <returns></returns>
        internal static IEnumerable<GetYcDescriptor> GetGetYcs(this IHost host)
        {
            return host.Services.GetServices<GetYcDescriptor>();
        }
        /// <summary>
        /// 获取遥测值
        /// </summary>
        /// <param name="southHost"></param>
        /// <param name="name"></param>
        /// <param name="parms"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetYc(this IHost host, string name, object[] parms, string code)
        {
            return host.GetGetYcs()
                  ?.Where(t => t.Name == name)
                  .FirstOrDefault()
                  ?.Func(parms, code);
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
    public class GetYcDescriptor
    {
        public string Name { get; set; }
        /// <summary>
        /// 数据源,code,结果
        /// </summary>
        public Func<object[], string, string> Func { get; set; }
    }
}
