using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Common_CJL;
using System.Threading;
using System.Threading.Tasks;

namespace TCPClient_CJL
{
    class Program
    {
        static void Main(string[] args)
        {
            //获取建造者实例
            ISocketBuild socketBuild = new SocketBuild()
                //初始化客户端
                .ClientInit("127.0.0.1", 20000)
                //注册事件
                .ClientConnectionEvent(() =>
                {
                    Console.WriteLine("服务端连接成功");
                })
                .ClientMessageEvent((string msg) =>
                {
                    Console.WriteLine("收到服务端消息:" + msg);
                })
                .ClientCloseEvent((Exception ex) =>
                {
                    Console.WriteLine("客户端异常:" + ex.ToString());
                });
            //构建Socket客户端帮助类实例
            ISocketClientHelp socketClientHelp = socketBuild.BuildClientHelp();
            Task.Run(() =>
            {
                //连接
                socketClientHelp.Connection();
                socketClientHelp.Send("服务端发送了消息1");
                Thread.Sleep(1000);
                socketClientHelp.Send("服务端发送了消息2");
                Thread.Sleep(1000);
                socketClientHelp.Send("服务端发送了消息3");
                Thread.Sleep(1000);
                socketClientHelp.Close();
            });

            Console.ReadLine();
        }
    }
}
