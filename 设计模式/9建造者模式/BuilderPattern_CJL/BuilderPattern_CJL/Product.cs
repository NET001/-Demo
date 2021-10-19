using System;
using System.Collections.Generic;
using System.Text;

namespace BuilderPattern_CJL
{
    /// <summary>
    /// 产品类
    /// </summary>
    public class Product
    {
        IList<string> parts = new List<string>();
        /// <summary>
        /// 添加产品部件
        /// </summary>
        /// <param name="part"></param>
        public void Add(string part)
        {
            parts.Add(part);
        }
        public void Show()
        {
            Console.WriteLine("\n产品 创建 ----");
            foreach (string part in parts)
            {
                Console.WriteLine(part);
            }
        }
    }
}
