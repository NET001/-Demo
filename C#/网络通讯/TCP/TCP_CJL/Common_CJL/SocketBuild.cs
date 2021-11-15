using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Common_CJL
{

    /// <summary>
    /// 具体Socket建造者
    /// </summary>
    public class SocketBuild : ISocketBuild
    {
        string ClientIp = null;
        int? ClientPoint = null;
        /// <summary>
        /// 客户端连接ip地址,端口号
        /// </summary>
        private Action CConnectionEvent;
        /// <summary>
        /// 接受到消息ip地址,端口号,获取的值
        /// </summary> 
        private Action<string> CMessageEvent;
        /// <summary>
        /// 客户端关闭
        /// </summary>
        private Action<Exception> CCloseEvent;
        string ServerIp = null;
        int? ServerPoint = null;
        int? ServerListen = null;
        /// <summary>
        /// 有客户端连接了
        /// </summary>
        private Action<string, string> SConnectionEvent;
        /// <summary>
        /// 发送消息
        /// </summary>
        private Action<string, string, string> SMessageEvent;
        /// <summary>
        /// 客户端关闭
        /// </summary>
        private Action<Exception> SCloseEvent;

        /// <summary>
        /// 初始化服务端
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public SocketBuild ServerInit(string Ip, int Point, int Listen = 20)
        {
            if (!string.IsNullOrEmpty(Ip) && !IPAddress.TryParse(Ip, out _))
            {
                throw new Exception("Ip格式不正确");
            }
            ServerIp = Ip;
            ServerPoint = Point;
            ServerListen = Listen;
            return this;
        }
        /// <summary>
        /// 添加连接事件
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public SocketBuild ServerConnectionEvent(Action<string, string> action)
        {
            SConnectionEvent = action;
            return this;
        }
        /// <summary>
        /// 添加获取消息
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public SocketBuild ServerMessageEvent(Action<string, string, string> action)
        {
            SMessageEvent = action;
            return this;
        }
        /// <summary>
        /// 添加断开连接事件
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public SocketBuild ServerCloseEvent(Action<Exception> action)
        {
            SCloseEvent = action;
            return this;
        }
        /// <summary>
        /// 构造服务端
        /// </summary>
        /// <returns></returns>
        public ISocketServerHelp BuildServerHelp()
        {
            ServerIp = ServerIp == null ? "127.0.0.1" : ServerIp;
            ServerPoint = ServerPoint == null ? 20000 : ServerPoint;
            ServerListen = ServerListen == null ? 20 : ServerListen;
            return new SocketServerHelp(IPAddress.Parse(ServerIp), (int)ServerPoint, (int)ServerListen, SConnectionEvent, SMessageEvent, SCloseEvent);
        }
        /// <summary>
        /// 初始化服务端
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public SocketBuild ClientInit(string Ip, int Point)
        {
            if (string.IsNullOrEmpty(Ip) || !IPAddress.TryParse(Ip, out _))
            {
                throw new Exception("Ip地址格式不正确");
            }
            ClientIp = Ip;
            ClientPoint = Point;
            return this;
        }
        /// <summary>
        /// 连接成功
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public SocketBuild ClientConnectionEvent(Action action)
        {
            CConnectionEvent = action;
            return this;
        }
        /// <summary>
        /// 添加获取消息
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public SocketBuild ClientMessageEvent(Action<string> action)
        {

            CMessageEvent = action;
            return this;
        }
        /// <summary>
        /// 添加断开连接事件
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public SocketBuild ClientCloseEvent(Action<Exception> action)
        {
            CCloseEvent = action;
            return this;
        }
        /// <summary>
        /// 构造客户端
        /// </summary>
        /// <returns></returns>
        public ISocketClientHelp BuildClientHelp()
        {
            if (ClientIp == null)
            {
                throw new Exception("Ip为空");
            }
            else if (CConnectionEvent == null)
            {
                throw new Exception("端口为空");
            }
            return new SocketClientHelp(IPAddress.Parse(ClientIp), (int)ClientPoint, CConnectionEvent, CMessageEvent, CCloseEvent);
        }

    }
}
