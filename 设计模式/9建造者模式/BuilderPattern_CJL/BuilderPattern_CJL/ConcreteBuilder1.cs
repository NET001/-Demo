using System;
using System.Collections.Generic;
using System.Text;

namespace BuilderPattern_CJL
{
    /// <summary>
    /// 具体建造者A用于生产指定的产品部件
    /// </summary>
    public class ConcreteBuilder1 : Builder
    {
        private Product product = new Product();

        public override void BuildPartA()
        {
            product.Add("部件A");
        }

        public override void BuildPartB()
        {
            product.Add("部件B");
        }

        public override Product GetResult()
        {
            return product;
        }
    }
}
