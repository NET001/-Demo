using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethodPattern
{
    public class MinusFactory : IFactory
    {
        Calculation calculation;
        public Calculation CreateCalculation()
        {
            calculation = new MinusCalculation();
            return calculation;
        }
    }
}
