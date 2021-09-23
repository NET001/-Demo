using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExteriorPattern
{
    /// <summary>
    /// 外观模式
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            ExteriorMethod exteriorMethod = new ExteriorMethod();
            exteriorMethod.Created();
            exteriorMethod.Show();
            Console.ReadKey();
        }
    }
}
