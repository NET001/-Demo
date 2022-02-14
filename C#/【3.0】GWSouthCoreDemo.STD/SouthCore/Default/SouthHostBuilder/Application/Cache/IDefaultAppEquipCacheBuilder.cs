using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace SouthCore.Default.Cache
{
    public interface IDefaultAppEquipCacheBuilder
    {
        public IDefaultAppEquipBuilder Builder { get; }
    }
}
