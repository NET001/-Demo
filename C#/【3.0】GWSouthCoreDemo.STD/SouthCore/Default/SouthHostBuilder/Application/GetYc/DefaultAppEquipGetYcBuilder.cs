using System;
using System.Collections.Generic;
using System.Text;

namespace SouthCore.Default.GetYc
{
    public class DefaultAppEquipGetYcBuilder : IDefaultAppEquipGetYcBuilder
    {
        private IDefaultAppEquipBuilder builder;
        public IDefaultAppEquipBuilder Builder => builder;
        public DefaultAppEquipGetYcBuilder(IDefaultAppEquipBuilder builder)
        {
            this.builder = builder;
        }
    }
}
