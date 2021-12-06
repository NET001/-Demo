using System;
using System.Collections.Generic;
using System.Text;

namespace Flyweight_CJL
{
    /// <summary>
    /// 享元抽象
    /// </summary>
    public abstract class Flyweight
    {
        public abstract void Operation(int extrinsicstate);
    }

}
