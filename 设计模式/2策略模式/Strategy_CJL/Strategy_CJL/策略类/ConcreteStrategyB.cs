using System;
using System.Collections.Generic;
using System.Text;

namespace Strategy_CJL
{
    /// <summary>
    /// 算法B
    /// </summary>
    public class ConcreteStrategyB : Strategy
    {
        public override void AlgorithmInterface()
        {
            Console.WriteLine("算法B实现");
        }
    }
}
