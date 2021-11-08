using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Common_CJL;

namespace TCPServer_CJL
{
    class Program
    {
        static void Main(string[] args)
        {
            //获取建造者实例
            ISocketBuild socketBuild = new SocketBuild()
                //服务端初始化配置
                .ServerInit("127.0.0.1", 20000)
                //注册事件
                .ServerConnectionEvent((string ip, string point) =>
                {
                    Console.WriteLine("有客户端连接了(" + ip + "," + point + ")");
                })
                .ServerMessageEvent((string ip, string point, string msg) =>
                {
                    Console.WriteLine("(" + ip + "," + point + ")" + msg);
                })
                .ServerCloseEvent((Exception ex) =>
                {
                    Console.WriteLine(ex.ToString());
                });
            //构建服务端实例
            ISocketServerHelp socketServerHelp = socketBuild.BuildServerHelp();
            //启动
            socketServerHelp.Start();
            Console.ReadLine();
        }
    }
}
