using System;
using System.Collections.Generic;
using System.Text;

namespace SouthCore.Default.Http
{
    public interface IDefaultAppEquipHttpBuilder
    {
        IDictionary<string, object> Services { get; }
    }
}
