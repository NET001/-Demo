using System;
using System.Collections.Generic;
using System.Text;

namespace AdapterPattern_CJL
{
    /// <summary>
    /// 适配器
    /// </summary>
    public class Adapter : Target
    {
        private Adaptee adaptee = new Adaptee();

        public override void Request()
        {
            adaptee.SpecificRequest();
        }
    }
}
