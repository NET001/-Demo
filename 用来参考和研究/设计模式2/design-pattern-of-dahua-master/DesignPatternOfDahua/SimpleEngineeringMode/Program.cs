using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactoryPattern
{
    /// <summary>
    /// 简单工厂模式
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("请输入第一个数！");
            var numberA = Console.ReadLine();
            Console.WriteLine("请输入运算符！('+','-')");
            var ope = Console.ReadLine();
            Console.WriteLine("请输入第二个数！");
            var numberB = Console.ReadLine();
            CalculationFactory factory = new CalculationFactory(ope);
            Calculation calculation = factory.GetCalculation();
            var result = calculation.GetResult(numberA, numberB);
            Console.WriteLine("{0}{1}{2}={3}", numberA, ope, numberB, result);
            Console.ReadKey();
        }
    }
}
