using System;
using System.Collections.Generic;
using System.Text;

namespace AdapterPattern_CJL
{
    /// <summary>
    /// 类适配器
    /// </summary>
    public class Adapter2 : Adaptee, ITarget
    {
        public void Request()
        {
            base.SpecificRequest();
        }
    }
}
