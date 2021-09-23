using AbstractFactoryPattern.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactoryPattern
{
    /// <summary>
    /// 抽象工厂模式
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var user = UserService.GetUserById("1");
            Console.ReadKey();
        }
    }
}
