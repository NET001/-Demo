using System;
using System.Collections.Generic;
using System.Text;

namespace AdapterPattern_CJL
{
    public class Target
    {
        public virtual void Request()
        {
            Console.WriteLine("普通请求");
        }
    }
}
