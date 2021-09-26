using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleFactory_CJL
{
    /// <summary>
    /// 简单工厂类
    /// </summary>
    public class SimpleFactory
    {
        /// <summary>
        /// 获取产品实例
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public IProduct GetProduct(string code = "")
        {
            IProduct product = null;
            switch (code)
            {
                case "产品A":
                    {
                        product = new ConcreteProductA();
                    }
                    break;
                case "产品B":
                    {
                        product = new ConcreteProductB("测试产品B");
                    }
                    break;
                default:
                    {
                        product = new ConcreteProductDefault();
                    }
                    break;
            }
            return product;
        }
    }
}
