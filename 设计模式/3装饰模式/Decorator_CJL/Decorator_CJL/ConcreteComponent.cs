using System;
using System.Collections.Generic;
using System.Text;

namespace Decorator_CJL
{
    /// <summary>
    /// 具体对象待扩展的对象
    /// </summary>
    public class ConcreteComponent : Component
    {
        public override void Operation()
        {
            Console.WriteLine("具体对象的操作");
        }
    }
}
