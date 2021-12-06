using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Concurrent;

namespace Strategy_CJL
{
    /// <summary>
    /// 策略工厂
    /// </summary>
    public class StrategyFactory
    {
        static ConcurrentDictionary<Type, Strategy> keyValuePairs = new ConcurrentDictionary<Type, Strategy>();
        public static Strategy GetStrategy(Type type)
        {
            if (keyValuePairs.TryGetValue(type, out _))
            {
                return keyValuePairs[type];
            }
            else
            {
                Strategy strategy = Activator.CreateInstance(type) as Strategy;
                keyValuePairs[type] = strategy;
                return strategy;
            }
        }
    }
}
