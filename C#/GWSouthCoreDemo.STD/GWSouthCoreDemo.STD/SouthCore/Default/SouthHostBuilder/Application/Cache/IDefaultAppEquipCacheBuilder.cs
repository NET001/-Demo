using System;
using System.Collections.Generic;
using System.Text;

namespace SouthCore.Default.Cache
{
    public interface IDefaultAppEquipCacheBuilder
    {
        IDictionary<string, object> Services { get; }
    }
}
