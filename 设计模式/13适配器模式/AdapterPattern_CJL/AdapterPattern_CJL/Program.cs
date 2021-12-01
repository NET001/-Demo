using System;

namespace AdapterPattern_CJL
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo1();
            Demo2();
            Console.Read();
        }
        //对象适配器
        static void Demo1()
        {
            ITarget target = new Adapter1();
            target.Request();
        }
        //类适配器
        static void Demo2()
        {
            ITarget target = new Adapter2();
            target.Request();
        }
    }
}
