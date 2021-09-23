using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatePattern
{
    class ConcreteStateB : State
    {
        public override void Show(Context context)
        {
            context.state = new ConcreteStateA();
            Console.WriteLine("当前状态是{0}", context.state.GetType().Name);
        }
    }
}
