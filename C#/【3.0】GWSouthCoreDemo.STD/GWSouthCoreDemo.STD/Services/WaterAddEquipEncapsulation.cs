using GWBasics.STD.Services;
using GWSouthCoreDemo.STD.Model;
using SouthCore.Common;
using SouthCore.Default.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GWSouthCoreDemo.STD.Services
{
    public class WaterAddEquipEncapsulation : DefaultAddEquipEncapsulation
    {
        public WaterAddEquipEncapsulation(string dllName, string iotequip_nm, IDataManageService dataManageService)
            : base(dllName, iotequip_nm, dataManageService)
        {
            EquipThreadQuantity = 10;
        }
        protected override List<object> DeficiencyData(List<EquipDto> equips)
        {
            List<object> result = new List<object>();
            result = AppServices.SouthHost.Services.GetCacheStorage<List<WaterModel>>().Select(t => (object)t).ToList();
            return result;
        }
        protected override void AddEquipAOP(object obj, IotEquipDto iotEquip, EquipDto equipDto)
        {

        }
    }
}
