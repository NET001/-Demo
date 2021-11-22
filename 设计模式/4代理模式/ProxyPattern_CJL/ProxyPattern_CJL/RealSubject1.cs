using System;
using System.Collections.Generic;
using System.Text;

namespace ProxyPattern_CJL
{
    /// <summary>
    /// 具体实现
    /// </summary>
    public class RealSubject1 : Subject
    {
        public override void Request()
        {
            Console.WriteLine("真实的请求");
        }
    }
}
