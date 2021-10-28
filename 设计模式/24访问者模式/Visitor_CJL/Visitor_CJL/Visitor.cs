using System;
using System.Collections.Generic;
using System.Text;

namespace Visitor_CJL
{
    /// <summary>
    /// 抽象访问者
    /// </summary>
    public abstract class Visitor
    {
        public abstract void VisitConcreteElementA(ConcreteElementA concreteElementA);

        public abstract void VisitConcreteElementB(ConcreteElementB concreteElementB);
    }

}
