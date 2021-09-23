using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactoryPattern
{
    public class Minus : Calculation
    {
        public override string GetResult(string NumberA, string NumberB)
        {
            double NumA = double.Parse(NumberA);
            double NumB = double.Parse(NumberB);
            return (NumA - NumB).ToString();
        }
    }
}
