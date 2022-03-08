using System;
using System.Collections.Generic;
using System.Text;

namespace SouthCore.Default.Http
{
    public class DefaultAppEquipHttpBuilder : IDefaultAppEquipHttpBuilder
    {
        private IDefaultAppEquipBuilder builder;
        public IDefaultAppEquipBuilder Builder => builder;
        public DefaultAppEquipHttpBuilder(IDefaultAppEquipBuilder builder)
        {
            this.builder = builder;
        }
    }
}
