using SouthCore.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SouthCore.Default
{
    public static class DefaultSouthHostBuilderExtend
    {
        public static ISouthHostBuilder ConfigureSouthHostDefaults(this ISouthHostBuilder builder, Action<IDefaultAppEquipBuilder> configure)
        {
            IDefaultAppEquipBuilder defaultAppEquipBuilder = new DefaultAppEquipBuilder();
            configure(defaultAppEquipBuilder);
            foreach (var key in defaultAppEquipBuilder.Services.Keys)
            {
                builder.Services[key] = defaultAppEquipBuilder.Services[key];
            }
            return builder;
        }
        
    }
}
