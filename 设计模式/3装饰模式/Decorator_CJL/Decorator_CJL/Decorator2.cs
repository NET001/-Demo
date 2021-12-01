using System;
using System.Collections.Generic;
using System.Text;

namespace Decorator_CJL
{
    public abstract class Decorator2
    {
        public abstract List<string> Operation();
    }
    /// <summary>
    /// 初始类
    /// </summary>
    public class ConcreteComponent2 : Decorator2
    {
        public override List<string> Operation()
        {
            return new List<string>()
            {
                "数据1",
                "数据2",
                "数据3",
            };
        }
    }
    /// <summary>
    /// 装饰器类
    /// </summary>
    public class ConcreteDecorator2A : Decorator2
    {
        Decorator2 decorator2;
        public ConcreteDecorator2A(Decorator2 decorator2)
        {
            this.decorator2 = decorator2;
        }
        //将内容进行双倍操作
        public override List<string> Operation()
        {
            List<string> result = decorator2.Operation();
            for (int i = 0; i < result.Count; i++)
            {
                result[i] = result[i] + result[i];
            }
            return result;
        }
    }

    public class ConcreteDecorator2B : Decorator2
    {
        Decorator2 decorator2;
        public ConcreteDecorator2B(Decorator2 decorator2)
        {
            this.decorator2 = decorator2;
        }
        //将内容进行加前缀_的操作
        public override List<string> Operation()
        {
            List<string> result = decorator2.Operation();
            for (int i = 0; i < result.Count; i++)
            {
                result[i] = "_" + result[i];
            }
            return result;
        }
    }


}
