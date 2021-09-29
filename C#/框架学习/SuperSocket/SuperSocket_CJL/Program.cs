using System;

namespace SuperSocketServer_CJL
{
    class Program
    {
        static void Main(string[] args)
        {
            SocketServerHelp socketServerHelp = new SocketServerHelp();
            socketServerHelp.Start(new int[] { 4567, 5678 });
            Console.WriteLine("SocketServeer已开启");
            Console.WriteLine("输入任意内容结结束");
            Console.ReadLine();
        }
    }
}
