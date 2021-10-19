using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactoryPattern_CJL
{
    /// <summary>
    /// 产品B类实现2
    /// </summary>
    public class ProductB2 : AbstractProductB
    {
        public override void Interact(AbstractProductA a)
        {
            Console.WriteLine(this.GetType().Name +
              " interacts with " + a.GetType().Name);
        }
    }
}
