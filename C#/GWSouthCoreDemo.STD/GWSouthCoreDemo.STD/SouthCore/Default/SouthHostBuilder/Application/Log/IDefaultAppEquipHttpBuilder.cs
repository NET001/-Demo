using System;
using System.Collections.Generic;
using System.Text;

namespace SouthCore.Default.Log
{
    public interface IDefaultAppEquipLogBuilder
    {
        IDictionary<string, object> Services { get; }
    }
}
