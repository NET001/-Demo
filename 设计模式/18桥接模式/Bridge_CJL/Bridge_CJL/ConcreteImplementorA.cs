using System;
using System.Collections.Generic;
using System.Text;

namespace Bridge_CJL
{
    public class ConcreteImplementorA : Implementor
    {
        public override void Operation()
        {
            Console.WriteLine("具体实现A的方法执行");
        }
    }
}
