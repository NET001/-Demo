using System;
using System.Collections.Generic;
using System.Text;

namespace ProxyPattern_CJL
{
    /// <summary>
    /// 代理类
    /// </summary>
    public class Proxy2 : RealSubject2
    {
        public new void Request()
        {
            Console.WriteLine("代理执行前");
            base.Request();
            Console.WriteLine("代理执行后");
        }
    }
}
