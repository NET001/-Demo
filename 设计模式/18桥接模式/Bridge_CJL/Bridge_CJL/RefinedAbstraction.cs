using System;
using System.Collections.Generic;
using System.Text;

namespace Bridge_CJL
{
    /// <summary>
    /// 抽象化扩展/实现
    /// </summary>
    public class RefinedAbstraction : Abstraction
    {
        public override void Operation()
        {
            Console.WriteLine("扩展抽象化(RefinedAbstraction)角色被访问");
            implementor.Operation();
        }
    }
}
