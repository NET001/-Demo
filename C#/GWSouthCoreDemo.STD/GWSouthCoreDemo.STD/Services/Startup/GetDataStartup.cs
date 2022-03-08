using GWSouthCoreDemo.STD.Model;
using SouthCore.Default;
using SouthCore.Default.GetData;

namespace GWSouthCoreDemo.STD.Services
{
    //Data获取
    public class GetDataStartup
    {
        public void ConfigureServices(IDefaultAppEquipBuilder builder)
        {
            builder.SetGetDatas(datas => datas
                .SetGetData(GlobalModel.EquipGroupNames[0], (object[] parms, IDefaultAppEquipGetDataExe exe) =>
                {
                    object result = exe.GetCacheData<ElectricModel>(t => t.electricCode == parms[0].ToString());
                    return new object[] { result };
                })
                .SetGetData(GlobalModel.EquipGroupNames[1], (object[] parms, IDefaultAppEquipGetDataExe exe) =>
                {
                    object result = exe.GetCacheData<WaterModel>(t => t.waterCode == parms[0].ToString());
                    return new object[] { result };
                })
            );
        }
    }
}
