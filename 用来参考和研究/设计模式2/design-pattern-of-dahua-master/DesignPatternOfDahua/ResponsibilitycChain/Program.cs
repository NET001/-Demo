using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponsibilitycChain
{
    class Program
    {
        /// <summary>
        /// 职责链模式
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Handler handler1 = new ConcreteHandler1();
            Handler handler2 = new ConcreteHandler2();
            handler1.setHandler(handler2);
            handler1.HandlerExcute(30);
            Console.ReadKey();
        }
    }
}