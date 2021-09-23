using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactoryPattern
{
    public class CalculationFactory
    {
        /// <summary>
        /// 可用传参形式替代
        /// </summary>
        public string Operator { get; set; }
        public CalculationFactory(string Operator)
        {
            this.Operator = Operator;
        }
        public Calculation GetCalculation()
        {
            Calculation calculation = null;
            switch (Operator)
            {
                case "+":
                    calculation = new Plus();
                    break;
                case "-":
                    calculation = new Minus();
                    break;
            }
            return calculation;
        }
    }
}
