using System;
using System.Collections.Generic;
using System.Text;

namespace Visitor_CJL
{
    /// <summary>
    /// 具体元素A
    /// </summary>
    public class ConcreteElementA : Element
    {
        public override void Accept(Visitor visitor)
        {
            visitor.VisitConcreteElementA(this);
        }

        public void OperationA()
        { }
    }
}
