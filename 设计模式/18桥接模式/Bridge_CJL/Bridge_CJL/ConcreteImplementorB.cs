using System;
using System.Collections.Generic;
using System.Text;

namespace Bridge_CJL
{
    /// <summary>
    /// 具体实现化B
    /// </summary>
    public class ConcreteImplementorB : Implementor
    {
        public override void Operation()
        {
            Console.WriteLine("具体实现B的方法执行");
        }
    }
}
