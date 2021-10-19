using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactoryPattern_CJL
{
    /// <summary>
    /// 具体工厂1
    /// </summary>
    public class ConcreteFactory1 : AbstractFactory
    {
        public override AbstractProductA CreateProductA()
        {
            return new ProductA1();
        }
        public override AbstractProductB CreateProductB()
        {
            return new ProductB1();
        }
    }

}
