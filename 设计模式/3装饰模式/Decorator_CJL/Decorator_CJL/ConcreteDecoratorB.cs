using System;
using System.Collections.Generic;
using System.Text;

namespace Decorator_CJL
{
    /// <summary>
    /// 具体装饰行为B
    /// </summary>
    public class ConcreteDecoratorB : Decorator
    {

        public override void Operation()
        {
            base.Operation();
            AddedBehavior();
            Console.WriteLine("具体装饰对象B的操作");
        }

        private void AddedBehavior()
        {

        }
    }
}
