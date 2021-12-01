using System;
using System.Collections.Generic;
using System.Text;

namespace Flyweight_CJL
{
    /// <summary>
    /// 具体享元
    /// </summary>
    public class ConcreteFlyweight : Flyweight
    {
        public override void Operation(int extrinsicstate)
        {
            Console.WriteLine("具体Flyweight:" + extrinsicstate);
        }
    }

}
