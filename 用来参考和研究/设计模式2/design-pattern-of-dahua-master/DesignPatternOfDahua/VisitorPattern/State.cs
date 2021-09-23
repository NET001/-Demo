using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorPattern
{
    /// <summary>
    /// 状态
    /// </summary>
    abstract class State
    {
        public abstract void getStateForFactionA(FactionA factionA);
        public abstract void getStateForFactionB(FactionB factionB);
    }
}
