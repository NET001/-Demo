using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace TCPClient_CJL
{
    class Program
    {
        static void Main(string[] args)
        {
             Socket clientSocket;
            //将网络端点表示为IP地址和端口 用于socket侦听时绑定  
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 3001); //填写自己电脑的IP或者其他电脑的IP，如果是其他电脑IP的话需将ConsoleApplication_socketServer工程放在对应的电脑上。 
            clientSocket = new Socket(ipep.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            //将Socket连接到服务器  
            try
            {
                clientSocket.Connect(ipep);
                String outBufferStr;
                Byte[] outBuffer = new Byte[1024];
                Byte[] inBuffer = new Byte[1024];
                while (true)
                {
                    //发送消息  
                    outBufferStr = Console.ReadLine();
                    outBuffer = Encoding.ASCII.GetBytes(outBufferStr);
                    clientSocket.Send(outBuffer, outBuffer.Length, SocketFlags.None);

                    //接收服务器端信息        
                    clientSocket.Receive(inBuffer, 1024, SocketFlags.None);//如果接收的消息为空 阻塞 当前循环 
                    Console.WriteLine("服务器说：");
                    Console.WriteLine(Encoding.ASCII.GetString(inBuffer));
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("服务未开启！");
                Console.ReadLine();
            }
        }
    }
}
