using System;
using System.Collections.Generic;
using System.Text;

namespace SouthCore.Default
{
    public class DefaultAppEquipBuilder : IDefaultAppEquipBuilder
    {
        IDictionary<string, object> services = new Dictionary<string, object>();
        public IDictionary<string, object> Services => services;
    }
}