using SouthCore.Common;
using SouthCore.Default.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SouthCore.Default.AddEquips
{
    public static class DefaultAppEquipAddEquipsBuilderExtend
    {
        #region Builder扩展
        /// <summary>
        /// 配置设备生成
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IDefaultAppEquipBuilder SetAddEquips(this IDefaultAppEquipBuilder builder, Action<IDefaultAppEquipAddEquipsBuilder> configure)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }
            configure(new DefaultAppEquipAddEquipsBuilder(builder));
            return builder;
        }
        /// <summary>
        /// 初始化设备发现
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="dllName"></param>
        /// <returns></returns>
        public static IDefaultAppEquipAddEquipsBuilder SetAddEquipsInit(this IDefaultAppEquipAddEquipsBuilder builder, string dllName)
        {
            builder.Builder.ConfigureServices((IServiceCollection services) =>
            {
                services.AddSingleton(new EquipsInit()
                {
                    dllName = dllName
                });
            });
            return builder;
        }
        /// <summary>
        ///注入数据库操作类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IDefaultAppEquipAddEquipsBuilder SetAddEquipsIDataManageService<T>(this IDefaultAppEquipAddEquipsBuilder builder) where T : class, IDataManageService
        {
            builder.Builder.ConfigureServices((IServiceCollection services) =>
            {
                services.AddSingleton<IDataManageService, T>();
            });
            return builder;
        }
        /// <summary>
        /// 注入设备发现实现
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <param name="iotequip_nm"></param>
        /// <returns></returns>
        public static IDefaultAppEquipAddEquipsBuilder SetAddEquipsImplementation<T>(this IDefaultAppEquipAddEquipsBuilder builder, string iotequip_nm) where T : BaseAddEquipEncapsulation
        {

            builder.Builder.ConfigureServices((IServiceCollection services) =>
            {
                services.AddSingleton(new EquipsImplementation()
                {
                    iotequip_nm = iotequip_nm,
                    EquipEncapsulation = typeof(T)
                });
            });
            return builder;
        }
        #endregion

        #region IServiceProvider扩展
        /// <summary>
        /// 获取初始配置
        /// </summary>
        /// <param name="southHost"></param>
        /// <returns></returns>
        private static EquipsInit GetAddEquipsInit(this IServiceProvider serviceProvider)
        {
            return serviceProvider.GetRequiredService<EquipsInit>();
        }
        /// <summary>
        /// 获取数据库操作类
        /// </summary>
        /// <param name="southHost"></param>
        /// <returns></returns>
        private static IDataManageService GetAddEquipsIDataManageService(this IServiceProvider serviceProvider)
        {
            return serviceProvider.GetRequiredService<IDataManageService>();
        }
        /// <summary>
        /// 获取设备生成类
        /// </summary>
        /// <param name="southHost"></param>
        /// <returns></returns>
        private static IEnumerable<EquipsImplementation> GetAddEquipsImplementations(this IServiceProvider serviceProvider)
        {
            return serviceProvider.GetServices<EquipsImplementation>();
        }
        /// <summary>
        /// 设备生成
        /// </summary>
        /// <param name="southHost"></param>
        public static void AddEquipsDefault(this IServiceProvider serviceProvider)
        {
            try
            {
                serviceProvider.WriteDefaultLog("开始执行设备生成");
                IDataManageService dataManageService = serviceProvider.GetAddEquipsIDataManageService();
                string dllName = serviceProvider.GetAddEquipsInit().dllName;
                IEnumerable<EquipsImplementation> addEquips = serviceProvider.GetAddEquipsImplementations();
                if (dataManageService != null && dllName != null && addEquips != null)
                {
                    foreach (var item in addEquips)
                    {
                        ((BaseAddEquipEncapsulation)Activator.CreateInstance(item.EquipEncapsulation, new object[] {
                            dllName,
                            item.iotequip_nm,
                            dataManageService
                        })).AddEquips();
                    }
                    serviceProvider.WriteDefaultLog("设备生成执行完成");
                }
                else
                {
                    serviceProvider.WriteDefaultLog("缺少执行AddEquips的必要参数");
                }
            }
            catch (Exception ex)
            {
                serviceProvider.WriteDefaultLog("AddEquips执行异常", ex);
            }
        }
        #endregion
    }
    /// <summary>
    /// 初始化配置
    /// </summary>
    internal class EquipsInit
    {
        /// <summary>
        /// dll名称
        /// </summary>
        public string dllName { get; set; }
    }
    /// <summary>
    /// 设备初始化实现
    /// </summary>
    internal class EquipsImplementation
    {
        public string iotequip_nm { get; set; }
        public Type EquipEncapsulation { get; set; }
    }
}