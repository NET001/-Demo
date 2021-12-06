using System;
using System.Collections.Generic;
using System.Text;

namespace CompositePattern_CJL
{
    /// <summary>
    /// 抽象遍历器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface Iterator2<T>
    {
        bool hasNext();
        void Next();
        T First();
        T CurrentItem();
    }
    /// <summary>
    /// 遍历器(顺序)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ConcreteIterator2<T> : Iterator2<T>
    {
        Concrete2Aggregate<T> aggregate;
        private int current = 0;
        private int expectedModCount;
        public ConcreteIterator2(Concrete2Aggregate<T> aggregate)
        {
            this.aggregate = aggregate;
            expectedModCount = aggregate.Count;
        }
        public T CurrentItem()
        {
            CheckForComodification();
            return aggregate[current];
        }
        public T First()
        {
            CheckForComodification();
            return aggregate[0];
        }
        public bool hasNext()
        {
            CheckForComodification();
            return current < aggregate.Count;
        }
        public void Next()
        {
            CheckForComodification();
            current++;
        }
        private void CheckForComodification()
        {
            if (aggregate.Count != expectedModCount)
            {
                throw new Exception("迭代器的数据发送了修改");
            }
        }
    }
    public class ConcreteIterator2Desc<T> : Iterator2<T>
    {
        Concrete2Aggregate<T> aggregate;
        private int current = 0;
        private int expectedModCount;
        public ConcreteIterator2Desc(Concrete2Aggregate<T> aggregate)
        {
            this.aggregate = aggregate;
            current = aggregate.Count - 1;
            expectedModCount = aggregate.Count;
        }
        public T CurrentItem()
        {
            CheckForComodification();
            return aggregate[current];
        }
        public T First()
        {
            CheckForComodification();
            return aggregate[aggregate.Count - 1];
        }
        public bool hasNext()
        {
            CheckForComodification();
            return current >= 0;
        }
        public void Next()
        {
            CheckForComodification();
            current--;
        }
        private void CheckForComodification()
        {
            if (aggregate.Count != expectedModCount)
            {
                throw new Exception("迭代器的数据发送了修改");
            }
        }
    }
    /// <summary>
    /// 集合容器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Concrete2Aggregate<T> : List<T>
    {
        public Iterator2<T> CreateIterator()
        {
            return new ConcreteIterator2<T>(this);
        }
    }
}
