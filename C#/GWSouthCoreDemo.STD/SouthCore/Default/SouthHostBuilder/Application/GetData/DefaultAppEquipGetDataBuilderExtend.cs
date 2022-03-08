using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SouthCore.Common;

using System;
using System.Collections.Generic;
using System.Linq;

namespace SouthCore.Default.GetData
{
    public static class DefaultAppEquipGetDataBuilderExtend
    {
        #region Builder扩展
        /// <summary>
        /// 设置data获取
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IDefaultAppEquipBuilder SetGetDatas(this IDefaultAppEquipBuilder builder, Action<IDefaultAppEquipGetDataBuilder> configure)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }
            configure(new DefaultAppEquipGetDataBuilder(builder));
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
            builder.Builder.ConfigureServices((IServiceCollection services) =>
            {
                services.AddSingleton(new GetDataDescriptor()
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
        /// 获取Data
        /// </summary>
        /// <param name="southHost"></param>
        /// <param name="name"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static object[] GetData(this IHost host, string name, params object[] parms)
        {
            return (host.Services.GetServices<GetDataDescriptor>())
                ?.Where(t => t.Name == name)
                .FirstOrDefault()
                ?.Func(parms, new DefaultAppEquipGetDataExe(host.Services));
        }
        #endregion
    }
    public class GetDataDescriptor
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 入参,扩展操作,返回值
        /// </summary>
        public Func<object[], IDefaultAppEquipGetDataExe, object[]> Func { get; set; }
    }
}
