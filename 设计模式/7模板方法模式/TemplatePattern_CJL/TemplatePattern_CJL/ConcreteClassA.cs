using System;
using System.Collections.Generic;
using System.Text;

namespace TemplatePattern_CJL
{
    /// <summary>
    /// 模板方法实现A
    /// </summary>
    public class ConcreteClassA : AbstractClass
    {
        public override void PrimitiveOperation1()
        {
            Console.WriteLine("具体类A方法1实现");
        }
        public override void PrimitiveOperation2()
        {
            Console.WriteLine("具体类A方法2实现");
        }
    }
}
