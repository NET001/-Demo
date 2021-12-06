using System;
using System.Collections.Generic;
using System.Text;

namespace Flyweight_CJL
{
    /// <summary>
    /// 非享元
    /// </summary>
    public class UnsharedConcreteFlyweight : Flyweight
    {
        public override void Operation(int extrinsicstate)
        {
            Console.WriteLine("不共享的具体Flyweight:" + extrinsicstate);
        }
    }
}
