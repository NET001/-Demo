using System;
using System.Collections.Generic;
using System.Text;

namespace AdapterPattern_CJL
{
    /// <summary>
    /// 客户端期待的接口
    /// </summary>
    public class Target
    {
        public virtual void Request()
        {
            Console.WriteLine("普通请求");
        }
    }
}
