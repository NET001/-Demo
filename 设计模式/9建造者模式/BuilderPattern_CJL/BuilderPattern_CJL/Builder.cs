using System;
using System.Collections.Generic;
using System.Text;

namespace BuilderPattern_CJL
{
    public abstract class Builder
    {
        public abstract void BuildPartA();
        public abstract void BuildPartB();
        public abstract Product0 GetResult();
    }
    public class ConcreteBuilder1 : Builder
    {
        private Product0 product = new Product0();
        public override void BuildPartA()
        {
            product.Add("部件A");
        }
        public override void BuildPartB()
        {
            product.Add("部件B");
        }
        public override Product0 GetResult()
        {
            return product;
        }
    }
    public class ConcreteBuilder2 : Builder
    {
        private Product0 product = new Product0();
        public override void BuildPartA()
        {
            product.Add("部件X");
        }
        public override void BuildPartB()
        {
            product.Add("部件Y");
        }
        public override Product0 GetResult()
        {
            return product;
        }
    }
}   