using System;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;

namespace MultithreadingTest
{
    class Program
    {
        //对static静态变量的处理
        static object obj1 = new object();
        static object lockObj = new object();
        static void Main(string[] args)
        {
            //SafetyAdd();
            //NoSafety();
            //Safety();
            Console.ReadLine();
        }
        //普通集合
        private static void NoSafety()
        {
            List<int> list = new List<int>();
            for (int i = 0; i < 10; i++)
            {
                Task.Run(() =>
                {
                    while (true)
                    {
                        if (list.Count == 0)
                        {
                            list.Add(1);
                            Thread.Sleep(10);
                            try
                            {
                                list.RemoveAt(0);
                                Console.WriteLine(list.Count);
                            }
                            catch
                            {
                                Console.WriteLine("异常" + list.Count);
                            }
                        }
                        Thread.Sleep(10);
                    }
                });
            }
            Task.Run(() =>
            {
                while (true)
                {
                    Console.WriteLine("元素个数" + list.Count);
                    Thread.Sleep(10);
                }
            });
        }
        //线程安全集合
        private static void Safety()
        {
            ConcurrentBag<int> list = new ConcurrentBag<int>();
            for (int i = 0; i < 10; i++)
            {
                Task.Run(() =>
                {
                    while (true)
                    {
                        if (list.Count == 0)
                        {
                            list.Add(1);
                            Thread.Sleep(10);
                            int i;
                            bool flag = list.TryTake(out i);
                            Console.WriteLine(list.Count + flag.ToString());
                        }
                        Thread.Sleep(10);
                    }
                });
            }
            Task.Run(() =>
            {
                while (true)
                {
                    Console.WriteLine("元素个数" + list.Count);
                    Thread.Sleep(10);
                }
            });
        }
        //加锁普通集合
        private static void LockNoSafety()
        {
            List<int> list = new List<int>();
            for (int i = 0; i < 10; i++)
            {
                Task.Run(() =>
                {
                    while (true)
                    {
                        lock (lockObj)
                        {
                            if (list.Count == 0)
                            {
                                list.Add(1);
                                Thread.Sleep(10);
                                try
                                {
                                    list.RemoveAt(0);
                                    Console.WriteLine(list.Count);
                                }
                                catch
                                {
                                    Console.WriteLine("异常" + list.Count);
                                }
                            }
                        }
                        Thread.Sleep(10);
                    }
                });
            }
            Task.Run(() =>
            {
                while (true)
                {
                    Console.WriteLine("元素个数" + list.Count);
                    Thread.Sleep(10);
                }
            });
        }
        //加锁安全集合
        private static void LockSafety()
        {
            ConcurrentBag<int> list = new ConcurrentBag<int>();
            for (int i = 0; i < 10; i++)
            {
                Task.Run(() =>
                {
                    while (true)
                    {
                        lock (lockObj)
                        {
                            if (list.Count == 0)
                            {
                                list.Add(1);
                                Thread.Sleep(10);
                                int i;
                                bool flag = list.TryTake(out i);
                                Console.WriteLine(list.Count + flag.ToString());
                            }
                        }
                        Thread.Sleep(10);
                    }
                });
            }
            Task.Run(() =>
            {
                while (true)
                {
                    Console.WriteLine("元素个数" + list.Count);
                    Thread.Sleep(10);
                }
            });
        }
        private static void NoSafetyAdd()
        {
            List<int> list = new List<int>();
            Task t1 = Task.Run(() =>
              {
                  for (int i = 0; i < 1000; i++)
                  {
                      list.Add(1);
                  }
              });

            Task t2 = Task.Run(() =>
            {
                for (int i = 0; i < 1000; i++)
                {
                    list.Add(1);
                }
            });

            Task t3 = Task.Factory.StartNew(() =>
             {
                 for (int i = 0; i < 1000; i++)
                 {
                     list.Add(1);
                 }
             });
            Task.WaitAll(t1, t2, t3);
            Console.WriteLine(list.Count);
        }

        private static void SafetyAdd()
        {
            ConcurrentBag<int> list = new ConcurrentBag<int>();
            Task t1 = Task.Run(() =>
            {
                for (int i = 0; i < 1000; i++)
                {
                    list.Add(1);
                }
            });

            Task t2 = Task.Run(() =>
            {
                for (int i = 0; i < 1000; i++)
                {
                    list.Add(1);
                }
            });

            Task t3 = Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 1000; i++)
                {
                    list.Add(1);
                }
            });
            Task.WaitAll(t1, t2, t3);
            Console.WriteLine(list.Count);
        }
    }
}
