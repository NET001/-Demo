using System;
using System.Collections.Generic;
using System.Text;

namespace Mediator_CJL
{
    /// <summary>
    /// 抽象中介者
    /// </summary>
    public abstract class Mediator
    {
        public abstract void Send(string message, Colleague colleague);
    }
}
