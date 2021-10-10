using System;
using System.Collections.Generic;
using System.Text;

namespace Mediator_CJL
{
    public abstract class Colleague
    {
        protected Mediator mediator;

        public Colleague(Mediator mediator)
        {
            this.mediator = mediator;
        }
    }

}
