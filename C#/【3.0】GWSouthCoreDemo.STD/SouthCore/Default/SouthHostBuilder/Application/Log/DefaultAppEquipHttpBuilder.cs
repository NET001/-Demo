using System;
using System.Collections.Generic;
using System.Text;

namespace SouthCore.Default.Log
{
    public class DefaultAppEquipLogBuilder : IDefaultAppEquipLogBuilder
    {
        private IDefaultAppEquipBuilder builder;
        public IDefaultAppEquipBuilder Builder => builder;
        public DefaultAppEquipLogBuilder(IDefaultAppEquipBuilder builder)
        {
            this.builder = builder;
        }
    }
}
