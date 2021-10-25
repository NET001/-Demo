using System;
using System.Collections.Generic;
using System.Text;

namespace Mediator_CJL
{
    /// <summary>
    /// 抽象同事类
    /// </summary>
    public abstract class Colleague
    {
        protected Mediator mediator;

        public Colleague(Mediator mediator)
        {
            this.mediator = mediator;
        }
    }
}
