using System;
using System.Collections.Generic;
using System.Text;

namespace Decorator_CJL
{
    /// <summary>
    /// 具体装饰行为A
    /// </summary>
    public class ConcreteDecoratorA : Decorator
    {
        private string addedState;

        public override void Operation()
        {
            base.Operation();
            addedState = "New State";
            Console.WriteLine("具体装饰对象A的操作");
        }
    }

}
