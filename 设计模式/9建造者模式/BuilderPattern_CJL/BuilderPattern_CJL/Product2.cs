using System;
using System.Collections.Generic;
using System.Text;

namespace BuilderPattern_CJL
{
    /// <summary>
    /// 内部类实现当前类的建造过程
    /// </summary>
    public class Product2
    {
        Builder builder;
        private Product2(Builder builder)
        {
            this.builder = builder;
        }
        public void Operation()
        {
            Console.WriteLine(builder.Parameter1);
            Console.WriteLine(builder.Parameter2);
            Console.WriteLine(builder.Parameter3);
        }
        //建造者在类的内部
        public class Builder
        {
            private string parameter1;
            private string parameter2;
            private string parameter3;
            public string Parameter1 => parameter1;
            public string Parameter2 => parameter2;
            public string Parameter3 => parameter3;
            public Product2 Build()
            {
                if (string.IsNullOrWhiteSpace(parameter1))
                {
                    throw new Exception("parameter1未赋值");
                }
                if (string.IsNullOrWhiteSpace(parameter2))
                {
                    throw new Exception("parameter2未赋值");
                }
                if (string.IsNullOrWhiteSpace(parameter3))
                {
                    throw new Exception("parameter3未赋值");
                }
                return new Product2(this);
            }
            public Builder SetParameter1(string str)
            {
                this.parameter1 = str;
                return this;
            }
            public Builder SetParameter2(string str)
            {
                this.parameter2 = str;
                return this;
            }
            public Builder SetParameter3(string str)
            {
                this.parameter3 = str;
                return this;
            }
        }
    }
}
