
using SouthCore.Default;
using SouthCore.Default.Synchronizer;
using SouthCore.Default.Http;
using System.Collections.Generic;
using GWSouthCoreDemo.STD.Model;
using SouthCore.Common;

namespace GWSouthCoreDemo.STD.Services
{
    //同步器设置
    public class SynchronizerStartup
    {
        public void ConfigureServices(IDefaultAppEquipBuilder builder)
        {
            builder.SetSynchronizers(config => config
                //接口同步器
                .SetSynchronizer("DataSource", (IDefaultAppEquipSynchronizerExe exe) =>
                {
                    //进行接口缓存映射
                    exe.HttpMapCacheStorage(GlobalModel.EquipGroupNames[0], (object[] resp) =>
                    {
                        List<ElectricModel> result = null;
                        ElectricModelResult electricModelResult = resp[0].ToString().ToObject<ElectricModelResult>();
                        result = electricModelResult?.content;
                        return result;
                    });
                    exe.HttpMapCacheStorage(GlobalModel.EquipGroupNames[1], (object[] resp) =>
                    {
                        List<WaterModel> result = null;
                        WaterModelResult waterModelResult = resp[0].ToString().ToObject<WaterModelResult>();
                        result = waterModelResult?.content;
                        return result;
                    });
                }, 1000)
            );
        }
    }
}
