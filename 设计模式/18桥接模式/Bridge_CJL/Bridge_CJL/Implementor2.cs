using System;
using System.Collections.Generic;
using System.Text;

namespace Bridge_CJL
{
    /// <summary>
    /// 实现化2
    /// </summary>
    public abstract class Implementor2
    {
        public abstract void Operation2();
    }
    public class ConcreteImplementor2A : Implementor2
    {
        public override void Operation2()
        {
            Console.WriteLine("Implementor2具体实现A的方法执行");
        }
    }
    public class ConcreteImplementor2B : Implementor2
    {
        public override void Operation2()
        {
            Console.WriteLine("Implementor2具体实现B的方法执行");
        }
    }
}
