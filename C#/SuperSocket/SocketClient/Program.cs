using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Newtonsoft.Json;

namespace SocketClient_CJL
{
    class Program
    {
        static byte[] buffer = new byte[2048];
        static Socket socket;
        static Thread thread;
        static int count = 0;
        static void Main(string[] args)
        {
            try
            {
                //实例化socket
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                var ippoint = new IPEndPoint(IPAddress.Any, int.Parse("4567"));
                //连接服务器
                socket.Connect(ippoint);
                thread = new Thread(StartReceive);
                thread.IsBackground = true;
                thread.Start(socket);
                SocketMessage socketMessage;
                while (true)
                {
                    socketMessage = new SocketMessage();
                    Console.WriteLine("输入Data1:");
                    socketMessage.Data1 = Console.ReadLine();
                    Console.WriteLine("输入Data2:");
                    socketMessage.Data1 = Console.ReadLine();
                    Console.WriteLine("输入Data3:");
                    socketMessage.Data1 = Console.ReadLine();
                    Send(socketMessage);
                    Console.WriteLine("已发送");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// 开启接收
        /// </summary>
        /// <param name="obj"></param>
        private static void StartReceive(object obj)
        {
            string str;
            while (true)
            {
                Socket receiveSocket = obj as Socket;
                try
                {
                    int result = receiveSocket.Receive(buffer);
                    if (result == 0)
                    {
                        break;
                    }
                    else
                    {
                        str = Encoding.UTF8.GetString(buffer);
                        Console.WriteLine("接收到服务器数据: " + str);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("服务器异常:" + ex.Message);

                }
            }
        }
        /// <summary>
        /// 关闭连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Close()
        {
            try
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                thread.Abort();
                Console.WriteLine("关闭与远程服务器的连接!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("异常" + ex.Message);
            }
        }
        private static void Send(SocketMessage message)
        {
            socket.Send(Encoding.Default.GetBytes(JsonConvert.SerializeObject(message)));
        }

    }
}