using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateMethodPatternB
{
    public class Realization2 : Template
    {
        public override void TemplateMethod1()
        {
            Console.WriteLine("我是模板方法1的实现方法2");
        }

        public override void TemplateMethod2()
        {
            Console.WriteLine("我是模板方法2的实现方法2");
        }
    }
}
