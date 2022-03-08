using System;
using System.Collections.Generic;
using System.Text;

namespace SouthCore.Default
{
    public interface IDefaultAppEquipBuilder
    {
        IDictionary<string, object> Services { get; }
    }
}
