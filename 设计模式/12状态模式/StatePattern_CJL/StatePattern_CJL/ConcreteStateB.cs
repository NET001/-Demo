using System;
using System.Collections.Generic;
using System.Text;

namespace StatePattern_CJL
{
    /// <summary>
    /// 具体状态B
    /// </summary>
    public class ConcreteStateB : State
    {
        public override void Handle(Context context)
        {
            context.State = new ConcreteStateA();
        }
    }
}