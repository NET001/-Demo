using System;
using System.Collections.Generic;
using System.Text;

namespace CommandPattern_CJL
{
    /// <summary>
    /// 具体命令实现
    /// </summary>
    public class ConcreteCommand : Command
    {
        public ConcreteCommand(Receiver receiver) : base(receiver)
        { }

        public override void Execute()
        {
            receiver.Action();
        }
    }
}
