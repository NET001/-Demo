using System;
using System.Linq;

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
