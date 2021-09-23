using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyPattern
{
    public class NormalStrategy : Strategy
    {
        public override double GetResult(double money)
        {
            return money;
        }
    }
}
