using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyPattern
{
    public class FullReductionStrategy : Strategy
    {
        public FullReductionStrategy(double fullMoney, double reductionMoney)
        {
            this.fullMoney = fullMoney;
            this.reductionMoney = reductionMoney;
        }

        public double fullMoney { get; set; }
        public double reductionMoney { get; set; }
        public override double GetResult(double money)
        {
            if (fullMoney >= 0)
            {
                return money - Math.Floor(money / fullMoney) * reductionMoney;
            }
            return money;
        }
    }
}
