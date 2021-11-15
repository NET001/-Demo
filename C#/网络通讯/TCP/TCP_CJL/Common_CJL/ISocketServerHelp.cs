using System;
using System.Collections.Generic;
using System.Text;

namespace Common_CJL
{
    public interface ISocketServerHelp
    {
        /// <summary>
        /// 启动
        /// </summary>
        void Start();
        /// <summary>
        /// 关闭
        /// </summary>
        void Close();
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        int Send(string msg);
    }
}
