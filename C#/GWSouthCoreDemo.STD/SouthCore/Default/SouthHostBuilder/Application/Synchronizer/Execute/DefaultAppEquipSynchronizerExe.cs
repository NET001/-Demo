using System;

namespace SouthCore.Default.Synchronizer
{
    public class DefaultAppEquipSynchronizerExe : IDefaultAppEquipSynchronizerExe
    {
        private IServiceProvider serviceProvider = null;
        public IServiceProvider ServiceProvider => serviceProvider;
        public DefaultAppEquipSynchronizerExe(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
    }
}