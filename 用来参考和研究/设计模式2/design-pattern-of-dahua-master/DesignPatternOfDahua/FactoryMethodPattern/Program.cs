using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethodPattern
{
    /// <summary>
    /// 工厂方法模式
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            IFactory factory = new AddFactory();
            Calculation calculation = factory.CreateCalculation();
            calculation.GetResult(1, 2);
        }
    }
}
