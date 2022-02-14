using SouthCore;
using SouthCore.Common;

using SouthCore.Default;
using SouthCore.Default.Http;
using SouthCore.Default.Cache;
using SouthCore.Default.AddEquips;
using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using GWSouthCoreDemo.STD.Model;
using GWSouthCoreDemo.STD.Services;
using SouthCore.Default.Log;
using SouthCore.Default.GetData;
using SouthCore.Default.GetYc;
using GWDataCenter;
using Microsoft.Extensions.Hosting;

namespace GWBasics.STD.Services
{
    public static class AppServices
    {
        #region 变量
        #endregion
        #region 属性
        #region 项目参数
        /// <summary>
        /// 承载实例
        /// </summary>
        public static IHost SouthHost { get; set; }
        #endregion
        #region 公共
        public static bool AddEquipFlag { get; set; } = false;
        public static bool FirstInitFlag { get; set; } = false;
        public static int GetDataSleep { get; set; } = 1000;
        public static int SourceSyncSleep { get; set; } = 1000 * 60;
        public static int DataCacheSyncSleep { get; set; } = 1000 * 60;
        #endregion
        #endregion
        #region 外部方法
        /// <summary>
        /// 插件启动运行
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void FirstInit()
        {
            if (!FirstInitFlag)
            {
                try
                {
                    FirstInitFlag = true;
                    SouthHost = 
                        //获取一个默认构建的承载系统构建对象
                        Host.CreateDefaultBuilder()
                        //南向设备接入框架
                        .ConfigureSouthHostDefaults(builder => builder
                            //设置日志
                            .SetStartup<LogStartup>()
                            //设置设备新增
                            .SetStartup<AddEquipStartup>()
                            //设置缓存
                            .SetStartup<CacheStartup>()
                            //设置Http请求
                            .SetStartup<HttpStartup>()
                            //设置GetData
                            .SetStartup<GetDataStartup>()
                            //设置GetYc
                            .SetStartup<GetYcStartup>()
                            //设置同步器
                            .SetStartup<SynchronizerStartup>()
                        )
                        //构建承载系统
                        .Build();
                    //运行承载系统
                    SouthHost.Run();
                }
                catch (Exception ex)
                {
                    DataCenter.WriteLogFile(ex.ToString());
                }
            }
        }
        #endregion
    }
}