using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatePattern
{
    public class ConcreteStateA : State
    {
        public override void Show(Context context)
        {
            context.state = new ConcreteStateB();
            Console.WriteLine("当前状态是{0}", context.state.GetType().Name);
        }
    }
}
