using System;
using System.Collections.Generic;
using System.Text;

namespace Visitor_CJL
{
    /// <summary>
    /// 具体元素B
    /// </summary>
    public class ConcreteElementB : Element
    {
        public override void Accept(Visitor visitor)
        {
            visitor.VisitConcreteElementB(this);
        }

        public void OperationB()
        { }
    }
}
