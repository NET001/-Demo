using System;
using System.Collections.Generic;
using System.Text;

namespace StatePattern_CJL
{
    public class ConcreteStateA : State
    {
        public override void Handle(Context context)
        {
            context.State = new ConcreteStateB();
        }
    }
}
