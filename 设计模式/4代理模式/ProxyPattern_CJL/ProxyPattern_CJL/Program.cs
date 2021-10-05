using System;

namespace ProxyPattern_CJL
{
    class Program
    {
        static void Main(string[] args)
        {
            Proxy proxy = new Proxy();
            proxy.Request();
            Console.Read();
        }
    }
}
