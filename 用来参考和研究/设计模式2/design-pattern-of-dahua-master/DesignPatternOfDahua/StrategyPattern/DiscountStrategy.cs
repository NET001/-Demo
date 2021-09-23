using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyPattern
{
    public class DiscountStrategy : Strategy
    {
        public DiscountStrategy(double discountRate)
        {
            this.discountRate = discountRate;
        }

        public double discountRate { get; set; }
        public override double GetResult(double money)
        {
            return money * discountRate;
        }
    }
}
