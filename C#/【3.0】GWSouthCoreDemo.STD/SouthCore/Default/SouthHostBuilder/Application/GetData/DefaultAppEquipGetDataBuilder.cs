using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace SouthCore.Default.GetData
{
    public class DefaultAppEquipGetDataBuilder : IDefaultAppEquipGetDataBuilder
    {
        private IDefaultAppEquipBuilder builder;
        public IDefaultAppEquipBuilder Builder => builder;
        public DefaultAppEquipGetDataBuilder(IDefaultAppEquipBuilder builder)
        {
            this.builder = builder;
        }
    }
}
