using GWSouthCoreDemo.STD.Model;
using SouthCore.Default;
using SouthCore.Default.AddEquips;

namespace GWSouthCoreDemo.STD.Services
{

    public class AddEquipStartup
    {
        public void ConfigureServices(IDefaultAppEquipBuilder builder)
        {
            builder.SetAddEquips(addEquips => addEquips
                //指定动态库名称
                .SetAddEquipsInit(GlobalModel.DllName)
                //指定数据库操作适配器类
                .SetAddEquipsIDataManageService<DataManageServiceAdapter>()
                //配置设备生成实现
                .SetAddEquipsImplementation<ElectricAddEquipEncapsulation>(GlobalModel.EquipGroupNames[0])
                .SetAddEquipsImplementation<WaterAddEquipEncapsulation>(GlobalModel.EquipGroupNames[1])
            );
        }
    }
}
