using System;
using System.Collections.Generic;
using System.Text;

namespace GWSouthCoreDemo.STD.Model
{
    public static class GlobalModel
    {
        public static readonly string[] EquipGroupNames = new string[] { "电表","水表" };
        public const string DllName = "GWSouthCoreDemo.STD";
        public const string HostUrl = "http://localhost:5000";
        public const string ElectricUrl = "/gecp/electric/queryList";
        public const string WaterUrl = "/gecp/water_meter/queryList";
    }
}
