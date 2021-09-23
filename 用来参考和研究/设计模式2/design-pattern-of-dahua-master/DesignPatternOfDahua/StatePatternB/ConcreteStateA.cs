using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatePatternB
{
    class ConcreteStateA : State
    {
        public override void ShowState(Work work)
        {
            if (work.Hour < 12)
            {
                Console.WriteLine("状态A");
            }
            else
            {
                work.setWork(new ConcreteStateB());
                work.Show();
            }
        }
    }
}
