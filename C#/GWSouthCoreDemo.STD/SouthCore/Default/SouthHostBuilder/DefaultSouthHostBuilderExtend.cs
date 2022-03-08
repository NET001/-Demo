
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace SouthCore.Default
{
    public static class DefaultSouthHostBuilderExtend
    {
        public static IHostBuilder ConfigureSouthHostDefaults(this IHostBuilder builder, Action<IDefaultAppEquipBuilder> configure)
        {
            configure(new DefaultAppEquipBuilder(builder));
            builder.ConfigureServices(delegate (HostBuilderContext context, IServiceCollection services)
            {
                services.AddHostedService<GenericSouthHostService>();
            });
            return builder;
        }
    }
}
