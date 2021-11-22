using System;
using System.Collections.Generic;
using System.Text;

namespace ProxyPattern_CJL
{
    /// <summary>
    /// 代理类
    /// </summary>
    public class Proxy1 : Subject
    {
        RealSubject1 realSubject;
        public override void Request()
        {
            if (realSubject == null)
            {
                realSubject = new RealSubject1();
            }
            Console.WriteLine("代理执行前");
            realSubject.Request();
            Console.WriteLine("代理执行后");
        }
    }

}
