using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Common_CJL
{

    /// <summary>
    /// Socket客户端实现
    /// </summary>
    public class SocketClientHelp : ISocketClientHelp
    {
        /// <summary>
        /// 连接成功
        /// </summary>
        private Action ConnectionEvent;
        /// <summary>
        /// 接受到消息ip地址,端口号,获取的值
        /// </summary> 
        private Action<string> MessageEvent;
        /// <summary>
        /// 客户端关闭
        /// </summary>
        private Action<Exception> CloseEvent;
        Socket clientSocket;
        private IPAddress Ip;
        private int Point;
        public SocketClientHelp(IPAddress Ip, int Point, Action ConnectionEvent, Action<string> MessageEvent, Action<Exception> CloseEvent)
        {
            this.Ip = Ip;
            this.Point = Point;
            this.ConnectionEvent = ConnectionEvent;
            this.MessageEvent = MessageEvent;
            this.CloseEvent = CloseEvent;
        }
        public void Connection()
        {
            //将网络端点表示为IP地址和端口 用于socket侦听时绑定  
            IPEndPoint ipep = new IPEndPoint(Ip, Point); //填写自己电脑的IP或者其他电脑的IP，如果是其他电脑IP的话需将ConsoleApplication_socketServer工程放在对应的电脑上。 
            clientSocket = new Socket(ipep.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            //将Socket连接到服务器  
            try
            {
                clientSocket.Connect(ipep);
                ConnectionEvent?.Invoke();
                byte[] inBuffer = new byte[1024 * 1024 * 4];
                Task.Run(() =>
                {
                    while (true)
                    {
                        //接收服务器端信息        
                        int length = clientSocket.Receive(inBuffer);//如果接收的消息为空 阻塞 当前循环 
                        if (length > 0)
                        {
                            string message = Encoding.UTF8.GetString(inBuffer, 0, length);
                            MessageEvent?.Invoke(message);
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                CloseEvent?.Invoke(ex);
            }
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
        public void Close()
        {
            clientSocket.Close();
            clientSocket.Dispose();
        }
    }
}
