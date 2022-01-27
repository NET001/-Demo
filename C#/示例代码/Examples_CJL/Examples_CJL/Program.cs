using System;
using System.Diagnostics;

namespace Examples_CJL
{
    class Program
    {
        static void Main(string[] args)
        {
            //获取当前程序的进程名称
            var name = Process.GetCurrentProcess().ProcessName;
            var cpuCounter = new PerformanceCounter("Process", "% Processor Time", name);
            var ramCounter = new PerformanceCounter("Process", "Working Set", name);
            //获取cpu情况
            float cpu = cpuCounter.NextValue();
            //获取内存情况
            float ram = ramCounter.NextValue();
            Console.WriteLine(cpu);
            Console.WriteLine(ram);
            Console.ReadKey();
        }
    }
}
