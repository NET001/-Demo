using System;
using System.Collections.Generic;
using System.Text;

namespace CommandPattern_CJL
{
    /// <summary>
    /// 接收者A/实现者A
    /// </summary>
    public class ReceiverA: Receiver
    {
        public override void Action()
        {
            Console.WriteLine("A执行请求！");
        }
    }
}
