using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgingPattern
{
    /// <summary>
    /// 桥接模式
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            MobileSoft mobileSoft = new MobileSoftA();
            MobileBrand mobileBrand = new MobileBrandA();
            mobileBrand.SetSoft(mobileSoft);
            mobileBrand.Run();
            mobileBrand.SetSoft(new MobileSoftB());
            mobileBrand.Run();
            Console.ReadKey();
        }
    }
}
