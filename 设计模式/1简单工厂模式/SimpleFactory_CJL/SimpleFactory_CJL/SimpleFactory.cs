using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Concurrent;

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

    public class CacheSimpleFactory
    {
        private static ConcurrentDictionary<string, IProduct> keyValuePairs = new ConcurrentDictionary<string, IProduct>();
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
                        if (keyValuePairs.TryGetValue(code, out product))
                        {
                        }
                        else
                        {
                            product = new ConcreteProductA();
                            keyValuePairs.TryAdd(code, product);
                        }
                    }
                    break;
                case "产品B":
                    {
                        if (keyValuePairs.TryGetValue(code, out product))
                        {
                        }
                        else
                        {
                            product = new ConcreteProductB("测试产品B");
                            keyValuePairs.TryAdd(code, product);
                        }
                    }
                    break;
                default:
                    {
                        if (keyValuePairs.TryGetValue(code, out product))
                        {
                        }
                        else
                        {
                            product = new ConcreteProductDefault();
                            keyValuePairs.TryAdd(code, product);
                        }
                    }
                    break;
            }
            return product;
        }
    }
}
