using System;
using System.Diagnostics;

namespace Log_CJL
{
    class Program
    {
        static void Main(string[] args)
        {
            //在输出的编辑器中可以查看到
            Debugger.Log(1, null, "这是一个日志1");
            Debug.WriteLine("这是一个日志2");
            Console.Read();
        }
    }
}
