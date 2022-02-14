using SouthCore.Common;
using System;
using System.Collections.Generic;
using System.Text;
using SouthCore.Default.Cache;
using GWBasics.STD.Services;
using GWSouthCoreDemo.STD.Model;
using System.Linq;

namespace GWSouthCoreDemo.STD.Services
{
    public class ElectricAddEquipEncapsulation : DefaultAddEquipEncapsulation
    {
        public ElectricAddEquipEncapsulation(string dllName, string iotequip_nm, IDataManageService dataManageService)
            : base(dllName, iotequip_nm, dataManageService)
        {
            EquipThreadQuantity = 10;
        }
        protected override List<object> DeficiencyData(List<EquipDto> equips)
        {
            List<object> result = new List<object>();
            result = AppServices.SouthHost.Services.GetCacheStorage<List<ElectricModel>>().Select(t => (object)t).ToList();
            return result;
        }
        protected override void AddEquipAOP(object obj, IotEquipDto iotEquip, EquipDto equipDto)
        {

        }
    }
}
