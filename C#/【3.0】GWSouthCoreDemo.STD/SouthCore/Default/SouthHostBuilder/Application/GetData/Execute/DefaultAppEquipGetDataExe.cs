
using System;
using System.Collections.Generic;
using System.Text;

namespace SouthCore.Default.GetData
{
    public class DefaultAppEquipGetDataExe : IDefaultAppEquipGetDataExe
    {
        private IServiceProvider serviceProvider = null;
        public IServiceProvider ServiceProvider => serviceProvider;
        public DefaultAppEquipGetDataExe(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
    }
}