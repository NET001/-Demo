using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common_CJL
{
    /// <summary>
    ///　Socket服务端实现
    /// </summary>
    public class SocketServerHelp : ISocketServerHelp
    {
        /// <summary>
        /// 有客户端连接了
        /// </summary>
        private Action<string, string> ConnectionEvent;
        /// <summary>
        /// 发送消息
        /// </summary>
        private Action<string, string, string> MessageEvent;
        /// <summary>
        /// 客户端关闭
        /// </summary>
        private Action<Exception> CloseEvent;
        private Socket serverSocket;
        private Socket clientSocket;
        private Thread thread;
        private IPAddress Ip;
        private int Point;
        private int Listen;
        public SocketServerHelp(IPAddress Ip, int Point, int Listen, Action<string, string> ConnectionEvent, Action<string, string, string> MessageEvent, Action<Exception> CloseEvent)
        {
            this.Ip = Ip;
            this.Point = Point;
            this.Listen = Listen;
            this.ConnectionEvent = ConnectionEvent;
            this.MessageEvent = MessageEvent;
            this.CloseEvent = CloseEvent;
        }
        public void Start()
        {
            IPEndPoint ipep = new IPEndPoint(Ip, Point);
            serverSocket = new Socket(ipep.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(ipep);
            serverSocket.Listen(Listen);
            Task.Run(() =>
            {
                while (true)
                {
                    clientSocket = serverSocket.Accept();
                    thread = new Thread(new ThreadStart(DoWork));
                    thread.Start();
                }
            });

        }
        private void DoWork()
        {
            Socket s = clientSocket;//客户端信息 
            IPEndPoint ipEndPoint = (IPEndPoint)s.RemoteEndPoint;
            string address = ipEndPoint.Address.ToString();
            string port = ipEndPoint.Port.ToString();
            ConnectionEvent?.Invoke(address, port);
            byte[] buffer = new byte[1024 * 1024 * 4];
            string inBufferStr;
            try
            {
                while (true)
                {
                    int length = s.Receive(buffer);//如果接收的消息为空 阻塞 当前循环 
                    if (length > 0)
                    {
                        inBufferStr = Encoding.UTF8.GetString(buffer, 0, length);
                        MessageEvent?.Invoke(address, port, inBufferStr);
                    }
                }
            }
            catch (Exception ex)
            {
                CloseEvent.Invoke(ex);
            }
        }
        public void Close()
        {
            serverSocket.Close();
        }
        public int Send(string message)
        {
            String outBufferStr;
            byte[] outBuffer = new byte[1024];
            //发送消息  
            outBufferStr = message;
            outBuffer = Encoding.UTF8.GetBytes(outBufferStr);
            return clientSocket.Send(outBuffer, outBuffer.Length, SocketFlags.None);
        }
    }
}
