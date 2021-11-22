using System;
using System.Collections.Generic;
using System.Text;

namespace BuilderPattern_CJL
{
    /// <summary>
    /// 产品类
    /// </summary>
    public class Product1
    {
        Dictionary<string, string> parts = new Dictionary<string, string>();
        public Product1(Dictionary<string, string> parts)
        {
            this.parts = parts;
        }
        public void Show()
        {
            Console.WriteLine("\n产品 展示 ----");
            foreach (string key in parts.Keys)
            {
                Console.WriteLine(key + ":" + parts[key]);
            }
        }
    }
}
