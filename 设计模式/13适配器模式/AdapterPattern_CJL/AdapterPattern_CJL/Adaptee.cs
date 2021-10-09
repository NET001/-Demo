using System;
using System.Collections.Generic;
using System.Text;

namespace AdapterPattern_CJL
{
    public class Adaptee
    {
        public void SpecificRequest()
        {
            Console.WriteLine("特殊请求");
        }
    }
}
