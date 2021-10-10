using System;
using System.Collections.Generic;
using System.Text;

namespace CommandPattern_CJL
{
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
