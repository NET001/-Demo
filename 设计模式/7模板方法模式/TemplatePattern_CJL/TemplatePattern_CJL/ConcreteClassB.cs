using System;
using System.Collections.Generic;
using System.Text;

namespace TemplatePattern_CJL
{
    /// <summary>
    /// 模板方法实现B
    /// </summary>
    public class ConcreteClassB : AbstractClass
    {
        public override void PrimitiveOperation1()
        {
            Console.WriteLine("具体类B方法1实现");
        }
        public override void PrimitiveOperation2()
        {
            Console.WriteLine("具体类B方法2实现");
        }
    }
}
