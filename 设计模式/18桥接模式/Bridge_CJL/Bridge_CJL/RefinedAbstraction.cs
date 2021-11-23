using System;
using System.Collections.Generic;
using System.Text;

namespace Bridge_CJL
{
    /// <summary>
    /// 抽象化扩展1(骨架)
    /// </summary>
    public class RefinedAbstraction1 : Abstraction
    {
        public override void Operation()
        {
            Console.WriteLine("扩展抽象化(RefinedAbstraction1)角色被访问");
            implementor1.Operation1();
            implementor2.Operation2();
        }
    }
    /// <summary>
    /// 抽象化扩展2(骨架)
    /// </summary>
    public class RefinedAbstraction2 : Abstraction
    {
        public override void Operation()
        {
            Console.WriteLine("扩展抽象化(RefinedAbstraction2)角色被访问");
            implementor2.Operation2();
            implementor1.Operation1();
        }
    }
}