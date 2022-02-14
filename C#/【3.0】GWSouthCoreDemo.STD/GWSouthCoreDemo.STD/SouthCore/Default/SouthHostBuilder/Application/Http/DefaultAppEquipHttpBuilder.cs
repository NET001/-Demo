using System;
using System.Collections.Generic;
using System.Text;

namespace SouthCore.Default.Http
{
    public class DefaultAppEquipHttpBuilder : IDefaultAppEquipHttpBuilder
    {
        private IDictionary<string, object> services = new Dictionary<string, object>();
        public IDictionary<string, object> Services => services;
    }
}
