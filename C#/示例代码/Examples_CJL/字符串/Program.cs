using System;
using System.Threading.Tasks;
using System.Threading;

namespace 异步
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //字符串以指定开头
            string str = "XL123";
            if (!str.StartsWith("GL"))
            {
                Console.WriteLine("字符串没有一个GL开头");
            }

        }
    }
}
