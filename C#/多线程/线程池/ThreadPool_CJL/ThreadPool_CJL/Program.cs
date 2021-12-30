using System;
using System.Threading;

namespace ThreadPool_CJL
{
    class Program
    {
        static void Main(string[] args)
        {
            //设置线程池的最大线程数超过的则需要排队
            ThreadPool.SetMaxThreads(10, 10);
            for (int i = 0; i < 100; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback((object obj) =>
                {
                    Console.WriteLine(obj);
                    Thread.Sleep(5000);
                }), i);
            }
            Console.ReadKey();
        }
    }
}
