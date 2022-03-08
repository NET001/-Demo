using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Text;

namespace SouthCore.Default.AddEquips
{
    public class DefaultAppEquipAddEquipsBuilder : IDefaultAppEquipAddEquipsBuilder
    {
        private IDefaultAppEquipBuilder builder;
        public IDefaultAppEquipBuilder Builder => builder;
        public DefaultAppEquipAddEquipsBuilder(IDefaultAppEquipBuilder builder)
        {
            this.builder = builder;
        }
    }
}