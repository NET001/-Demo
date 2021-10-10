using System;
using System.Collections.Generic;
using System.Text;

namespace Bridge_CJL
{
    public class Abstraction
    {
        protected Implementor implementor;

        public void SetImplementor(Implementor implementor)
        {
            this.implementor = implementor;
        }

        public virtual void Operation()
        {
            implementor.Operation();
        }

    }

}
