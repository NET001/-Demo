using System;
using System.Collections.Generic;
using System.Text;

namespace AdapterPattern_CJL
{
    /// <summary>
    /// 对象适配器
    /// </summary>
    public class Adapter1 : ITarget
    {
        private Adaptee adaptee = new Adaptee();

        public void Request()
        {
            adaptee.SpecificRequest();
        }
    }
}
