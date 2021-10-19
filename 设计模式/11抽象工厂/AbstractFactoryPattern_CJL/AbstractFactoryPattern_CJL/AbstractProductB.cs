using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactoryPattern_CJL
{
    /// <summary>
    /// 抽象产品族B
    /// </summary>
    public abstract class AbstractProductB
    {
        public abstract void Interact(AbstractProductA a);
    }
}
