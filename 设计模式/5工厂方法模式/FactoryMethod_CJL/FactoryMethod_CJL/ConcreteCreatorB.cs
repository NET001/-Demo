using System;
using System.Collections.Generic;
using System.Text;

namespace FactoryMethod_CJL
{
    /// <summary>
    /// 对应B产品实现的工厂
    /// </summary>
    public class ConcreteCreatorB : Creator
    {
        public Product CeateProduct()
        {
            return new ConcreteProductB();
        }
    }
}
