using System;
using System.Collections.Generic;
using System.Text;

namespace Bridge_CJL
{
    /// <summary>
    /// 抽象化(骨架)
    /// </summary>
    public class Abstraction
    {
        protected Implementor1 implementor1;
        protected Implementor2 implementor2;

        public void SetImplementor(Implementor1 implementor1, Implementor2 implementor2)
        {
            this.implementor1 = implementor1;
            this.implementor2 = implementor2;
        }
        public virtual void Operation()
        {
            implementor1.Operation1();
            implementor2.Operation2();
        }
    }
}
