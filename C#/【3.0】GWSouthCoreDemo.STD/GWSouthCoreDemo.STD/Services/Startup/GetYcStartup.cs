using GWSouthCoreDemo.STD.Model;
using SouthCore.Default;
using SouthCore.Default.GetYc;
using System.Collections.Generic;
using System.Linq;

namespace GWSouthCoreDemo.STD.Services
{
    //遥测值获取
    public class GetYcStartup
    {
        public void ConfigureServices(IDefaultAppEquipBuilder builder)
        {
            builder.SetGetYcs(ycs => ycs
                //注册获取电表遥测的实现
                .SetGetYc(GlobalModel.EquipGroupNames[0], (object[] datas, string code) =>
               {
                   string result = "无";
                   ElectricModel electricModel = datas.ToBO<ElectricModel>(0);
                   switch (code)
                   {
                       case "electricCode":
                           {
                               result = electricModel?.electricCode;
                           }
                           break;
                       case "electricName":
                           {
                               result = electricModel?.electricName;
                           }
                           break;
                       case "quantity":
                           {
                               result = electricModel?.quantity;
                           }
                           break;
                       case "area":
                           {
                               result = electricModel?.area;
                           }
                           break;
                       case "type":
                           {
                               result = electricModel?.type;
                           }
                           break;
                   }
                   return result;
               })
                //注册获取水表遥测的实现
                .SetGetYc(GlobalModel.EquipGroupNames[1], (object[] datas, string code) =>
                 {
                     string result = "无";
                     WaterModel waterModel = datas.ToBO<WaterModel>(0);
                     switch (code)
                     {
                         case "":
                             {

                             }
                             break;
                     }
                     return result;
                 })
            );
        }
    }
}
