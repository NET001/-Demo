using System;
using System.Collections.Generic;
using System.Text;

namespace BuilderPattern_CJL
{
    public class Director
    {
        public void Construct(Builder builder)
        {
            builder.BuildPartA();
            builder.BuildPartB();
        }
    }
}
