using System;
using System.Collections.Generic;
using System.Text;

namespace SouthCore.Default.AddEquips
{
    public class DefaultAppEquipAddEquipsBuilder : IDefaultAppEquipAddEquipsBuilder
    {
        IDictionary<string, object> services = new Dictionary<string, object>();
        public IDictionary<string, object> Services => services;
    }
}