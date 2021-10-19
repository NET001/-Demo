using System;
using System.Collections.Generic;
using System.Text;

namespace FactoryMethod_CJL
{
    /// <summary>
    /// 对应A产品实现的工厂
    /// </summary>
    public class ConcreteCreatorA : Creator
    {
        public Product CeateProduct()
        {
            return new ConcreteProductA();
        }
    }
}
