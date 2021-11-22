using System;
using System.Collections.Generic;
using System.Text;

namespace BuilderPattern_CJL
{
    /// <summary>
    /// 抽象建造者指定建造行为
    /// </summary>
    public interface IBuilder
    {
        Product1 Build();
    }
    public class ConcreteIBuilder1 : IBuilder
    {
        Dictionary<string, string> parts = new Dictionary<string, string>();
        public Product1 Build()
        {
            return new Product1(parts);
        }
        public ConcreteIBuilder1 SetPart1(string str)
        {
            parts["part1"] = str;
            return this;
        }
        public ConcreteIBuilder1 SetPart2(string str)
        {
            parts["part2"] = str;
            return this;
        }
        public ConcreteIBuilder1 SetPart3(string str)
        {
            parts["part3"] = str;
            return this;
        }
    }
    public class ConcreteIBuilder2 : IBuilder
    {
        Dictionary<string, string> parts = new Dictionary<string, string>();
        public Product1 Build()
        {
            return new Product1(parts);
        }
        public ConcreteIBuilder2 SetPart11(string str)
        {
            parts["part11"] = str;
            return this;
        }
        public ConcreteIBuilder2 SetPart22(string str)
        {
            parts["part22"] = str;
            return this;
        }
    }
}
