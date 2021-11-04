using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace TCPServer_CJL
{
    class Program
    {
        static Socket serverSocket;
        static Socket clientSocket;
        static Thread thread;
        static void Main(string[] args)
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 3001);
            serverSocket = new Socket(ipep.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(ipep);
            serverSocket.Listen(10);
            while (true)
            {
                clientSocket = serverSocket.Accept();
                thread = new Thread(new ThreadStart(doWork));
                thread.Start();
            }
        }
        private static void doWork()
        {
            Socket s = clientSocket;//客户端信息 
            IPEndPoint ipEndPoint = (IPEndPoint)s.RemoteEndPoint;
            String address = ipEndPoint.Address.ToString();
            String port = ipEndPoint.Port.ToString();
            Console.WriteLine(address + ":" + port + " 连接过来了");
            Byte[] inBuffer = new Byte[1024];
            Byte[] outBuffer = new Byte[1024];
            String inBufferStr;
            String outBufferStr;
            try
            {
                while (true)
                {
                    s.Receive(inBuffer, 1024, SocketFlags.None);//如果接收的消息为空 阻塞 当前循环 
                    inBufferStr = Encoding.ASCII.GetString(inBuffer);
                    Console.WriteLine(address + ":" + port + "说:");
                    Console.WriteLine(inBufferStr);
                    outBufferStr = Console.ReadLine();
                    outBuffer = Encoding.ASCII.GetBytes(outBufferStr);
                    s.Send(outBuffer, outBuffer.Length, SocketFlags.None);
                }
            }
            catch
            {
                Console.WriteLine("客户端已关闭！");
            }
        }
    }
}
