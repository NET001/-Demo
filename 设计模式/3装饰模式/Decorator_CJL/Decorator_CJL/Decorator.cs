using System;
using System.Collections.Generic;
using System.Text;

namespace Decorator_CJL
{
    /// <summary>
    /// 装饰抽象类
    /// </summary>
    public abstract class Decorator : Component
    {
        protected Component component;
        public void SetComponent(Component component)
        {
            this.component = component;
        }
        public override void Operation()
        {
            if (component != null)
            {
                component.Operation();
            }
        }
    }
}
