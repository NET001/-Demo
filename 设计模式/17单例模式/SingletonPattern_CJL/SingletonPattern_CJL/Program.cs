using System;

namespace SingletonPattern_CJL
{
    class Program
    {
        static void Main(string[] args)
        {
            Singleton s1 = Singleton.GetInstance();
            Singleton s2 = Singleton.GetInstance();
            if (s1 == s2)
            {
                Console.WriteLine("Objects are the same instance");
            }
            Console.Read();
        }
    }
}
