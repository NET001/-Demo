using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace SouthCore.Default.GetData
{
    public interface IDefaultAppEquipGetDataBuilder
    {
        public IDefaultAppEquipBuilder Builder { get; }
    }
}
