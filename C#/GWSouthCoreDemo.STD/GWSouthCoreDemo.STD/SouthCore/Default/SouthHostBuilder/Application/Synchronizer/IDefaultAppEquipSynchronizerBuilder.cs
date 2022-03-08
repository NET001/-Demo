using System;
using System.Collections.Generic;
using System.Text;

namespace SouthCore.Default.Synchronizer
{
    public interface IDefaultAppEquipSynchronizerBuilder
    {
        IDictionary<string, object> Services { get; }
    }
}
