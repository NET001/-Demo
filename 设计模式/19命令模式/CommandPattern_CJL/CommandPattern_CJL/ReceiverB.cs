using System;
using System.Collections.Generic;
using System.Text;

namespace CommandPattern_CJL
{
    /// <summary>
    /// 接收者B/实现者B
    /// </summary>
    public class ReceiverB : Receiver
    {
        public override void Action()
        {
            Console.WriteLine("B执行请求！");
        }
    }
}
