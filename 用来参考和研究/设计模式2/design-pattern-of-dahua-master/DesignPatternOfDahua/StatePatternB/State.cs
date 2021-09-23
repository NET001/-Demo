using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatePatternB
{
    public abstract class State
    {
        public abstract void ShowState(Work work);
    }
}
