using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatePatternB
{
    public class ConcreteStateB : State
    {
        public override void ShowState(Work work)
        {
            if (work.Hour < 18)
            {
                Console.WriteLine("状态B");
            }
            else
            {
                work.setWork(new ConcreteStateC());
                work.Show();
            }
        }
    }
}
