using System;
using System.Collections.Generic;
using System.Text;

namespace ProxyPattern_CJL
{
    /// <summary>
    /// 具体实现
    /// </summary>
    public class RealSubject2
    {
        public void Request()
        {
            Console.WriteLine("真实的请求");
        }
    }
}
