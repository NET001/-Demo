using System;
using System.Collections.Generic;
using System.Text;

namespace FactoryMethod_CJL
{
    public class ConcreteCreatorB : Creator
    {
        public Product CeateProduct()
        {
            return new ConcreteProductB();
        }
    }
}
