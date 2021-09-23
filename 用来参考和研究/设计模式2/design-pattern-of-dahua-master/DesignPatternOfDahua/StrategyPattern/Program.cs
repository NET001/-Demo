using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyPattern
{
    /// <summary>
    /// 策略模式
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            double money = StrategyContext.GetResult(500);
            Console.WriteLine(money);
            Console.ReadKey();
        }
    }
}
