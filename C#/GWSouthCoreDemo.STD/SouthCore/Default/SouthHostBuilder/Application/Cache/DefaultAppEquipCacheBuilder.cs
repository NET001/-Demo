using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace SouthCore.Default.Cache
{
    public class DefaultAppEquipCacheBuilder : IDefaultAppEquipCacheBuilder
    {
        private IDefaultAppEquipBuilder builder;
        public IDefaultAppEquipBuilder Builder => builder;
        public DefaultAppEquipCacheBuilder(IDefaultAppEquipBuilder builder)
        {
            this.builder = builder;
        }
    }
}
