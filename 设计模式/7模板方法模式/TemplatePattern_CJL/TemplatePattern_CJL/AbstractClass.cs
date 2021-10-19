using System;
using System.Collections.Generic;
using System.Text;

namespace TemplatePattern_CJL
{
    /// <summary>
    /// 模板方法抽象
    /// </summary>
    public abstract class AbstractClass
    {
        public abstract void PrimitiveOperation1();
        public abstract void PrimitiveOperation2();

        public void TemplateMethod()
        {
            PrimitiveOperation1();
            PrimitiveOperation2();
            Console.WriteLine("");
        }
    }
}
