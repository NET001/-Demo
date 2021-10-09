using System;

namespace AdapterPattern_CJL
{
    class Program
    {
        static void Main(string[] args)
        {
            Target target = new Adapter();
            target.Request();
            Console.Read();
        }
    }
}
