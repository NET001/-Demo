using System;
using System.Collections.Generic;
using System.Text;

namespace Visitor_CJL
{
    /// <summary>
    /// 抽象元素
    /// </summary>
    public abstract class Element
    {
        public abstract void Accept(Visitor visitor);
    }
}
