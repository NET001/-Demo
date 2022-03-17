using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UDP_CJL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Task.Run(() =>
            {
                Server();
            });
            Thread.Sleep(4000);
            Task.Run(() =>
            {
                Client();
            });
            Console.Read();
        }
        static void Server()
        {
            //监听socket
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            //监听所有网卡
            var ippoint = new IPEndPoint(IPAddress.Parse("0.0.0.0"), 20001);
            socket.Bind(ippoint);
            byte[] data = new byte[1024 * 1024];
            EndPoint Remote = (EndPoint)(ippoint);
            while (true)
            {
                int recv = socket.ReceiveFrom(data, ref Remote);
                Console.WriteLine(Encoding.UTF8.GetString(data, 0, recv));
                socket.SendTo(Encoding.UTF8.GetBytes("服务端收到消息并回复"), Encoding.UTF8.GetBytes("服务端收到消息并回复").Length, SocketFlags.None, Remote);
            }
        }
        static void Client()
        {
            //监听socket
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            //监听所有网卡
            var ippoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 20001);
            byte[] data = new byte[1024 * 1024];
            EndPoint Remote = (EndPoint)(ippoint);
            socket.SendTo(Encoding.UTF8.GetBytes("这是客户端发送的消息"), Encoding.UTF8.GetBytes("这是客户端发送的消息").Length, SocketFlags.None, Remote);
            while (true)
            {
                int recv = socket.ReceiveFrom(data, ref Remote);
                Console.WriteLine(Encoding.UTF8.GetString(data, 0, recv));
                socket.SendTo(Encoding.UTF8.GetBytes("客户端收到消息并回复"), Encoding.UTF8.GetBytes("客户端收到消息并回复").Length, SocketFlags.None, Remote);
            }
        }
    }
}
