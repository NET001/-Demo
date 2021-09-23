using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatePatternB
{
    public class ConcreteStateC : State
    {
        public override void ShowState(Work work)
        {
            Console.WriteLine("状态C");
        }
    }
}
