using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SouthCore.Default
{
    public class DefaultAppEquipBuilder : IDefaultAppEquipBuilder
    {
        private readonly IHostBuilder builder;
        public DefaultAppEquipBuilder(IHostBuilder builder)
        {
            this.builder = builder;
        }
        public IDefaultAppEquipBuilder ConfigureServices(Action<IServiceCollection> configureServices)
        {
            builder.ConfigureServices((HostBuilderContext context, IServiceCollection builder) =>
            {
                configureServices(builder);
            });
            return this;
        }
    }
}