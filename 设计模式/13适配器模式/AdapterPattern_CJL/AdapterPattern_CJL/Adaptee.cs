using System;
using System.Collections.Generic;
using System.Text;

namespace AdapterPattern_CJL
{
    /// <summary>
    /// 待适配的类
    /// </summary>
    public class Adaptee
    {
        public void SpecificRequest()
        {
            Console.WriteLine("特殊请求");
        }
    }
}
