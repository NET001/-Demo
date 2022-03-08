using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace SouthCore.Default
{
    public interface IDefaultAppEquipBuilder
    {
        IDefaultAppEquipBuilder ConfigureServices(Action<IServiceCollection> configureServices);
    }
}
