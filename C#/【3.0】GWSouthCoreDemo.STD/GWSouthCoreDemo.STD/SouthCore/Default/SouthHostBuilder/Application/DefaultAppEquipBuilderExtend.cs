using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SouthCore.Default.AddEquips;
using SouthCore.Default.GetData;
using SouthCore.Default.GetYc;
using SouthCore.Default.Cache;
using SouthCore.Default.Http;
using SouthCore.Common;

namespace SouthCore.Default
{
    public static class DefaultAppEquipBuilderExtend
    {
        public static IDefaultAppEquipBuilder SetStartup<TEquipStartup>(this IDefaultAppEquipBuilder builder) where TEquipStartup : class
        {
            var equipStartup = Activator.CreateInstance(typeof(TEquipStartup));
            equipStartup.GetType()
                .GetMethods()
                .Where(t => t.Name == "ConfigureServices")
                .FirstOrDefault()?.Invoke(equipStartup, new object[] { builder });
            return builder;
        }
    }
}
