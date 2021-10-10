using System;
using System.Collections.Generic;
using System.Text;

namespace Flyweight_CJL
{
    public class UnsharedConcreteFlyweight : Flyweight
    {
        public override void Operation(int extrinsicstate)
        {
            Console.WriteLine("不共享的具体Flyweight:" + extrinsicstate);
        }
    }
}
