using System;
using System.Collections.Generic;
using System.Text;

namespace FactoryMethod_CJL
{
    public class ConcreteCreatorA : Creator
    {
        public Product CeateProduct()
        {
            return new ConcreteProductA();
        }
    }
}
