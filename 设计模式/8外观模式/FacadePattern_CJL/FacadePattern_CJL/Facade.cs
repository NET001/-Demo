using System;
using System.Collections.Generic;
using System.Text;

namespace FacadePattern_CJL
{
    /// <summary>
    /// 外观类
    /// </summary>
    public class Facade
    {
        SubSystemOne one;
        SubSystemTwo two;
        SubSystemThree three;
        SubSystemFour four;

        public Facade()
        {
            one = new SubSystemOne();
            two = new SubSystemTwo();
            three = new SubSystemThree();
            four = new SubSystemFour();
        }

        public void MethodA()
        {
            Console.WriteLine("\n方法组A() ---- ");
            one.MethodOne();
            two.MethodTwo();
            four.MethodFour();
        }

        public void MethodB()
        {
            Console.WriteLine("\n方法组B() ---- ");
            two.MethodTwo();
            three.MethodThree();
        }
    }
}
