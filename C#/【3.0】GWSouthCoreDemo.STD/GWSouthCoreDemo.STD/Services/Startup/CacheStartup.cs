using GWSouthCoreDemo.STD.Model;
using SouthCore.Default;
using SouthCore.Default.Cache;
using System.Collections.Generic;

namespace GWSouthCoreDemo.STD.Services
{
    public class CacheStartup
    {
        public void ConfigureServices(IDefaultAppEquipBuilder builder)
        {
            builder.SetCaches(caches => caches
                //配置缓存
                .SetCache<List<WaterModel>>()
                .SetCache<List<ElectricModel>>()
            );
        }
    }
}
