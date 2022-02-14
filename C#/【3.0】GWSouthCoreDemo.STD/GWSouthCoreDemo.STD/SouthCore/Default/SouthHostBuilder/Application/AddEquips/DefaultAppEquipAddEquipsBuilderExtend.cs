using SouthCore.Common;
using SouthCore.Core;
using SouthCore.Default.Log;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SouthCore.Default.AddEquips
{
    public static class DefaultAppEquipAddEquipsBuilderExtend
    {
        #region 服务名称
        private const string AddEquipsInit = "AddEquipsInit";
        private const string AddEquipsIDataManageService = "AddEquipsIDataManageService";
        private const string AddEquipsImplementation = "AddEquipsImplementation";
        #endregion

        #region Builder扩展
        /// <summary>
        /// 配置设备生成
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IDefaultAppEquipBuilder SetAddEquips(this IDefaultAppEquipBuilder builder, Action<IDefaultAppEquipAddEquipsBuilder> configure)
        {
            IDefaultAppEquipAddEquipsBuilder appbuilder = new DefaultAppEquipAddEquipsBuilder();
            configure(appbuilder);
            foreach (var key in appbuilder.Services.Keys)
            {
                builder.Services[key] = appbuilder.Services[key];
            }
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
            builder.Services.AddServices(AddEquipsInit, dllName);
            return builder;
        }
        /// <summary>
        ///注入数据库操作类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IDefaultAppEquipAddEquipsBuilder SetAddEquipsIDataManageService<T>(this IDefaultAppEquipAddEquipsBuilder builder) where T : IDataManageService
        {
            builder.Services.AddServices(AddEquipsIDataManageService, Activator.CreateInstance(typeof(T)));
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
            builder.Services.AddServices(AddEquipsImplementation, new Tuple<string, Type>(iotequip_nm, typeof(T)));
            return builder;
        }
        #endregion

        #region Host扩展
        /// <summary>
        /// 获取初始配置
        /// </summary>
        /// <param name="southHost"></param>
        /// <returns></returns>
        private static string GetAddEquipsInit(this ISouthHost southHost)
        {
            string result = southHost.Services.GetServices<string>(AddEquipsInit).FirstOrDefault();
            return result;
        }
        /// <summary>
        /// 获取数据库操作类
        /// </summary>
        /// <param name="southHost"></param>
        /// <returns></returns>
        private static IDataManageService GetAddEquipsIDataManageService(this ISouthHost southHost)
        {
            IDataManageService result = southHost.Services.GetServices<IDataManageService>(AddEquipsIDataManageService).FirstOrDefault();
            return result;
        }
        /// <summary>
        /// 获取设备生成类
        /// </summary>
        /// <param name="southHost"></param>
        /// <returns></returns>
        private static List<Tuple<string, Type>> GetAddEquipsImplementations(this ISouthHost southHost)
        {
            List<Tuple<string, Type>> result = southHost.Services.GetServices<Tuple<string, Type>>(AddEquipsImplementation);
            return result;
        }
        /// <summary>
        /// 设备生成
        /// </summary>
        /// <param name="southHost"></param>
        public static void AddEquips(this ISouthHost southHost)
        {
            try
            {
                southHost.WriteDefaultLog("开始执行设备生成");
                IDataManageService dataManageService = southHost.GetAddEquipsIDataManageService();
                string dllName = southHost.GetAddEquipsInit();
                List<Tuple<string, Type>> addEquips = southHost.GetAddEquipsImplementations();
                if (dataManageService != null && dllName != null && addEquips != null)
                {
                    addEquips.ForEach(t =>
                    {
                        ((BaseAddEquipEncapsulation)Activator.CreateInstance(t.Item2, new object[] {
                            dllName,
                            t.Item1,
                            dataManageService
                        })).AddEquips();
                    });
                    southHost.WriteDefaultLog("设备生成执行完成");
                }
                else
                {
                    southHost.WriteDefaultLog("缺少执行AddEquips的必要参数");
                }
            }
            catch (Exception ex)
            {
                southHost.WriteDefaultLog("AddEquips执行异常", ex);
            }
        }
        #endregion
    }
}