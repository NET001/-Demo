using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgingPattern
{
    class MobileSoftB : MobileSoft
    {
        public override void Run()
        {
            Console.WriteLine("实现软件B的功能");
        }
    }
}
