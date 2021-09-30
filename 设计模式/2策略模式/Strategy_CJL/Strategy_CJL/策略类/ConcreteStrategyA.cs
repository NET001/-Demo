using System;
using System.Collections.Generic;
using System.Text;

namespace Strategy_CJL
{
    /// <summary>
    /// 算法A
    /// </summary>
    public class ConcreteStrategyA : Strategy
    {
        public override void AlgorithmInterface()
        {
            Console.WriteLine("算法A实现");
        }
    }
}
