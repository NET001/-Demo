using System;
using System.Collections.Generic;
using System.Text;

namespace Strategy_CJL
{
    /// <summary>
    /// 策略上下文
    /// </summary>
    public class Context
    {
        Strategy strategy;
        public Context(Strategy strategy)
        {
            this.strategy = strategy;
        }
        //上下文接口
        public void ContextInterface()
        {
            strategy.AlgorithmInterface();
        }
    }
}
