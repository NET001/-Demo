using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethodPattern
{
    /// <summary>
    /// n个工厂 1种产品
    /// </summary>
    interface IFactory
    {
        Calculation CreateCalculation();
    }
}
