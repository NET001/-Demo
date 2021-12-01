using System;
using System.Collections.Generic;
using System.Text;

namespace TemplatePattern_CJL
{
    /// <summary>
    /// 委托回调的方式
    /// </summary>
    public class CallBackTemplateClass
    {
        public void TemplateMethod(Func<string, string> callback)
        {
            string str = "123";
            str = callback(str);
            str = str + str;
            Console.WriteLine(str);
        }
    }
    /// <summary>
    /// 类组合的方式实现
    /// </summary>
    public class CompositionTemplateClass
    {
        ICompositionTemplate compositionTemplate;
        public CompositionTemplateClass(ICompositionTemplate compositionTemplate)
        {
            this.compositionTemplate = compositionTemplate;
        }
        public void TemplateMethod()
        {
            compositionTemplate.PrimitiveOperation1();
            compositionTemplate.PrimitiveOperation2();
        }
    }
    public class CompositionTemplateA : ICompositionTemplate
    {
        public void PrimitiveOperation1()
        {
            Console.WriteLine("具体实现1");
        }

        public void PrimitiveOperation2()
        {
            Console.WriteLine("具体实现2");
        }
    }
    public interface ICompositionTemplate
    {
        public void PrimitiveOperation1();
        public void PrimitiveOperation2();
    }
}
