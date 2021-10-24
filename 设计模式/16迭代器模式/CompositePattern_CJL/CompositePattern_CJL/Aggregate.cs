using System;
using System.Collections.Generic;
using System.Text;

namespace CompositePattern_CJL
{
    /// <summary>
    /// 聚集抽象类
    /// </summary>
    public abstract class Aggregate
    {
        public abstract Iterator CreateIterator();
    }
}
