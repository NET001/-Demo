using System;
using System.Collections.Generic;
using System.Text;

namespace CommandPattern_CJL
{
    /// <summary>
    /// 抽象命令类
    /// </summary>
   public abstract class Command
    {
        protected Receiver receiver;

        public Command(Receiver receiver)
        {
            this.receiver = receiver;
        }

        abstract public void Execute();
    }

}
