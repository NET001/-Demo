using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Data;
using System.Diagnostics;

namespace Linked_CJL
{
    class Program
    {
        static void Main(string[] args)
        {
            //Task.Run(() =>
            //{
            //    var name = Process.GetCurrentProcess().ProcessName;
            //    var cpuCounter = new PerformanceCounter("Process", "% Processor Time", name);
            //    var ramCounter = new PerformanceCounter("Process", "Working Set", name);
            //    while (true)
            //    {
            //        float cpu = cpuCounter.NextValue();
            //        float ram = ramCounter.NextValue();
            //        //Console.WriteLine(cpu);
            //        Console.WriteLine(ram);
            //        Thread.Sleep(1000);
            //    }
            //});

            Demo1();
            Console.ReadKey();
        }
        static void Demo1()
        {
            List<string> guids = new List<string>();
            for (int i = 0; i < 100000; i++)
            {
                guids.Add(Guid.NewGuid().ToString());
            }
            long time = 0;
            for (int i = 0; i < 10; i++)
            {
                var tiem1 = DateTime.Now;
                LinkedList<string> link = new LinkedList<string>();
                for (int j = 0; j < 100; j++)
                {
                    foreach (var item in guids)
                    {
                        link.AddLast(item);
                    }
                }
                var tiem2 = DateTime.Now;
                var d = tiem2 - tiem1;
                time += d.Ticks;
                Console.WriteLine(d.Ticks);
            }
            Console.WriteLine("平均:" + (time / 10));
        }
        static void Demo2()
        {
            List<string> guids = new List<string>();
            for (int i = 0; i < 100000; i++)
            {
                guids.Add(Guid.NewGuid().ToString());
            }
            long time = 0;
            for (int i = 0; i < 10; i++)
            {
                var tiem1 = DateTime.Now;
                List<string> list = new List<string>();
                for (int j = 0; j < 100; j++)
                {
                    foreach (var item in guids)
                    {
                        list.Add(item);
                    }
                }
                var tiem2 = DateTime.Now;
                var d = tiem2 - tiem1;
                time += d.Ticks;
                Console.WriteLine(d.Ticks);
            }
            Console.WriteLine("平均:" + (time / 10));
        }
        static void Demo3()
        {

        }
    }
}
