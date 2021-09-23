using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorPattern
{
    /// <summary>
    /// 派别
    /// </summary>
    abstract class Faction
    {
        public abstract void Accept(State state);
    }
}
