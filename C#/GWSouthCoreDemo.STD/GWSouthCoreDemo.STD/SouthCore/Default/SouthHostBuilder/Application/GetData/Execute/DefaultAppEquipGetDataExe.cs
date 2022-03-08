using SouthCore.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SouthCore.Default.GetData
{
    public class DefaultAppEquipGetDataExe : IDefaultAppEquipGetDataExe
    {
        private ISouthHost southHost = null;
        public ISouthHost SouthHost => southHost;
        public DefaultAppEquipGetDataExe(ISouthHost southHost)
        {
            this.southHost = southHost;
        }
    }
}