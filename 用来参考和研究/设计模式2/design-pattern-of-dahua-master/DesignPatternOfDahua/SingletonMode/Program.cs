using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingletonMode
{
    /// <summary>
    /// 单例模式
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var model1 = Singleton.CreateInstance();
            var model2 = Singleton.CreateInstance();
            var m1 = SingletonB.CreateInstance();
            var m2 = SingletonB.CreateInstance();
            Console.WriteLine(model1 == model2);
            Console.WriteLine(m1 == m2);
            Console.ReadKey();
        }
    }
}
