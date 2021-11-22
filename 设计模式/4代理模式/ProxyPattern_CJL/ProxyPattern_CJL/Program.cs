using System;

namespace ProxyPattern_CJL
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo2();
            Console.Read();
        }
        /// <summary>
        /// 使用抽象
        /// </summary>
        static void Demo1()
        {
            Proxy1 proxy = new Proxy1();
            proxy.Request();
        }
        //使用继承
        static void Demo2()
        {
            Proxy2 proxy = new Proxy2();
            proxy.Request();
        }
    }


}
