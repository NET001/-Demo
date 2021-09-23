using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterPattern
{
    /// <summary>
    /// 适配器模式
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            OdlInterface odlInterface = new Adapter();
            odlInterface.Request();
            Console.ReadKey();
        }
    }
}