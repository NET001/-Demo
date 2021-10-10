using System;
using System.Collections.Generic;
using System.Text;

namespace CompositePattern_CJL
{
    public class ConcreteIteratorDesc : Iterator
    {
        private ConcreteAggregate aggregate;
        private int current = 0;

        public ConcreteIteratorDesc(ConcreteAggregate aggregate)
        {
            this.aggregate = aggregate;
            current = aggregate.Count - 1;
        }

        public override object First()
        {
            return aggregate[aggregate.Count - 1];
        }

        public override object Next()
        {
            object ret = null;
            current--;
            if (current >= 0)
            {
                ret = aggregate[current];
            }

            return ret;
        }

        public override object CurrentItem()
        {
            return aggregate[current];
        }

        public override bool IsDone()
        {
            return current < 0 ? true : false;
        }

    }
}
