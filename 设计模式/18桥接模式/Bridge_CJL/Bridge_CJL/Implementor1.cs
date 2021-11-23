using System;
using System.Collections.Generic;
using System.Text;

namespace Bridge_CJL
{
    /// <summary>
    /// 实现化1
    /// </summary>
    public abstract class Implementor1
    {
        public abstract void Operation1();
    }
    public class ConcreteImplementor1A : Implementor1
    {
        public override void Operation1()
        {
            Console.WriteLine("Implementor1具体实现A的方法执行");
        }
    }
    public class ConcreteImplementor1B : Implementor1
    {
        public override void Operation1()
        {
            Console.WriteLine("Implementor1具体实现B的方法执行");
        }
    }
}
