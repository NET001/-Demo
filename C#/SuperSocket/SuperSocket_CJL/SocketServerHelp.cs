using Microsoft.Extensions.Hosting;
using SuperSocket;
using SuperSocket.Channel;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SuperSocketServer_CJL
{

    public class SocketServerHelp
    {
        /// <summary>
        /// 使用自定义的数据包和过滤器创建的服务宿主
        /// </summary>
        private readonly ISuperSocketHostBuilder<SocketMessage> superSocket = SuperSocketHostBuilder.Create<SocketMessage, SocketProgramFilter>();
        /// <summary>
        /// 会话集合
        /// </summary>
        private static ConcurrentDictionary<string, IAppSession> SessionsDict = new ConcurrentDictionary<string, IAppSession>();
        /// <summary>
        /// 启动Socket服务(只须调用此方法即可启动)
        /// </summary>
        public void Start(int[] ports)
        {
            var listeners = new List<ListenOptions>();
            foreach (var port in ports)
            {
                listeners.Add(new ListenOptions { Ip = "Any", Port = port });
            }
            superSocket.ConfigureSuperSocket(options =>//配置服务器如服务器名和监听端口等基本信息
            {
                options.Name = "SocketServer";
                //缓冲大小
                options.ReceiveBufferSize = 1000;
                //超时时间
                options.ReceiveTimeout = 300000;
                //开启的端口
                options.Listeners = listeners;
            });
            //会话连接和关闭的事件
            superSocket.UseSessionHandler(OnConnectedAsync, OnClosedAsync);
            //接收数据的事件
            superSocket.UsePackageHandler(OnPackageAsync);
            //开启socket
            Task.Run(() =>
            {
                superSocket.Build().Run();
            });
        }

        /// <summary>
        /// 关闭所有连接会话
        /// </summary>
        /// <returns></returns>
        public void StopServer()
        {
            foreach (var v in SessionsDict)
            {
                v.Value.CloseAsync(CloseReason.ServerShutdown);
            }
            SessionsDict.Clear();
        }

        /// <summary>
        /// 会话的连接事件
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        private async ValueTask OnConnectedAsync(IAppSession session)
        {
            await Task.Run(() =>
            {
                while (!SessionsDict.ContainsKey(session.SessionID))
                {
                    //添加不成功则重复添加
                    if (!SessionsDict.TryAdd(session.SessionID, session))
                        Thread.Sleep(1);
                }
                Console.WriteLine("会话" + session.SessionID + "已连接");
            });
        }

        /// <summary>
        /// 会话的断开事件
        /// </summary>
        private async ValueTask OnClosedAsync(IAppSession session, CloseEventArgs e)
        {
            await Task.Run(() =>
            {
                while (SessionsDict.ContainsKey(session.SessionID))
                {
                    //移除不成功则重复移除
                    if (!SessionsDict.TryRemove(session.SessionID, out _))
                        Thread.Sleep(1);
                }
                Console.WriteLine("会话" + session.SessionID + "已断开");
            });
        }
        /// <summary>
        /// 数据接收事件
        /// </summary>
        /// <param name="session"></param>
        /// <param name="package"></param>
        /// <returns></returns>
        private async ValueTask OnPackageAsync(IAppSession session, SocketMessage package)
        {
            Console.WriteLine("[" + session.SessionID + "]" + JsonConvert.SerializeObject(package));
        }
    }
    public class SocketConfig
    {

    }
}
