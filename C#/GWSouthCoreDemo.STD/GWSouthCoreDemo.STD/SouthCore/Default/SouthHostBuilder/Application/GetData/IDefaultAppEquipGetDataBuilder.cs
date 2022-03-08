using System;
using System.Collections.Generic;
using System.Text;

namespace SouthCore.Default.GetData
{
    public interface IDefaultAppEquipGetDataBuilder
    {
        IDictionary<string, object> Services { get; }
    }
}
