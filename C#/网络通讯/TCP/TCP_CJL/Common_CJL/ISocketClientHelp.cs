using System;
using System.Collections.Generic;
using System.Text;

namespace Common_CJL
{
    public interface ISocketClientHelp
    {    /// <summary>
         /// 连接
         /// </summary>
         /// <returns></returns>
        void Connection();
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <returns></returns>
        int Send(string message);
        //断开连接
        void Close();
    }
}
