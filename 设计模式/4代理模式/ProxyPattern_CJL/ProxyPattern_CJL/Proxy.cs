using System;
using System.Collections.Generic;
using System.Text;

namespace ProxyPattern_CJL
{
    /// <summary>
    /// 代理类
    /// </summary>
    public class Proxy : Subject
    {
        RealSubject realSubject;
        public override void Request()
        {
            if (realSubject == null)
            {
                realSubject = new RealSubject();
            }

            realSubject.Request();
        }
    }

}
