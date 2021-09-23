using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethodPattern
{
    public class MinusCalculation : Calculation
    {
        public override double GetResult(double A, double B)
        {
            return A - B;
        }
    }
}
