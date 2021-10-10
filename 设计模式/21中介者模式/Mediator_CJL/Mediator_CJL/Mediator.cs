using System;
using System.Collections.Generic;
using System.Text;

namespace Mediator_CJL
{
    public abstract class Mediator
    {
        public abstract void Send(string message, Colleague colleague);
    }
}
