using System;
using System.Collections.Generic;
using System.Text;

namespace SouthCore.Default.GetData
{
    public class DefaultAppEquipGetDataBuilder : IDefaultAppEquipGetDataBuilder
    {
        private IDictionary<string, object> services = new Dictionary<string, object>();
        public IDictionary<string, object> Services => services;
        public DefaultAppEquipGetDataBuilder()
        {
        }
    }
}
