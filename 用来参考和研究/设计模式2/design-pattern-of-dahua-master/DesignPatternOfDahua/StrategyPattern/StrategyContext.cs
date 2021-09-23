using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyPattern
{
    public class StrategyContext
    {
        public static Strategy strategy = null;
        public static readonly string calculationWay = "fullReduction";
        public static readonly double discountRate = 0.95;
        public static readonly double fullMoney = 300;
        public static readonly double reductionMoney = 100;
        public static double GetResult(double money)
        {
            switch (calculationWay)
            {
                case "normal":
                    strategy = new NormalStrategy();
                    break;
                case "discount":
                    strategy = new DiscountStrategy(discountRate);
                    break;
                case "fullReduction":
                    strategy = new FullReductionStrategy(fullMoney, reductionMoney);
                    break;
            }
            return strategy.GetResult(money);
        }
    }
}
