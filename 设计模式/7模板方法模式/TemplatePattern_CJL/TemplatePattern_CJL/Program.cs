using System;

namespace TemplatePattern_CJL
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo1();
            Console.Read();
        }
        /// <summary>
        /// 通过继承的方式
        /// </summary>
        static void Demo1()
        {
            AbstractClass c;
            c = new ConcreteClassA();
            c.TemplateMethod();
            c = new ConcreteClassB();
            c.TemplateMethod();
        }
        /// <summary>
        /// 通过委托回调的方式
        /// </summary>
        static void Demo2()
        {
            CallBackTemplateClass callBackTemplateClass = new CallBackTemplateClass();
            callBackTemplateClass.TemplateMethod((string str) =>
            {
                return str + "000";
            });
        }
        static void Demo3()
        {
            CompositionTemplateA compositionTemplateA = new CompositionTemplateA();
            CompositionTemplateClass compositionTemplateClass = new CompositionTemplateClass(compositionTemplateA);
            compositionTemplateClass.TemplateMethod();
        }
    }
}
