using System;
using System.Collections.Generic;
using System.Text;

namespace SouthCore.Default.Synchronizer
{
    public class DefaultAppEquipSynchronizerBuilder : IDefaultAppEquipSynchronizerBuilder
    {
        private IDefaultAppEquipBuilder builder;
        public IDefaultAppEquipBuilder Builder => builder;
        public DefaultAppEquipSynchronizerBuilder(IDefaultAppEquipBuilder builder)
        {
            this.builder = builder;
        }
    }
}
