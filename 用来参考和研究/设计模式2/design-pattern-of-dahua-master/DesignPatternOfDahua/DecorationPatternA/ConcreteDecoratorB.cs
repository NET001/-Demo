using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecorationPatternA
{
    public class ConcreteDecoratorB : Decorator
    {
        public override void Show()
        {
            base.Show();
            Console.Write("我是装饰师B");
        }
    }
}
