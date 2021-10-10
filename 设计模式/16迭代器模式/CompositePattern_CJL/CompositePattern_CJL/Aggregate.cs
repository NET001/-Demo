using System;
using System.Collections.Generic;
using System.Text;

namespace CompositePattern_CJL
{
    public abstract class Aggregate
    {
        public abstract Iterator CreateIterator();
    }
}
