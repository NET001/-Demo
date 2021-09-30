using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace SocketServer_CJL
{

    public static class SocketHelp
    {
        static IPEndPoint serverEndPoint;
        static Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        static Socket clientSocket;

        /// <summary>
        ///  监听
        /// </summary>
        static void SetServerSocketListen()
        {
            // 绑定
            serverSocket.Bind(serverEndPoint);
            // 监听
            serverSocket.Listen(20);
            // 新的客户端socket
            clientSocket = serverSocket.Accept();
            // 有新连接创建时 
            //clientEndPoint = (IPEndPoint)clientSocket.RemoteEndPoint;
            ////先客户端 发送
            //string welcome = "welcome !";
            //byte[] welcomeByte = Encoding.UTF8.GetBytes(welcome);
            //clientSocket.Send(welcomeByte);
            ReviceData();
        }
        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="args"></param>
        static void ReviceData()
        {
            /// 循环接收数据
            while (true)
            {
                if (clientSocket != null)
                {
                    byte[] data = new byte[1024];
                    //将数据 缓存到 data
                    int buffer = clientSocket.Receive(data);
                    if (buffer > 0)
                    {
                        //  数据不到1024 时使用Encoding.UTF8.GetString（byte[] ) 回接收大片空白
                        string strData = Encoding.UTF8.GetString(data, 0, buffer);// 只转换相应的数据大小                   
                    }
                }
            }
        }
        public static void Start(int port)
        {
            serverEndPoint = new IPEndPoint(IPAddress.Any, port);
            Thread serverThrad = new Thread(new ThreadStart(SetServerSocketListen));
            serverThrad.IsBackground = true;
            serverThrad.Start();
            //serverThrad.Join();
        }
    }
}
