using System;

namespace Test1
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            CTest1 cTest1 = new CTest1();
            cTest1.EAction1();
            cTest1.EAction2();
            CTest2 cTest2 = new CTest2();
            cTest2.EAction1();
        }
    }

    public interface ITest1
    {
        void EAction1();
    }
    public class CTest1 : ITest1
    {
        public void EAction1()
        {
            Console.WriteLine("1");
        }
    }

    public class CTest2 : CTest1
    { 
    
    }
    public static class Excent
    {
        public static void EAction1(this CTest1 cTest1)
        {
            Console.WriteLine("2");
        }

        public static void EAction2(this ITest1 test1)
        {

        }
    }

}
